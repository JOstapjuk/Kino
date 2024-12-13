using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kino
{
    public partial class KasutajaKino : Form
    {
        public KasutajaKino()
        {
            InitializeComponent();
        }

        private void KavaBtn_Click(object sender, EventArgs e)
        {
            Kava KavaForm = new Kava();
            KavaForm.Show();
            this.Close();
        }

        private void OstaPiletBtn_Click(object sender, EventArgs e)
        {
            OstaPilet OstaPiletForm = new OstaPilet();
            OstaPiletForm.Show();
            this.Close();
        }

        private void Valjuda_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
