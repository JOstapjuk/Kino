using Kino.Kasutaja;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Kino
{
    public partial class OstaPilet : Form
    {
        private SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jeliz\Source\Repos\Kino\KinoDB.mdf;Integrated Security=True");

        public OstaPilet()
        {
            InitializeComponent();
        }

        private void OstaPilet_Load(object sender, EventArgs e)
        {
            LoadSeans();
            if (UserDetails.IsLoggedIn)
            {
                txtEmail.Text = UserDetails.Email;
            }
            else
            {
                MessageBox.Show("Palun sisestage oma e-posti aadress.", "E-post vajalik", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadSeans()
        {
            try
            {
                string query = @"
                SELECT Id, FilmID, Paev, Aeg 
                FROM seans 
                WHERE Paev >= GETDATE()"; // Show future Seans

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable seansData = new DataTable();
                adapter.Fill(seansData);

                cmbSeans.DataSource = seansData;
                cmbSeans.DisplayMember = "FilmID"; // Display film name or other relevant info
                cmbSeans.ValueMember = "Id"; // Bind Seans Id to each item
            }
            catch (Exception ex)
            {
                MessageBox.Show("Viga Seanside laadimisel: " + ex.Message);
            }
        }

        private void cmbSeans_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedSeansId = (int)cmbSeans.SelectedValue;
            ShowSeatLayout(selectedSeansId);
        }

        private void ShowSeatLayout(int seansId)
        {
            SaalLayout seatLayoutForm = new SaalLayout(seansId);  // Pass SeansID instead of SaalID
            seatLayoutForm.ShowDialog();
        }

        private void btnBuyTickets_Click(object sender, EventArgs e)
        {
            try
            {
                int ticketCount = (int)numTickets.Value;

                if (ticketCount == 0)
                {
                    MessageBox.Show("Please select at least one ticket.");
                    return;
                }

                var selectedSeats = GetSelectedSeats(ticketCount);

                if (selectedSeats.Count == 0)
                {
                    MessageBox.Show("Please select at least one seat.");
                    return;
                }

                string customerEmail;
                if (UserDetails.IsLoggedIn)
                {
                    customerEmail = UserDetails.Email;
                }
                else
                {
                    customerEmail = txtEmail.Text;

                    if (string.IsNullOrWhiteSpace(customerEmail) || !IsValidEmail(customerEmail))
                    {
                        MessageBox.Show("Please enter a valid email address.");
                        return;
                    }
                }

                InsertTicketsToDatabase(selectedSeats, customerEmail);

                string ticketInfo = $"Tickets purchased: {ticketCount} tickets\n";
                foreach (var seat in selectedSeats)
                {
                    ticketInfo += $"Seat: {seat}\n";
                }

                SendEmailWithPDF(customerEmail, ticketInfo);
                MessageBox.Show("Tickets purchased and email sent!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error purchasing tickets: " + ex.Message);
            }
        }

        private List<string> GetSelectedSeats(int ticketCount)
        {
            List<string> selectedSeats = new List<string>();

            for (int i = 0; i < ticketCount; i++)
            {
                string seat = GetNextAvailableSeat();
                if (seat != null)
                {
                    selectedSeats.Add(seat);
                }
                else
                {
                    MessageBox.Show("Not enough seats available.");
                    break;
                }
            }
            return selectedSeats;
        }

        private string GetNextAvailableSeat()
        {
            return "A1"; // This is a placeholder
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void InsertTicketsToDatabase(List<string> selectedSeats, string customerEmail)
        {
            try
            {
                foreach (var seat in selectedSeats)
                {
                    string query = "INSERT INTO Piletid (KasutajaID, SeansID, Koht, Email) VALUES (@KasutajaID, @SeansID, @Koht, @Email)";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        int? kasutajaID = UserDetails.IsLoggedIn ? UserDetails.KasutajaId : (int?)null;

                        command.Parameters.AddWithValue("@KasutajaID", kasutajaID);
                        command.Parameters.AddWithValue("@SeansID", cmbSeans.SelectedValue);
                        command.Parameters.AddWithValue("@Koht", seat);
                        command.Parameters.AddWithValue("@Email", customerEmail);

                        conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting tickets into database: " + ex.Message);
            }
        }

        private void SendEmailWithPDF(string recipientEmail, string ticketInfo)
        {
            try
            {
                string sessionDetails = GetSessionDetails(cmbSeans.SelectedValue);
                string pdfFilePath = GeneratePDF(ticketInfo, sessionDetails);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("your-email@hotmail.com"),
                    Subject = "Your Cinema Tickets",
                    Body = "Thank you for purchasing tickets. Please find the attached PDF with your ticket details."
                };
                mailMessage.To.Add(recipientEmail);
                mailMessage.Attachments.Add(new Attachment(pdfFilePath));

                var smtpClient = new SmtpClient("smtp.office365.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("your-email@hotmail.com", "your-password"),
                    EnableSsl = true,
                };

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message);
            }
        }

        private string GetSessionDetails(object seansID)
        {
            return "Movie Name: Example Movie\nDate: 2024-12-31\nTime: 19:00\nPrice: 10 EUR"; // Placeholder
        }

        private string GeneratePDF(string ticketInfo, string sessionDetails)
        {
            string pdfFilePath = @"C:\path\to\your\ticket.pdf";
            return pdfFilePath;
        }
    }
}
