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
    public partial class KosulForm : Form
    {
        bool[,] kosulCikis;
        bool[,] kosullar;
        public KosulForm(ref bool[,] kosulGiris, string baslik)
        {
            InitializeComponent();
            kosulCikis = kosulGiris;
            kosullar = araclar.diziKopyala(kosulGiris);
            this.Text = baslik;
            

        }

        private void KosulForm_Load(object sender, EventArgs e)
        {
            pnlUygun.BackColor = Color.Green;
            pnlUygunDegil.BackColor = Color.Red;
            kosulPanel pnl = new kosulPanel(ref kosullar);
            pnl.Location = new Point(10, 10);
            this.Controls.Add(pnl);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            araclar.diziKopyala(ref kosulCikis,kosullar);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
