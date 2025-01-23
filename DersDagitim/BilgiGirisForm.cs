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
    public partial class BilgiGirisForm : Form
    {
        DataTable dtDersler;
        DataTable dtSiniflar;
        DataTable dtDerslikler;
        DataTable dtOgretmenler;
        DataTable dtGruplar;

        public BilgiGirisForm(ushort tabNo)
        {
            InitializeComponent();

            this.tbBilgiGirisleri.SelectedIndex = tabNo;
        }

        public void bilgileriYenile()
        {
            tanim.program.temizle();
            dersleriGetir();
            ogretmenleriGetir();
            derslikleriGetir();
            siniflariGetir();
        }

        private void BilgiGirisForm_Load(object sender, EventArgs e)
        {            
            
            bilgileriYenile();
        }

        #region DERS METOTLARI

        private void dersleriGetir()
        {
            int sira = -1;
            
            int ilkSatir=-1;
            if (dgvDersler.Rows.Count > 0)
                ilkSatir = dgvDersler.FirstDisplayedScrollingRowIndex;

            if (dgvDersler.SelectedRows.Count > 0)
                sira = dgvDersler.SelectedRows[0].Index;

            dtDersler = new DataTable();
            dtDersler.Columns.Add("id", typeof(ushort));
            dtDersler.Columns.Add("adi", typeof(string));
            dtDersler.Columns.Add("kisaadi", typeof(string));
            dtDersler.Columns.Add("derssayisi", typeof(ushort));
            dtDersler.Columns.Add("kosul", typeof(Bitmap));


            for (int i = 0; i < tanim.program.dersler.Count; i++)
            {
                bilesenDers ders = tanim.program.dersler[i] as bilesenDers;
                dtDersler.Rows.Add(ders.id, ders.adi, ders.kisaAdi, tanim.program.bilesenDersSayisi(ders), araclar.kosulResim(ders.kosul));
            }

            dgvDersler.DataSource = dtDersler;
            if (sira > dgvDersler.Rows.Count - 1) sira--;
            if (sira != -1)
            {
                dgvDersler.Rows[sira].Selected = true;
            }

            if (ilkSatir < dgvDersler.Rows.Count && ilkSatir != -1)
                dgvDersler.FirstDisplayedScrollingRowIndex = ilkSatir;

        }

        private void btnDersEkle_Click(object sender, EventArgs e)
        {
            if (txtDersAdi.Text != "" && txtDersKisaAdi.Text != "")
            {
                bilesenDers yeniDers = new bilesenDers(++tanim.program.idDersSon, araclar.diziOlustur(), txtDersAdi.Text, txtDersKisaAdi.Text);
                tanim.program.dersler.Add(yeniDers);
                dersleriGetir();
                txtDersAdi.Text = txtDersKisaAdi.Text = "";
                txtDersAdi.Focus();
            }
        }
                
        private void btnDersKosullar_Click(object sender, EventArgs e)
        {
            if (dgvDersler.SelectedRows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvDersler.SelectedRows[0].Cells[0].Value);
                bilesenDers b = tanim.program.dersGetir(id);
                KosulForm kosul = new KosulForm(ref b.kosul, b.adi);
                kosul.ShowDialog();
                dersleriGetir();
            }
        }

        private void dgvDersler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDersler.SelectedRows.Count > 0 && dgvDersler.Rows.Count>0)
            {
                ushort id = Convert.ToUInt16(dgvDersler.SelectedRows[0].Cells[0].Value);
                bilesenDers b = tanim.program.dersGetir(id);
                txtDersAdi.Text = b.adi;
                txtDersKisaAdi.Text = b.kisaAdi;
            } 
        }

        private void btnDersDuzelt_Click(object sender, EventArgs e)
        {
            if (dgvDersler.SelectedRows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvDersler.SelectedRows[0].Cells[0].Value);
                bilesenDers b = tanim.program.dersGetir(id);
                b.adi = txtDersAdi.Text;
                b.kisaAdi = txtDersKisaAdi.Text;
                dersleriGetir();
            } 
        }

        private void btnDersSil_Click(object sender, EventArgs e)
        {
            if (dgvDersler.SelectedRows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvDersler.SelectedRows[0].Cells[0].Value);
                bilesenDers b = tanim.program.dersGetir(id);
                for (int i = 0; i < tanim.program.dersler.Count; i++)
                {
                    if (b.id == ((bilesenDers)tanim.program.dersler[i]).id)
                        tanim.program.dersler.RemoveAt(i);
                }
                bilgileriYenile();
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvDersler.SelectedRows.Count > 0)
            {
                ushort seciliId = Convert.ToUInt16(dgvDersler.SelectedRows[0].Cells[0].Value);
                bilesenDers ders = tanim.program.dersGetir(seciliId);
                TanimliDersListesi tanimliDersListe = new TanimliDersListesi(ders);
                tanimliDersListe.ShowDialog();
                bilgileriYenile();
            }
        }

        #endregion        
        
        #region OGRETMEN METOTLARI

        private void ogretmenleriGetir()
        {
            int sira = -1;

            int ilkSatir = -1;
            if (dgvOgretmenler.SelectedRows.Count > 0)
                ilkSatir = dgvOgretmenler.FirstDisplayedScrollingRowIndex;

            if (dgvOgretmenler.SelectedRows.Count > 0)
                sira = dgvOgretmenler.SelectedRows[0].Index;

            dtOgretmenler = new DataTable();
            dtOgretmenler.Columns.Add("id", typeof(ushort));
            dtOgretmenler.Columns.Add("adisoyadi", typeof(string));
            dtOgretmenler.Columns.Add("kisaadi", typeof(string));
            dtOgretmenler.Columns.Add("derssayisi", typeof(ushort));
            dtOgretmenler.Columns.Add("kosul", typeof(Bitmap));


            for (int i = 0; i < tanim.program.ogretmenler.Count; i++)
            {
                bilesenOgretmen ogretmen = tanim.program.ogretmenler[i] as bilesenOgretmen;
                dtOgretmenler.Rows.Add(ogretmen.id, ogretmen.adi, ogretmen.kisaAdi, tanim.program.bilesenDersSayisi(ogretmen), araclar.kosulResim(ogretmen.kosul));
            }
            dgvOgretmenler.DataSource = dtOgretmenler;

            if (sira > dgvOgretmenler.Rows.Count - 1) sira--;
            if (sira != -1)
            {
                dgvOgretmenler.Rows[sira].Selected = true;
            }

            if (ilkSatir < dgvOgretmenler.Rows.Count && ilkSatir != -1)
                dgvOgretmenler.FirstDisplayedScrollingRowIndex = ilkSatir;

        }

        private void btnOgretmenEkle_Click(object sender, EventArgs e)
        {
            if (txtOgretmenAdi.Text != "" && txtOgretmenKisaAdi.Text != "")
            {
                bilesenOgretmen yeniogretmen = new bilesenOgretmen(++tanim.program.idOgretmenSon, araclar.diziOlustur(), txtOgretmenAdi.Text, txtOgretmenKisaAdi.Text);
                tanim.program.ogretmenler.Add(yeniogretmen);
                ogretmenleriGetir();
                txtOgretmenAdi.Text = txtOgretmenKisaAdi.Text = "";
                txtOgretmenAdi.Focus();
            }
        }


        private void btnOgretmenKosullar_Click(object sender, EventArgs e)
        {
            if (dgvOgretmenler.SelectedRows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvOgretmenler.SelectedRows[0].Cells[0].Value);
                bilesenOgretmen b = tanim.program.ogretmenGetir(id);
                KosulForm kosul = new KosulForm(ref b.kosul, b.adi);
                kosul.ShowDialog();
                ogretmenleriGetir();
            }
        }

        private void dgvOgretmenler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOgretmenler.SelectedRows.Count > 0 && dgvOgretmenler.Rows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvOgretmenler.SelectedRows[0].Cells[0].Value);
                bilesenOgretmen b = tanim.program.ogretmenGetir(id);
                txtOgretmenAdi.Text = b.adi;
                txtOgretmenKisaAdi.Text = b.kisaAdi;
            }
        }

        private void btnOgretmenDuzelt_Click(object sender, EventArgs e)
        {
            if (dgvOgretmenler.SelectedRows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvOgretmenler.SelectedRows[0].Cells[0].Value);
                bilesenOgretmen b = tanim.program.ogretmenGetir(id);
                b.adi = txtOgretmenAdi.Text;
                b.kisaAdi = txtOgretmenKisaAdi.Text;
                ogretmenleriGetir();
            }
        }

        private void btnOgretmenSil_Click(object sender, EventArgs e)
        {
            if (dgvOgretmenler.SelectedRows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvOgretmenler.SelectedRows[0].Cells[0].Value);
                bilesenOgretmen b = tanim.program.ogretmenGetir(id);
                for (int i = 0; i < tanim.program.ogretmenler.Count; i++)
                {
                    if (b.id == ((bilesenOgretmen)tanim.program.ogretmenler[i]).id)
                        tanim.program.ogretmenler.RemoveAt(i);
                }
                bilgileriYenile();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvOgretmenler.SelectedRows.Count > 0)
            {
                ushort seciliId = Convert.ToUInt16(dgvOgretmenler.SelectedRows[0].Cells[0].Value);
                bilesenOgretmen ogretmen = tanim.program.ogretmenGetir(seciliId);
                TanimliDersListesi tanimliDersListe = new TanimliDersListesi(ogretmen);
                tanimliDersListe.ShowDialog();
                bilgileriYenile();
            }
        }

        #endregion

        #region DERSLİK METOTLARI

        private void derslikleriGetir()
        {
            int sira = -1;

            int ilkSatir = -1;
            if (dgvDerslikler.SelectedRows.Count > 0)
                ilkSatir = dgvDerslikler.FirstDisplayedScrollingRowIndex;

            if (dgvDerslikler.SelectedRows.Count > 0)
                sira = dgvDerslikler.SelectedRows[0].Index;

            dtDerslikler = new DataTable();
            dtDerslikler.Columns.Add("id", typeof(ushort));
            dtDerslikler.Columns.Add("adi", typeof(string));
            dtDerslikler.Columns.Add("kisaadi", typeof(string));
            dtDerslikler.Columns.Add("Derssayisi", typeof(ushort));
            dtDerslikler.Columns.Add("kosul", typeof(Bitmap));

            for (int i = 0; i < tanim.program.derslikler.Count; i++)
            {
                bilesenDerslik Derslik = tanim.program.derslikler[i] as bilesenDerslik;
                dtDerslikler.Rows.Add(Derslik.id, Derslik.adi, Derslik.kisaAdi, tanim.program.bilesenDersSayisi(Derslik), araclar.kosulResim(Derslik.kosul));
            }
            dgvDerslikler.DataSource = dtDerslikler;

            if (sira > dgvDerslikler.Rows.Count - 1) sira--;
            if (sira != -1)
            {
                dgvDerslikler.Rows[sira].Selected = true;
            }

            if (ilkSatir < dgvDerslikler.Rows.Count && ilkSatir != -1)
                dgvDerslikler.FirstDisplayedScrollingRowIndex = ilkSatir;

        }

        private void btnDerslikEkle_Click(object sender, EventArgs e)
        {
            if (txtDerslikAdi.Text != "" && txtDerslikKisaAdi.Text != "")
            {
                bilesenDerslik yeniDerslik = new bilesenDerslik(++tanim.program.idDerslikSon, araclar.diziOlustur(), txtDerslikAdi.Text, txtDerslikKisaAdi.Text);
                tanim.program.derslikler.Add(yeniDerslik);
                derslikleriGetir();
                txtDerslikAdi.Text = txtDerslikKisaAdi.Text = "";
                txtDerslikAdi.Focus();
            }
        }

        private void btnDerslikKosullar_Click(object sender, EventArgs e)
        {
            if (dgvDerslikler.SelectedRows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvDerslikler.SelectedRows[0].Cells[0].Value);
                bilesenDerslik b = tanim.program.derslikGetir(id);
                KosulForm kosul = new KosulForm(ref b.kosul, b.adi);
                kosul.ShowDialog();
                derslikleriGetir();
            }
        }

        private void dgvDerslikler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDerslikler.SelectedRows.Count > 0 && dgvDerslikler.Rows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvDerslikler.SelectedRows[0].Cells[0].Value);
                bilesenDerslik b = tanim.program.derslikGetir(id);
                txtDerslikAdi.Text = b.adi;
                txtDerslikKisaAdi.Text = b.kisaAdi;
            }
        }

        private void btnDerslikDuzelt_Click(object sender, EventArgs e)
        {
            if (dgvDerslikler.SelectedRows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvDerslikler.SelectedRows[0].Cells[0].Value);
                bilesenDerslik b = tanim.program.derslikGetir(id);
                b.adi = txtDerslikAdi.Text;
                b.kisaAdi = txtDerslikKisaAdi.Text;
                derslikleriGetir();
            }
        }

        private void btnDerslikSil_Click(object sender, EventArgs e)
        {
            if (dgvDerslikler.SelectedRows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvDerslikler.SelectedRows[0].Cells[0].Value);
                bilesenDerslik b = tanim.program.derslikGetir(id);
                for (int i = 0; i < tanim.program.derslikler.Count; i++)
                {
                    if (b.id == ((bilesenDerslik)tanim.program.derslikler[i]).id)
                        tanim.program.derslikler.RemoveAt(i);
                }
                bilgileriYenile();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (dgvDerslikler.SelectedRows.Count > 0)
            {
                ushort seciliId = Convert.ToUInt16(dgvDerslikler.SelectedRows[0].Cells[0].Value);
                bilesenDerslik derslik = tanim.program.derslikGetir(seciliId);
                TanimliDersListesi tanimliDersListe = new TanimliDersListesi(derslik);
                tanimliDersListe.ShowDialog();
                bilgileriYenile();
            }
        }

        #endregion

        #region SINIF METOTLARI

        private void siniflariGetir()
        {
            int sira = -1;

            int ilkSatir = -1;
            if (dgvSiniflar.SelectedRows.Count > 0)
                ilkSatir = dgvSiniflar.FirstDisplayedScrollingRowIndex;

            if (dgvSiniflar.SelectedRows.Count > 0)
                sira = dgvSiniflar.SelectedRows[0].Index;

            dtSiniflar = new DataTable();
            dtSiniflar.Columns.Add("id", typeof(ushort));
            dtSiniflar.Columns.Add("adi", typeof(string));
            dtSiniflar.Columns.Add("kisaadi", typeof(string));
            dtSiniflar.Columns.Add("Derssayisi", typeof(ushort));
            dtSiniflar.Columns.Add("kosul", typeof(Bitmap));


            for (int i = 0; i < tanim.program.siniflar.Count; i++)
            {
                bilesenSinif Sinif = tanim.program.siniflar[i] as bilesenSinif;
                dtSiniflar.Rows.Add(Sinif.id, Sinif.adi, Sinif.kisaAdi, tanim.program.bilesenDersSayisi(Sinif), araclar.kosulResim(Sinif.kosul));
            }
            dgvSiniflar.DataSource = dtSiniflar;

            if (sira > dgvSiniflar.Rows.Count - 1) sira--;
            if (sira != -1)
            {
                dgvSiniflar.Rows[sira].Selected = true;
            }

            if (ilkSatir < dgvSiniflar.Rows.Count && ilkSatir != -1)
                dgvSiniflar.FirstDisplayedScrollingRowIndex = ilkSatir;

        }

        private void btnSinifEkle_Click(object sender, EventArgs e)
        {
            if (txtSinifAdi.Text != "" && txtSinifKisaAdi.Text != "")
            {
                bilesenSinif yeniSinif = new bilesenSinif(++tanim.program.idSinifSon, araclar.diziOlustur(), txtSinifAdi.Text, txtSinifKisaAdi.Text,new ArrayList());
                tanim.program.siniflar.Add(yeniSinif);
                siniflariGetir();
                txtSinifAdi.Text = txtSinifKisaAdi.Text = "";
                txtSinifAdi.Focus();
            }
        }

        private void btnSinifKosullar_Click(object sender, EventArgs e)
        {
            if (dgvSiniflar.SelectedRows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvSiniflar.SelectedRows[0].Cells[0].Value);
                bilesenSinif b = tanim.program.sinifGetir(id);
                KosulForm kosul = new KosulForm(ref b.kosul, b.adi);
                kosul.ShowDialog();
                siniflariGetir();
            }
        }

        private void gruplariGetir(ushort sinifId)
        {
            int sira = -1;
            if (dgvSinifGruplar.SelectedRows.Count > 0 && dgvSinifGruplar.Rows.Count>0)
                sira = dgvSinifGruplar.SelectedRows[0].Index;

            ushort id = Convert.ToUInt16(dgvSiniflar.SelectedRows[0].Cells[0].Value);
            bilesenSinif b = tanim.program.sinifGetir(id);

            dtGruplar = new DataTable();
            dtGruplar.Columns.Add("id", typeof(ushort));
            dtGruplar.Columns.Add("adi", typeof(string));
            dtGruplar.Columns.Add("kisaadi", typeof(string));
            dtGruplar.Columns.Add("Derssayisi", typeof(ushort));

            for (int i = 0; i < b.gruplar.Count; i++)
            {
                bilesenGrup grup=b.gruplar[i] as bilesenGrup;
                dtGruplar.Rows.Add(grup.id, grup.adi, grup.kisaAdi, tanim.program.bilesenDersSayisi(b,grup));
            }
            dgvSinifGruplar.DataSource = dtGruplar;

            if (sira > dgvSinifGruplar.Rows.Count - 1) sira--;
            if (sira != -1 && sira<dgvSinifGruplar.Rows.Count)
            {
                dgvSinifGruplar.Rows[sira].Selected = true;
            }
        }

        private void dgvSiniflar_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSiniflar.SelectedRows.Count > 0 && dgvSiniflar.Rows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvSiniflar.SelectedRows[0].Cells[0].Value);
                bilesenSinif b = tanim.program.sinifGetir(id);
                txtSinifAdi.Text = b.adi;
                txtSinifKisaAdi.Text = b.kisaAdi;
                gruplariGetir(id);
            }
        }

        private void btnSinifDuzelt_Click(object sender, EventArgs e)
        {
            if (dgvSiniflar.SelectedRows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvSiniflar.SelectedRows[0].Cells[0].Value);
                bilesenSinif b = tanim.program.sinifGetir(id);
                b.adi = txtSinifAdi.Text;
                b.kisaAdi = txtSinifKisaAdi.Text;
                siniflariGetir();
            }
        }

        private void btnSinifSil_Click(object sender, EventArgs e)
        {
            if (dgvSiniflar.SelectedRows.Count > 0)
            {
                ushort id = Convert.ToUInt16(dgvSiniflar.SelectedRows[0].Cells[0].Value);
                bilesenSinif b = tanim.program.sinifGetir(id);
                for (int i = 0; i < tanim.program.siniflar.Count; i++)
                {
                    if (b.id == ((bilesenSinif)tanim.program.siniflar[i]).id)
                        tanim.program.siniflar.RemoveAt(i);
                }
                bilgileriYenile();
            }
        }

        private void btnGrupEkle_Click(object sender, EventArgs e)
        {
            if (txtGrupAdi.Text != "" && txtGrupKisaAdi.Text != "" && dgvSiniflar.SelectedRows.Count>0)
            {
                ushort aktifSinifId=Convert.ToUInt16(dgvSiniflar.SelectedRows[0].Cells[0].Value);
                bilesenSinif aktifSinif = tanim.program.sinifGetir(aktifSinifId);

                bilesenGrup yeniGrup = new bilesenGrup(++aktifSinif.grupIdSon, txtGrupAdi.Text, txtGrupKisaAdi.Text);
                aktifSinif.gruplar.Add(yeniGrup);
                gruplariGetir(aktifSinifId);
                txtGrupAdi.Text = txtGrupKisaAdi.Text = "";
                txtGrupAdi.Focus();

            }
        }

        private void btnGrupDuzelt_Click(object sender, EventArgs e)
        {
            if (dgvSinifGruplar.SelectedRows.Count > 0 && dgvSiniflar.SelectedRows.Count>0)
            {
                ushort idSinif = Convert.ToUInt16(dgvSiniflar.SelectedRows[0].Cells[0].Value);
                bilesenSinif b = tanim.program.sinifGetir(idSinif);
                ushort idGrup=Convert.ToUInt16(dgvSinifGruplar.SelectedRows[0].Cells[0].Value);

                bilesenGrup g = b.grupGetir(idGrup);
                if (idGrup != 0)
                {
                    g.adi = txtGrupAdi.Text;
                    g.kisaAdi = txtGrupKisaAdi.Text;
                    gruplariGetir(idSinif);
                }
            }
        }

        private void btnGrupSil_Click(object sender, EventArgs e)
        {
            if (dgvSinifGruplar.SelectedRows.Count > 0 && dgvSiniflar.SelectedRows.Count>0)
            {
                ushort idSinif = Convert.ToUInt16(dgvSiniflar.SelectedRows[0].Cells[0].Value);
                bilesenSinif b = tanim.program.sinifGetir(idSinif);
                ushort idGrup=Convert.ToUInt16(dgvSinifGruplar.SelectedRows[0].Cells[0].Value);

                for (int i = 0; i < b.gruplar.Count; i++)
                {
                    bilesenGrup g = b.gruplar[i] as bilesenGrup;
                    if (g.id == idGrup && idGrup!=0)
                        b.gruplar.RemoveAt(i);
                }
                bilgileriYenile();
            }


        }

        private void dgvSinifGruplar_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSinifGruplar.SelectedRows.Count > 0 && dgvSiniflar.SelectedRows.Count>0)
            {
                ushort idSinif = Convert.ToUInt16(dgvSiniflar.SelectedRows[0].Cells[0].Value);
                bilesenSinif b = tanim.program.sinifGetir(idSinif);

                bilesenGrup g = b.grupGetir(Convert.ToUInt16(dgvSinifGruplar.SelectedRows[0].Cells[0].Value));
                if (g.id != 0)
                {
                    txtGrupAdi.Text = g.adi;
                    txtGrupKisaAdi.Text = g.kisaAdi;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvSiniflar.SelectedRows.Count > 0)
            {
                ushort seciliId = Convert.ToUInt16(dgvSiniflar.SelectedRows[0].Cells[0].Value);
                bilesenSinif sinif = tanim.program.sinifGetir(seciliId);
                TanimliDersListesi tanimliDersListe = new TanimliDersListesi(sinif);
                tanimliDersListe.ShowDialog();
                bilgileriYenile();
            }
        }

        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgvOgretmenler.SelectedRows.Count > 0)
            {
                ushort seciliId = Convert.ToUInt16(dgvOgretmenler.SelectedRows[0].Cells[0].Value);
                bilesenOgretmen ogretmen = tanim.program.ogretmenGetir(seciliId);
                DersProgramiOnIzlemeForm onizlemeForm = new DersProgramiOnIzlemeForm(ogretmen);
                onizlemeForm.MdiParent = this.MdiParent;
                onizlemeForm.Show();
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dgvDerslikler.SelectedRows.Count > 0)
            {
                ushort seciliId = Convert.ToUInt16(dgvDerslikler.SelectedRows[0].Cells[0].Value);
                bilesenDerslik derslik = tanim.program.derslikGetir(seciliId);
                DersProgramiOnIzlemeForm onizlemeForm = new DersProgramiOnIzlemeForm(derslik);
                onizlemeForm.MdiParent = this.MdiParent;
                onizlemeForm.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dgvSiniflar.SelectedRows.Count > 0)
            {
                ushort seciliId = Convert.ToUInt16(dgvSiniflar.SelectedRows[0].Cells[0].Value);
                bilesenSinif sinif = tanim.program.sinifGetir(seciliId);
                DersProgramiOnIzlemeForm onizlemeForm = new DersProgramiOnIzlemeForm(sinif);
                onizlemeForm.MdiParent = this.MdiParent;
                onizlemeForm.Show();
            }
        }

    }
}
