using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DersDagitim
{
    public partial class AkilliDagitimForm : Form
    {
        bool iyilestir;
        public AkilliDagitimForm(bool iyilestir=false)
        {
            InitializeComponent();
            this.iyilestir = iyilestir;
        }

        iyilestirmeliTaramaYap akilliDagitim;

        private void AkilliDagitimForm_Load(object sender, EventArgs e)
        {
            akilliDagitim = new iyilestirmeliTaramaYap(tanim.program,iyilestir);
            timer1.Enabled = true;
            thrYenile = new Thread(bilgileriYenile);
            thrYenile.Start();

        }

        int sayac = 0;

        int yuzde;



        string strYerlesmeyenSayisi;
        bool bitti = false;
        string[] yerlesmeyenler;
        ulong cevirim;
        string aciklama;
        string[] enZorOgretmenler; 

        void bilgileriYenile()
        {
            lock (akilliDagitim.kilitle)
            {
                yuzde = akilliDagitim.yerlesenYuzde;
                strYerlesmeyenSayisi = akilliDagitim.yerlesmeyenSayisi.ToString();
                yerlesmeyenler = akilliDagitim.yerlesmeyenlerListe;
                cevirim = akilliDagitim.sayac;
                this.bitti = akilliDagitim.bitti;
                aciklama = "Süre: " + akilliDagitim.gecenSure + "  Çevirim: " + cevirim.ToString();
                if (sayac % 5 == 0)
                    enZorOgretmenler = akilliDagitim.enZorOnOgretmen;

            }
        }


        Thread thrYenile;


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sayac++ > 240)
            {
                sayac = 0;
                GC.Collect();
            }

            if (thrYenile.ThreadState == ThreadState.Stopped)
            {
                lblYerlesmeyenSayisi.Text = strYerlesmeyenSayisi;

                lstYerlesmeyenler.Items.Clear();
                foreach (string s in yerlesmeyenler)
                    lstYerlesmeyenler.Items.Add(s);

                if (sayac % 5 == 0 && enZorOgretmenler!=null)
                {
                    lstEnZorOgretmenler.Items.Clear();
                    for (int i = 0; i < enZorOgretmenler.Length; i++)
                        if (enZorOgretmenler[i] != null)
                            lstEnZorOgretmenler.Items.Add(enZorOgretmenler[i]);
                }

                this.Text = aciklama;

                prbYerlesmeYuzdesi.Value = yuzde;
                lblYerlesmeYuzdesi.Text = "%" + yuzde.ToString();
                thrYenile = new Thread(bilgileriYenile);
                thrYenile.Start();
            }

            if (bitti)
            {
                timer1.Enabled = false;
                thrYenile.Abort();
                araclar.marioMelodiCal();
                MessageBox.Show(String.Format("Yerleşim Gerçekleştirildi.\nHesaplama Süresi: {0}\nÇevirim: {1}",akilliDagitim.gecenSure, akilliDagitim.sayac));
                this.Close();
            }
        }

        private void AkilliDagitimForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            akilliDagitim.durdur();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void btnSifirla_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!akilliDagitim.bitti)
            {
                if (MessageBox.Show("Tarama sonlandırılacak emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    this.Close();
            }
            
        }






    }
}
