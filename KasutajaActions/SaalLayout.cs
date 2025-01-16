using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms;
using System.Linq;
using Kino.Kasutaja;
using System.Drawing;
using Image = iTextSharp.text.Image;

namespace Kino.KasutajaActions
{
    public partial class SaalLayout : Form
    {
        private int saalId;
        public string saalName;
        private int seansId;
        private List<string> selectedSeatsList = new List<string>();
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jeliz\source\repos\Kino\KinoDB.mdf;Integrated Security=True");

        private string userEmail;

        public SaalLayout(int saalId, string saalName, string userEmail, int seansId)
        {
            InitializeComponent();
            this.saalId = saalId;
            this.saalName = saalName;
            this.userEmail = userEmail;
            this.seansId = seansId;

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

                if (seatData.Rows.Count == 0)
                {
                    MessageBox.Show("No data found for SaalId " + saalId);
                }

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
                MessageBox.Show("Seansil ei ole kohti määratud või andmed ei ole saadaval.");
                return;
            }

            bool hasAvailableSeats = seatData.AsEnumerable().Any(r => r["KohtStatus"].ToString() == "saadaval");

            if (!hasAvailableSeats)
            {
                MessageBox.Show("Kõik kohad on broneeritud sellel seansil.");
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
                    Text = $"Rida {rowNum}",
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
                        Text = $"{rowNum},{seat["Koht"]}",
                        Width = 50,
                        Height = 50,
                        Tag = seat["Id"],
                        Margin = new Padding(2)
                    };

                    string seatStatus = seat["KohtStatus"].ToString().ToLower();

