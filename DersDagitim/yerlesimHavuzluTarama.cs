using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DersDagitim
{
    class yerlesimHavuzluTarama
    {
        DersProgrami dersprogrami;
        List<bilesenTanimliDers> dersListesi = new List<bilesenTanimliDers>();
        Thread threadYerlestir;
        Random rnd;

        public static object kilitle = new object();

        const double HAVUZ = 40;
        const double CAPRAZLAMA = 0.20d;
        const double MUTASYON = 0.25d;
        const double YENIBIREY = 0.25d;

        const double ELITE = HAVUZ - HAVUZ * CAPRAZLAMA - HAVUZ * YENIBIREY;

        //const double ONEALMAVERIMLILIGI = 0.5d;

        List<Kromozon> populasyon;

        struct Kromozon
        {
            public void hesapla()
            {
                verimlilik = 0;
                this.tumAilesiYerlesenler = new List<yerlesimYeri>();

                double[] verimlilikOranlari = new double[this.yerlesimListesi.Count];

                for(int i=0;i<this.yerlesimListesi.Count;i++)
                {
                    yerlesimYeri yer = this.yerlesimListesi[i];
                    int sayac = 0;
                    bilesenTanimliDers ders = yer.tanimliDers;
                    foreach (yerlesimYeri yer2 in this.yerlesimListesi)
                    {
                        if(ders.iliskiListesi.Contains(yer2.tanimliDers))
                            sayac++;
                    }
                    if (sayac == ders.iliskiListesi.Count)
                    {
                        this.tumAilesiYerlesenler.Add(yer);
                    }

                    verimlilikOranlari[i] = (double)sayac / (double)ders.iliskiListesi.Count;
                    verimlilik += sayac;
                }


                for(int i=0;i<verimlilikOranlari.Length;i++)
                    for(int j=i;j<verimlilikOranlari.Length;j++)
                        if (verimlilikOranlari[j] > verimlilikOranlari[i])
                        {
                            double yedek = verimlilikOranlari[i];
                            verimlilikOranlari[i] = verimlilikOranlari[j];
                            verimlilikOranlari[j] = yedek;

                            yerlesimYeri yer = this.yerlesimListesi[i];
                            yerlesimListesi[i] = yerlesimListesi[j];
                            yerlesimListesi[j] = yer;
                        }

            }

            public int verimlilik;
            public List<yerlesimYeri> tumAilesiYerlesenler;
            public double yerlesimOrani;
            public List<yerlesimYeri> yerlesimListesi;
        }

        struct yerlesimYeri
        {
            public void yerles()
            {
                tanimliDers.yerles(olasilik);
            }

            public bool sina()
            {
                return tanimliDers.olasilikSina(olasilik);
            }

            public bilesenTanimliDers tanimliDers;
            public bilesenTanimliDers.yerlesimOlasilik olasilik;
        }

        public yerlesimHavuzluTarama(DersProgrami dersprogrami)
        {
            this.dersprogrami = dersprogrami;
            dersprogrami.dagitimaHazirla();

            rnd = new Random();

            foreach (bilesenTanimliDers dr in dersprogrami.tanimliDersler)
            {
                dersListesi.Add(dr);
                //dr.yerlesimListeleriniOlustur();
            }

            threadYerlestir = new Thread(baslat);
            threadYerlestir.Start();
        }

        #region POPULASYON İŞLEMLERİ

        public void dagitimaHazirla()
        {
            dersprogrami.dagitimaHazirla();
            foreach (bilesenTanimliDers ders in dersprogrami.tanimliDersler)
                ders.aktifYerlesim = null;
        }

        private double rastgeleYerles(ref Kromozon kromozon)
        {
            int yerlesen = 0;
            int yerlesmeyen = 0;

            dagitimaHazirla();

            List<bilesenTanimliDers> liste = dersListesi.ToList();

            while(liste.Count>0)
            {
                int s = rnd.Next(liste.Count);

                if (liste[s].rastgeleYerles())
                {
                    yerlesen++;
                    //yerleşeni kromozona ekle
                    yerlesimYeri yerlesim = new yerlesimYeri();
                    yerlesim.tanimliDers = liste[s];
                    yerlesim.olasilik = liste[s].aktifYerlesim;
                    kromozon.yerlesimListesi.Add(yerlesim);
                }
                else
                    yerlesmeyen++;
                liste.RemoveAt(s);
            }
            return ((double)yerlesen * 100) / ((double)yerlesmeyen + (double)yerlesen);
        }
        
        void populasyonOlustur()
        {
            while(populasyon.Count<HAVUZ)
            {
                Kromozon kromozon = new Kromozon();
                kromozon.yerlesimListesi = new List<yerlesimYeri>();

                double yuzde=rastgeleYerles(ref kromozon);
                if (yuzde > 80d)
                {
                    kromozon.yerlesimOrani = yuzde;
                    kromozon.hesapla();
                    populasyon.Add(kromozon);
                }
                lock (kilitle)
                    _yuzde = (populasyon.Count) * 100 / (int)HAVUZ;
            }
        }

        void yeniBirey()
        {
            lock (kilitle)
                _durum = "Yeni Bireyler Ekleniyor";
            for(int i=0;i<HAVUZ*YENIBIREY;i++)
            {
                Kromozon kromozon = new Kromozon();
                kromozon.yerlesimListesi = new List<yerlesimYeri>();

                double yuzde = rastgeleYerles(ref kromozon);
                if (yuzde > 80d)
                {
                    kromozon.yerlesimOrani = yuzde;
                    kromozon.hesapla();
                    populasyon.Add(kromozon);
                }
                lock (kilitle)
                    _yuzde = (i+1) * 100 / (int)(HAVUZ*YENIBIREY);
            }
        }

        void sirala()
        {
            List<Kromozon> sirali = populasyon.OrderByDescending(kr => kr.verimlilik).ToList();
            populasyon = sirali;
        }

        void caprazla()
        {
            lock (kilitle)
                _durum = "Çaprazlanıyor";

            for (int SS = 0; SS < CAPRAZLAMA * HAVUZ; SS++)
            {
                int r, s;
                do
                {
                    r = rnd.Next(populasyon.Count/2);
                    s = rnd.Next(populasyon.Count/2);
                } while (r == s);

                Kromozon birey1 = populasyon[r];
                Kromozon birey2 = populasyon[s];

                Kromozon yeniBirey = new Kromozon();
                yeniBirey.yerlesimListesi = new List<yerlesimYeri>();

                dagitimaHazirla();

                int sira=0;
                while (sira < birey1.yerlesimListesi.Count || sira < birey2.yerlesimListesi.Count)
                {
                    if (sira < birey1.yerlesimListesi.Count)
                    {
                        yerlesimYeri yer = birey1.yerlesimListesi[sira];
                        if (yer.tanimliDers.aktifYerlesim == null)
                            if (yer.sina())
                                yer.yerles();
                    }

                    if (sira < birey2.yerlesimListesi.Count)
                    {
                        yerlesimYeri yer = birey2.yerlesimListesi[sira];
                        if (yer.tanimliDers.aktifYerlesim == null)
                            if (yer.sina())
                                yer.yerles();
                    }
                    sira++;
                }



                for (int i = 0; i < dersprogrami.tanimliDersler.Count; i++)
                {
                    if (dersprogrami.tanimliDersler[i].aktifYerlesim == null)
                    {
                        if (dersprogrami.tanimliDersler[i].rastgeleYerles())
                        {
                            yerlesimYeri yer = new yerlesimYeri();
                            yer.tanimliDers = dersprogrami.tanimliDersler[i];
                            yer.olasilik = dersprogrami.tanimliDersler[i].aktifYerlesim;
                            yeniBirey.yerlesimListesi.Add(yer);
                        }
                    }
                    else
                    {
                        yerlesimYeri yer = new yerlesimYeri();
                        yer.tanimliDers = dersprogrami.tanimliDersler[i];
                        yer.olasilik = dersprogrami.tanimliDersler[i].aktifYerlesim;
                        yeniBirey.yerlesimListesi.Add(yer);
                    }
                        
                }

                yeniBirey.yerlesimOrani = ((double)yeniBirey.yerlesimListesi.Count * 100) / (double)dersprogrami.tanimliDersler.Count;

                #region HATA KONTROL SONRA SİLİNEBİLİR
                dersprogrami.dagitimaHazirla();

                foreach (bilesenTanimliDers t in dersprogrami.tanimliDersler)
                {
                    if (t.aktifYerlesim != null)
                        if (t.olasilikSina(t.aktifYerlesim))
                            t.yerles(t.aktifYerlesim);
                        else
                            MessageBox.Show("Hata var");
                }
                #endregion

                yeniBirey.hesapla();
                populasyon.Add(yeniBirey);

                lock (kilitle)
                    _yuzde = (SS + 1) * 100 / (int)(CAPRAZLAMA * HAVUZ);
            }
        }

        #endregion

        public void baslat()
        {
            lock (kilitle)
                _durum = "Populasyon Oluşturuluyor";
            populasyon = new List<Kromozon>();

            populasyonOlustur();

            do
            {
                yeniBirey();
                caprazla();
                sirala();
                lock (kilitle)
                    _enIyiYerlesimYuzde = populasyon[0].yerlesimOrani;
                if (populasyon[0].yerlesimOrani == 100)
                    break;
                populasyon.RemoveRange((int)ELITE, populasyon.Count-(int)ELITE);

            } while (true);

        }

        public void durdur()
        {
            threadYerlestir.Abort();
        }

        #region DIŞARIYA AKTARILANLAR

        private string _durum;
        private int _yuzde;
        private double _enIyiYerlesimYuzde;

        public string durum
        {
            get
            {
                lock (kilitle)
                {
                    return _durum;
                }
            }
        }

        public int yuzde
        {
            get
            {
                lock(kilitle)
                {
                    if (_yuzde < 100)
                        return _yuzde;
                    else
                        return 100;
                }
            }
        }

        public double enIyiYerlesimYuzde
        {
            get
            {
                lock(kilitle)
                {
                    return _enIyiYerlesimYuzde;
                }
            }
        }

        #endregion

    }
}
