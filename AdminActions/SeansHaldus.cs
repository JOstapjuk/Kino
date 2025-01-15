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

namespace Kino.AdminActions
{
    public partial class SeansHaldus : Form
    {
        private SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\Source\Repos\Kino\KinoDB.mdf;Integrated Security=True");

        public SeansHaldus()
        {
            InitializeComponent();
            LoadFilms();
            LoadSaals();
            LoadSeanss();
        }

        private void LoadFilms()
        {
            try
            {
                string query = "SELECT Id, Nimetus FROM film";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable filmTable = new DataTable();
                    adapter.Fill(filmTable);
                    cmbFilms.DataSource = filmTable;
                    cmbFilms.DisplayMember = "Nimetus";
                    cmbFilms.ValueMember = "Id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading films: " + ex.Message);
            }
        }

        private void LoadSaals()
        {
            try
            {
                string query = "SELECT Id, Nimetus FROM saal";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable saalTable = new DataTable();
                    adapter.Fill(saalTable);
                    cmbSaals.DataSource = saalTable;
                    cmbSaals.DisplayMember = "Nimetus";
                    cmbSaals.ValueMember = "Id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading saals: " + ex.Message);
            }
        }

        private void LoadSeanss()
        {
            try
            {
                string query = @"
            SELECT s.Id, f.Nimetus as Film, sl.Nimetus as Saal, 
                   s.Paev, s.Aeg, s.Hind
            FROM seans s
            JOIN Film f ON s.FilmID = f.Id
            JOIN Saal sl ON s.SaalID = sl.Id
            ORDER BY s.Paev, s.Aeg";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable seansTable = new DataTable();
                    adapter.Fill(seansTable);
                    dgvSeanss.DataSource = seansTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading seanss: " + ex.Message);
            }
        }

        private void btnAddSeans_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate price input
                if (!decimal.TryParse(txtHind.Text, out decimal hind))
                {
                    MessageBox.Show("Please enter a valid price.");
                    return;
                }

                // Insert the date from Paevtxt (which is in dd.MM.yyyy format)
                string paev = Paevtxt.Text;

                // Insert the time from Aegtxt (which is in HH:mm format)
                string aeg = Aegtxt.Text;

                // Ensure the date and time are valid
                if (string.IsNullOrEmpty(paev) || string.IsNullOrEmpty(aeg))
                {
                    MessageBox.Show("Please ensure both date and time are filled.");
                    return;
                }

                // Query to insert data into the database
                string query = @"
            INSERT INTO seans (FilmID, SaalID, Paev, Aeg, Hind) 
            VALUES (@FilmID, @SaalID, @Paev, @Aeg, @Hind)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FilmID", cmbFilms.SelectedValue);
                    cmd.Parameters.AddWithValue("@SaalID", cmbSaals.SelectedValue);
                    cmd.Parameters.AddWithValue("@Paev", paev);  // Directly use the text from Paevtxt
                    cmd.Parameters.AddWithValue("@Aeg", aeg);   // Directly use the text from Aegtxt
                    cmd.Parameters.AddWithValue("@Hind", hind);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Seans added successfully!");
                    LoadSeanss();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding seans: " + ex.Message);
            }
        }

        private void btnDeleteSeans_Click(object sender, EventArgs e)
        {
            if (dgvSeanss.SelectedRows.Count > 0)
            {
                try
                {
                    // Get the column value safely
                    if (dgvSeanss.SelectedRows[0].Cells["Id"] != null &&
                        int.TryParse(dgvSeanss.SelectedRows[0].Cells["Id"].Value?.ToString(), out int seansId))
                    {
                        string query = "DELETE FROM seans WHERE Id = @SeansID";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@SeansID", seansId);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            MessageBox.Show("Seans deleted successfully!");
                            LoadSeanss();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unable to find a valid SeansID. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting seans: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a seans to delete.");
            }
        }

    }
}
