using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms;
using System.Configuration;
using System.Linq;
using System.Drawing;
using Kino.Kasutaja;

namespace Kino.KasutajaActions
{
    public partial class SaalLayout : Form
    {
        private int saalId;
        public string saalName;
        public List<string> SelectedSeats { get; set; }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\Source\Repos\Kino\KinoDB.mdf;Integrated Security=True");

        public SaalLayout(int saalId, string saalName)
        {
            InitializeComponent();
            this.saalId = saalId;
            this.saalName = saalName;
            SelectedSeats = new List<string>();
            LoadSeats();
        }

        public List<string> GetSelectedSeats()
        {
            List<string> selectedSeats = new List<string>();

            foreach (Control control in flowLayoutPanelSeats.Controls)
            {
                if (control is Button seatButton && seatButton.BackColor == Color.Green)
                {
                    selectedSeats.Add(seatButton.Text);
                }
            }

            return selectedSeats;
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
                    ReserveSeat(Convert.ToInt32(seatButton.Tag), "broneeritud", seatButton.Text);
                    seatButton.BackColor = Color.Red;
                    SelectedSeats.Add(seatButton.Text);
                    DialogResult = DialogResult.OK;
                }
            }
            else if (seatButton.BackColor == Color.Red)
            {
                MessageBox.Show("See koht on juba reserveeritud.");
            }
        }

        private void ReserveSeat(int seatId, string status, string seatKoht)
        {
            try
            {
                conn.Open();
                string updateQuery = "UPDATE kohad SET KohtStatus = @Status WHERE Id = @SeatId";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@Status", status);
                updateCmd.Parameters.AddWithValue("@SeatId", seatId);
                updateCmd.ExecuteNonQuery();

                string insertQuery = "INSERT INTO piletid (KasutajaID, SeansID, Koht) VALUES (@KasutajaID, @SeansID, @Koht)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@KasutajaID", UserDetails.KasutajaId);
                insertCmd.Parameters.AddWithValue("@SeansID", this.saalId);
                insertCmd.Parameters.AddWithValue("@Koht", seatKoht);

                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Koht reserveeritud ja pilet lisatud.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Viga istekoha broneerimisel: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
