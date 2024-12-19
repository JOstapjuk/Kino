using Kino.Kasutaja;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Kino.KasutajaActions
{
    public partial class SaalLayout : Form
    {
        private int saalId;
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jeliz\Source\Repos\Kino\KinoDB.mdf;Integrated Security=True");

        public SaalLayout(int saalId)
        {
            InitializeComponent();
            this.saalId = saalId;

            // Immediate feedback
            MessageBox.Show($"Constructor called with SaalId: {saalId}");
            LoadSeats();
        }

        private void LoadSeats()
        {
            try
            {
                conn.Open();
                string query = @"
                SELECT Id, Rida, Koht, KohtStatus 
                FROM kohad 
                WHERE SaalId = @SaalId 
                ORDER BY Rida, Koht";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SaalId", saalId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable seatData = new DataTable();
                adapter.Fill(seatData);

                GenerateSeatLayout(seatData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading seats: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void GenerateSeatLayout(DataTable seatData)
        {
            flowLayoutPanelSeats.Controls.Clear();

            if (seatData.Rows.Count == 0)
            {
                MessageBox.Show("No seats available for this hall.");
                return;
            }

            // Get unique rows and sort them
            var uniqueRows = seatData.AsEnumerable()
                                    .Select(r => Convert.ToInt32(r["Rida"]))
                                    .Distinct()
                                    .OrderBy(r => r);

            foreach (int rowNum in uniqueRows)
            {
                // Create panel for each row
                FlowLayoutPanel rowPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true,
                    WrapContents = false,
                    Margin = new Padding(0, 10, 0, 10)
                };

                // Add row label
                Label rowLabel = new Label
                {
                    Text = $"Row {rowNum}",
                    Width = 60,
                    Height = 50,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                rowPanel.Controls.Add(rowLabel);

                // Get and sort seats for the row
                var rowSeats = seatData.AsEnumerable()
                                      .Where(r => Convert.ToInt32(r["Rida"]) == rowNum)
                                      .OrderBy(r => Convert.ToInt32(r["Koht"]));

                foreach (var seat in rowSeats)
                {
                    Button seatButton = new Button
                    {
                        Text = seat["Koht"].ToString(),
                        Width = 50,
                        Height = 50,
                        Tag = seat["Id"],
                        BackColor = seat["KohtStatus"].ToString() == "broneeritud" ? Color.Red : Color.Green,
                        Margin = new Padding(2)
                    };

                    seatButton.Click += (s, e) => HandleSeatClick(seatButton);
                    rowPanel.Controls.Add(seatButton);
                }

                // Add row panel to main panel
                flowLayoutPanelSeats.Controls.Add(rowPanel);
            }

            // Set main panel properties
            flowLayoutPanelSeats.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelSeats.WrapContents = false;
            flowLayoutPanelSeats.AutoScroll = true;
        }

        private void HandleSeatClick(Button seatButton)
        {
            if (seatButton.BackColor == Color.Green)
            {
                DialogResult result = MessageBox.Show(
                    $"Do you want to reserve seat {seatButton.Text}?",
                    "Confirm Reservation",
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    ReserveSeat(Convert.ToInt32(seatButton.Tag), "broneeritud", seatButton.Text);
                    seatButton.BackColor = Color.Red;
                    seatButton.Text = "Reserved"; // Label reserved seats
                }
            }
            else if (seatButton.BackColor == Color.Red)
            {
                MessageBox.Show("This seat is already reserved.");
            }
        }

        private void ReserveSeat(int seatId, string status, string seatKoht)
        {
            try
            {
                conn.Open();
                // Update seat status in the database
                string updateQuery = "UPDATE kohad SET KohtStatus = @Status WHERE Id = @SeatId";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@Status", status);
                updateCmd.Parameters.AddWithValue("@SeatId", seatId);
                updateCmd.ExecuteNonQuery();

                // Insert into tickets table
                string insertQuery = "INSERT INTO piletid (KasutajaID, SeansID, Koht) VALUES (@KasutajaID, @SeansID, @Koht)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@KasutajaID", UserDetails.KasutajaId);
                insertCmd.Parameters.AddWithValue("@SeansID", this.saalId); // Use saalId as SeansID
                insertCmd.Parameters.AddWithValue("@Koht", seatKoht); // Seat number

                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Seat reserved and ticket added.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reserving seat: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
