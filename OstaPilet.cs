using Kino.KasutajaActions;
using Kino.Kasutaja;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Kino
{
    public partial class OstaPilet : Form
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jeliz\source\repos\Kino\KinoDB.mdf;Integrated Security=True";
        private int currentSeansId;
        private int saalId;
        private string saalName;

        public OstaPilet()
        {
            InitializeComponent();
            this.Load += OstaPilet_Load;
        }

        private void OstaPilet_Load(object sender, EventArgs e)
        {
            LoadSeans();
            if (UserDetails.IsLoggedIn)
            {
                txtEmail.Text = UserDetails.Email;
            }
        }

        private void LoadSeans()
        {
            try
            {
                string query = @"
            SELECT s.Id AS SeansId, s.SaalId AS SaalId, 
            CAST(s.Paev AS VARCHAR) AS Paev, 
            CAST(s.Aeg AS VARCHAR) AS Aeg, 
            f.Nimetus 
            FROM seans s
            INNER JOIN film f ON s.FilmID = f.Id
            ORDER BY s.Paev, s.Aeg";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString);
                DataTable seansData = new DataTable();

                try
                {
                    int count = adapter.Fill(seansData);

                    if (count == 0)
                    {
                        MessageBox.Show("Seansse ei leitud.");
                        return;
                    }

                    seansData.Columns.Add("DisplayInfo", typeof(string));

                    seansData.Columns["SeansId"].DataType = typeof(int);

                    foreach (DataRow row in seansData.Rows)
                    {
                        string paevStr = row["Paev"].ToString();
                        string aegStr = row["Aeg"].ToString();
                        string filmTitle = row["Nimetus"].ToString();

                        row["DisplayInfo"] = $"{filmTitle} - {paevStr} {aegStr}";
                    }

                    cmbSeans.DataSource = seansData;
                    cmbSeans.DisplayMember = "DisplayInfo";
                    cmbSeans.ValueMember = "SeansId";

                    if (seansData.Rows.Count > 0)
                    {
                        saalId = Convert.ToInt32(seansData.Rows[0]["SaalId"]);
                        saalName = seansData.Rows[0]["Nimetus"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Seansse ei leitud.");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Viga sessioonide laadimises: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Teised vigad: {ex.Message}");
            }
        }

        public void btnBuyTickets_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSeans.SelectedItem == null)
                {
                    MessageBox.Show("Palun valige filmiseanss.");
                    return;
                }

                DataRowView selectedRow = (DataRowView)cmbSeans.SelectedItem;
                int seansId = Convert.ToInt32(selectedRow["SeansId"]);

                string userEmail = txtEmail.Text;

                if (string.IsNullOrWhiteSpace(userEmail))
                {
                    MessageBox.Show("Palun sisestage oma e-posti aadress.");
                    return;
                }

                using (Kino.KasutajaActions.SaalLayout saalLayout = new Kino.KasutajaActions.SaalLayout(saalId, saalName, userEmail, seansId))
                {
                    if (saalLayout.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("Piletid ostetud edukalt! Kontrollige oma e-kirja.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ostu töötlemise viga: {ex.Message}");
            }
        }

    }
}