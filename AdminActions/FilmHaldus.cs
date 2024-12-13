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
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\source\repos\OstapjukTARpv23\KinoDB.mdf;Integrated Security=True");
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
            if (!string.IsNullOrWhiteSpace(NimetusBox.Text) && !string.IsNullOrWhiteSpace(ZanrBox.Text) &&
        !string.IsNullOrWhiteSpace(RezisoorBox.Text) && !string.IsNullOrWhiteSpace(PikkusBox.Text) &&
        !string.IsNullOrWhiteSpace(OsataitjadBox.Text))
            {
                try
                {
                    OpenFileDialog open = new OpenFileDialog();
                    open.InitialDirectory = @"C:\Users\opilane\Pictures\";
                    open.Multiselect = false;
                    open.Filter = "Image Files(*.jpeg;*.png;*.bmp;*.jpg)|*.jpeg;*.png;*.bmp;*.jpg";

                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        string saveDirectory = Path.GetFullPath(@"..\..\PosterImg");
                        if (!Directory.Exists(saveDirectory))
                        {
                            Directory.CreateDirectory(saveDirectory);
                        }
                        string extension = Path.GetExtension(open.FileName);
                        string savePath = Path.Combine(saveDirectory, NimetusBox.Text + extension);

                        File.Copy(open.FileName, savePath, true);

                        conn.Open();
                        cmd = new SqlCommand("INSERT INTO film (Nimetus, Zanr, Rezisoor, Pikkus, Osataitjad, Poster) VALUES (@nimetus, @zanr, @rezisoor, @pikkus, @osataitjad, @poster)", conn);
                        cmd.Parameters.AddWithValue("@nimetus", NimetusBox.Text);
                        cmd.Parameters.AddWithValue("@zanr", ZanrBox.Text);
                        cmd.Parameters.AddWithValue("@rezisoor", RezisoorBox.Text);
                        cmd.Parameters.AddWithValue("@pikkus", PikkusBox.Text);
                        cmd.Parameters.AddWithValue("@osataitjad", OsataitjadBox.Text);
                        cmd.Parameters.AddWithValue("@poster", savePath);

                        cmd.ExecuteNonQuery();
                        conn.Close();

                        PosterPictureBox.Image = Image.FromFile(savePath);

                        MessageBox.Show("Film lisatud!");
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

                    string fullPath = Path.Combine(Path.GetFullPath(@"..\..\PosterImg"), posterFileName);
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

                        MessageBox.Show($"Film ja poster kustutatud!");
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
            if (dataGridView1.CurrentRow != null && !string.IsNullOrWhiteSpace(NimetusBox.Text) &&
                !string.IsNullOrWhiteSpace(ZanrBox.Text) && !string.IsNullOrWhiteSpace(RezisoorBox.Text) &&
                !string.IsNullOrWhiteSpace(PikkusBox.Text) && !string.IsNullOrWhiteSpace(OsataitjadBox.Text))
            {
                try
                {
                    conn.Open();
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);

                    cmd = new SqlCommand("UPDATE film SET Nimetus = @nimetus, Zanr = @zanr, Rezisoor = @rezisoor, Pikkus = @pikkus, Osataitjad = @osataitjad, Poster = @poster WHERE Id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nimetus", NimetusBox.Text);
                    cmd.Parameters.AddWithValue("@zanr", ZanrBox.Text);
                    cmd.Parameters.AddWithValue("@rezisoor", RezisoorBox.Text);
                    cmd.Parameters.AddWithValue("@pikkus", PikkusBox.Text);
                    cmd.Parameters.AddWithValue("@osataitjad", OsataitjadBox.Text);

                    MemoryStream ms = new MemoryStream();
                    PosterPictureBox.Image.Save(ms, PosterPictureBox.Image.RawFormat);
                    byte[] imageBytes = ms.ToArray();
                    cmd.Parameters.AddWithValue("@poster", imageBytes);

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Film uuendatud!");
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

                    string posterPath = dataGridView1.Rows[e.RowIndex].Cells["Poster"].Value.ToString();

                    try
                    {
                        string fullPosterPath = Path.Combine(Path.GetFullPath(@"..\..\PosterImg"), posterPath);
                        if (File.Exists(fullPosterPath))
                        {
                            PosterPictureBox.Image = Image.FromFile(fullPosterPath);
                            PosterPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            PosterPictureBox.Image = null;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Viga postri laadimisel!");
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
