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
    public partial class GenelAyarlar : Form
    {
        public GenelAyarlar()
        {
            InitializeComponent();
        }

        private void GenelAyarlar_Load(object sender, EventArgs e)
        {
            txtOkulAdi.Text = tanim.program.okulAdi;
            txtOkulMuduru.Text = tanim.program.okulMuduru;
            txtOkulMudurYrd.Text = tanim.program.okulMudurYrd;
            chkBasYardimci.Checked = tanim.program.mudurYrdBas;
            txtOgretimYili.Text = tanim.program.ogretimYili;
            cmbGunlukDersSay.SelectedIndex = cmbGunlukDersSay.Items.IndexOf(tanim.program.gunlukDersSaatiSayisi.ToString());
            cmbHaftalikGunSay.SelectedIndex = cmbHaftalikGunSay.Items.IndexOf(tanim.program.haftalikGunSayisi.ToString());
            for (int i = 0; i < tanim.program.gunler.Length; i++)
            {
                lstGunler.Items.Add(tanim.program.gunler[i]);

            }
            for (int i = 0; i < tanim.program.derssaatleri.Length; i++)
            {
                lstDersSaatleri.Items.Add(tanim.program.derssaatleri[i]);
            }

            
        }

        private void lstGunler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGunler.SelectedIndex != -1)
                txtGunAdi.Text = lstGunler.SelectedItem.ToString();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbHaftalikGunSay.SelectedItem) > lstGunler.Items.Count)
            {
                if (txtGunAdi.Text.Length > 0)
                    lstGunler.Items.Add(txtGunAdi.Text);
                else
                    MessageBox.Show("Boş girilemez!");
            }
            else
                MessageBox.Show("Haftalık gün sayısı doldu!");


        }

        private void cmbGunlukDersSay_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbHaftalikGunSay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbHaftalikGunSay.SelectedIndex!=-1)
                for (int i = Convert.ToInt32(cmbHaftalikGunSay.SelectedItem); i < lstGunler.Items.Count; i++)
                {
                    lstGunler.Items.RemoveAt(i--);

                }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (lstGunler.SelectedIndex != -1)
                lstGunler.Items.RemoveAt(lstGunler.SelectedIndex);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (lstGunler.SelectedIndex != -1)
                lstGunler.Items[lstGunler.SelectedIndex] = txtGunAdi.Text;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDersSaatleri.SelectedIndex != -1)
                txtDersSaati.Text = lstDersSaatleri.SelectedItem.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbGunlukDersSay.SelectedItem) > lstDersSaatleri.Items.Count)
            {
                if (txtDersSaati.Text!= "  :  -  :")
                    lstDersSaatleri.Items.Add(txtDersSaati.Text);
                else
                    MessageBox.Show("Boş girilemez");
            }
            else
                MessageBox.Show("Ders Saati Sayısı Doldu!");
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            if (lstDersSaatleri.SelectedIndex != -1)
                lstDersSaatleri.Items.RemoveAt(lstDersSaatleri.SelectedIndex);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (lstDersSaatleri.SelectedIndex != -1)
                lstDersSaatleri.Items[lstDersSaatleri.SelectedIndex] = txtDersSaati.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lstGunler.SelectedIndex >0)
            {
                int sira=lstGunler.SelectedIndex;
                string yedek = lstGunler.Items[sira - 1].ToString();
                lstGunler.Items[sira - 1] = lstGunler.Items[sira];
                lstGunler.Items[sira] = yedek;
                lstGunler.SelectedIndex = sira - 1;


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lstGunler.SelectedIndex < lstGunler.Items.Count-1 && lstGunler.SelectedIndex>-1)
            {
                int sira=lstGunler.SelectedIndex;
                string yedek = lstGunler.Items[sira + 1].ToString();
                lstGunler.Items[sira + 1] = lstGunler.Items[sira];
                lstGunler.Items[sira] = yedek;
                lstGunler.SelectedIndex = sira + 1;


            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (lstDersSaatleri.SelectedIndex >0)
            {
                int sira=lstDersSaatleri.SelectedIndex;
                string yedek = lstDersSaatleri.Items[sira - 1].ToString();
                lstDersSaatleri.Items[sira - 1] = lstDersSaatleri.Items[sira];
                lstDersSaatleri.Items[sira] = yedek;
                lstDersSaatleri.SelectedIndex = sira - 1;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (lstDersSaatleri.SelectedIndex < lstDersSaatleri.Items.Count-1 && lstDersSaatleri.SelectedIndex>-1)
            {
                int sira=lstDersSaatleri.SelectedIndex;
                string yedek = lstDersSaatleri.Items[sira + 1].ToString();
                lstDersSaatleri.Items[sira + 1] = lstDersSaatleri.Items[sira];
                lstDersSaatleri.Items[sira] = yedek;
                lstDersSaatleri.SelectedIndex = sira + 1;


            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool formAc = true;
            if (Convert.ToByte(cmbGunlukDersSay.SelectedItem) != tanim.program.gunlukDersSaatiSayisi || Convert.ToByte(cmbHaftalikGunSay.SelectedItem) != tanim.program.haftalikGunSayisi)
            {
                DialogResult cevap = MessageBox.Show("Yapılan değişiklikler uygulansın mı?", "Uyarı", MessageBoxButtons.YesNo);
                if (cevap == DialogResult.Yes)
                    try
                    {
                        uygula();
                        formAc = true;
                    }
                    catch { formAc = false; }
                if (cevap == DialogResult.No)
                    formAc = false;
            }

            if(formAc)
                new KosulForm(ref tanim.program.kosullar, "Okul Genel Koşulları").ShowDialog();
        }

        private void uygula()
        {
            bool kontrol=true;
            string hata = "";
            if (Convert.ToInt32(cmbHaftalikGunSay.SelectedItem) != lstGunler.Items.Count)
            {
                hata+="Haftalık gün sayısı ile günler uyuşmuyor!\n";
                kontrol=false;
            }

            if (Convert.ToInt32(cmbGunlukDersSay.SelectedItem) != lstDersSaatleri.Items.Count)
            {
                hata+="Günlük ders saati sayısı ile ders saatleri uyuşmuyor!";
                kontrol=false;
            }
            if (kontrol)
            {
                tanim.program.okulAdi = txtOkulAdi.Text;
                tanim.program.okulMuduru = txtOkulMuduru.Text;
                tanim.program.okulMudurYrd = txtOkulMudurYrd.Text;
                tanim.program.mudurYrdBas = chkBasYardimci.Checked;
                tanim.program.ogretimYili = txtOgretimYili.Text;
                tanim.program.haftalikGunSayisi = Convert.ToByte(cmbHaftalikGunSay.SelectedItem);
                tanim.program.gunlukDersSaatiSayisi = Convert.ToByte(cmbGunlukDersSay.SelectedItem);
                tanim.program.gunler = new string[tanim.program.haftalikGunSayisi];
                tanim.program.derssaatleri = new string[tanim.program.gunlukDersSaatiSayisi];
                for (int i = 0; i < tanim.program.haftalikGunSayisi; i++)
                    tanim.program.gunler[i] = lstGunler.Items[i].ToString();
                for (int i = 0; i < tanim.program.gunlukDersSaatiSayisi; i++)
                    tanim.program.derssaatleri[i] = lstDersSaatleri.Items[i].ToString();

                bool[,] yedek = new bool[tanim.program.haftalikGunSayisi, tanim.program.gunlukDersSaatiSayisi];
                for (int i = 0; i < yedek.GetLength(0); i++)
                    for (int j = 0; j < yedek.GetLength(1); j++)
                        yedek[i, j] = true;

                araclar.diziKopyala(ref yedek, tanim.program.kosullar);
                tanim.program.kosullar = yedek;

                ArrayList tumBilesenler=new ArrayList();
                tumBilesenler.AddRange(tanim.program.dersler);
                tumBilesenler.AddRange(tanim.program.ogretmenler);
                tumBilesenler.AddRange(tanim.program.derslikler);
                tumBilesenler.AddRange(tanim.program.siniflar);


                for (int i = 0; i < tumBilesenler.Count; i++)
                {
                    bilesenTaban bilesen = tumBilesenler[i] as bilesenTaban;
                    bool[,] yedekKosul = araclar.diziOlustur();
                    araclar.diziKopyala(ref yedekKosul, bilesen.kosul);
                    bilesen.kosul = yedekKosul;
                }

            }
            else
            {
                MessageBox.Show(hata);
                throw new Exception();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                uygula();
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                uygula();
                this.Close();
            }
            catch { }

            
        }

        private void cmbGunlukDersSay_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbGunlukDersSay.SelectedIndex != -1)
                for (int i = Convert.ToInt32(cmbGunlukDersSay.SelectedItem); i < lstDersSaatleri.Items.Count; i++)
                {
                    lstDersSaatleri.Items.RemoveAt(i--);

                }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