                    if (seatStatus == "saadaval")
                    {
                        seatButton.BackColor = Color.Green;
                    }
                    else if (seatStatus == "broneeritud")
                    {
                        seatButton.BackColor = Color.Red;
                    }
                    else
                    {
                        seatButton.BackColor = Color.Gray;
                    }

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
                if (!selectedSeatsList.Contains(seatButton.Text))
                {
                    selectedSeatsList.Add(seatButton.Text);
                    seatButton.BackColor = Color.Yellow;
                }
                else
                {
                    selectedSeatsList.Remove(seatButton.Text);
                    seatButton.BackColor = Color.Green;
                }
            }
            else if (seatButton.BackColor == Color.Red)
            {
                MessageBox.Show("See koht on juba reserveeritud.");
            }
        }


        private void ReserveSeat(string seatRow, string seatNumber)
        {
            try
            {
                conn.Open();

                string seatQuery = "SELECT Id FROM kohad WHERE Rida = @Rida AND Koht = @Koht AND SaalId = @SaalId";
                SqlCommand seatCmd = new SqlCommand(seatQuery, conn);
                seatCmd.Parameters.AddWithValue("@Rida", seatRow);
                seatCmd.Parameters.AddWithValue("@Koht", seatNumber);
                seatCmd.Parameters.AddWithValue("@SaalId", saalId);

                int seatId = Convert.ToInt32(seatCmd.ExecuteScalar());

                string updateQuery = "UPDATE kohad SET KohtStatus = @Status WHERE Id = @SeatId";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@Status", "broneeritud");
                updateCmd.Parameters.AddWithValue("@SeatId", seatId);
                updateCmd.ExecuteNonQuery();

                int userId = UserDetails.KasutajaId;

                if (userId <= 0)
                {
                    string guestInsertQuery = "INSERT INTO kasutajad (KasutajaNimi, Email) VALUES ('Külaline', @Email)";
                    SqlCommand guestCmd = new SqlCommand(guestInsertQuery, conn);
                    guestCmd.Parameters.AddWithValue("@Email", userEmail);
                    guestCmd.ExecuteNonQuery();

                    userId = Convert.ToInt32(new SqlCommand("SELECT SCOPE_IDENTITY()", conn).ExecuteScalar());
                }

                string insertQuery = "INSERT INTO piletid (KasutajaID, SeansID, Koht) VALUES (@KasutajaID, @SeansID, @Koht)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@KasutajaID", userId);
                insertCmd.Parameters.AddWithValue("@SeansID", seansId);
                insertCmd.Parameters.AddWithValue("@Koht", $"{seatRow},{seatNumber}");
                insertCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga istekoha broneerimisel: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }

        private readonly string posterImgPath = Path.GetFullPath(@"..\..\PosterImg");

        public void GenerateAndSendPDFTicket(string seatKoht, string userEmail)
        {
            int seansId = this.seansId;
            string filmTitle = string.Empty;
            string seansDateTimeFormatted = string.Empty;
            string filmPosterPath = string.Empty;
            decimal price = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jeliz\source\repos\Kino\KinoDB.mdf;Integrated Security=True"))
                {
                    conn.Open();

                    string seansQuery = "SELECT Paev, Aeg, FilmID, Hind FROM seans WHERE Id = @SeansId";
                    SqlCommand seansCmd = new SqlCommand(seansQuery, conn);
                    seansCmd.Parameters.AddWithValue("@SeansId", seansId);
                    SqlDataReader seansReader = seansCmd.ExecuteReader();

                    if (seansReader.Read())
                    {
                        string seansDate = Convert.ToString(seansReader["Paev"]); 
                        string seansTime = Convert.ToString(seansReader["Aeg"]);
                        price = Convert.ToDecimal(seansReader["Hind"]);

                        seansDateTimeFormatted = $"{seansDate} {seansTime}";
                        int filmId = Convert.ToInt32(seansReader["FilmID"]);
                        seansReader.Close();

                        string filmQuery = "SELECT Nimetus, Poster FROM film WHERE Id = @FilmId";
                        SqlCommand filmCmd = new SqlCommand(filmQuery, conn);
                        filmCmd.Parameters.AddWithValue("@FilmId", filmId);
                        SqlDataReader filmReader = filmCmd.ExecuteReader();

                        if (filmReader.Read())
                        {
                            filmTitle = Convert.ToString(filmReader["Nimetus"]);
                            filmPosterPath = Convert.ToString(filmReader["Poster"]);
                        }
                    }
                }

                string[] seatParts = seatKoht.Split(',');

                if (seatParts.Length != 2)
                {
                    MessageBox.Show("Viga: Istekoha formaat on vale.");
                    return;
                }

                string formattedSeat = $"{seatParts[0]} rida {seatParts[1]} koht";

                string fullPosterPath = Path.Combine(posterImgPath, filmPosterPath);

                if (!File.Exists(fullPosterPath))
                {
                    MessageBox.Show($"Pildi fail ei leitud: {fullPosterPath}");
                    return;
                }

                Document document = new Document();
                string pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Pilet_" + seatKoht + ".pdf");

                PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
                document.Open();

                document.Add(new Paragraph($"Kinopilet"));
                document.Add(new Paragraph($"Film: {filmTitle}"));
                document.Add(new Paragraph($"Koht: {formattedSeat}"));
                document.Add(new Paragraph($"Seanss: {seansDateTimeFormatted}"));
                document.Add(new Paragraph($"Hind: {price:N2} €"));

                try
                {
                    Image filmPosterImage = Image.GetInstance(fullPosterPath);
                    filmPosterImage.ScaleToFit(150, 150);
                    filmPosterImage.Alignment = Image.ALIGN_CENTER;
                    document.Add(filmPosterImage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Viga pildi laadimisel: " + ex.Message);
                }

                document.Close();
                MessageBox.Show("Pilet loodud ja PDF genereeritud!");

                SendEmailWithAttachment(pdfPath, userEmail);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Viga piletite genereerimisel: " + ex.Message);
            }
        }




        private void SendEmailWithAttachment(string attachmentPath, string recipientEmail)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("jelizaveta.ostapjuk.work@gmail.com", "lsrs danp cdwm ogmd"),
                    EnableSsl = true,
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("jelizaveta.ostapjuk.work@gmail.com"),
                    Subject = "Teie kinopilet",
                    Body = "Tere, \n\nTeie kinopilet on lisatud manusena.",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(recipientEmail);

                mailMessage.Attachments.Add(new Attachment(attachmentPath));

                smtpClient.Send(mailMessage);
                MessageBox.Show("Pilet saadetud e-postiga.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Viga e-kirja saatmisel: " + ex.Message);
            }
        }


        private void BtnReserveSeats_Click(object sender, EventArgs e)
        {
            if (selectedSeatsList.Count == 0)
            {
                MessageBox.Show("Palun valige kohti enne broneerimist.");
                return;
            }

            foreach (var seat in selectedSeatsList)
            {
                string[] seatParts = seat.Split(',');
                if (seatParts.Length == 2)
                {
                    string seatRow = seatParts[0];
                    string seatNumber = seatParts[1];

                    ReserveSeat(seatRow, seatNumber);
                }
            }

            MessageBox.Show("Kõik valitud kohad on broneeritud.");
            LoadSeats();

            foreach (var seat in selectedSeatsList)
            {
                string[] seatParts = seat.Split(',');
                if (seatParts.Length == 2)
                {
                    string seatRow = seatParts[0];
                    string seatNumber = seatParts[1];

                    GenerateAndSendPDFTicket($"{seatRow},{seatNumber}", userEmail);
                }
            }

            selectedSeatsList.Clear();
        }

    }
}
