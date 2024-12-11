using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Kino
{
    public partial class RollValik : Form
    {
        public RollValik()
        {
            InitializeComponent();
        }

        private void EdasiBtn_Click(object sender, EventArgs e)
        {
            if (radio_kasutaja.Checked)
            {
                KasutajaLogin kasutajaForm = new KasutajaLogin();
                kasutajaForm.Show();
            }
            else if (radio_admin.Checked)
            {
                AdminLogin adminForm = new AdminLogin();
                adminForm.Show();
            }
            else
            {
                MessageBox.Show("Palun valige valik.", "Valik nõutav", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void valjudaBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
