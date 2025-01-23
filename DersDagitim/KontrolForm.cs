using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DersDagitim
{
    public partial class KontrolForm : Form
    {
        public KontrolForm()
        {
            InitializeComponent();
        }

        private void KontrolForm_Load(object sender, EventArgs e)
        {
            bool hata = false;
            ArrayList lstTumu = new ArrayList();
            lstTumu.AddRange(tanim.program.ogretmenler);
            lstTumu.AddRange(tanim.program.derslikler);
            lstTumu.AddRange(tanim.program.siniflar);

            lstHatalar.Items.Add("Toplam saat ile uygun saat kontrol ediliyor!");
            lstHatalar.Refresh();

            for (int i = 0; i < lstTumu.Count; i++)
            {
                bilesenTaban bilesen = lstTumu[i] as bilesenTaban;
                int toplamSaat=tanim.program.bilesenDersSayisi(bilesen);
                int uygunSaat=tanim.program.uygunDersSaatiSay(bilesen);
                if (uygunSaat < toplamSaat)
                {
                    lstHatalar.Items.Add(bilesen.adi + " Uygun olduğu saat yetersiz");
                    hata = true;
                }
            }

            lstHatalar.Items.Add("Tanimli derslerin yerleşim olasılıkları kontrol ediliyor!");
            lstHatalar.Refresh();
            for (int i = 0; i < tanim.program.tanimliDersler.Count; i++)
            {
                bilesenTanimliDers tanimliders = tanim.program.tanimliDersler[i] as bilesenTanimliDers;
                tanimliders.yerlesimeHazirla();
                if (tanimliders.olasiliklar.Count == 0)
                {
                    hata = true;
                    lstHatalar.Items.Add(tanimliders.aciklama + " Tanımlı dersin yerleştirilmesi imkansız");
                }
            }

            foreach (bilesenTanimliDers tanimliDers in tanim.program.tanimliDersler)
                tanimliDers.iliskileriOlustur();

            if (hata)
                lstHatalar.Items.Add("Hatalar var! Dağıtım yapılamaz!");
            else
            {
                lstHatalar.Items.Add("Hata Yok!! Ders Dağıtımı yapılabilir");
                btnKorDagitimYap.Enabled = true;
                btnAkilliDagitim.Enabled = true;
            }


        }

        private void btnDagitimYap_Click(object sender, EventArgs e)
        {
            KorDagitimiForm frmDagitim = new KorDagitimiForm();
            frmDagitim.ShowDialog();

        }

        private void btnAkilliDagitim_Click(object sender, EventArgs e)
        {
            this.Close();
            AkilliDagitimForm frmAkilliDagit = new AkilliDagitimForm();
            frmAkilliDagit.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmHavuzluTarama frmhavuzluTarama = new frmHavuzluTarama();
            frmhavuzluTarama.ShowDialog();
        }
    }
}
