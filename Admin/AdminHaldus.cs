using Kino.AdminActions;
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
    public partial class AdminHaldus : Form
    {
        public AdminHaldus()
        {
            InitializeComponent();
        }

        private void SaalHaldus_Click(object sender, EventArgs e)
        {
            SaalHaldus SaalHaldusForm = new SaalHaldus();
            SaalHaldusForm.Show();
            this.Close();
        }

        private void PiletHaldus_Click(object sender, EventArgs e)
        {
            PiletHaldus PiletHaldusForm = new PiletHaldus();
            PiletHaldusForm.Show();
            this.Close();
        }

        private void FilmHaldus_Click(object sender, EventArgs e)
        {
            FilmHaldus FilmHaldusForm = new FilmHaldus();
            FilmHaldusForm.Show();
            this.Close();
        }
    }
}
