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
            this.userEmail = userEmail; // Store the email
            this.seansId = seansId; // Store the SeansId

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

                // Debug: Check if seat data was loaded
                if (seatData.Rows.Count == 0)
                {
                    MessageBox.Show("No data found for SaalId " + saalId);
                }

                GenerateSeatLayout(seatData); // Re-generate seat layout after the load
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

            // Check if there are available seats
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
                        // Update this line to use both row and seat number
                        Text = $"{rowNum},{seat["Koht"]}",
                        Width = 50,
                        Height = 50,
                        Tag = seat["Id"],
                        Margin = new Padding(2)
                    };

                    // Get the seat status from the database
                    string seatStatus = seat["KohtStatus"].ToString().ToLower();

                    // Set the button color based on seat status
                    if (seatStatus == "saadaval") // Available
                    {
                        seatButton.BackColor = Color.Green;
                    }
                    else if (seatStatus == "broneeritud") // Reserved
                    {
                        seatButton.BackColor = Color.Red;
                    }
                    else
                    {
                        seatButton.BackColor = Color.Gray; // Other states (e.g., maybe "occupied" or "reserved for VIP")
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
                // Check if this seat is already selected
                if (!selectedSeatsList.Contains(seatButton.Text))
                {
                    selectedSeatsList.Add(seatButton.Text); // Add seat to the selected list
                    seatButton.BackColor = Color.Yellow; // Mark the seat as selected (e.g., Yellow)
                }
                else
                {
                    // If the seat is already selected, deselect it
                    selectedSeatsList.Remove(seatButton.Text);
                    seatButton.BackColor = Color.Green; // Mark the seat as available (green)
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

                // Find the seat ID from the database based on row and seat number
                string seatQuery = "SELECT Id FROM kohad WHERE Rida = @Rida AND Koht = @Koht AND SaalId = @SaalId";
                SqlCommand seatCmd = new SqlCommand(seatQuery, conn);
                seatCmd.Parameters.AddWithValue("@Rida", seatRow);
                seatCmd.Parameters.AddWithValue("@Koht", seatNumber);
                seatCmd.Parameters.AddWithValue("@SaalId", saalId);

                int seatId = Convert.ToInt32(seatCmd.ExecuteScalar());

                // Now update the seat status to "broneeritud"
                string updateQuery = "UPDATE kohad SET KohtStatus = @Status WHERE Id = @SeatId";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@Status", "broneeritud");
                updateCmd.Parameters.AddWithValue("@SeatId", seatId);
                updateCmd.ExecuteNonQuery();

                // Insert into the 'piletid' table for the user
                int userId = UserDetails.KasutajaId;

                if (userId <= 0)
                {
                    // Insert guest with email if no logged-in user and email is provided
                    string guestInsertQuery = "INSERT INTO kasutajad (KasutajaNimi, Email) VALUES ('Külaline', @Email)";
                    SqlCommand guestCmd = new SqlCommand(guestInsertQuery, conn);
                    guestCmd.Parameters.AddWithValue("@Email", userEmail); // Use the email passed into the constructor
                    guestCmd.ExecuteNonQuery();

                    userId = Convert.ToInt32(new SqlCommand("SELECT SCOPE_IDENTITY()", conn).ExecuteScalar());
                }

                // Insert the reservation into the 'piletid' table
                string insertQuery = "INSERT INTO piletid (KasutajaID, SeansID, Koht) VALUES (@KasutajaID, @SeansID, @Koht)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@KasutajaID", userId);
                insertCmd.Parameters.AddWithValue("@SeansID", seansId);
                insertCmd.Parameters.AddWithValue("@Koht", $"{seatRow},{seatNumber}");
                insertCmd.ExecuteNonQuery();

                // Update UI to reflect reservation (optional)
                // This part depends on how you generate and display the seats, it could be done using a similar flow to your existing UI updates

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
            // Get session and film details from the database
            int seansId = this.seansId; // Assuming seansId is stored in the class
            string filmTitle = string.Empty;
            string seansDateTimeFormatted = string.Empty;
            string filmPosterPath = string.Empty;
            decimal price = 0;

            try
            {
                // Use 'using' statement to ensure the connection is properly disposed of and closed
                using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jeliz\source\repos\Kino\KinoDB.mdf;Integrated Security=True"))
                {
                    conn.Open();

                    // Get session details including price from the 'seans' table
                    string seansQuery = "SELECT Paev, Aeg, FilmID, Hind FROM seans WHERE Id = @SeansId";
                    SqlCommand seansCmd = new SqlCommand(seansQuery, conn);
                    seansCmd.Parameters.AddWithValue("@SeansId", seansId);
                    SqlDataReader seansReader = seansCmd.ExecuteReader();

                    if (seansReader.Read())
                    {
                        string seansDate = Convert.ToString(seansReader["Paev"]);  // As string
                        string seansTime = Convert.ToString(seansReader["Aeg"]);  // As string
                        price = Convert.ToDecimal(seansReader["Hind"]);  // Assuming price is in the 'Hind' column

                        seansDateTimeFormatted = $"{seansDate} {seansTime}"; // Combine them into "date time" format
                        int filmId = Convert.ToInt32(seansReader["FilmID"]);
                        seansReader.Close();

                        // Get film title and poster from the 'film' table using FilmId
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

                // Format seat (e.g. "3 rida 2 koht" from "3,2")
                string[] seatParts = seatKoht.Split(',');

                // Check if the seatKoht format is correct before accessing the parts
                if (seatParts.Length != 2)
                {
                    MessageBox.Show("Viga: Istekoha formaat on vale.");
                    return;  // Exit the method or handle the error as appropriate
                }

                string formattedSeat = $"{seatParts[0]} rida {seatParts[1]} koht";

                // Combine the poster path from the relative folder and film poster file name
                string fullPosterPath = Path.Combine(posterImgPath, filmPosterPath);

                // Check if the file exists
                if (!File.Exists(fullPosterPath))
                {
                    MessageBox.Show($"Pildi fail ei leitud: {fullPosterPath}");
                    return; // Exit or handle error
                }

                // Generate PDF ticket
                Document document = new Document();
                string pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Pilet_" + seatKoht + ".pdf");

                PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
                document.Open();

                // Add content to PDF
                document.Add(new Paragraph($"Kinopilet"));
                document.Add(new Paragraph($"Film: {filmTitle}"));
                document.Add(new Paragraph($"Koht: {formattedSeat}")); // Using the new seat format
                document.Add(new Paragraph($"Seanss: {seansDateTimeFormatted}"));  // Use the combined date and time
                document.Add(new Paragraph($"Hind: {price:N2} €"));  // Add price with currency format

                // Add film poster image if available
                try
                {
                    Image filmPosterImage = Image.GetInstance(fullPosterPath);
                    filmPosterImage.ScaleToFit(150, 150);  // Adjust size as necessary
                    filmPosterImage.Alignment = Image.ALIGN_CENTER; // Optional: center the image
                    document.Add(filmPosterImage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Viga pildi laadimisel: " + ex.Message);
                }

                document.Close();
                MessageBox.Show("Pilet loodud ja PDF genereeritud!");

                // Now, send the generated PDF as email attachment
                SendEmailWithAttachment(pdfPath, userEmail); // Pass the email address
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
                // Set up SMTP client (using Gmail as in your example)
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("jelizaveta.ostapjuk.work@gmail.com", "lsrs danp cdwm ogmd"),
                    EnableSsl = true,
                };

                // Set up email message
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("jelizaveta.ostapjuk.work@gmail.com"),
                    Subject = "Teie kinopilet",
                    Body = "Tere, \n\nTeie kinopilet on lisatud manusena.",
                    IsBodyHtml = false,
                };

                // Add recipient email (taken from the Osta Pilet form)
                mailMessage.To.Add(recipientEmail);

                // Attach the PDF
                mailMessage.Attachments.Add(new Attachment(attachmentPath));

                // Send the email
                smtpClient.Send(mailMessage);
                MessageBox.Show("Pilet saadetud e-postiga.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Viga e-kirja saatmisel: " + ex.Message);
            }
        }


        // Reserve seats button click handler
        private void BtnReserveSeats_Click(object sender, EventArgs e)
        {
            if (selectedSeatsList.Count == 0)
            {
                MessageBox.Show("Palun valige kohti enne broneerimist.");
                return;
            }

            // Loop through each selected seat and reserve them
            foreach (var seat in selectedSeatsList)
            {
                string[] seatParts = seat.Split(',');
                if (seatParts.Length == 2)
                {
                    string seatRow = seatParts[0];
                    string seatNumber = seatParts[1];

                    // Reserve the seat in the database and UI
                    ReserveSeat(seatRow, seatNumber);
                }
            }

            MessageBox.Show("Kõik valitud kohad on broneeritud.");
            LoadSeats(); // Refresh seat status from the database

            // Generate and send the PDF ticket for each selected seat
            foreach (var seat in selectedSeatsList)
            {
                string[] seatParts = seat.Split(',');
                if (seatParts.Length == 2)
                {
                    string seatRow = seatParts[0];
                    string seatNumber = seatParts[1];

                    // Pass the seat and user email to generate and send the PDF
                    GenerateAndSendPDFTicket($"{seatRow},{seatNumber}", userEmail);
                }
            }

            // Clear the selected seats after processing
            selectedSeatsList.Clear();
        }

    }
}
