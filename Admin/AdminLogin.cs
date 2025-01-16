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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string AdminNimi = LoginBox.Text;
            string parool = ParoolBox.Text;

            if (string.IsNullOrWhiteSpace(AdminNimi) || string.IsNullOrWhiteSpace(parool))
            {
                MessageBox.Show("Kõik väljad on kohustuslikud.", "Viga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jeliz\\source\\repos\\Kino\\KinoDB.mdf;Integrated Security=True";
            string query = "SELECT COUNT(*) FROM AdminDB WHERE AdminNimi = @AdminNimi AND Parool = @Parool";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AdminNimi", AdminNimi);
                        command.Parameters.AddWithValue("@Parool", parool);

                        int userCount = (int)command.ExecuteScalar();

                        if (userCount > 0)
                        {
                            MessageBox.Show("Login õnnestus!", "Edu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AdminHaldus AdminHaldusForm = new AdminHaldus();
                            AdminHaldusForm.Show();
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

        private void Valjuda_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
