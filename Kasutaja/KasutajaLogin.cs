using Kino.Kasutaja;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kino
{
    public partial class KasutajaLogin : Form
    {
        public static bool login;

        public KasutajaLogin()
        {
            InitializeComponent();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            KasutajaReg kasutajaRegForm = new KasutajaReg();
            kasutajaRegForm.Show();
            this.Close();
        }

        private void valjudaBtn_Click(object sender, EventArgs e)
        {
            RollValik RollValikForm = new RollValik();
            RollValikForm.Show();
            this.Close();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string kasutajaNimi = loginBox.Text;
            string parool = paroolBox.Text;

            if (string.IsNullOrWhiteSpace(kasutajaNimi) || string.IsNullOrWhiteSpace(parool))
            {
                MessageBox.Show("Kõik väljad on kohustuslikud.", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jeliz\\Source\\Repos\\Kino\\KinoDB.mdf;Integrated Security=True";
            string query = "SELECT Id, KasutajaNimi, Parool, Email FROM kasutajad WHERE KasutajaNimi = @KasutajaNimi AND Parool = @Parool";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@KasutajaNimi", kasutajaNimi);
                        command.Parameters.AddWithValue("@Parool", parool);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // If login is successful, set login to true and store user details
                                UserDetails.IsLoggedIn = true;
                                UserDetails.KasutajaId = (int)reader["Id"];
                                UserDetails.KasutajaNimi = reader["KasutajaNimi"].ToString();
                                UserDetails.Email = reader["Email"].ToString();

                                MessageBox.Show("Login õnnestus!", "Edu", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                KasutajaKino KasutajaKinoForm = new KasutajaKino();
                                KasutajaKinoForm.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Kasutajanimi või parool on vale.", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga andmebaasiga ühendamisel: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ilmaLoginBtn_Click(object sender, EventArgs e)
        {
            login = false;
            KasutajaKino KasutajaKinoForm = new KasutajaKino();
            KasutajaKinoForm.Show();
            this.Close();
        }
    }
}
