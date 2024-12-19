using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Kino.Kasutaja;
using System.Configuration;
using Kino.KasutajaActions;

namespace Kino
{
    public partial class OstaPilet : Form
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jeliz\Source\Repos\Kino\KinoDB.mdf;Integrated Security=True";
        private readonly decimal TICKET_PRICE = 10.00M;
        private int currentSeansId;
        private int currentSaalId;
        private List<string> selectedSeats = new List<string>();


        public OstaPilet()
        {
            InitializeComponent();
            this.Load += OstaPilet_Load;
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
                SELECT s.Id, s.FilmID, 
                CAST(s.Paev AS VARCHAR) AS Paev, 
                CAST(s.Aeg AS VARCHAR) AS Aeg, 
                f.Nimetus 
                FROM seans s
                INNER JOIN film f ON s.FilmID = f.Id
                ORDER BY s.Paev, s.Aeg";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString);
                DataTable seansData = new DataTable();
                adapter.Fill(seansData);

                if (seansData.Rows.Count == 0)
                {
                    MessageBox.Show("Seansse ei leitud.");
                    return;
                }

                seansData.Columns.Add("DisplayInfo", typeof(string));
                seansData.Columns["Id"].DataType = typeof(int);

                foreach (DataRow row in seansData.Rows)
                {
                    string paevStr = row["Paev"].ToString(); 
                    string aegStr = row["Aeg"].ToString();
                    string filmTitle = row["Nimetus"].ToString();

                    row["DisplayInfo"] = $"{filmTitle} - {paevStr} {aegStr}";
                }

                cmbSeans.DataSource = seansData;
                cmbSeans.DisplayMember = "DisplayInfo";
                cmbSeans.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Viga sessioonide laadimises: " + ex.Message);
            }
        }

        public void LoadSeatLayout(int seansId)
        {
            try
            {
                string query = @"
                SELECT SaalId, SaalName
                FROM Seans
                WHERE Id = @SeansId";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SeansId", seansId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentSaalId = Convert.ToInt32(reader["SaalId"]);
                                string saalName = reader["SaalName"].ToString();

                                SaalLayout saalLayout = new SaalLayout(currentSaalId, saalName);

                                if (saalLayout.ShowDialog() == DialogResult.OK)
                                {
                                    List<string> selectedSeats = SaalLayout.GetSelectedSeats();

                                    int ticketCount = (int)numTickets.Value;
                                    if (selectedSeats.Count != ticketCount)
                                    {
                                        MessageBox.Show($"Palun valige täpselt {ticketCount} istekohta.");
                                        return;
                                    }

                                    this.selectedSeats = selectedSeats;
                                }
                            }
                            else
                            {
                                MessageBox.Show("SaalId not found for the selected session.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Viga istmete paigutuse laadimisel: " + ex.Message);
            }
        }

        private bool ValidateSeatsAvailability(List<string> seats)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT COUNT(*) 
                        FROM Piletid 
                        WHERE SeansID = @SeansId 
                        AND Koht IN (@Seats)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SeansId", currentSeansId);
                        cmd.Parameters.AddWithValue("@Seats", string.Join(",", seats));

                        int takenSeats = (int)cmd.ExecuteScalar();
                        if (takenSeats > 0)
                        {
                            MessageBox.Show("Mõned valitud kohad ei ole enam saadaval. Palun valige teised istekohad.");
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga istekohtade vabade kohtade kontrollimisel: {ex.Message}");
                return false;
            }
        }

        private bool ProcessTicketPurchase(List<string> seats, decimal totalPrice)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (string seat in seats)
                        {
                            string insertQuery = @"
                        INSERT INTO Piletid (KasutajaID, SeansID, Koht, Email, Hind)
                        VALUES (@KasutajaId, @SeansId, @Koht, @Email, @Hind)";

                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                object userId = UserDetails.IsLoggedIn ? (object)UserDetails.KasutajaId : DBNull.Value;
                                cmd.Parameters.AddWithValue("@KasutajaId", userId);
                                cmd.Parameters.AddWithValue("@SeansId", currentSeansId);
                                cmd.Parameters.AddWithValue("@Koht", seat);
                                cmd.Parameters.AddWithValue("@Email", UserDetails.Email);
                                cmd.Parameters.AddWithValue("@Hind", totalPrice / seats.Count);
                                cmd.ExecuteNonQuery();
                            }

                            string updateQuery = @"
                        UPDATE kohad 
                        SET KohtStatus = 'broneeritud'
                        WHERE SaalId = @SaalId AND Koht = @Koht";

                            using (SqlCommand cmd = new SqlCommand(updateQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@SaalId", currentSaalId);
                                cmd.Parameters.AddWithValue("@Koht", seat);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private string GenerateTicketPDF(List<string> seats, decimal totalPrice)
        {
            string pdfPath = Path.Combine(Path.GetTempPath(), $"tickets_{Guid.NewGuid()}.pdf");

            using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
            {
                using (Document document = new Document())
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);
                    document.Open();

                    document.Add(new Paragraph("Kinopiletid"));
                    document.Add(new Paragraph($"Ostmise kuupäev: {DateTime.Now}\n\n"));

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = @"
                        SELECT f.Nimetus, s.Paev, s.Aeg
                        FROM seans s
                        INNER JOIN film f ON s.FilmID = f.Id
                        WHERE s.Id = @SeansId";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@SeansId", currentSeansId);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    document.Add(new Paragraph($"Film: {reader["Pealkiri"]}"));
                                    document.Add(new Paragraph($"Kuupäev: {((DateTime)reader["Paev"]).ToShortDateString()}"));
                                    document.Add(new Paragraph($"Aeg: {((TimeSpan)reader["Aeg"]).ToString(@"hh\:mm")}\n\n"));
                                }
                            }
                        }
                    }

                    document.Add(new Paragraph("Istekohad:"));
                    foreach (string seat in seats)
                    {
                        document.Add(new Paragraph($"• Istekohad {seat}"));
                    }

                    document.Add(new Paragraph($"\nKoguhind: €{totalPrice:F2}"));
                    document.Close();
                }
            }

            return pdfPath;
        }

        private void btnBuyTickets_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSeans.SelectedValue == null)
                {
                    MessageBox.Show("Palun valige filmiseanss.");
                    return;
                }

                int ticketCount = (int)numTickets.Value;
                if (!ValidateTicketQuantity(ticketCount)) return;

                if (cmbSeans.SelectedItem != null)
                {
                    DataRowView selectedRow = (DataRowView)cmbSeans.SelectedItem;
                    currentSeansId = Convert.ToInt32(selectedRow["Id"]);
                    LoadSeatLayout(currentSeansId);
                }

                if (!ValidateSeatsAvailability(selectedSeats)) return;  

                decimal totalPrice = CalculateTicketPrice(ticketCount);

                var result = MessageBox.Show(
                    $"Koguhind: €{totalPrice:F2}\nJätkata ostuga?",
                    "Kinnitage ostu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No) return;

                if (ProcessTicketPurchase(selectedSeats, totalPrice))
                {
                    string pdfPath = GenerateTicketPDF(selectedSeats, totalPrice);
                    SendTicketEmail(UserDetails.Email, pdfPath);
                    MessageBox.Show("Piletid ostetud edukalt! Kontrollige piletite kättesaamiseks oma e-kirja.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ostu töötlemise viga: {ex.Message}");
            }
        }


        private decimal CalculateTicketPrice(int quantity)
        {
            decimal totalPrice = TICKET_PRICE * quantity;
            if (quantity >= 5) totalPrice *= 0.85M;
            return totalPrice;
        }

        private bool ValidateTicketQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                MessageBox.Show("Palun valige vähemalt üks pilet.");
                return false;
            }

            if (quantity > 10)
            {
                MessageBox.Show("Maksimaalselt 10 piletit ühe ostu kohta.");
                return false;
            }

            return true;
        }

        private void SendTicketEmail(string recipientEmail, string pdfPath)
        {
            try
            {
                string email = ConfigurationManager.AppSettings["Email"];
                string emailPassword = ConfigurationManager.AppSettings["EmailPassword"];

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(email),
                    Subject = "Teie kinopiletid",
                    Body = "Täname teid piletite ostmise eest. Palun leiate lisatud PDF-faili oma piletite üksikasjadega."
                };
                mailMessage.To.Add(recipientEmail);
                mailMessage.Attachments.Add(new Attachment(pdfPath));

                var smtpClient = new SmtpClient("smtp.office365.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(email, emailPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga e-kirja saatmisel: {ex.Message}");
            }
        }

    }
}
