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
    public partial class DersProgramiOnIzlemeForm : Form
    {
        bilesenTaban bilesen;
        bool ilkacilis = true;

        public DersProgramiOnIzlemeForm(bilesenTaban _bilesen=null)
        {
            InitializeComponent();
            bilesen = _bilesen;
        }

        DataTable dtOgretmenler, dtSiniflar, dtDerslikler;
        
        public DataTable dtOlustur()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(UInt16));
            dt.Columns.Add("adi", typeof(string));
            return dt;
        }

        void listeyiOlustur()
        {
            dtOgretmenler = dtOlustur();
            dtDerslikler = dtOlustur();
            dtSiniflar = dtOlustur();

            foreach (bilesenOgretmen ogretmen in tanim.program.ogretmenler)
                dtOgretmenler.Rows.Add(ogretmen.id, ogretmen.adi);
            foreach (bilesenSinif sinif in tanim.program.siniflar)
                dtSiniflar.Rows.Add(sinif.id, sinif.adi);
            foreach (bilesenDerslik derslik in tanim.program.derslikler)
                dtDerslikler.Rows.Add(derslik.id, derslik.adi);

            cmbOgretmenler.DisplayMember = "adi";
            cmbOgretmenler.ValueMember = "id";
            cmbOgretmenler.DataSource = dtOgretmenler;

            cmbDerslikler.DisplayMember = "adi";
            cmbDerslikler.ValueMember = "id";
            cmbDerslikler.DataSource = dtDerslikler;

            cmbSiniflar.DisplayMember = "adi";
            cmbSiniflar.ValueMember = "id";
            cmbSiniflar.DataSource = dtSiniflar;
        }

        private void DersProgramiOnIzlemeForm_Load(object sender, EventArgs e)
        {
            listeyiOlustur();
            cmbSiniflar.SelectedIndex = -1;
            cmbOgretmenler.SelectedIndex = -1;
            cmbDerslikler.SelectedIndex = -1;
            ilkacilis = false;
            if (bilesen != null)
            {
                Bitmap bmp = araclar.dersProgramiCizelgesi(bilesen);
                lblProgramSahibi.Text = bilesen.adi;
                pbOnizleme.Image = bmp;
                pbOnizleme.Width = bmp.Width;
                pbOnizleme.Height = bmp.Height;

            }
        }

        private void cmbOgretmenler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ilkacilis) return;
            Bitmap bmp = araclar.dersProgramiCizelgesi(tanim.program.ogretmenGetir(Convert.ToUInt16((cmbOgretmenler.SelectedValue.ToString()))));
            lblProgramSahibi.Text = "Öğretmen: "+cmbOgretmenler.Text;
            pbOnizleme.Image = bmp;
            pbOnizleme.Width = bmp.Width;
            pbOnizleme.Height = bmp.Height;
            cmbSiniflar.Text="";
            //cmbOgretmenler.SelectedIndex = -1;
            cmbDerslikler.Text="";
        }

        private void cmbSiniflar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ilkacilis) return;
            Bitmap bmp = araclar.dersProgramiCizelgesi(tanim.program.sinifGetir(Convert.ToUInt16((cmbSiniflar.SelectedValue.ToString()))));
            lblProgramSahibi.Text = "Sınıf: "+cmbSiniflar.Text;
            pbOnizleme.Image = bmp;
            pbOnizleme.Width = bmp.Width;
            pbOnizleme.Height = bmp.Height;
            //cmbSiniflar.SelectedIndex = -1;
            cmbOgretmenler.Text = "";
            cmbDerslikler.Text = "";

        }

        private void cmbDerslikler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ilkacilis) return;
            Bitmap bmp = araclar.dersProgramiCizelgesi(tanim.program.derslikGetir(Convert.ToUInt16((cmbDerslikler.SelectedValue.ToString()))));
            lblProgramSahibi.Text = "Derslik: "+ cmbDerslikler.Text;
            pbOnizleme.Image = bmp;
            pbOnizleme.Width = bmp.Width;
            pbOnizleme.Height = bmp.Height;
            cmbSiniflar.Text = "";
            cmbOgretmenler.Text = "";
            //cmbDerslikler.SelectedIndex = -1;

        }

    }
}
