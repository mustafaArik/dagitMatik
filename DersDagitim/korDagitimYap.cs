using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DersDagitim
{
    class korDagitimYap
    {
        DersProgrami dprg;
        List<bilesenTanimliDers> yerlesimSirasi=new List<bilesenTanimliDers>();

        Thread threadTara;

        public korDagitimYap(DersProgrami dersprogrami)
        {
            dprg = dersprogrami;
            yerlesimSirasi.Clear();
            dersprogrami.dagitimaHazirla();
            int[] olasilikListe = new int[dersprogrami.tanimliDersler.Count];

            /*
            //Kolay Dersleri Ekle
            for (int i = 0; i < dprg.ogretmenler.Count; i++)
            {
                bilesenOgretmen ogretmen = dprg.ogretmenler[i] as bilesenOgretmen;

                for (int j = 0; j < dprg.tanimliDersler.Count; j++)
                {
                    bilesenTanimliDers tanimliDers = dprg.tanimliDersler[j] as bilesenTanimliDers;
                    if (tanimliDers.ogretmenler.Count == 1 && tanimliDers.derslikler.Count == 1 && tanimliDers.sinifGruplar.Count == 1 && ogretmen.id==(tanimliDers.ogretmenler[0] as bilesenOgretmen).id)
                    {
                        bool eklenmis = false;
                        foreach (bilesenTanimliDers eklenen in yerlesimSirasi)
                            if (eklenen.id == tanimliDers.id)
                                eklenmis = true;
                        if (!eklenmis)
                            yerlesimSirasi.Add(tanimliDers);
                    }
                }
            }

            */




            // TANIMLI DERSLERİ OLASILIKLARINA GÖRE EKLE (KÜÇÜKTEN BÜYÜĞE)
            foreach (bilesenTanimliDers tanimliders in dersprogrami.tanimliDersler)
            {
                yerlesimSirasi.Add(tanimliders);
                tanimliders.yerlesimeHazirla();
            }

            for(int i=0;i<yerlesimSirasi.Count;i++)
                for (int j = i; j < yerlesimSirasi.Count; j++)
                {
                    if (yerlesimSirasi[j].olasiliklar.Count < yerlesimSirasi[i].olasiliklar.Count)
                    {
                        bilesenTanimliDers yedek = yerlesimSirasi[i];
                        yerlesimSirasi[i] = yerlesimSirasi[j];
                        yerlesimSirasi[j] = yedek;
                    }
                }


            //*/

            threadTara = new Thread(baslat);
            threadTara.Start();
        }

        int seviye; 
        bool yerlesmis = false;
        int[] oSiralar;

        ulong genelYuzdeToplam;
        int genelYuzdeSira;

        int _yerlesimYuzde;

        public static object lockDegisken = new object();

        public int yerlesimYuzde
        {
            get
            {
                lock (lockDegisken)
                {
                    if (seviye < yerlesimSirasi.Count)
                        _yerlesimYuzde = ((seviye + 1) * 100) / (yerlesimSirasi.Count);
                    else
                        _yerlesimYuzde = 100;
                }
                return _yerlesimYuzde;
            }
        }

        public int yerlesmeyenDers
        {
            get
            {
                return yerlesimSirasi.Count - seviye + 1;
            }
        }

        public string suAnYerlestirilenDers
        {
            get
            {
                string str = "";
                if (seviye < yerlesimSirasi.Count)
                    str = yerlesimSirasi[seviye].aciklama;
                return str;
            }
        }

        public int genelYuzde
        {
            get
            {
                lock (lockDegisken)
                {
                    int yuzdeToplam = 0;
                    int yuzdeCarpan = 1;
                    for (int j = genelYuzdeSira; j >= 0; j--)
                    {
                        int islenen;
                        if (j%2==0)
                            islenen = yerlesimSirasi[j].olasiliklar.Count - oSiralar[j] + 1;
                        else
                            islenen = oSiralar[j];
                        yuzdeToplam += yuzdeCarpan * islenen;
                        yuzdeCarpan *= yerlesimSirasi[j].olasiliklar.Count;
                    }
                    
                    ulong a= (ulong) yuzdeToplam * 100 / genelYuzdeToplam;

                    return (int)a;
                }

            }
        }

        public void durdur()
        {
            threadTara.Abort();
        }



        void baslat()
        {
            seviye = 0; genelYuzdeToplam = 1;
            oSiralar=new int[yerlesimSirasi.Count];

            for(int i=0;i<yerlesimSirasi.Count;i++)
            {
                yerlesimSirasi[i].aktifOlasilikSayisi();
                if(i%2==0)
                    oSiralar[i]=yerlesimSirasi[i].olasiliklar.Count-1;
                else
                    oSiralar[i]=0;

                if (genelYuzdeToplam * (ulong)yerlesimSirasi[i].olasiliklar.Count < int.MaxValue)
                {
                    genelYuzdeToplam *= (ulong)yerlesimSirasi[i].olasiliklar.Count;
                    genelYuzdeSira = i;
                }
            }

            //DERS YERLEŞİMİ BAŞLANGICI

            while (!yerlesmis && seviye!=-1 && seviye!=yerlesimSirasi.Count)
            {
                taramaYap();
            }
            if (seviye == -1)
                MessageBox.Show("Yerleşim Gerçekleştirilemedi");
            if (seviye == yerlesimSirasi.Count)
            {
                MessageBox.Show("Yerleşim Gerçekleştirildi" + DateTime.Now.ToString());
                //dagitimiStrYaz();
            }
        }

        bool yerlesimHatasi = false;

        void seviyeSiraArtir()
        {
            if (seviye % 2 == 0)
                oSiralar[seviye]--;
            else
                oSiralar[seviye]++;
        }

        void taramaYap()
        {
            if (seviye < yerlesimSirasi.Count)
            {
                if (oSiralar[seviye] >= yerlesimSirasi[seviye].olasiliklar.Count || oSiralar[seviye] < 0)
                {
                    if (seviye%2==0)
                        oSiralar[seviye] = yerlesimSirasi[seviye].olasiliklar.Count-1;
                    else
                        oSiralar[seviye] = 0;
                    seviye--;
                    if (seviye > -1)
                    {
                        yerlesimSirasi[seviye].kaldir();
                        seviyeSiraArtir();
                    }
                }
                else
                {
                    bool yerlesir=true;
                    while (!yerlesimSirasi[seviye].olasiliklar[oSiralar[seviye]].aktif)
                    {
                        seviyeSiraArtir();                        
                        if(oSiralar[seviye] > yerlesimSirasi[seviye].olasiliklar.Count-1 || oSiralar[seviye]<0)
                        {
                            yerlesir = false;
                            break;
                        }
                    }

                    if (yerlesir)
                    {
                        yerlesimSirasi[seviye].yerles(oSiralar[seviye]);

                        if (seviye < yerlesimSirasi.Count)
                            for (int i = seviye+1; i < yerlesimSirasi.Count; i++)
                            {
                                if (yerlesimSirasi[i].aktifOlasilikSayisi() == 0)
                                {
                                    yerlesimHatasi = true;
                                    break;
                                }
                            }

                        if (yerlesimHatasi)
                        {
                            yerlesimSirasi[seviye].kaldir();
                            seviyeSiraArtir();
                            yerlesimHatasi = false;
                        }
                        else
                            seviye++;
                    }
                    else
                        seviyeSiraArtir();
                }

            }

        }

        void dagitimiStrYaz()
        {
            foreach (bilesenTanimliDers tanimliDers in yerlesimSirasi)
            {
                foreach (bilesenOgretmen ogretmen in tanimliDers.ogretmenler)
                {
                    if (ogretmen.prgStr == null)
                        ogretmen.prgStr = new string[dprg.haftalikGunSayisi, dprg.gunlukDersSaatiSayisi];
                    foreach (bilesenNode node in tanimliDers.nodes)
                    {
                        for (int i = 0; i < node.tSaat; i++)
                            ogretmen.prgStr[node.yerlesimGun, node.yerlesimSaat + i] += tanimliDers.aciklama;
                    }
                }

                foreach (bilesenDerslik derslik in tanimliDers.derslikler)
                {
                    if (derslik.prgStr == null)
                        derslik.prgStr = new string[dprg.haftalikGunSayisi, dprg.gunlukDersSaatiSayisi];
                    foreach (bilesenNode node in tanimliDers.nodes)
                    {
                        for (int i = 0; i < node.tSaat; i++)
                            derslik.prgStr[node.yerlesimGun, node.yerlesimSaat + i] += tanimliDers.aciklama;
                    }
                }

                foreach (bilesenSinifGrup sinifGrup in tanimliDers.sinifGruplar)
                {
                    if (sinifGrup.sinif.prgStr == null)
                        sinifGrup.sinif.prgStr = new string[dprg.haftalikGunSayisi, dprg.gunlukDersSaatiSayisi];
                    foreach (bilesenNode node in tanimliDers.nodes)
                    {
                        for (int i = 0; i < node.tSaat; i++)
                        {
                            sinifGrup.sinif.prgStr[node.yerlesimGun, node.yerlesimSaat + i] += tanimliDers.aciklama;
                            sinifGrup.grup.prgStr[node.yerlesimGun, node.yerlesimSaat + i] += tanimliDers.aciklama;
                        }
                    }
                }
            }





        }
    }
}

/* RECOURSİVE İLE DAĞITIM  >> ÇOK FAZLA DALLANMA HATA OLUŞTU
 void taramaYap(int tanimliDersSira=0, int olasilikSira=0)
{
    if (tanimliDersSira < yerlesimSirasi.Count && !yerlesmis)
    {
        if (olasilikSira < yerlesimSirasi[tanimliDersSira].olasiliklar.Count)
        {
            if (yerlesimSirasi[tanimliDersSira].olasilikSina(olasilikSira))
            {
                yerlesimSirasi[tanimliDersSira].yerles(olasilikSira);
                if (tanimliDersSira + 1 == yerlesimSirasi.Count)
                {
                    yerlesmis = true;
                }

                taramaYap(tanimliDersSira + 1, 0);

                yerlesimSirasi[tanimliDersSira].kaldir(olasilikSira);
            }
            taramaYap(tanimliDersSira, olasilikSira + 1);
        }
    }
}
*/