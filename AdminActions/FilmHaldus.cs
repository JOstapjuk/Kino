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
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\Source\Repos\Kino\KinoDB.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapter;

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
                        InitialDirectory = @"C:\Users\opilane\Pictures\",
                        Multiselect = false,
                        Filter = "Image Files(*.jpeg;*.png;*.bmp;*.jpg)|*.jpeg;*.png;*.bmp;*.jpg"
                    };

                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        string saveDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PosterImg");

                        if (!Directory.Exists(saveDirectory))
                        {
                            Directory.CreateDirectory(saveDirectory);
                        }

                        string extension = Path.GetExtension(open.FileName);
                        string fileName = NimetusBox.Text + extension; // Use film name as file name
                        string savePath = Path.Combine(saveDirectory, fileName);

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

                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PosterImg", posterFileName);
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
                    string saveDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PosterImg");
                    string newFileName = NimetusBox.Text + Path.GetExtension(currentFileName);

                    conn.Open();
                    cmd = new SqlCommand("UPDATE film SET Nimetus = @nimetus, Zanr = @zanr, Rezisoor = @rezisoor, Pikkus = @pikkus, Osataitjad = @osataitjad, Poster = @poster WHERE Id = @id", conn);
                    cmd.Parameters.AddWithValue("@nimetus", NimetusBox.Text);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value));
                    cmd.Parameters.AddWithValue("@zanr", ZanrBox.Text);
                    cmd.Parameters.AddWithValue("@rezisoor", RezisoorBox.Text);
                    cmd.Parameters.AddWithValue("@pikkus", PikkusBox.Text);
                    cmd.Parameters.AddWithValue("@osataitjad", OsataitjadBox.Text);
                    cmd.Parameters.AddWithValue("@poster", newFileName);

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    if (currentFileName != newFileName)
                    {
                        string oldPath = Path.Combine(saveDirectory, currentFileName);
                        string newPath = Path.Combine(saveDirectory, newFileName);

                        if (PosterPictureBox.Image != null)
                        {
                            PosterPictureBox.Image.Dispose();
                            PosterPictureBox.Image = null;
                        }

                        if (File.Exists(oldPath))
                        {
                            GC.Collect();
                            GC.WaitForPendingFinalizers();

                            try
                            {
                                File.Move(oldPath, newPath);
                            }
                            catch
                            {
                                File.SetAttributes(oldPath, FileAttributes.Normal);
                                File.Move(oldPath, newPath);
                            }
                        }
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
                        string fullPosterPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PosterImg", posterFileName);

                        if (File.Exists(fullPosterPath))
                        {
                            // Dispose of the existing image in the PictureBox
                            if (PosterPictureBox.Image != null)
                            {
                                PosterPictureBox.Image.Dispose();
                                PosterPictureBox.Image = null;
                            }

                            // Load the new image
                            PosterPictureBox.Image = Image.FromFile(fullPosterPath);
                            PosterPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            MessageBox.Show($"Posterit ei leitud: {fullPosterPath}");
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
    }
}
