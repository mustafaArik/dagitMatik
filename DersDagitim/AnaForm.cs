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
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }

        private void yeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool yeniAc = true;
            if (tanim.program != null)
            {
                DialogResult cevap = MessageBox.Show("Açık ders programı var!!\nKaydetmek ister misiniz?", "Uyarı", MessageBoxButtons.YesNoCancel);
                if (cevap == DialogResult.Yes)
                {
                    //Kaydetme Komutları

                }
                if (cevap == DialogResult.Cancel)
                    yeniAc = false;
            }

            if (yeniAc)
            {
                tanim.program = new DersProgrami();
                GenelAyarlar genelAyarlar = new GenelAyarlar();
                //genelAyarlar.MdiParent = this;
                genelAyarlar.ShowDialog();
            }
            
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {

            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text=string.Format("DağıtMatik Ders Dağıtım Programı v{0}.{1}.{2} ({3})", version.Major, version.Minor, version.Build, version.Revision);
        }

        private void genelBilgilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tanim.program != null)
            {
                new GenelAyarlar().ShowDialog();
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is BilgiGirisForm)
                        (frm as BilgiGirisForm).bilgileriYenile();
                }
            }
            else
                MessageBox.Show("Açık ders programı yok!");
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tanim.program != null)
                tanim.program.kaydet();
            else
                MessageBox.Show("Ders programı oluşturulmamış!!");
        }

        private void farklıKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tanim.program != null)
                tanim.program.kaydet(true);
            else
                MessageBox.Show("Ders programı oluşturulmamış!!");
        }

        private void açToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool dosyaAc=true;
            if (tanim.program != null)
            {
                if (MessageBox.Show("Açık olan ders programı kapatılarak yeni ders programı açılacaktır?\nOnaylıyor musunuz?","Uyarı", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    dosyaAc = false;   
            }

            if (dosyaAc)
            {
                DersProgrami prgYedek = new DersProgrami(false);
                if (prgYedek.ac())
                {
                    tanim.program = prgYedek;

                    tanim.program.dagitimaHazirla();
                    foreach (bilesenTanimliDers tanimliDers in tanim.program.tanimliDersler)
                    {
                        tanimliDers.yerlesimeHazirla();
                    }

                    for (int i = 0; i < tanim.program.tanimliDersler.Count; i++)
                    {
                        bilesenTanimliDers tanimliDers = tanim.program.tanimliDersler[i];
                        for (int ii = 0; ii < tanimliDers.olasiliklar.Count; ii++)
                        {
                            bilesenTanimliDers.yerlesimOlasilik ol = tanimliDers.olasiliklar[ii];
                            if (ol.yerlesimStr == tanimliDers.baslangicYerlesimi)
                                if (tanimliDers.olasilikSina(ol))
                                {
                                    tanimliDers.yerles(ol);
                                }
                        }
                    }




                }
            }

        }

        private void bilgiFormGoster(ushort tabNo)
        {
            if (tanim.program != null)
            {
                bool bulundu = false;
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is BilgiGirisForm)
                    {
                        frm.Activate();
                        (frm as BilgiGirisForm).tbBilgiGirisleri.SelectedIndex = tabNo;
                        bulundu = true;
                    }

                }
                if (!bulundu)
                {
                    BilgiGirisForm formBilgiler = new BilgiGirisForm(tabNo);
                    formBilgiler.MdiParent = this;
                    formBilgiler.Show();
                }
            }
            else
                MessageBox.Show("Açık ders programı yok!");
        }

        private void formGoster(int frmNo, bool ozgur=true)
        {
            if (tanim.program != null)
            {
                Form frm=null;
                if (frmNo == 1) frm = new DersTanimlamaForm();
                if (frmNo == 2) frm = new TanimliDersListesi();
                if (frmNo == 3) frm = new KontrolForm();
                if (frmNo == 4) frm = new DersProgramiOnIzlemeForm();
                if (frmNo == 5) frm = new ElProgramiFrm();
                if (ozgur)
                {
                    frm.MdiParent = this;
                    frm.Show();
                }
                else
                {
                    frm.ShowDialog();
                }

            }
            else
            {
                MessageBox.Show("Açık ders programı yok!");
            }
        }

        #region BİLGİ FORMU GÖSTERME
        private void dersliklerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bilgiFormGoster(tanim.TBDERSLIKLER);
        }

        private void derslerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bilgiFormGoster(tanim.TBDERSLER);
        }

        private void öğretmenlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bilgiFormGoster(tanim.TBOGRETMENLER);
        }

        private void sınıflarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bilgiFormGoster(tanim.TBSINIFLAR);
        }
        #endregion

        private void yeniDersTanımlamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formGoster(1,false);
        }

        private void tümTanımlıDerslerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formGoster(2, false);
        }

        private void kontrolEtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tanim.program == null)
            {
                MessageBox.Show("Açık ders programı yok!!");
                return;
            }
            if (tanim.program.tanimliDersler.Count == 0)
            {
                MessageBox.Show("Tanımlı Ders Yok");
                return;
            }
            bool hepsiYerlesmis = true;
            bool dagitimYapiliyor = false;
            foreach (bilesenTanimliDers ders in tanim.program.tanimliDersler)
                if (ders.aktifYerlesim == null)
                    hepsiYerlesmis = false;
            if (hepsiYerlesmis)
            {
                if (MessageBox.Show("Yerleşmiş ders dağılımı silinecek emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    formGoster(3, false);
                    dagitimYapiliyor = true;
                }
            }
            else
            {
                formGoster(3, false);
                dagitimYapiliyor = true;
            }


            if (dagitimYapiliyor)
            {
                hepsiYerlesmis = true;
                foreach (bilesenTanimliDers ders in tanim.program.tanimliDersler)
                    if (ders.aktifYerlesim == null)
                        hepsiYerlesmis = false;
                if (hepsiYerlesmis)
                    formGoster(4, true);
            }
        }

        private void önizlemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formGoster(4, true);            
        }

        private void elProgramlarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tanim.program == null)
                return;
            if (tanim.program.tumuYerlesmis())
            {
                formGoster(5, true);

            }
            else
                MessageBox.Show("Yerleşmeyen dersler var!");
        }

        private void programıİyileştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tanim.program != null && tanim.program.tanimliDersler.Count > 0)
            {
                AkilliDagitimForm formIyilestir = new AkilliDagitimForm(true);
                formIyilestir.MdiParent = this;
                formIyilestir.Show();
            }
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new formHakkinda().ShowDialog();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tanim.program != null)
            {
                if (DialogResult.Yes == MessageBox.Show("Açık ders programı var. Çıkmak istediğinizden emin misiniz?", "Uyarı",MessageBoxButtons.YesNo))
                    Application.Exit();
            }
            else
                Application.Exit();
        }
    }
}
