using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DersDagitim
{
    public partial class ElProgramiFrm : Form
    {
        DataTable dtListe;
        List<bilesenTaban> seciliNesneler = new List<bilesenTaban>();



        public ElProgramiFrm()
        {
            InitializeComponent();
            dtListe = new DataTable();
            dtListe.Columns.Add("id", typeof(UInt16));
            dtListe.Columns.Add("adi", typeof(string));
            lstListe.DisplayMember = "adi";
            lstListe.ValueMember = "id";
            lstListe.DataSource = dtListe;
        }
        

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dtListe.Rows.Clear();
            foreach (bilesenDerslik derslik in tanim.program.derslikler)
            {
                dtListe.Rows.Add(derslik.id, derslik.adi);
            }

        }

        private void rbOgretmen_CheckedChanged(object sender, EventArgs e)
        {
            dtListe.Rows.Clear();
            foreach (bilesenOgretmen ogretmen in tanim.program.ogretmenler)
            {
                dtListe.Rows.Add(ogretmen.id, ogretmen.adi);
            }

        }

        private void rbSinif_CheckedChanged(object sender, EventArgs e)
        {
            dtListe.Rows.Clear();
            foreach (bilesenSinif sinif in tanim.program.siniflar)
            {
                dtListe.Rows.Add(sinif.id, sinif.adi);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstListe.Items.Count; i++)
                lstListe.SetSelected(i, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstListe.Items.Count; i++)
                lstListe.SetSelected(i, false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            seciliNesneler.Clear();
            if (rbOgretmen.Checked)
            {
                for(int i=0;i<lstListe.Items.Count;i++)
                    if(lstListe.GetSelected(i))
                    seciliNesneler.Add(tanim.program.ogretmenGetir(Convert.ToUInt16(dtListe.Rows[i]["id"])));
                
            }
            if (rbDerslik.Checked)
            {
                for(int i=0;i<lstListe.Items.Count;i++)
                    if(lstListe.GetSelected(i))
                    seciliNesneler.Add(tanim.program.derslikGetir(Convert.ToUInt16(dtListe.Rows[i]["id"])));


            }
            if (rbSinif.Checked)
            {
                for(int i=0;i<lstListe.Items.Count;i++)
                    if(lstListe.GetSelected(i))
                    seciliNesneler.Add(tanim.program.sinifGetir(Convert.ToUInt16(dtListe.Rows[i]["id"])));
            }


            if (seciliNesneler.Count > 0)
            {
                PrintDocument rapor = new PrintDocument();

                //rapor.PrinterSettings.PrinterName = araclar.yaziciAyar.PrinterName;
                //rapor.PrinterSettings = (PrinterSettings)araclar.yaziciAyar.Clone();
                //rapor.PrinterSettings= new PrinterSettings as ((PrinterSettings)araclar.yaziciAyar);

                rapor.PrintPage += new PrintPageEventHandler(RaporPrint);
                //rapor.DefaultPageSettings.Landscape = true;
                rapor.DocumentName = "Ders Programı raporu";
                PrintPreviewDialog onizleme = new PrintPreviewDialog();
                onizleme.Document = rapor;
                onizleme.MdiParent = this.ParentForm;
                onizleme.PrintPreviewControl.Zoom = 1;
                ((Form)onizleme).WindowState = FormWindowState.Maximized;
                sayfa = 0;
                onizleme.Show();
            }
        }

        int sayfa;
        private void RaporPrint(object nesne, PrintPageEventArgs e)
        {
            e.HasMorePages = true;
            //e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            int sayfaGenisligi = e.PageBounds.Width;
            int sayfaYuksekligi = e.PageBounds.Height;

            Font fontBaslik = new Font("Times New Roman", 11, FontStyle.Bold);
            Font fontTarih = new Font("Times New Roman", 11.5f, FontStyle.Bold);
            Font fontDersler = new Font("Times New Roman", 12, FontStyle.Italic);
            Font fontKonu = new Font("Times New Roman", 10f, FontStyle.Regular);
            Font fontImza = new Font("Times New Roman", 10, FontStyle.Italic);
            Font fontIncelendi = new Font("Times New Roman", 10, FontStyle.Bold);
            Font fontBuyuk = new Font("Times New Roman", 14, FontStyle.Bold);


            SolidBrush brushNormal = new SolidBrush(Color.Black);
            StringFormat strFormatOrtaOrta = new StringFormat(); strFormatOrtaOrta.Alignment = StringAlignment.Center; strFormatOrtaOrta.LineAlignment = StringAlignment.Center;
            StringFormat strFormatSolUst = new StringFormat();
            StringFormat strFormatSagAlt = new StringFormat(); strFormatSagAlt.LineAlignment = StringAlignment.Far; strFormatSagAlt.Alignment = StringAlignment.Far;
            StringFormat strFormatSolAlt = new StringFormat(); strFormatSolAlt.LineAlignment = StringAlignment.Far; strFormatSolAlt.Alignment = StringAlignment.Near;
            StringFormat strFormatOrtaUst = new StringFormat(); strFormatOrtaUst.LineAlignment = StringAlignment.Near; strFormatOrtaUst.Alignment = StringAlignment.Center;


            StringFormat strFormatOrtaOrtaDik = new StringFormat(); strFormatOrtaOrtaDik.Alignment = StringAlignment.Center; strFormatOrtaOrtaDik.LineAlignment = StringAlignment.Center; strFormatOrtaOrtaDik.FormatFlags = StringFormatFlags.DirectionVertical;

            string baslik = string.Format("{0} MÜDÜRLÜĞÜ\n{1} EĞİTİM ÖĞRETİM YILI DERS PROGRAMI BİLGİSİ", tanim.program.okulAdi,tanim.program.ogretimYili);

            e.Graphics.DrawString(baslik, fontBaslik, brushNormal, new Rectangle(50, 50, sayfaGenisligi - 2 * 50, 100),strFormatOrtaUst);

            if (seciliNesneler[sayfa] is bilesenOgretmen)
            {
                string str = string.Format("Sayı\t\t:.........................................\nAdı Soyadı\t:{0}", seciliNesneler[sayfa].adi);
                e.Graphics.DrawString(str, fontKonu, brushNormal, new Point(50, 100));
            }
            else
            {
                string str = "";
                if (seciliNesneler[sayfa] is bilesenDerslik)
                    str = " DERSLİĞİ DERS PROGRAMI";
                if (seciliNesneler[sayfa] is bilesenSinif)
                    str = " SINIFI DERS PROGRAMI";
                e.Graphics.DrawString(seciliNesneler[sayfa].adi+str, fontBuyuk, brushNormal, new Rectangle(50, 100, sayfaGenisligi - 2 * 50, 50), strFormatOrtaUst);
            }

            
            araclar.dersProgramiCizelgesi(seciliNesneler[sayfa], e.Graphics,80,150);
            //e.Graphics.DrawImage(araclar.dersProgramiCizelgesi(seciliNesneler[sayfa]), 50, 130);
            if (seciliNesneler[sayfa] is bilesenOgretmen)
            {
                string str=string.Format("Yukarıdaki dersler {0} tarihinde şahsınıza verilmiştir. Bilgilerinizi rica ederim.\n\n...../..../.........\nAslını Aldım.",DateTime.Now.ToShortDateString());
                e.Graphics.DrawString(str, fontKonu, brushNormal, new Point(50, 150 + (tanim.program.gunlukDersSaatiSayisi + 1) * 60));

            }


            string stronay=string.Format("{0}\nOkul Müdürü",tanim.program.okulMuduru);
            e.Graphics.DrawString(stronay, fontKonu, brushNormal, new Point(sayfaGenisligi - 200, 250 + (tanim.program.gunlukDersSaatiSayisi + 1) * 60));







            if (++sayfa >= seciliNesneler.Count)
            {
                e.HasMorePages = false;
                sayfa = 0;
            }
        }


    }
}
