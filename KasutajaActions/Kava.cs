using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Kino
{
    public partial class Kava : Form
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataAdapter adapter;
        private DataTable filmTable;
        private int currentIndex = 0;

        public Kava()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadFilms();
            DisplayFilm(currentIndex);
        }

        private void InitializeDatabase()
        {
            // Replace this connection string with your database connection details
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\opilane\\Source\\Repos\\Kino\\KinoDB.mdf;Integrated Security=True";
            conn = new SqlConnection(connectionString);
        }

        private void LoadFilms()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("SELECT * FROM film", conn);
                adapter = new SqlDataAdapter(cmd);
                filmTable = new DataTable();
                adapter.Fill(filmTable);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading films: {ex.Message}");
            }
        }

        private void DisplayFilm(int index)
        {
            if (filmTable.Rows.Count > 0 && index >= 0 && index < filmTable.Rows.Count)
            {
                DataRow row = filmTable.Rows[index];

                // Assuming you have text boxes or labels for each field
                IdLabel.Text = $"ID: {row["Id"]}";
                NimetusLabel.Text = $"Nimetus: {row["Nimetus"]}";
                ZanrLabel.Text = $"Zanr: {row["Zanr"]}";
                RezisoorLabel.Text = $"Režissöör: {row["Rezisoor"]}";
                PikkusLabel.Text = $"Pikkus: {row["Pikkus"]} min";
                OsataitjadLabel.Text = $"Osatäitjad: {row["Osataitjad"]}";

                // Load the poster image
                string posterPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PosterImg", row["Poster"].ToString());
                if (System.IO.File.Exists(posterPath))
                {
                    PosterPictureBox.Image = Image.FromFile(posterPath);
                }
                else
                {
                    PosterPictureBox.Image = null;
                }
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (currentIndex < filmTable.Rows.Count - 1)
            {
                currentIndex++;
                DisplayFilm(currentIndex);
            }
            else
            {
                MessageBox.Show("See on viimane film.");
            }
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                DisplayFilm(currentIndex);
            }
            else
            {
                MessageBox.Show("See on esimene film.");
            }
        }
    }
}
