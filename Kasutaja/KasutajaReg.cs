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
    public partial class KasutajaReg : Form
    {
        public KasutajaReg()
        {
            InitializeComponent();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            string kasutajaNimi = nimetusBox.Text;
            string parool = paroolBox.Text;
            string email = emailBox.Text;

            if (string.IsNullOrWhiteSpace(kasutajaNimi) || string.IsNullOrWhiteSpace(parool) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Kõik väljad on kohustuslikud.", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jeliz\\Source\\Repos\\Kino\\KinoDB.mdf;Integrated Security=True";
            string query = "INSERT INTO kasutajad (KasutajaNimi, Parool, Email) VALUES (@KasutajaNimi, @Parool, @Email)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@KasutajaNimi", kasutajaNimi);
                        command.Parameters.AddWithValue("@Parool", parool);
                        command.Parameters.AddWithValue("@Email", email);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kasutaja on registreeritud!", "Edu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Registreerimine ebaõnnestus.", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga andmebaasiga ühendamisel: {ex.Message}", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void valjudaBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
