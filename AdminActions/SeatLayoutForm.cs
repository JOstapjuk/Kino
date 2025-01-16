using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Kino
{
        public partial class SaalLayout : Form
        {
            private int saalId;
            private string saalName;
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jeliz\source\repos\Kino\KinoDB.mdf;Integrated Security=True");

            public SaalLayout(int saalId, string saalName)
            {
                InitializeComponent();
                this.saalId = saalId;
                this.saalName = saalName;

                MessageBox.Show($"Konstruktor, mida kutsutakse SaalId-ga: {saalId}");

                if (flowLayoutPanelSeats == null)
                {
                    MessageBox.Show("flowLayoutPanelSeats is null!");
                    return;
                }
                LoadSeats();
            }

        public SaalLayout(int saalId)
        {
            this.saalId = saalId;
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
                    MessageBox.Show("Viga istmete laadimisel: " + ex.Message);
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
                MessageBox.Show("Selles saalis ei ole kohti saadaval.");
                return;
            }

            var uniqueRows = seatData.AsEnumerable()
                                    .Select(r => Convert.ToInt32(r["Rida"]))
                                    .Distinct()
                                    .OrderBy(r => r);

            foreach (int rowNum in uniqueRows)
            {
                FlowLayoutPanel rowPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true,
                    WrapContents = false,
                    Margin = new Padding(0, 10, 0, 10)
                };

                Label rowLabel = new Label
                {
                    Text = $"Row {rowNum}",
                    Width = 60,
                    Height = 50,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                rowPanel.Controls.Add(rowLabel);

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

                flowLayoutPanelSeats.Controls.Add(rowPanel);
            }

            flowLayoutPanelSeats.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelSeats.WrapContents = false;
            flowLayoutPanelSeats.AutoScroll = true;
        }

        private void HandleSeatClick(Button seatButton)
            {
                if (seatButton.BackColor == Color.Green)
                {
                    DialogResult result = MessageBox.Show(
                        $"Kas soovite broneerida koha {seatButton.Text}?",
                        "Kinnitage broneeringut",
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
                        $"Kas soovite vabastada istme {seatButton.Text}?",
                        "Kinnitage vabastamist",
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

                    MessageBox.Show("Istekoha staatus on edukalt uuendatud.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Viga istekoha staatuse ajakohastamisel: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
}
