﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Kino.AdminActions
{
    public partial class SaalHaldus : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jeliz\source\repos\Kino\KinoDB.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapter;

        public SaalHaldus()
        {
            InitializeComponent();
            LoadHalls();
        }

        private void LoadHalls()
        {
            try
            {
                DataTable dt = new DataTable();
                cmd = new SqlCommand("SELECT Id, Nimetus, Kirjeldus FROM saal", conn);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                dataGridViewHalls.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading halls: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnViewLayout_Click(object sender, EventArgs e)
        {
            if (dataGridViewHalls.SelectedRows.Count > 0)
            {
                int saalId = Convert.ToInt32(dataGridViewHalls.SelectedRows[0].Cells["Id"].Value);
                string saalName = dataGridViewHalls.SelectedRows[0].Cells["Nimetus"].Value.ToString();

                SaalLayout layoutForm = new SaalLayout(saalId, saalName);
                layoutForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a hall to view its layout.");
            }
        }

        private void Valjuda_Click(object sender, EventArgs e)
        {
            this.Close();

            AdminHaldus adminHaldusForm = new AdminHaldus();
            adminHaldusForm.Show();
        }
    }
}
