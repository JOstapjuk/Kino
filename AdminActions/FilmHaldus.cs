using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Kino.AdminActions
{
    public partial class FilmHaldus : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jeliz\source\repos\Kino\KinoDB.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapter;

        private readonly string posterImgPath = Path.GetFullPath(@"..\..\PosterImg"); // Define the path once

        public FilmHaldus()
        {
            InitializeComponent();
            NaitaAndmed();
        }

        public void NaitaAndmed()
        {
            conn.Open();
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT * FROM film", conn);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void LisaBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NimetusBox.Text) &&
                !string.IsNullOrWhiteSpace(ZanrBox.Text) &&
                !string.IsNullOrWhiteSpace(RezisoorBox.Text) &&
                !string.IsNullOrWhiteSpace(PikkusBox.Text) &&
                !string.IsNullOrWhiteSpace(OsataitjadBox.Text))
            {
                try
                {
                    OpenFileDialog open = new OpenFileDialog
                    {
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                        Multiselect = false,
                        Filter = "Image Files(*.jpeg;*.png;*.bmp;*.jpg)|*.jpeg;*.png;*.bmp;*.jpg"
                    };

                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        if (!Directory.Exists(posterImgPath))
                        {
                            Directory.CreateDirectory(posterImgPath);
                        }

                        string extension = Path.GetExtension(open.FileName);
                        string fileName = NimetusBox.Text + extension; // Use film name as file name
                        string savePath = Path.Combine(posterImgPath, fileName);

                        // Save poster image to designated folder
                        File.Copy(open.FileName, savePath, true);

                        conn.Open();
                        cmd = new SqlCommand("INSERT INTO film (Nimetus, Zanr, Rezisoor, Pikkus, Osataitjad, Poster) VALUES (@nimetus, @zanr, @rezisoor, @pikkus, @osataitjad, @poster)", conn);
                        cmd.Parameters.AddWithValue("@nimetus", NimetusBox.Text);
                        cmd.Parameters.AddWithValue("@zanr", ZanrBox.Text);
                        cmd.Parameters.AddWithValue("@rezisoor", RezisoorBox.Text);
                        cmd.Parameters.AddWithValue("@pikkus", PikkusBox.Text);
                        cmd.Parameters.AddWithValue("@osataitjad", OsataitjadBox.Text);
                        cmd.Parameters.AddWithValue("@poster", fileName);

                        cmd.ExecuteNonQuery();
                        conn.Close();

                        PosterPictureBox.Image = Image.FromFile(savePath);
                        PosterPictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                        MessageBox.Show("Film lisatud ja poster salvestatud!");
                        NaitaAndmed();
                    }
                    else
                    {
                        MessageBox.Show("Palun vali poster enne lisamist!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Viga: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Palun sisesta kõik andmed enne lisamist!");
            }
        }

        private void KustutaBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                    string posterFileName = dataGridView1.CurrentRow.Cells["Poster"].Value.ToString();

                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM film WHERE Id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    string fullPath = Path.Combine(posterImgPath, posterFileName);
                    if (File.Exists(fullPath))
                    {
                        if (PosterPictureBox.Image != null)
                        {
                            PosterPictureBox.Image.Dispose();
                            PosterPictureBox.Image = null;
                        }

                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        try
                        {
                            File.Delete(fullPath);
                        }
                        catch
                        {
                            File.SetAttributes(fullPath, FileAttributes.Normal);
                            File.Delete(fullPath);
                        }

                        MessageBox.Show("Film ja poster kustutatud!");
                    }
                    else
                    {
                        MessageBox.Show($"Posterit ei leitud: {fullPath}");
                    }

                    NaitaAndmed();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Viga: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Palun vali film, mida kustutada.");
            }
        }

        private void UuendaBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NimetusBox.Text) &&
                !string.IsNullOrWhiteSpace(ZanrBox.Text) &&
                !string.IsNullOrWhiteSpace(RezisoorBox.Text) &&
                !string.IsNullOrWhiteSpace(PikkusBox.Text) &&
                !string.IsNullOrWhiteSpace(OsataitjadBox.Text))
            {
                try
                {
                    string currentFileName = dataGridView1.SelectedRows[0].Cells["Poster"].Value.ToString();
                    string newFileName = NimetusBox.Text + Path.GetExtension(currentFileName);
                    string saveDirectory = Path.Combine(Path.GetFullPath(@"..\..\PosterImg"));
                    string newPosterPath = Path.Combine(saveDirectory, newFileName);

                    // Ask user if they want to update the poster
                    DialogResult result = MessageBox.Show(
                        "Kas soovite uuendada postrit?",
                        "Poster Update",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Open file dialog for selecting a new poster
                        OpenFileDialog open = new OpenFileDialog
                        {
                            InitialDirectory = @"C:\Users\opilane\Pictures\",
                            Multiselect = false,
                            Filter = "Image Files(*.jpeg;*.png;*.bmp;*.jpg)|*.jpeg;*.png;*.bmp;*.jpg"
                        };

                        if (open.ShowDialog() == DialogResult.OK)
                        {
                            // Ensure save directory exists
                            if (!Directory.Exists(saveDirectory))
                            {
                                Directory.CreateDirectory(saveDirectory);
                            }

                            // Copy the new poster to the designated folder
                            File.Copy(open.FileName, newPosterPath, true);

                            // Dispose of the existing image in the PictureBox
                            if (PosterPictureBox.Image != null)
                            {
                                PosterPictureBox.Image.Dispose();
                                PosterPictureBox.Image = null;
                            }

                            // Load the new image into the PictureBox
                            PosterPictureBox.Image = Image.FromFile(newPosterPath);
                            PosterPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            MessageBox.Show("Poster uuendamata! Kasutan olemasolevat postrit.");
                        }
                    }

                    // Update database with new details
                    conn.Open();
                    cmd = new SqlCommand("UPDATE film SET Nimetus = @nimetus, Zanr = @zanr, Rezisoor = @rezisoor, Pikkus = @pikkus, Osataitjad = @osataitjad, Poster = @poster WHERE Id = @id", conn);
                    cmd.Parameters.AddWithValue("@nimetus", NimetusBox.Text);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value));
                    cmd.Parameters.AddWithValue("@zanr", ZanrBox.Text);
                    cmd.Parameters.AddWithValue("@rezisoor", RezisoorBox.Text);
                    cmd.Parameters.AddWithValue("@pikkus", PikkusBox.Text);
                    cmd.Parameters.AddWithValue("@osataitjad", OsataitjadBox.Text);
                    cmd.Parameters.AddWithValue("@poster", Path.GetFileName(newPosterPath)); // Save only the file name

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Film ja poster uuendatud!");
                    NaitaAndmed(); // Refresh data in the grid
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Viga: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Palun täida kõik väljad ja vali film, mida uuendada!");
            }
        }




        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["Id"].Value != null)
                {
                    int filmId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
                    NimetusBox.Text = dataGridView1.Rows[e.RowIndex].Cells["Nimetus"].Value.ToString();
                    ZanrBox.Text = dataGridView1.Rows[e.RowIndex].Cells["Zanr"].Value.ToString();
                    RezisoorBox.Text = dataGridView1.Rows[e.RowIndex].Cells["Rezisoor"].Value.ToString();
                    PikkusBox.Text = dataGridView1.Rows[e.RowIndex].Cells["Pikkus"].Value.ToString();
                    OsataitjadBox.Text = dataGridView1.Rows[e.RowIndex].Cells["Osataitjad"].Value.ToString();

                    string posterFileName = dataGridView1.Rows[e.RowIndex].Cells["Poster"].Value.ToString();

                    if (!string.IsNullOrWhiteSpace(posterFileName))
                    {
                        string relativePosterPath = Path.Combine(Path.GetFullPath(@"..\..\PosterImg"), posterFileName);

                        if (File.Exists(relativePosterPath))
                        {
                            // Dispose of the existing image in the PictureBox
                            if (PosterPictureBox.Image != null)
                            {
                                PosterPictureBox.Image.Dispose();
                                PosterPictureBox.Image = null;
                            }

                            // Load the image
                            PosterPictureBox.Image = Image.FromFile(relativePosterPath);
                            PosterPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            MessageBox.Show($"Posterit ei leitud: {relativePosterPath}");
                            PosterPictureBox.Image = null; // Clear the PictureBox
                        }
                    }
                    else
                    {
                        PosterPictureBox.Image = null; // Clear the PictureBox if no poster filename
                    }
                }
                else
                {
                    MessageBox.Show("Rida ei sisalda ID-d!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga: {ex.Message}");
            }
        }

        private void ValjudaBtn_Click(object sender, EventArgs e)
        {
            this.Close();

            // Open the AdminHaldus form
            AdminHaldus adminHaldusForm = new AdminHaldus();
            adminHaldusForm.Show();
        }
    }
}
