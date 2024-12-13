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

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\opilane\\source\\repos\\OstapjukTARpv23\\KinoDB.mdf;Integrated Security=True";
            string query = "SELECT COUNT(*) FROM kasutajad WHERE KasutajaNimi = @KasutajaNimi AND Parool = @Parool";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@KasutajaNimi", kasutajaNimi);
                        command.Parameters.AddWithValue("@Parool", parool);

                        int userCount = (int)command.ExecuteScalar();

                        if (userCount > 0)
                        {
                            MessageBox.Show("Login õnnestus!", "Edu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            login = true;
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
