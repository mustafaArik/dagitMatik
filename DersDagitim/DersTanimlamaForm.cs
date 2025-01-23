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
    public partial class DersTanimlamaForm : Form
    {
        DataTable dtDersler;
        DataTable dtOgretmenler;
        DataTable dtDerslikler;
        DataTable dtSiniflar;
        DataTable dtGruplar;

        bilesenTanimliDers duzeltilecekDers;

        DataTable dtEOgretmenler;
        DataTable dtEDerslikler;
        DataTable dtESinifGruplar;

        DataTable dataTableUret()
        {
            DataTable dtUretilen = new DataTable();
            dtUretilen.Columns.Add("id", typeof(UInt16));
            dtUretilen.Columns.Add("adi", typeof(string));
            return dtUretilen;
        }

        public DersTanimlamaForm()
        {
            InitializeComponent();
            bilgileriDoldur();
        }

        public DersTanimlamaForm(bilesenTaban bilesentaban)
        {
            InitializeComponent();
            bilgileriDoldur();

            if (bilesentaban is bilesenDers)
            {
                ushort dersid = Convert.ToUInt16((bilesentaban as bilesenDers).id);
                for (int i = 0; i < dtDersler.Rows.Count; i++)
                    if (dtDersler.Rows[i]["id"].ToString() == dersid.ToString())
                    {
                        cmbDersler.SelectedIndex = i;
                    }
            }

            if (bilesentaban is bilesenOgretmen)
            {
                bilesenOgretmen ogretmen = bilesentaban as bilesenOgretmen;
                dtEOgretmenler.Rows.Add(ogretmen.id, ogretmen.adi);
            }

            if (bilesentaban is bilesenSinif)
            {
                bilesenSinif sinif = bilesentaban as bilesenSinif;
                dtESinifGruplar.Rows.Add(sinif.id, sinif.adi + " " + (sinif.gruplar[0] as bilesenGrup).adi, 0);
            }

            if (bilesentaban is bilesenDerslik)
            {
                bilesenDerslik derslik = bilesentaban as bilesenDerslik;
                dtEDerslikler.Rows.Add(derslik.id, derslik.adi);
            }

        }

        public DersTanimlamaForm(ushort _id)
        {
            InitializeComponent();
            bilgileriDoldur();

            duzeltilecekDers = tanim.program.tanimliDersGetir(_id);

            btnTanimliDersEkle.Text="Düzelt";
            btnEkleDevam.Visible = false;

            for (int i = 0; i < dtDersler.Rows.Count; i++)
            {
                if (dtDersler.Rows[i]["id"].ToString() == duzeltilecekDers.ders.id.ToString())
                    cmbDersler.SelectedIndex = i;
            }

            foreach (bilesenDerslik derslik in duzeltilecekDers.derslikler)
            {
                dtEDerslikler.Rows.Add(derslik.id, derslik.adi);
            }
            foreach (bilesenOgretmen ogretmen in duzeltilecekDers.ogretmenler)
            {
                dtEOgretmenler.Rows.Add(ogretmen.id, ogretmen.adi);
            }

            foreach (bilesenSinifGrup sinifGrup in duzeltilecekDers.sinifGruplar)
            {
                dtESinifGruplar.Rows.Add(sinifGrup.sinif.id, sinifGrup.sinif.kisaAdi + " " + sinifGrup.grup.kisaAdi,sinifGrup.grup.id);
            }
            txtToplamDersSaati.Text = duzeltilecekDers.toplamSaat.ToString();
            txtYerlesimSekli.Text = duzeltilecekDers.yerlesimStr;


        }

        public void bilgileriDoldur()
        {
            //dersleri doldur
            dtDersler = dataTableUret();
            for (int i = 0; i < tanim.program.dersler.Count; i++)
            {
                bilesenDers ders = tanim.program.dersler[i] as bilesenDers;
                dtDersler.Rows.Add(ders.id, ders.adi);
            }
            cmbDersler.DisplayMember = "adi";
            cmbDersler.ValueMember = "id";
            cmbDersler.DataSource = dtDersler;

            //ogretmenleri doldur
            dtOgretmenler = dataTableUret();
            for (int i = 0; i < tanim.program.ogretmenler.Count; i++)
            {
                bilesenOgretmen ogretmen = tanim.program.ogretmenler[i] as bilesenOgretmen;
                dtOgretmenler.Rows.Add(ogretmen.id, ogretmen.adi);
            }
            cmbOgretmenler.DisplayMember = "adi";
            cmbOgretmenler.ValueMember = "id";
            cmbOgretmenler.DataSource = dtOgretmenler;

            //sınıfları doldur
            dtSiniflar = dataTableUret();
            dtSiniflar.Columns.Add("kisaAdi", typeof(string));
            for (int i = 0; i < tanim.program.siniflar.Count; i++)
            {
                bilesenSinif sinif = tanim.program.siniflar[i] as bilesenSinif;
                dtSiniflar.Rows.Add(sinif.id, sinif.adi, sinif.kisaAdi);
            }
            cmbSiniflar.DisplayMember = "adi";
            cmbSiniflar.ValueMember = "id";
            cmbSiniflar.DataSource = dtSiniflar;

            //sınıfları doldur
            dtDerslikler = dataTableUret();
            for (int i = 0; i < tanim.program.derslikler.Count; i++)
            {
                bilesenDerslik derslik = tanim.program.derslikler[i] as bilesenDerslik;
                dtDerslikler.Rows.Add(derslik.id, derslik.adi);
            }
            cmbDerslikler.DisplayMember = "adi";
            cmbDerslikler.ValueMember = "id";
            cmbDerslikler.DataSource = dtDerslikler;

            //Eklenen liste kutularının datatable lerini oluştur
            dtEOgretmenler = dataTableUret();
            dtEDerslikler = dataTableUret();
            dtESinifGruplar = dataTableUret();
            dtESinifGruplar.Columns.Add("grupId", typeof(UInt16));

            lstDerslikler.DisplayMember = lstOgretmenler.DisplayMember = lstSinifGrup.DisplayMember = "adi";
            lstDerslikler.ValueMember = lstOgretmenler.ValueMember = lstSinifGrup.ValueMember = "id";

            lstDerslikler.DataSource = dtEDerslikler;
            lstOgretmenler.DataSource = dtEOgretmenler;
            lstSinifGrup.DataSource = dtESinifGruplar;

        }

        private void cmbSiniflar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSiniflar.SelectedIndex != -1)
            {
                dtGruplar = dataTableUret();
                bilesenSinif sinif = tanim.program.sinifGetir(Convert.ToUInt16(cmbSiniflar.SelectedValue));

                for (int i = 0; i < sinif.gruplar.Count; i++)
                {
                    bilesenGrup grup = sinif.gruplar[i] as bilesenGrup;
                    dtGruplar.Rows.Add(grup.id, grup.adi);
                }

                cmbGruplar.DisplayMember = "adi";
                cmbGruplar.ValueMember = "id";
                cmbGruplar.DataSource = dtGruplar;

            }
        }

        private void btnOgretmenEkle_Click(object sender, EventArgs e)
        {
            if (cmbOgretmenler.SelectedIndex != -1)
            {
                bool eklenmis = false;
                for (int i = 0; i < dtEOgretmenler.Rows.Count; i++)
                {
                    if (dtEOgretmenler.Rows[i]["id"].ToString() == cmbOgretmenler.SelectedValue.ToString())
                        eklenmis = true;
                }
                if (eklenmis)
                    MessageBox.Show("Eklenmiş!!");
                else
                    dtEOgretmenler.Rows.Add(dtOgretmenler.Rows[cmbOgretmenler.SelectedIndex]["id"], dtOgretmenler.Rows[cmbOgretmenler.SelectedIndex]["adi"]);
            }
        }

        private void btnOgretmenSil_Click(object sender, EventArgs e)
        {
            if (lstOgretmenler.SelectedIndex != -1)
            {
                dtEOgretmenler.Rows.RemoveAt(lstOgretmenler.SelectedIndex);
            }
        }

        private void btnDerslikEkle_Click(object sender, EventArgs e)
        {
            if (cmbDerslikler.SelectedIndex != -1)
            {
                if (lstOgretmenler.Items.Count > lstDerslikler.Items.Count)
                {
                    bool eklenmis = false;
                    for (int i = 0; i < dtEDerslikler.Rows.Count; i++)
                    {
                        if (dtEDerslikler.Rows[i]["id"].ToString() == cmbDerslikler.SelectedValue.ToString())
                            eklenmis = true;
                    }
                    if (eklenmis)
                        MessageBox.Show("Eklenmiş!!");
                    else
                        dtEDerslikler.Rows.Add(dtDerslikler.Rows[cmbDerslikler.SelectedIndex]["id"], dtDerslikler.Rows[cmbDerslikler.SelectedIndex]["adi"]);
                }
                else
                    MessageBox.Show("Derslik sayısı öğretmen sayısını geçemez.\nÖğretmenin kullanacağı derslik ekleme sırasına göre belirlenir.\n(Örn:1.Sıradaki öğretmen 1. Sıradaki dersliği kullanır.");
            }
        }

        private void btnDerslikSil_Click(object sender, EventArgs e)
        {
            if (lstDerslikler.SelectedIndex != -1)
            {
                dtEDerslikler.Rows.RemoveAt(lstDerslikler.SelectedIndex);
            }
        }

        private void btnSinifGrupEkle_Click(object sender, EventArgs e)
        {
            if (cmbSiniflar.SelectedIndex != -1 && cmbGruplar.SelectedIndex != -1)
            {
                bool eklenmis = false;
                for (int i = 0; i < dtESinifGruplar.Rows.Count; i++)
                {
                    if (dtESinifGruplar.Rows[i]["id"].ToString() == cmbSiniflar.SelectedValue.ToString() && dtESinifGruplar.Rows[i]["grupId"].ToString() == cmbGruplar.SelectedValue.ToString())
                        eklenmis = true;
                }
                if (eklenmis)
                    MessageBox.Show("Eklenmiş!!");
                else
                {
                    UInt16 sinifId = Convert.ToUInt16(cmbSiniflar.SelectedValue.ToString());
                    string sinifGrup = dtSiniflar.Rows[cmbSiniflar.SelectedIndex]["kisaAdi"].ToString() + " " + cmbGruplar.Text;
                    UInt16 grupId = Convert.ToUInt16(cmbGruplar.SelectedValue.ToString());

                    dtESinifGruplar.Rows.Add(sinifId, sinifGrup, grupId);
                }
            }
        }

        private void btnSinifGrupSil_Click(object sender, EventArgs e)
        {
            if (lstSinifGrup.SelectedIndex != -1)
            {
                dtESinifGruplar.Rows.RemoveAt(lstSinifGrup.SelectedIndex);
            }
        }

        private bool ekle()
        {
            #region BİLGİ GİRİŞİ KONTROL ET
            bool hata = false;
            string mesaj = "Hatalı bilgi girişi.\n";
            if (cmbDersler.SelectedIndex == -1)
            {
                mesaj += "Ders seçilmemiş!\n";
                hata = true;
            }
            //Dersliksiz ders girişi
            if (lstDerslikler.Items.Count == 0)
            {
                if (MessageBox.Show("Derslik önemsiz mi?", "Derslik Seçilmemiş", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    mesaj += "Derslik seçilmemiş!\n";
                    hata = true;
                }
            }
            if (lstOgretmenler.Items.Count == 0)
            {
                mesaj += "Öğretmen seçilmemiş!\n";
                hata = true;
            }
            if (lstSinifGrup.Items.Count == 0)
            {
                mesaj += "Sınıf seçilmemiş!\n";
                hata = true;
            }

            try
            {
                byte b = Convert.ToByte(txtToplamDersSaati.Text);
                string[] bolumler = txtYerlesimSekli.Text.Split('+');
                int toplamSaat = 0;
                for (int i = 0; i < bolumler.Length; i++)
                {
                    toplamSaat += Convert.ToByte(bolumler[i]);
                }
                if (toplamSaat != b)
                {
                    hata = true;
                    mesaj += "Tanımlanan toplam saat ile bölünme uyuşmuyor!";
                }
            }
            catch (Exception ex)
            {
                hata = true;
                mesaj += "Toplam Saat ve Yerleşim şekli girişi yanlış.\n(Örnek: Toplam Saat:4 Yerleşim Şekli:2+2)";
            }



            if (hata)
                MessageBox.Show(mesaj);
            #endregion
            else
            {
                bilesenDers ders = tanim.program.dersGetir(Convert.ToUInt16(cmbDersler.SelectedValue));

                List<bilesenOgretmen> ogretmenler = new List<bilesenOgretmen>();
                for (int i = 0; i < dtEOgretmenler.Rows.Count; i++)
                {
                    bilesenOgretmen ogretmen = tanim.program.ogretmenGetir(Convert.ToUInt16(dtEOgretmenler.Rows[i]["id"].ToString()));
                    ogretmenler.Add(ogretmen);
                }

                List<bilesenDerslik> derslikler = new List<bilesenDerslik>();
                for (int i = 0; i < dtEDerslikler.Rows.Count; i++)
                {
                    bilesenDerslik derslik = tanim.program.derslikGetir(Convert.ToUInt16(dtEDerslikler.Rows[i]["id"].ToString()));
                    derslikler.Add(derslik);
                }

                List<bilesenSinifGrup> sinifGruplar = new List<bilesenSinifGrup>();
                for (int i = 0; i < dtESinifGruplar.Rows.Count; i++)
                {
                    bilesenSinif sinif = tanim.program.sinifGetir(Convert.ToUInt16(dtESinifGruplar.Rows[i]["id"].ToString()));
                    bilesenSinifGrup sinifgrup = new bilesenSinifGrup(sinif, Convert.ToUInt16(dtESinifGruplar.Rows[i]["grupId"].ToString()));
                    sinifGruplar.Add(sinifgrup);
                }
                if (duzeltilecekDers == null)
                {
                    bilesenTanimliDers tanimliders = new bilesenTanimliDers(++tanim.program.idTanimliDersSon, ders, sinifGruplar, ogretmenler, derslikler, txtYerlesimSekli.Text,tanim.program);
                    tanim.program.tanimliDersler.Add(tanimliders);
                    MessageBox.Show("Eklendi");
                }
                else
                {
                    duzeltilecekDers.ders = ders;
                    duzeltilecekDers.ogretmenler = ogretmenler;
                    duzeltilecekDers.derslikler = derslikler;
                    duzeltilecekDers.sinifGruplar = sinifGruplar;
                    duzeltilecekDers.yerlesimStr = txtYerlesimSekli.Text;
                    MessageBox.Show("Düzeltildi");
                }
            }
            return hata;
        }




        private void btnTanimliDersEkle_Click(object sender, EventArgs e)
        {
            if (!ekle()) Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ekle())
                dtESinifGruplar.Rows.Clear();
        }

        private void btnTanimliDersDuzelt_Click(object sender, EventArgs e)
        {

        }
    }
}
