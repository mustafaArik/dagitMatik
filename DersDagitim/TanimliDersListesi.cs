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
    public partial class TanimliDersListesi : Form
    {
        DataTable dtTanimliDersler;
        bilesenTaban tabanBilesen;

        public TanimliDersListesi(bilesenTaban tabanBilesen=null)
        {
            InitializeComponent();
            tanimliDersleriYenile(tabanBilesen);
            this.tabanBilesen = tabanBilesen;
        }

        private void tanimliDersleriYenile(bilesenTaban tabanBilesen)
        {
            dtTanimliDersler = new DataTable();
            dtTanimliDersler.Columns.Add("id", typeof(UInt16));
            dtTanimliDersler.Columns.Add("dersadi", typeof(string));
            dtTanimliDersler.Columns.Add("ogretmenler", typeof(string));
            dtTanimliDersler.Columns.Add("sinifgruplar", typeof(string));
            dtTanimliDersler.Columns.Add("derslikler", typeof(string));
            dtTanimliDersler.Columns.Add("toplamders", typeof(UInt16));
            dtTanimliDersler.Columns.Add("yerlesim", typeof(string));

            for (int i = 0; i < tanim.program.tanimliDersler.Count; i++)
            {
                bilesenTanimliDers tanimliDers = tanim.program.tanimliDersler[i] as bilesenTanimliDers;
                bool ekle = false;
                if (tabanBilesen == null)
                    ekle = true;
                else
                {
                    if (tabanBilesen is bilesenDers)
                    {
                        if ((tabanBilesen as bilesenDers).id == tanimliDers.ders.id)
                            ekle = true;
                    }

                    if (tabanBilesen is bilesenOgretmen)
                    {
                        bilesenOgretmen ogretmen = tabanBilesen as bilesenOgretmen;
                        for (int j = 0; j < tanimliDers.ogretmenler.Count; j++)
                        {
                            if (ogretmen.id == (tanimliDers.ogretmenler[j] as bilesenOgretmen).id)
                                ekle = true;
                        }
                    }

                    if (tabanBilesen is bilesenDerslik)
                    {
                        bilesenDerslik derslik = tabanBilesen as bilesenDerslik;
                        for (int j = 0; j < tanimliDers.derslikler.Count; j++)
                        {
                            if (derslik.id == (tanimliDers.derslikler[j] as bilesenDerslik).id)
                                ekle = true;
                        }
                    }


                    if (tabanBilesen is bilesenSinif)
                    {
                        bilesenSinif sinif = tabanBilesen as bilesenSinif;
                        for (int j = 0; j < tanimliDers.sinifGruplar.Count; j++)
                        {
                            if (sinif.id == (tanimliDers.sinifGruplar[j] as bilesenSinifGrup).sinif.id)
                                ekle = true;
                        }
                    }



                }


                if (ekle)
                {
                    ushort id = tanimliDers.id;
                    string dersadi = tanimliDers.ders.adi;

                    string ogretmenler = "";
                    for (int j = 0; j < tanimliDers.ogretmenler.Count; j++)
                    {
                        ogretmenler += "(" + (tanimliDers.ogretmenler[j] as bilesenOgretmen).kisaAdi + ")  ";
                    }

                    string sinifgruplar = "";
                    for (int j = 0; j < tanimliDers.sinifGruplar.Count; j++)
                    {
                        string sinifadi = (tanimliDers.sinifGruplar[j] as bilesenSinifGrup).sinif.kisaAdi;
                        string grupadi = (tanimliDers.sinifGruplar[j] as bilesenSinifGrup).grup.kisaAdi;
                        sinifgruplar += string.Format("({0}:{1})  ", sinifadi, grupadi);
                    }

                    string derslikler = "";
                    for (int j = 0; j < tanimliDers.derslikler.Count; j++)
                    {
                        derslikler += "(" + (tanimliDers.derslikler[j] as bilesenDerslik).kisaAdi + ")  ";
                    }

                    ushort toplamDers = tanimliDers.toplamSaat;
                    string yerlesim = tanimliDers.yerlesimStr;
                    dtTanimliDersler.Rows.Add(id, dersadi, ogretmenler, sinifgruplar, derslikler, toplamDers, yerlesim);
                }

            }

            dgvTanimliDersler.DataSource = dtTanimliDersler;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new DersTanimlamaForm(tabanBilesen).ShowDialog();
            tanimliDersleriYenile(tabanBilesen);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvTanimliDersler.SelectedRows.Count > 0)
            {
                DersTanimlamaForm frmDersDuzelt = new DersTanimlamaForm(Convert.ToUInt16(dgvTanimliDersler.SelectedRows[0].Cells[0].Value));
                bilesenTanimliDers tanimliDers= tanim.program.tanimliDersGetir(Convert.ToUInt16(dgvTanimliDersler.SelectedRows[0].Cells[0].Value));
                bool onceYerlesmis = false;
                if (tanimliDers.aktifYerlesim != null)
                {
                    onceYerlesmis = true;
                    tanimliDers.kaldir();
                }
                frmDersDuzelt.ShowDialog();
                if (onceYerlesmis)
                    if (!tanimliDers.eskiyeYerles())
                        MessageBox.Show("Ders üzerinde yapılan değişiklik ile eski yerleşimine yerleşemedi!");

                int sira = dgvTanimliDersler.FirstDisplayedScrollingRowIndex;
                int secSira = dgvTanimliDersler.SelectedRows[0].Index;
                tanimliDersleriYenile(tabanBilesen);
                if (secSira!=-1 && sira != -1 && dgvTanimliDersler.Rows.Count > sira)
                    dgvTanimliDersler.FirstDisplayedScrollingRowIndex = sira;

                if(secSira!=-1 && secSira<dgvTanimliDersler.Rows.Count)
                    dgvTanimliDersler.Rows[secSira].Selected = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvTanimliDersler.SelectedRows.Count > 0)
            {
                ushort silinecekid = Convert.ToUInt16(dgvTanimliDersler.SelectedRows[0].Cells[0].Value);
                for (int i = 0; i < tanim.program.tanimliDersler.Count; i++)
                {
                    bilesenTanimliDers silders = tanim.program.tanimliDersler[i] as bilesenTanimliDers;
                    if (silders.id == silinecekid)
                        tanim.program.tanimliDersler.RemoveAt(i);
                }
                tanimliDersleriYenile(tabanBilesen);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvTanimliDersler.SelectedRows.Count > 0)
            {
                ushort analizId= Convert.ToUInt16(dgvTanimliDersler.SelectedRows[0].Cells[0].Value);
                bilesenTanimliDers tanimliDers = tanim.program.tanimliDersGetir(analizId);
                if (tanimliDers != null)
                    new TanimliDersAnaliz(tanimliDers).ShowDialog();
            }
        }
    }
}
