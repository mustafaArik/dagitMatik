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
    public partial class frmHavuzluTarama : Form
    {
        yerlesimHavuzluTarama havuzluTarama;

        public frmHavuzluTarama()
        {
            InitializeComponent();
        }

        private void frmHavuzluTarama_Load(object sender, EventArgs e)
        {
            havuzluTarama = new yerlesimHavuzluTarama(tanim.program);
            timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDurum.Text = havuzluTarama.durum;
            pbYuzde.Value = havuzluTarama.yuzde;
            lblEnIyiYerlesimYuzde.Text = "%"+ havuzluTarama.enIyiYerlesimYuzde.ToString();
        }

        private void frmHavuzluTarama_FormClosing(object sender, FormClosingEventArgs e)
        {
            havuzluTarama.durdur();
        }
    }
}
