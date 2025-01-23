using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DersDagitim
{
    public partial class TanimliDersAnaliz : Form
    {
        bilesenTanimliDers analizDers;

        public TanimliDersAnaliz(bilesenTanimliDers _analizDers)
        {
            InitializeComponent();
            analizDers = _analizDers;
        }

        private void TanimliDersAnaliz_Load(object sender, EventArgs e)
        {
            this.Text = analizDers.aciklama;
            analizDers.yerlesimeHazirla();
            pbYerlesim.Image = (Image)araclar.kosulResim(analizDers.kosul,true);
            lblOlasilikToplami.Text = analizDers.olasiliklar.Count.ToString();
        }

    }
}
