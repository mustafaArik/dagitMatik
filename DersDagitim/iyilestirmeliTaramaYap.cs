using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DersDagitim
{
    class iyilestirmeliTaramaYap
    {
        List<bilesenTanimliDers> yerlesenler = new List<bilesenTanimliDers>();
        List<bilesenTanimliDers> yerlesmeyenler = new List<bilesenTanimliDers>();

        List<int[]> enIyiYerlesimListesi = new List<int[]>();

        DersProgrami dersprogrami;
        public object kilitle = new object();

        Thread threadYerlestir;
        Random rnd;
        int toplamDersSaati = 0;

        public iyilestirmeliTaramaYap(DersProgrami dersprogrami, bool iyilestir=false)
        {
            rnd = new Random();
            this.dersprogrami = dersprogrami;
            dersprogrami.dagitimaHazirla();

            foreach (bilesenTanimliDers tDers in dersprogrami.tanimliDersler)
                toplamDersSaati += tDers.toplamSaat;

            if (iyilestir)
            {
                foreach (bilesenTanimliDers tanimliDers in dersprogrami.tanimliDersler)
                    if (tanimliDers.aktifYerlesim != null)
                    {
                        if (tanimliDers.olasilikSina(tanimliDers.aktifYerlesim))
                        {
                            tanimliDers.yerles(tanimliDers.aktifYerlesim);
                            yerlesenler.Add(tanimliDers);
                        }
                        else
                            yerlesmeyenler.Add(tanimliDers);
                    }
                    else
                        yerlesmeyenler.Add(tanimliDers);
            }
            else
            {
                List<bilesenTanimliDers> tumDerslerListesi = dersprogrami.tanimliDersler.ToList();

                for (int i = 0; i < tumDerslerListesi.Count; i++)
                    for (int j = i; j < tumDerslerListesi.Count; j++)
                        if (tumDerslerListesi[j].toplamSaat > tumDerslerListesi[i].toplamSaat)
                        {
                            bilesenTanimliDers yedek = tumDerslerListesi[i];
                            tumDerslerListesi[i] = tumDerslerListesi[j];
                            tumDerslerListesi[j] = yedek;
                        }
                yerlesmeyenler = tumDerslerListesi.ToList();

                //RASTGELE BAŞLANGIÇ YERLEŞİMİ
                List<bilesenTanimliDers> dersler = yerlesmeyenler.ToList();


                int s = dersler.Count;
                while (dersler.Count > 39 * s / 40)
                {
                    int r = rnd.Next(dersler.Count / 10);
                    if (dersler[r].rastgeleYerles())
                    {
                        yerlesenler.Add(dersler[r]);
                        yerlesmeyenler.Remove(dersler[r]);
                    }

                    dersler.RemoveAt(r);
                }

            }


            threadYerlestir = new Thread(baslat);
            threadYerlestir.Start();
        }





        #region DIŞARI GÖNDERİLENLER

        public int yerlesenYuzde
        {
            get
            {
                return ((yerlesenler.Count) * 100) / (yerlesenler.Count + yerlesmeyenler.Count);
            }
        }
        
        public int yerlesmeyenSayisi
        {
            get
            {
                return yerlesmeyenler.Count;
            }
        }

        Stopwatch kronometre = new Stopwatch();

        public string gecenSure
        {
            get
            {
                return string.Format("{0:00}:{1:00}:{2:00}:{3:00}", kronometre.Elapsed.Days, kronometre.Elapsed.Hours, kronometre.Elapsed.Minutes, kronometre.Elapsed.Seconds);
            }
        }

        public string[] yerlesmeyenlerListe
        {
            get
            {
                string[] liste = new string[yerlesmeyenler.Count];
                List<bilesenTanimliDers> dersler = yerlesmeyenler.ToList();
                for(int i=0;i<dersler.Count;i++)
                    for(int j=i;j<dersler.Count;j++)
                        if (dersler[j].denemeXsaat > dersler[i].denemeXsaat)
                        {
                            bilesenTanimliDers yedek = dersler[i];
                            dersler[i] = dersler[j];
                            dersler[j] = yedek;
                        }


                for (int i = 0; i < liste.Length; i++)
                    liste[i] = dersler[i].aciklama;
                return liste;
            }
        }

        public string[] enZorOnOgretmen
        {
            get
            {
                List<bilesenOgretmen> ogretmenler=new List<bilesenOgretmen>();

                foreach (bilesenOgretmen ogr in dersprogrami.ogretmenler)
                    ogretmenler.Add(ogr);

                string[] liste=new string[10];
                for (int i = 0; i < ogretmenler.Count; i++)
                    for (int j = i+1; j < ogretmenler.Count;j++ )
                        if (ogretmenler[j].yerlesmemeSayisi > ogretmenler[i].yerlesmemeSayisi)
                        {
                            bilesenOgretmen yedek = ogretmenler[i];
                            ogretmenler[i] = ogretmenler[j];
                            ogretmenler[j] = yedek;
                        }

                for (int i = 0; i < ogretmenler.Count; i++)
                {
                    if (i < 10)
                        liste[i] = ogretmenler[i].adi;
                    else
                        break;

                }
                return liste;
            }

        }

        #endregion


        public bool bitti=false;

        public ulong sayac = 1;



        public void baslat()
        {
            int yerlesekNo=0;
            double yerlesimOrani=0, enYuksekOran=0.9d;
            //bool ikinciTur;
            byte turSay = 0;
            kronometre.Start();
            int yerlesenToplamSaat = 0;

            bilesenTanimliDers yerlesecekDers=null;
            List<bilesenTanimliDers> enSonYuz = new List<bilesenTanimliDers>();

            while (yerlesmeyenler.Count > 0)
            {
                lock (kilitle)
                {
                    yerlesenToplamSaat = 0;
                    foreach (bilesenTanimliDers tDers in yerlesenler)
                        yerlesenToplamSaat += tDers.toplamSaat;

                    yerlesimOrani = ((double)yerlesenToplamSaat / (double)toplamDersSaati);

                    sayac++;

                    if (yerlesecekDers == null)
                    {
                        turSay = 0;
                        //ikinciTur = false;
                        if (yerlesimOrani > 0.80d)
                        {
                            int enAzOlasilik = yerlesmeyenler[0].aktifOlasilikSayisi();
                            int enAz = 0;
                            for (int i = 0; i < yerlesmeyenler.Count; i++)
                            {
                                int olSay = yerlesmeyenler[i].aktifOlasilikSayisi();
                                if (olSay < enAzOlasilik)
                                {
                                    enAzOlasilik = olSay;
                                    enAz = i;
                                }
                            }
                            yerlesecekDers = yerlesmeyenler[enAz];
                        }
                        else
                        {
                            int enBuyuk = -1;
                            for (int i = 0; i < yerlesmeyenler.Count; i++)
                            {
                                if (enBuyuk < yerlesmeyenler[i].denemeXsaat)
                                {
                                    enBuyuk = yerlesmeyenler[i].denemeXsaat;
                                    yerlesekNo = i;
                                }
                            }
                            yerlesecekDers = yerlesmeyenler[yerlesekNo];
                        }
                    }
                    //else
                        //ikinciTur = true;

                    bool yerlesti = false;

                    yerlesecekDers.denemeSayac++;

                    enSonYuz.Add(yerlesecekDers);
                    if (enSonYuz.Count > 100)
                    {
                        enSonYuz.RemoveAt(0);
                    }

                    int enSonSayac=0;

                    foreach (bilesenTanimliDers tders in enSonYuz)
                    {
                        if (tders == yerlesecekDers)
                            enSonSayac++;
                    }
                    if (enSonSayac > 50)
                    {
                        foreach(bilesenTanimliDers tders in yerlesecekDers.iliskiListesi)
                            if (tders.aktifYerlesim != null)
                            {
                                tders.kaldir();
                                yerlesenler.Remove(tders);
                                yerlesmeyenler.Add(tders);
                            }
                    }

                    if (yerlesimOrani > 0.25d)
                    {
                        if (yerlesecekDers.enIyiyeYerles())
                        {
                            bilesenTanimliDers.yerlesimOlasilik ol = yerlesecekDers.aktifYerlesim;
                            yerlesecekDers.olasiliklar.Remove(ol);
                            yerlesecekDers.olasiliklar.Insert(0, ol);
                            yerlesenler.Add(yerlesecekDers);
                            yerlesmeyenler.Remove(yerlesecekDers);
                            yerlesti = true;
                            yerlesecekDers = null;
                        }
                    }
                    else
                    {
                        if (yerlesecekDers.rastgeleYerles())
                        {
                            yerlesenler.Add(yerlesecekDers);
                            yerlesmeyenler.Remove(yerlesecekDers);
                            yerlesti = true;
                            yerlesecekDers = null;
                        }
                    }

                    /*
                    #region KOD İÇİ KONTROL

                    foreach (bilesenTanimliDers ders in yerlesenler)
                        if (ders.aktifYerlesim == null)
                            MessageBox.Show("hata");
                    foreach (bilesenTanimliDers ders in yerlesmeyenler)
                        if (ders.aktifYerlesim != null)
                            MessageBox.Show("hata");

                    #endregion

                    */

                    List<bilesenTanimliDers> kaldirilmayaMusayit = new List<bilesenTanimliDers>(); ;

                    List<bilesenTanimliDers> kaldirilacaklar = new List<bilesenTanimliDers>();
                    if (!yerlesti && yerlesecekDers != null)
                    {
                        kaldirilacaklar = yerlesecekDers.cikarilacakVerimliDers().ToList();
                    }


                    #region ESKİ 1 KALDIR 2 YERLEŞTİR
                    
                    if (!yerlesti)
                    {
                        yerlesecekDers.denemeSayac++;

                        foreach (bilesenTanimliDers kaldirilabilir in kaldirilacaklar)
                        {
                            //int eskiYerlesim = kaldirilabilir.aktifYerlesim.id;
                            kaldirilabilir.kaldir();
                            for (int i = 0; i < yerlesecekDers.olasiliklar.Count; i++)
                                if (yerlesecekDers.olasilikSina(i))
                                {
                                    kaldirilmayaMusayit.Add(kaldirilabilir);
                                    yerlesecekDers.yerles(i);
                                    for (int s = 0; s < kaldirilabilir.olasiliklar.Count; s++)
                                    {
                                        if (kaldirilabilir.olasilikSina(s))
                                        {

                                            kaldirilabilir.yerles(s);
                                            foreach (bilesenOgretmen ogretmen in kaldirilabilir.ogretmenler)
                                                ogretmen.yerlesmemeSayisi -= kaldirilabilir.toplamSaat;
                                            yerlesenler.Add(yerlesecekDers);
                                            yerlesmeyenler.Remove(yerlesecekDers);
                                            yerlesti = true;
                                            yerlesecekDers = null;
                                            break;
                                        }
                                    }
                                    if (!yerlesti)
                                        yerlesecekDers.kaldir();
                                    else
                                        break;
                                }

                            if (!yerlesti)
                                kaldirilabilir.eskiyeYerles();
                            else
                                break;
                        }

                    }
                     
                    #endregion

                    double oran = (double)sayac / (double)dersprogrami.tanimliDersler.Count;

                    #region ESKİ 2 KALDIR 3 YERLEŞTİR

                    if (!yerlesti && (((yerlesimOrani > 0.95d - oran / 10000d) && oran > 10d) || yerlesimOrani > 0.93d || turSay > 0))
                    {
                        yerlesecekDers.denemeSayac++;

                        for (int i = 0; i < kaldirilacaklar.Count; i++)
                        {
                            for (int j = i + 1; j < kaldirilacaklar.Count; j++)
                            {
                                bilesenTanimliDers b1 = kaldirilacaklar[i];
                                bilesenTanimliDers b2 = kaldirilacaklar[j];

                                //b1.eskiYerlesim = b1.aktifYerlesim.id;
                                //b2.eskiYerlesim = b2.aktifYerlesim.id;
                                bilesenTanimliDers.yerlesimOlasilik eskiOl_1 = b1.aktifYerlesim;
                                bilesenTanimliDers.yerlesimOlasilik eskiOl_2 = b2.aktifYerlesim;

                                b1.kaldir(); b2.kaldir();

                                for (int r = 0; r < yerlesecekDers.olasiliklar.Count; r++)
                                {
                                    if (yerlesecekDers.olasilikSina(r))
                                    {

                                        yerlesecekDers.yerles(r);
                                        for (int c = 0; c < b1.olasiliklar.Count; c++)
                                        {
                                            if (b1.olasilikSina(c))
                                            {
                                                b1.yerles(c);
                                                for (int s = 0; s < b2.olasiliklar.Count; s++)
                                                {
                                                    if (b2.olasilikSina(s))
                                                    {
                                                        b2.yerles(s);

                                                        yerlesenler.Add(yerlesecekDers);
                                                        yerlesmeyenler.Remove(yerlesecekDers);

                                                        yerlesti = true;
                                                        yerlesecekDers = null;
                                                        break;
                                                    }
                                                }
                                                if (!yerlesti)
                                                    b1.kaldir();
                                                else
                                                    break;
                                            }
                                        }
                                        if (!yerlesti)
                                            yerlesecekDers.kaldir();
                                        else
                                            break;
                                    }
                                }

                                if (!yerlesti)
                                {
                                    b1.yerles(eskiOl_1);
                                    b2.yerles(eskiOl_2);
                                }
                                else
                                    break;

                            }
                            if (yerlesti)
                                break;
                        }
                    }
                    #endregion

                    #region ESKİ 3 KALDIR 4 YERLEŞTİR

                    if (!yerlesti && ((yerlesimOrani > 1d - oran / 10000d) || yerlesimOrani > 0.98d || turSay > 2))
                    {
                        yerlesecekDers.denemeSayac++;
                        for (int i = 0; i < kaldirilacaklar.Count; i++)
                        {
                            for (int j = i + 1; j < kaldirilacaklar.Count; j++)
                            {
                                for (int t = j + 1; t < kaldirilacaklar.Count; t++)
                                {
                                    bilesenTanimliDers b1 = kaldirilacaklar[i];
                                    bilesenTanimliDers b2 = kaldirilacaklar[j];
                                    bilesenTanimliDers b3 = kaldirilacaklar[t];
                                    //b1.eskiYerlesim = b1.aktifYerlesim.id;
                                    //b2.eskiYerlesim = b2.aktifYerlesim.id;
                                    bilesenTanimliDers.yerlesimOlasilik eskiOl_1 = b1.aktifYerlesim;
                                    bilesenTanimliDers.yerlesimOlasilik eskiOl_2 = b2.aktifYerlesim;
                                    bilesenTanimliDers.yerlesimOlasilik eskiol_3 = b3.aktifYerlesim;

                                    b1.kaldir(); b2.kaldir(); b3.kaldir();

                                    for (int r = 0; r < yerlesecekDers.olasiliklar.Count; r++)
                                    {
                                        if (yerlesecekDers.olasilikSina(r))
                                        {
                                            //kaldirilmayaMusayit.Add(b1);
                                            //kaldirilmayaMusayit.Add(b2);
                                            //kaldirilmayaMusayit.Add(b3);
                                            yerlesecekDers.yerles(r);
                                            for (int c = 0; c < b1.olasiliklar.Count; c++)
                                            {
                                                if (b1.olasilikSina(c))
                                                {
                                                    b1.yerles(c);
                                                    for (int s = 0; s < b2.olasiliklar.Count; s++)
                                                    {
                                                        if (b2.olasilikSina(s))
                                                        {
                                                            b2.yerles(s);

                                                            for (int tt = 0; tt < b3.olasiliklar.Count; tt++)
                                                            {
                                                                if (b3.olasilikSina(tt))
                                                                {
                                                                    b3.yerles(tt);

                                                                    yerlesenler.Add(yerlesecekDers);
                                                                    yerlesmeyenler.Remove(yerlesecekDers);

                                                                    yerlesti = true;
                                                                    yerlesecekDers = null;
                                                                    break;
                                                                }
                                                            }
                                                            if (!yerlesti)
                                                                b2.kaldir();
                                                            else
                                                                break;
                                                        }
                                                    }
                                                    if (!yerlesti)
                                                        b1.kaldir();
                                                    else
                                                        break;
                                                }
                                            }
                                            if (!yerlesti)
                                                yerlesecekDers.kaldir();
                                            else
                                                break;
                                        }
                                    }


                                    if (!yerlesti)
                                    {
                                        b1.yerles(eskiOl_1);
                                        b2.yerles(eskiOl_2);
                                        b3.yerles(eskiol_3);
                                    }
                                    else
                                        break;
                                }
                                if (yerlesti)
                                    break;
                            }
                            if (yerlesti)
                                break;
                        }
                    }
                    #endregion

                    #region YENİ 1 KALDIR 2 YERLEŞTİR
                    /*    
                if (!yerlesti)
                {
                    yerlesecekDers.denemeSayac++;

                    List<bilesenTanimliDers> kDersler = new List<bilesenTanimliDers>();

                    int olasilikSayisi = 0;
                    int enUygunSira = -1;
                    //Yerleşecek derse komşu dersleri süz
                    //kDersler = yerlesecekDers.cikarilacakVerimliDers().ToList();

                    foreach (bilesenTanimliDers ders in yerlesecekDers.iliskiListesi)
                        if (ders.aktifYerlesim != null)
                            kDersler.Add(ders);

                    for (int i = 0; i < kDersler.Count; i++)
                    {
                        kDersler[i].kaldir();

                        bilesenTanimliDers.yerlesimOlasilik[] olYerDers = yerlesecekDers.yerlesilebilirOlasiliklar();
                        bilesenTanimliDers.yerlesimOlasilik[] olD_i = kDersler[i].yerlesilebilirOlasiliklar();

                        if (olD_i.Length > olasilikSayisi && olD_i.Length>olYerDers.Length+2 && olYerDers.Length>0)
                        {
                            olasilikSayisi = olD_i.Length;
                            enUygunSira = i;
                        }
                        #region OLASILIKLARI DENE
                        for (int olYer = 0; olYer < olYerDers.Length; olYer++)
                        {
                            bool[,] yertablo = olYerDers[olYer].tablo;
                            for (int ol_i = 0; ol_i < olD_i.Length; ol_i++)
                            {
                                bool[,] tablo_i = olD_i[ol_i].tablo;

                                if (araclar.diziKesisiyormu(yertablo, tablo_i))
                                    continue;

                                    if (yerlesecekDers.olasilikSina(olYerDers[olYer]))
                                        yerlesecekDers.yerles(olYerDers[olYer]);
                                    else
                                        MessageBox.Show("hata");

                                    if (kDersler[i].olasilikSina(olD_i[ol_i]))
                                        kDersler[i].yerles(olD_i[ol_i]);
                                    else
                                        MessageBox.Show("hata");


                                    yerlesenler.Add(yerlesecekDers);
                                    yerlesmeyenler.Remove(yerlesecekDers);
                                    yerlesti = true;
                                    yerlesecekDers = null;
                                    break;
                                // }
                            }
                            if (yerlesti) break;
                        }

                        #endregion

                        if (!yerlesti)
                            kDersler[i].eskiyeYerles();
                        else
                            break;
                    }

                    if (!yerlesti && enUygunSira>-1)
                    {
                        kaldirilmayaMusayit=kDersler[enUygunSira];
                    }

                }
        */
                    #endregion

                    #region YENİ 2 KALDIR 3 YERLEŞTİR
                    /*
                if (!yerlesti && yerlesimOrani > 0.70d && oran>4d)
                {
                    yerlesecekDers.denemeSayac++;

                    List<bilesenTanimliDers> kDersler = new List<bilesenTanimliDers>();

                    //Yerleşecek derse komşu dersleri süz
                    //kDersler = yerlesecekDers.cikarilacakVerimliDers().ToList();

                    foreach (bilesenTanimliDers ders in yerlesecekDers.iliskiListesi)
                        if (ders.aktifYerlesim != null)
                            kDersler.Add(ders);

                    for (int i = 0; i < kDersler.Count; i++)
                    {
                        kDersler[i].kaldir();
                        for (int ii = i + 1; ii < kDersler.Count; ii++)
                        {
                            kDersler[ii].kaldir();


                            bilesenTanimliDers.yerlesimOlasilik[] olYerDers = yerlesecekDers.yerlesilebilirOlasiliklar();
                            bilesenTanimliDers.yerlesimOlasilik[] olD_i = kDersler[i].yerlesilebilirOlasiliklar();
                            bilesenTanimliDers.yerlesimOlasilik[] olD_ii = kDersler[ii].yerlesilebilirOlasiliklar();

                            #region OLASILIKLARI DENE
                            for (int olYer = 0; olYer < olYerDers.Length; olYer++)
                            {
                                bool[,] yertablo = olYerDers[olYer].tablo;
                                for (int ol_i = 0; ol_i < olD_i.Length; ol_i++)
                                {
                                    bool[,] tablo_i = olD_i[ol_i].tablo;
                                    if (araclar.diziKesisiyormu(yertablo, tablo_i))
                                        continue;
                                    bool[,] birlesim = araclar.diziBirlestir(yertablo, tablo_i);

                                    for (int ol_ii = 0; ol_ii < olD_ii.Length; ol_ii++)
                                    {
                                        bool[,] tablo_ii = olD_ii[ol_ii].tablo;
                                        if (araclar.diziKesisiyormu(birlesim, tablo_ii))
                                            continue;


                                            if (yerlesecekDers.olasilikSina(olYerDers[olYer]))
                                                yerlesecekDers.yerles(olYerDers[olYer]);
                                            else
                                                MessageBox.Show("hata");

                                            if (kDersler[i].olasilikSina(olD_i[ol_i]))
                                                kDersler[i].yerles(olD_i[ol_i]);
                                            else
                                                MessageBox.Show("hata");


                                            if (kDersler[ii].olasilikSina(olD_ii[ol_ii]))
                                                kDersler[ii].yerles(olD_ii[ol_ii]);
                                            else
                                                MessageBox.Show("hata");

                                            yerlesenler.Add(yerlesecekDers);
                                            yerlesmeyenler.Remove(yerlesecekDers);
                                            yerlesti = true;
                                            yerlesecekDers = null;
                                            break;

                                        // }

                                    }
                                    if (yerlesti) break;
                                }
                                if (yerlesti) break;
                            }
                            #endregion


                            if (!yerlesti)
                                kDersler[ii].eskiyeYerles();
                            else
                                break;
                        }
                        if (!yerlesti)
                            kDersler[i].eskiyeYerles();
                        else
                            break;
                    }
                }
                */
                    #endregion

                    #region 3 KALDIR 4 YERLEŞTİR
                    /*
                if (!yerlesti && yerlesimOrani>0.80d && oran>7d)
                {
                    yerlesecekDers.denemeSayac++;

                    List<bilesenTanimliDers> kDersler = new List<bilesenTanimliDers>();

                    //Yerleşecek derse komşu dersleri süz
                    //kDersler = yerlesecekDers.cikarilacakVerimliDers().ToList();

                    foreach (bilesenTanimliDers ders in yerlesecekDers.iliskiListesi)
                        if (ders.aktifYerlesim != null)
                            kDersler.Add(ders);


                    for (int i = 0; i < kDersler.Count; i++)
                    {
                        kDersler[i].kaldir();
                        for (int ii = i + 1; ii < kDersler.Count; ii++)
                        {
                            kDersler[ii].kaldir();
                            for (int iii = ii + 1; iii < kDersler.Count; iii++)
                            {
                                kDersler[iii].kaldir();

                                bilesenTanimliDers.yerlesimOlasilik[] olYerDers = yerlesecekDers.yerlesilebilirOlasiliklar();
                                bilesenTanimliDers.yerlesimOlasilik[] olD_i = kDersler[i].yerlesilebilirOlasiliklar();
                                bilesenTanimliDers.yerlesimOlasilik[] olD_ii = kDersler[ii].yerlesilebilirOlasiliklar();
                                bilesenTanimliDers.yerlesimOlasilik[] olD_iii = kDersler[iii].yerlesilebilirOlasiliklar();

                                #region OLASILIKLARI DENE
                                for (int olYer = 0; olYer < olYerDers.Length; olYer++)
                                {
                                    bool[,] yertablo = olYerDers[olYer].tablo;

                                    for (int ol_i = 0; ol_i < olD_i.Length; ol_i++)
                                    {
                                        bool[,] tablo_i = olD_i[ol_i].tablo;
                                        if (araclar.diziKesisiyormu(yertablo, tablo_i))
                                            continue;

                                        bool[,] birlesim = araclar.diziBirlestir(yertablo, tablo_i);
                                        for (int ol_ii = 0; ol_ii < olD_ii.Length; ol_ii++)
                                        {
                                            bool[,] tablo_ii = olD_ii[ol_ii].tablo;
                                            if (araclar.diziKesisiyormu(birlesim, tablo_ii))
                                                continue;
                                            birlesim = araclar.diziBirlestir(birlesim, tablo_ii);

                                            for (int ol_iii = 0; ol_iii < olD_iii.Length; ol_iii++)
                                            {
                                                bool[,] tablo_iii = olD_iii[ol_iii].tablo;
                                                if (araclar.diziKesisiyormu(birlesim, tablo_iii))
                                                    continue;

                                                    if (yerlesecekDers.olasilikSina(olYerDers[olYer]))
                                                        yerlesecekDers.yerles(olYerDers[olYer]);
                                                    else
                                                        MessageBox.Show("hata");
                                                        
                                                    if (kDersler[i].olasilikSina(olD_i[ol_i]))
                                                        kDersler[i].yerles(olD_i[ol_i]);
                                                    else
                                                        MessageBox.Show("hata");

                                                        
                                                    if (kDersler[ii].olasilikSina(olD_ii[ol_ii]))
                                                        kDersler[ii].yerles(olD_ii[ol_ii]);
                                                    else
                                                        MessageBox.Show("hata");


                                                    if (kDersler[iii].olasilikSina(olD_iii[ol_iii]))
                                                        kDersler[iii].yerles(olD_iii[ol_iii]);
                                                    else
                                                        MessageBox.Show("hata");

                                                    yerlesenler.Add(yerlesecekDers);
                                                    yerlesmeyenler.Remove(yerlesecekDers);
                                                    yerlesti = true;
                                                    yerlesecekDers = null;
                                                    break;

                                                //}
                                                if (yerlesti) break;
                                            }
                                            if (yerlesti) break;
                                        }
                                        if (yerlesti) break;
                                    }
                                    if (yerlesti) break;
                                }
                                #endregion

                                if (!yerlesti)
                                    kDersler[iii].eskiyeYerles();
                                else
                                    break;
                            }
                            if (!yerlesti)
                                kDersler[ii].eskiyeYerles();
                            else
                                break;
                        }
                        if (!yerlesti)
                            kDersler[i].eskiyeYerles();
                        else
                            break;
                    }
                }
                */
                    #endregion

                    if (!yerlesti)
                    {
                        yerlesecekDers.denemeSayac++;

                        if (kaldirilmayaMusayit.Count > 0)
                        {
                            int enAzUgrastiran = 0;
                            int enAzUgrasim = kaldirilmayaMusayit[0].denemeXsaat;

                            for (int i = 0; i <kaldirilmayaMusayit.Count; i++)
                            {
                                if (enAzUgrasim > kaldirilmayaMusayit[i].denemeXsaat)
                                {
                                    enAzUgrastiran = i;
                                    enAzUgrasim = kaldirilmayaMusayit[i].denemeXsaat;
                                }
                            }

                            kaldirilmayaMusayit[enAzUgrastiran].kaldir();
                            yerlesenler.Remove(kaldirilmayaMusayit[enAzUgrastiran]);
                            yerlesmeyenler.Add(kaldirilmayaMusayit[enAzUgrastiran]);
                        }
                        

                        if (kaldirilacaklar.Count > 0 && (kaldirilmayaMusayit.Count<0 || turSay>0 || enSonSayac>3))
                        {
                            int enAzUgrastiran = 0;
                            int enAzUgrasim = kaldirilacaklar[0].denemeXsaat;

                            for (int i = 0; i < kaldirilacaklar.Count; i++)
                            {
                                if (enAzUgrasim > kaldirilacaklar[i].denemeXsaat)
                                {
                                    enAzUgrasim = kaldirilacaklar[i].denemeXsaat;
                                    enAzUgrastiran = i;
                                }
                            }

                            bilesenTanimliDers kaldirilacak = kaldirilacaklar[enAzUgrastiran];
                            if (yerlesenler.Contains(kaldirilacak))
                            {
                                kaldirilacak.kaldir();
                                yerlesenler.Remove(kaldirilacak);
                                yerlesmeyenler.Add(kaldirilacak);
                            }
                        }
                        if(kaldirilacaklar.Count==0)
                        {
                            foreach (bilesenTanimliDers ders in yerlesecekDers.iliskiListesi)
                                if (yerlesenler.Contains(ders))
                                {
                                    ders.kaldir();
                                    yerlesenler.Remove(ders);
                                    yerlesmeyenler.Add(ders);
                                }
                        }

                        //Yerleşmeyen dersin öğretmenlerini derecelendir
                        if (!yerlesti)
                        {
                            foreach(bilesenTanimliDers kders in kaldirilacaklar)
                            foreach (bilesenOgretmen ogretmen in kders.ogretmenler)
                                ogretmen.yerlesmemeSayisi++;//=kders.toplamSaat; // += (uint)yerlesecekDers.denemeXsaat;
                        }

                    }

                }
                turSay++;
            }



            //DAĞITIM GERÇEKLEŞTİ

            #region DERS DAĞITIMINI KONTROL ET
            foreach (bilesenTanimliDers t in dersprogrami.tanimliDersler)
            {
                if (t.aktifYerlesim == null)
                    MessageBox.Show("Hatalı yerleşim");
            }

            dersprogrami.dagitimaHazirla();

            foreach (bilesenTanimliDers t in dersprogrami.tanimliDersler)
            {
                if(t.olasilikSina(t.aktifYerlesim.id,true))
                    t.yerles(t.aktifYerlesim.id,true);
                else
                    MessageBox.Show("Hata var");
            }
            #endregion

            bitti = true;
            kronometre.Stop();
        }

        public void durdur()
        {
            threadYerlestir.Abort();
        }


    }
}
