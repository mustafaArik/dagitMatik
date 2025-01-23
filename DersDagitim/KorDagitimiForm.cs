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
    public partial class KorDagitimiForm : Form
    {
        korDagitimYap korDagitim;

        int sayac = 0;

        public KorDagitimiForm()
        {
            InitializeComponent();
        }

        private void DersDagitimiForm_Load(object sender, EventArgs e)
        {
            korDagitim = new korDagitimYap(tanim.program);
            timerYuzdeleriAl.Enabled = true;
        }

        private void timerYuzdeleriAl_Tick(object sender, EventArgs e)
        {
            if (sayac++ == 240)
            {
                GC.Collect();
                sayac = 0;
            }

            int yerYuzde=korDagitim.yerlesimYuzde;
            if(yerYuzde>=0 && yerYuzde<=100)
            {
                pbYerlesmisYuzde.Value = yerYuzde;
                lblYerlesimYuzdesi.Text = "%" + yerYuzde;
                lblYerlesmeyenDersSayisi.Text = korDagitim.yerlesmeyenDers.ToString();
                lblUzerindeCalisilanDers.Text = korDagitim.suAnYerlestirilenDers;
            }
            int genelYuzde = korDagitim.genelYuzde;
            if (genelYuzde >= 0 && genelYuzde <= 100)
            {
                lblGenelTaramaYuzde.Text = "%"+genelYuzde.ToString();
                pbGenelYuzde.Value = genelYuzde;
            }
        }

        private void DersDagitimiForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            korDagitim.durdur();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
