using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Kino
{
        public partial class SeatLayoutForm : Form
        {
            private int saalId;
            private string saalName;
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jeliz\Source\Repos\Kino\KinoDB.mdf;Integrated Security=True");

            public SeatLayoutForm(int saalId, string saalName)
            {
                InitializeComponent();
                this.saalId = saalId;
                this.saalName = saalName;

                // Let's add immediate feedback
                MessageBox.Show($"Constructor called with SaalId: {saalId}");

                // Let's also verify the flowLayoutPanel exists
                if (flowLayoutPanelSeats == null)
                {
                    MessageBox.Show("flowLayoutPanelSeats is null!");
                    return;
                }
                LoadSeats();
            }

            private void LoadSeats()
            {
                try
                {
                    conn.Open();
                    // Modified query to order by Rida and Koht
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

                // Configure FlowLayoutPanel
                flowLayoutPanelSeats.FlowDirection = FlowDirection.LeftToRight;
                flowLayoutPanelSeats.WrapContents = true;
                flowLayoutPanelSeats.AutoSize = true;

                int currentRow = -1;
                int seatsPerRow = 5;

                foreach (DataRow row in seatData.Rows)
                {
                    int rowNumber = Convert.ToInt32(row["Rida"]);
                    int seatNumber = Convert.ToInt32(row["Koht"]);

                    // Add row label if we're starting a new row
                    if (currentRow != rowNumber)
                    {
                        currentRow = rowNumber;
                        // Add spacing at the start of each new row (except first row)
                        if (rowNumber > 1)
                        {
                            // Add empty space to create a new line
                            for (int i = seatNumber; i <= seatsPerRow; i++)
                            {
                                Panel spacer = new Panel
                                {
                                    Width = 50,
                                    Height = 0
                                };
                                flowLayoutPanelSeats.Controls.Add(spacer);
                            }
                        }

                        Label rowLabel = new Label
                        {
                            Text = $"Row {rowNumber}",
                            Width = 60,
                            Height = 50,
                            TextAlign = ContentAlignment.MiddleCenter
                        };
                        flowLayoutPanelSeats.Controls.Add(rowLabel);
                    }

                    Button seatButton = new Button
                    {
                        Text = seatNumber.ToString(),
                        Width = 50,
                        Height = 50,
                        Tag = row["Id"],
                        BackColor = row["KohtStatus"].ToString() == "broneeritud" ? Color.Red : Color.Green,
                        Margin = new Padding(2)
                    };

                    seatButton.Click += (s, e) => HandleSeatClick(seatButton);
                    flowLayoutPanelSeats.Controls.Add(seatButton);
                }
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
                        UpdateSeatStatus(Convert.ToInt32(seatButton.Tag), "broneeritud");
                        seatButton.BackColor = Color.Red;
                    }
                }
                else if (seatButton.BackColor == Color.Red)
                {
                    DialogResult result = MessageBox.Show(
                        $"Do you want to release seat {seatButton.Text}?",
                        "Confirm Release",
                        MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        UpdateSeatStatus(Convert.ToInt32(seatButton.Tag), "saadaval");
                        seatButton.BackColor = Color.Green;
                    }
                }
            }

            private void UpdateSeatStatus(int seatId, string status)
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE kohad SET KohtStatus = @Status WHERE Id = @SeatId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@SeatId", seatId);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Seat status updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating seat status: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
}
