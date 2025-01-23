using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace DersDagitim
{
    public static class tanim
    {
        public static DersProgrami program;

        public static string[] gunler = { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi", "Pazar" };
        public const ushort TBDERSLER = 0;
        public const ushort TBOGRETMENLER = 1;
        public const ushort TBDERSLIKLER = 2;
        public const ushort TBSINIFLAR = 3;
    }

    #region BİLEŞENLER

    public class bilesenTaban
    {
        public ushort id;
        public bool[,] kosul;
        public string adi;
        public string kisaAdi;
        public bool[,] yKosul;

        public string[,] prgStr;
    }

    public class bilesenOgretmen : bilesenTaban
    {
        public uint yerlesmemeSayisi;
        public bilesenOgretmen(ushort _id, bool[,] _kosul, string _adisoyadi, string _kisaAdi)
        {
            this.id = _id;
            this.kosul = _kosul;
            this.adi = _adisoyadi;
            this.kisaAdi = _kisaAdi;
        }

    }

    public class bilesenDers : bilesenTaban
    {
        public bilesenDers(ushort _id, bool[,] _kosul, string _adi, string _kisaAdi)
        {
            this.id = _id;
            this.kosul = _kosul;
            this.adi = _adi;
            this.kisaAdi = _kisaAdi;
        }

    }

    public class bilesenDerslik : bilesenTaban
    {
        public bilesenDerslik(ushort _id, bool[,] _kosul, string _adi, string _kisaAdi)
        {
            this.id = _id;
            this.kosul = _kosul;
            this.adi = _adi;
            this.kisaAdi = _kisaAdi;
        }
    }

    public class bilesenSinif : bilesenTaban
    {
        public ArrayList gruplar;
        public ushort grupIdSon;

        public bilesenSinif(ushort _id, bool[,] _kosul, string _adi, string _kisaAdi, ArrayList _gruplar, ushort _grupIdSon = 1)
        {
            this.id = _id;
            this.kosul = _kosul;
            this.adi = _adi;
            this.kisaAdi = _kisaAdi;
            this.gruplar = _gruplar;
            this.grupIdSon = _grupIdSon;
            if (this.grupGetir(0) == null)
                this.gruplar.Add(new bilesenGrup(0, "Tüm Sınıf", "Tümü"));
        }

        public bilesenGrup grupGetir(ushort grupId)
        {

            bilesenGrup grup = null;
            for (int i = 0; i < gruplar.Count; i++)
            {
                bilesenGrup g = gruplar[i] as bilesenGrup;
                if (g.id == grupId)
                    grup = g;
            }
            return grup;

        }
    }

    public class bilesenGrup : bilesenTaban
    {
        public bilesenGrup(ushort _id, string _adi, string _kisaAdi)
        {
            this.id = _id;
            this.adi = _adi;
            this.kisaAdi = _kisaAdi;
        }
    }

    public class bilesenSinifGrup
    {
        public bilesenSinif sinif;
        public bilesenGrup grup;
        public bilesenSinifGrup(bilesenSinif _sinif, ushort _grupId)
        {
            sinif = _sinif;
            grup = sinif.grupGetir(_grupId);
        }
    }

    public class bilesenNode
    {
        public ushort id;
        public bilesenTanimliDers tanimliDers;
        public ushort tSaat;
        public ushort yerlesimGun;
        public ushort yerlesimSaat;

        #region YERLEŞİM İŞLEMLERİ

        public struct yerlesimYeri
        {
            public ushort gun;
            public ushort saat;
        }

        public yerlesimYeri[] yerlesimYerleri;

        public ushort toplamYerlesim
        {
            get 
            {
                return Convert.ToUInt16(yerlesimYerleri.Length); 
            }
        }

        public void hesapla(bool[,] kosul)
        {
            yerlesimYerleri = olasiliklariHesapla(kosul);
        }

        public bool nodeYerlesirmi(bool[,] kosul,ushort gun, ushort saat)
        {
            bool yerlesir = true;
            for (ushort i = 0; i < tSaat; i++)
            {
                if (saat + i < kosul.GetLength(1))
                {
                    if (!kosul[gun, saat + i])
                        yerlesir = false;
                }
                else
                    yerlesir = false;
            }
            return yerlesir;
        }

        yerlesimYeri[] olasiliklariHesapla(bool[,] kosul)
        {
            List<yerlesimYeri> yerlesimyerleri=new List<yerlesimYeri>();
            for(ushort i=0;i<kosul.GetLength(0);i++)
                for (ushort j = 0; j < kosul.GetLength(1); j++)
                {
                    if (nodeYerlesirmi(kosul, i, j))
                    {
                        yerlesimYeri yer = new yerlesimYeri();
                        yer.gun = i;
                        yer.saat = j;
                        yerlesimyerleri.Add(yer);
                    }
                }
            return yerlesimyerleri.ToArray();
        }
        #endregion

        public bilesenNode(ushort _id, bilesenTanimliDers _tanimliDers, ushort _toplamSaat, ushort _gun = 0, ushort _saat = 0)
        {
            this.id = _id;
            this.tanimliDers = _tanimliDers;
            this.tSaat = _toplamSaat;
            //this.yerlesmis = _yerlesmis;
            this.yerlesimGun = _gun;
            this.yerlesimSaat = _saat;
        }

    }

    public class bilesenTanimliDers
    {
        public ushort id;

        public DersProgrami dersProgrami;

        public bilesenDers ders;
        public List<bilesenSinifGrup> sinifGruplar;
        public List<bilesenOgretmen> ogretmenler;
        public List<bilesenDerslik> derslikler;

        public string aciklama
        {
            get
            {
                string str = this.ders.adi+"/";
                foreach (bilesenOgretmen ogretmen in ogretmenler)
                    str += ogretmen.kisaAdi+" ";
                str += "/";
                foreach (bilesenSinifGrup sinifGrup in sinifGruplar)
                    str += sinifGrup.sinif.kisaAdi + "-" + sinifGrup.grup.kisaAdi+" ";
                str += "/";
                foreach (bilesenDerslik derslik in derslikler)
                    str += derslik.kisaAdi + " ";
                return str;
            }
        }
        public string baslangicYerlesimi;

        public string yerlesimStr;
        public static ushort[] yerlesimHesapla(string yerlesim)
        {
            string[] bolumler = yerlesim.Split('+');
            ushort[] yer = new ushort[bolumler.Length];
            for (int i = 0; i < bolumler.Length; i++)
            {
                yer[i] = Convert.ToUInt16(bolumler[i]);
            }
            return yer;
        }
        public ushort[] yerlesim
        {
            get
            {
                return yerlesimHesapla(yerlesimStr);
            }
        }
        public ushort toplamSaat
        {
            get
            {
                ushort tpl = 0;
                ushort[] bolumler = yerlesimHesapla(this.yerlesimStr);
                for (int i = 0; i < bolumler.Length; i++)
                    tpl += bolumler[i];
                return tpl;
            }
        }

        public bilesenNode[] nodes;

        public bool[,] kosul;

        #region YERLEŞİM İŞLEMLERİ

        public struct nodeOlasilik
        {
            public ushort gun;
            public ushort saat;
        }

        public class yerlesimOlasilik
        {
            public int id;
            public string yerlesimStr
            {
                get
                {
                    string kodlar = "ABCDEFGHIJKLMNOPRSTUVYZXWQ0123456789*+&$/-";
                    string str = "";
                    for (int i = 0; i < yerlesimler.Count; i++)
                    {
                        str += string.Format("{0}{1}{2}", kodlar[i], kodlar[yerlesimler[i].gun], kodlar[yerlesimler[i].saat]);
                    }
                    return str;
                }



            }
            //public int yildiz;
           // public int sayac;
            public bool aktif=false;
            public List<nodeOlasilik> yerlesimler=new List<nodeOlasilik>();
            public yerlesimOlasilik kopya()
            {
                yerlesimOlasilik yo = new yerlesimOlasilik();
                for (int i = 0; i < this.yerlesimler.Count; i++)
                {
                    yo.yerlesimler.Add(this.yerlesimler[i]);
                }
                return yo;
            }
            public bool[,] tablo;
        }

        public List<yerlesimOlasilik> olasiliklar = new List<yerlesimOlasilik>();

        public yerlesimOlasilik aktifYerlesim;
        public yerlesimOlasilik eskiYerlesim;

        public bool olasilikSina(int olasilikno, bool id=false)
        {
            if (id)
            {
                for (int i = 0; i < olasiliklar.Count; i++)
                {
                    if (olasiliklar[i].id == olasilikno)
                    {
                        olasilikno = i;
                        break;
                    }
                }
            }
            bool sonuc = true;
            bool[,] toplamKosul=araclar.diziOlustur();

            foreach (bilesenOgretmen ogretmen in ogretmenler)
                toplamKosul = araclar.diziBirlestir(toplamKosul, ogretmen.yKosul);
            
            foreach (bilesenDerslik derslik in derslikler)
                toplamKosul = araclar.diziBirlestir(toplamKosul, derslik.yKosul);

            foreach (bilesenSinifGrup sinifgrup in sinifGruplar)
            {
                if (sinifgrup.grup.id != 0)
                {
                    toplamKosul = araclar.diziBirlestir(toplamKosul, sinifgrup.sinif.grupGetir(0).yKosul);
                    toplamKosul = araclar.diziBirlestir(toplamKosul, sinifgrup.grup.yKosul);
                }
                else
                {
                    foreach (bilesenGrup grup in sinifgrup.sinif.gruplar)
                        toplamKosul = araclar.diziBirlestir(toplamKosul, grup.yKosul);
                }
            }

            for (int i = 0; i < nodes.Length; i++)
            {
                sonuc &= nodes[i].nodeYerlesirmi(toplamKosul, olasiliklar[olasilikno].yerlesimler[i].gun, olasiliklar[olasilikno].yerlesimler[i].saat);
                if (!sonuc) break;
            }
            olasiliklar[olasilikno].aktif = sonuc;
            return sonuc;
        }

        public bool olasilikSina(yerlesimOlasilik ol)
        {
            bool sonuc = true;
            bool[,] toplamKosul = araclar.diziOlustur();

            foreach (bilesenOgretmen ogretmen in ogretmenler)
                toplamKosul = araclar.diziBirlestir(toplamKosul, ogretmen.yKosul);

            foreach (bilesenDerslik derslik in derslikler)
                toplamKosul = araclar.diziBirlestir(toplamKosul, derslik.yKosul);

            foreach (bilesenSinifGrup sinifgrup in sinifGruplar)
            {
                if (sinifgrup.grup.id != 0)
                {
                    toplamKosul = araclar.diziBirlestir(toplamKosul, sinifgrup.sinif.grupGetir(0).yKosul);
                    toplamKosul = araclar.diziBirlestir(toplamKosul, sinifgrup.grup.yKosul);
                }
                else
                {
                    foreach (bilesenGrup grup in sinifgrup.sinif.gruplar)
                        toplamKosul = araclar.diziBirlestir(toplamKosul, grup.yKosul);
                }
            }

            for (int i = 0; i < nodes.Length; i++)
            {
                sonuc &= nodes[i].nodeYerlesirmi(toplamKosul, ol.yerlesimler[i].gun, ol.yerlesimler[i].saat);
                if (!sonuc) break;
            }
            ol.aktif = sonuc;
            return sonuc;
        }

        /*
        public bool[,] olasilikTablo(yerlesimOlasilik ol)
        {
            bool[,] tablo = araclar.diziOlustur(true);
            for (int i = 0; i < this.nodes.Length; i++)
            {
                for (int j = 0; j < this.nodes[i].tSaat; j++)
                    tablo[ol.yerlesimler[i].gun, ol.yerlesimler[i].saat + j] = false;
            }
            return tablo;

        }
        */
        public int aktifOlasilikSayisi()
        {
            int toplam = 0;
            for (int i = 0; i < olasiliklar.Count; i++)
            {
                if (olasilikSina(i))
                    toplam++;
            }

            return toplam;
        }

        public bool eskiyeYerles()
        {
            if (olasilikSina(eskiYerlesim))
            {
                yerles(eskiYerlesim);
                return true;
            }
            else
                return false;
        }

        public bool rastgeleYerles()
        {
            Random rnd = new Random();
            bool yerlesti = false;
            List<yerlesimOlasilik> tumOlasiliklar = this.olasiliklar.ToList();

            while (tumOlasiliklar.Count > 0)
            {
                int r = rnd.Next(tumOlasiliklar.Count);
                if (this.olasilikSina(tumOlasiliklar[r]))
                {
                    this.yerles(tumOlasiliklar[r]);
                    yerlesti = true;
                    break;
                }
                else
                    tumOlasiliklar.RemoveAt(r);
            }
            return yerlesti;
        }

        public double verimHesapla(bool[,] yerlesimKosul, bilesenTanimliDers yerlesecekDers)//, bool ilkYerlesimCik=false)
        {
            if(araclar.catalmi(this,yerlesecekDers))
                return 100d;

            List<yerlesimOlasilik> yerlesenler = new List<yerlesimOlasilik>();
            foreach (yerlesimOlasilik ol in this.olasiliklar)
            {
                if (this.olasilikSina(ol))
                    yerlesenler.Add(ol);
            }
            int once = yerlesenler.Count;
            int sayac = 0;
            if (once == 0)
                return 0d;
            foreach (yerlesimOlasilik ol in yerlesenler)
            {
                bool yerlesir=true;
                for (int i = 0; i < this.nodes.Length; i++)
                {
                    for (int j = 0; j < this.nodes[i].tSaat; j++)
                    {
                        if (yerlesimKosul[ol.yerlesimler[i].gun, ol.yerlesimler[i].saat + j] == false)
                        {
                            yerlesir = false;
                            sayac++;
                            break;
                        }
                        if (!yerlesir)
                            break;
                    }
                    if (!yerlesir)
                        break;
                }
            }

            int sonra = once - sayac;

            return (100d * (double)sonra) / (double)once;
        }

        public yerlesimOlasilik[] yerlesilebilirOlasiliklar()
        {
            List<yerlesimOlasilik> olasilikListesi = new List<yerlesimOlasilik>();
            foreach (yerlesimOlasilik ol in this.olasiliklar)
                if (olasilikSina(ol))
                    olasilikListesi.Add(ol);
            return olasilikListesi.ToArray();
        }

        public bool enIyiyeYerles()
        {
            bool yerlesti = false;
            double[] verim = new double[this.olasiliklar.Count];
            for (int i = 0; i < this.olasiliklar.Count; i++)
            {
                if (this.olasilikSina(i))
                {
                    /*
                    bool[,] olasilikKosul = araclar.diziOlustur(true);
                    for (int j = 0; j < nodes.Length; j++)
                        for (int s = 0; s < nodes[j].tSaat; s++)
                            olasilikKosul[this.olasiliklar[i].yerlesimler[j].gun, this.olasiliklar[i].yerlesimler[j].saat + s] = false;

                    */

                    double v=0d, ss=0;
                    foreach (bilesenTanimliDers tanimliDers in this.iliskiListesi)
                        if (tanimliDers.aktifYerlesim == null || araclar.catalmi(this,tanimliDers))
                        {
                            v += tanimliDers.verimHesapla(this.olasiliklar[i].tablo,this);
                            ss++;
                        }

                    verim[i] = v/ss;

                }
                else
                    verim[i] = -1d;
            }

            double enVerimli = -1d;
            int no = -1;
            for (int i = 0; i < verim.Length; i++)
            {
                if (verim[i] > enVerimli)
                {
                    enVerimli = verim[i];
                    no = i;
                }
            }

            if (enVerimli > -1d && no>-1)
            {
                //if (verim[no] > 0)
                {
                    this.yerles(no);
                    yerlesti = true;
                }

            }
            return yerlesti;
        }

        /*
        public bool ilkIyiyeYerles()
        {
            bool yerlesti = false;
            int olasilikNo=-1;
            for (int i = 0; i < this.olasiliklar.Count; i++)
            {
                if (this.olasilikSina(i))
                {
                    bool[,] olasilikKosul = araclar.diziOlustur(true);
                    for (int j = 0; j < nodes.Length; j++)
                        for (int s = 0; s < nodes[j].tSaat; s++)
                            olasilikKosul[this.olasiliklar[i].yerlesimler[j].gun, this.olasiliklar[i].yerlesimler[j].saat + s] = false;
                    double v = 1d;
                    foreach (bilesenTanimliDers tanimliDers in this.iliskiListesi)
                        if (tanimliDers.aktifYerlesim == null)
                            v *= tanimliDers.verimHesapla(olasilikKosul,this);

                    if (v > 0)
                    {
                        olasilikNo = i;
                        break;
                    }

                }
            }

            if (olasilikNo!=-1)
            {
                this.yerles(olasilikNo);
                yerlesti = true;
            }


            return yerlesti;
        }
         * */

        private void aktifOlasiligiIlkeGetir()
        {
            olasiliklar.Remove(aktifYerlesim);
            olasiliklar.Insert(0, aktifYerlesim);
        }

        public void yerles(int olasilikno, bool id = false)
        {
            if (id)
            {
                for (int i = 0; i < olasiliklar.Count; i++)
                {
                    if (olasiliklar[i].id == olasilikno)
                    {
                        olasilikno = i;
                        break;
                    }
                }
            }

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].yerlesimGun=olasiliklar[olasilikno].yerlesimler[i].gun;
                nodes[i].yerlesimSaat=olasiliklar[olasilikno].yerlesimler[i].saat;
                //nodes[i].yerlesmis=true;
            }

            foreach (bilesenOgretmen ogretmen in ogretmenler)
                ogretmen.yKosul = araclar.diziBirlestir(ogretmen.yKosul, olasiliklar[olasilikno].tablo);

            foreach (bilesenDerslik derslik in derslikler)
                derslik.yKosul = araclar.diziBirlestir(derslik.yKosul, olasiliklar[olasilikno].tablo);

            foreach (bilesenSinifGrup sinifgrup in sinifGruplar)
            {
                    sinifgrup.grup.yKosul = araclar.diziBirlestir(olasiliklar[olasilikno].tablo, sinifgrup.grup.yKosul);
            }
            aktifYerlesim = olasiliklar[olasilikno];

            aktifOlasiligiIlkeGetir();

        }

        public void yerles(yerlesimOlasilik ol)
        {
            if (this.olasiliklar.Contains(ol))
            {

                for (int i = 0; i < nodes.Length; i++)
                {
                    nodes[i].yerlesimGun = ol.yerlesimler[i].gun;
                    nodes[i].yerlesimSaat = ol.yerlesimler[i].saat;
                    //nodes[i].yerlesmis = true;
                }

                

                foreach (bilesenOgretmen ogretmen in ogretmenler)
                    ogretmen.yKosul = araclar.diziBirlestir(ogretmen.yKosul, ol.tablo);

                foreach (bilesenDerslik derslik in derslikler)
                    derslik.yKosul = araclar.diziBirlestir(derslik.yKosul, ol.tablo);

                foreach (bilesenSinifGrup sinifgrup in sinifGruplar)
                {
                    sinifgrup.grup.yKosul = araclar.diziBirlestir(ol.tablo, sinifgrup.grup.yKosul);
                }
                aktifYerlesim = ol;

                aktifOlasiligiIlkeGetir();

            }
            else
                MessageBox.Show("Hata");


        }

        public void kaldir()
        {
            eskiYerlesim = aktifYerlesim;
            bool[,] kaldirilacak = araclar.diziOlustur(false);
            for (int i = 0; i < nodes.Length; i++)
            {
                //nodes[i].yerlesmis = false;

                for (int j = 0; j < nodes[i].tSaat; j++)
                    kaldirilacak[nodes[i].yerlesimGun, nodes[i].yerlesimSaat + j] = true;
            }

            foreach (bilesenOgretmen ogretmen in ogretmenler)
                ogretmen.yKosul = araclar.diziEkle(ogretmen.yKosul, kaldirilacak);

            foreach (bilesenDerslik derslik in derslikler)
                derslik.yKosul = araclar.diziEkle(derslik.yKosul, kaldirilacak);

            foreach (bilesenSinifGrup sinifgrup in sinifGruplar)
            {
                    sinifgrup.grup.yKosul = araclar.diziEkle(kaldirilacak, sinifgrup.grup.yKosul);
            }
            aktifYerlesim = null;
        }

        private void olasilikTara(yerlesimOlasilik _yerlesimOlasilik = null, ushort _nodeSira = 0, ushort _olasilikSira = 0)
        {
            if (_yerlesimOlasilik == null)
                _yerlesimOlasilik = new yerlesimOlasilik();
            
            yerlesimOlasilik yerlesimYedek = _yerlesimOlasilik.kopya();

            if (_nodeSira < nodes.Length)
            {
                bilesenNode node = nodes[_nodeSira];

                if (_olasilikSira < node.yerlesimYerleri.Length)
                {
                    bool olur = true;
                    bilesenNode.yerlesimYeri yer = node.yerlesimYerleri[_olasilikSira];

                    for (int b = 0; b < _nodeSira; b++)
                    {
                        if (nodes[b].tSaat == node.tSaat && yerlesimYedek.yerlesimler[b].gun > yer.gun)
                            olur = false;
                        if (yerlesimYedek.yerlesimler[b].gun == yer.gun)
                            olur = false;
                    }

                    if (olur)
                    {
                        nodeOlasilik nodeyerles = new nodeOlasilik();
                        nodeyerles.gun = yer.gun;
                        nodeyerles.saat = yer.saat;
                        yerlesimYedek.yerlesimler.Add(nodeyerles);
                        if (yerlesimYedek.yerlesimler.Count == nodes.Length)
                        {
                            olasiliklar.Add(yerlesimYedek);
                        }
                        else
                            olasilikTara(yerlesimYedek, Convert.ToUInt16(_nodeSira + 1), 0);
                    }

                    olasilikTara(_yerlesimOlasilik, _nodeSira, Convert.ToUInt16(_olasilikSira + 1));
                }

            }
        }

        private void olasiliklariOlustur()
        {
            olasiliklar = new List<yerlesimOlasilik>();
            olasilikTara();
            foreach (yerlesimOlasilik ol in this.olasiliklar)
            {
                bool[,] tablo = araclar.diziOlustur(true);
                for (int i = 0; i < this.nodes.Length; i++)
                {
                    for (int j = 0; j < this.nodes[i].tSaat; j++)
                        tablo[ol.yerlesimler[i].gun, ol.yerlesimler[i].saat + j] = false;
                }
                ol.tablo = tablo;
            }
        }

        private void kosullariTopla()
        {
            kosul=araclar.diziKopyala(dersProgrami.kosullar);

            kosul = araclar.diziBirlestir(kosul, this.ders.kosul);
            
            foreach (bilesenSinifGrup sinifGrup in this.sinifGruplar)
                kosul = araclar.diziBirlestir(kosul, sinifGrup.sinif.kosul);
            
            foreach (bilesenOgretmen ogretmen in this.ogretmenler)
                kosul = araclar.diziBirlestir(kosul, ogretmen.kosul);

            foreach (bilesenDerslik derslik in this.derslikler)
                kosul = araclar.diziBirlestir(kosul, derslik.kosul);
        }

        private void nodelariOlustur()
        {
            ushort[] nodSaatler = yerlesim;
            nodes = new bilesenNode[nodSaatler.Length];
            for (ushort i = 0; i < nodSaatler.Length; i++)
            {
                nodes[i] = new bilesenNode(i,this, nodSaatler[i]);
            }
        }

        public bilesenTanimliDers[] cikarilacakVerimliDers()
        {
            List<bilesenTanimliDers> cDers=new List<bilesenTanimliDers>();
            //int enYuksekSayac = -1;

            foreach (bilesenTanimliDers d in this.iliskiListesi)
            {
                if (d.aktifYerlesim != null)
                {
                    bilesenTanimliDers.yerlesimOlasilik olDers = d.aktifYerlesim;
                    //bool[,] kesisimKum = araclar.diziOlustur(true);

                    //YERLEŞİM YERİNİ BELİRLE
                    foreach (yerlesimOlasilik ol in this.olasiliklar)
                    {
                        if (araclar.diziKesisiyormu(ol.tablo, olDers.tablo))
                        {
                            cDers.Add(d);
                            break;
                        }
                    }


                }
            }
            return cDers.ToArray();
        }

        public List<bilesenTanimliDers> iliskiListesi;

        public void iliskileriOlustur()
        {
            for(int i=0;i<iliskiListesi.Count;i++)
                for(int j=i;j<iliskiListesi.Count;j++)
                    if (iliskiListesi[j].olasiliklar.Count > iliskiListesi[i].olasiliklar.Count)
                    {
                        bilesenTanimliDers yedek = iliskiListesi[i];
                        iliskiListesi[i] = iliskiListesi[j];
                        iliskiListesi[j] = yedek;
                    }
        }

        public void yerlesimeHazirla()
        {
            kosullariTopla();
            nodelariOlustur();
            foreach (bilesenNode node in nodes)
                node.hesapla(kosul);
            olasiliklariOlustur();

            int sayac=0;
            foreach (yerlesimOlasilik ol in olasiliklar)
            {
                ol.id = sayac++;
            }
            iliskiListesi = new List<bilesenTanimliDers>();
            foreach (bilesenTanimliDers tanimliDers in this.dersProgrami.tanimliDersler)
            {
                bool ekle=false;
                if(tanimliDers!=this)
                {
                    if(this.iliskiListesi.Contains(tanimliDers))
                        continue;
                    foreach(bilesenDerslik derslik in tanimliDers.derslikler)
                        if(this.derslikler.Contains(derslik))
                            ekle=true;

                    foreach (bilesenOgretmen ogretmen in tanimliDers.ogretmenler)
                        if (this.ogretmenler.Contains(ogretmen))
                            ekle = true;

                    foreach (bilesenSinifGrup sinifgrup in tanimliDers.sinifGruplar)
                        foreach (bilesenSinifGrup sinifgrup2 in this.sinifGruplar)
                            if (sinifgrup.sinif == sinifgrup2.sinif)
                                ekle = true;

                    if (ekle)
                        this.iliskiListesi.Add(tanimliDers);
                }
                aktifYerlesim = null;
                denemeSayac = 0;
            }




/*
            string str = "";
            for (int i = 0; i < olasiliklar.Count; i++)
            {
                yerlesimOlasilik yerlesim = olasiliklar[i];
                for (int j = 0; j < yerlesim.yerlesimler.Count; j++)
                    str += string.Format("({0}-{1})", yerlesim.yerlesimler[j].gun, yerlesim.yerlesimler[j].saat);
                str += "\n";
            }
            Clipboard.SetText(str);
*/
        }

        #endregion

        public bilesenTanimliDers(ushort _id, bilesenDers _ders, List<bilesenSinifGrup> _sinifGruplar, List<bilesenOgretmen> _ogretmenler, List<bilesenDerslik> _derslikler, string _yerlesimStr, DersProgrami _dpr)
        {
            this.dersProgrami = _dpr;
            this.id = _id;
            this.ders = _ders;
            this.sinifGruplar = _sinifGruplar;
            this.ogretmenler = _ogretmenler;
            this.derslikler = _derslikler;
            this.yerlesimStr = _yerlesimStr;
        }

        #region İYİLEŞTİRMELİ YERLEŞTİRME İÇİN KULLANILACAKLAR
        public int denemeSayac;

        public int denemeXsaat
        {
            get 
            {
                int yerlesenIliskiSay = 0;
                foreach (bilesenTanimliDers ders in this.iliskiListesi)
                    if (ders.aktifYerlesim != null)
                        yerlesenIliskiSay++;
                return this.denemeSayac * toplamSaat+yerlesenIliskiSay; 
            }
        }

       

        #endregion

        /*
        #region HAVUZLU TARAMA
        int[] YerlesimListesi;

        List<int[]> elitYerlesimListesi;
        List<int[]> curukYerlesimListesi;

        public void yerlesimListeleriniOlustur()
        {
            YerlesimListesi = new int[this.iliskiListesi.Count];
            elitYerlesimListesi=new List<int[]>();
            curukYerlesimListesi=new List<int[]>();
        }

        
        #endregion
        */

    }

    #endregion

    public class kosulPanel : Control
    {
        public bool[,] kosullar;

        int gunSay, saatSay;

        int aktifSatir, aktifSutun;

        Pen KalinCizgi= new Pen(new SolidBrush(Color.Black));

        SolidBrush renkYesil = new SolidBrush(Color.Green);
        SolidBrush renkSari = new SolidBrush(Color.Yellow);
        SolidBrush renkSiyah = new SolidBrush(Color.Black);
        SolidBrush renkKirmizi = new SolidBrush(Color.Red);
        
        Font fontYazilar = new Font("Arial",9, FontStyle.Bold);

        StringFormat formatDikeyOrtali = new StringFormat();
        StringFormat formatYatayDikeyOrtali = new StringFormat();
                
        const int BTNGEN = 70;
        const int BTNYUK = 30;

        int GEN = 570;
        int YUK = 330;

        int secGen;
        int secYuk;

        Rectangle[,] recSecimler;
        Rectangle[] recGunler;
        Rectangle[] recSaatler;
                
        public kosulPanel(ref bool[,] kosulGirdi)
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            gunSay =  kosulGirdi.GetLength(0);
            saatSay = kosulGirdi.GetLength(1);

            /*
            kosullar = new bool[gunSay, saatSay];
            for (int i = 0; i < gunSay; i++)
                for (int j = 0; j < saatSay; j++)
                    kosullar[i, j] = kosulGirdi[i, j];
             */
            kosullar = kosulGirdi;

            GEN = BTNGEN + saatSay * (int)((GEN-BTNGEN)/saatSay);
            YUK = BTNYUK + gunSay * (int)((YUK-BTNYUK)/gunSay);

            secGen = (GEN-BTNGEN) / saatSay;
            secYuk = (YUK-BTNYUK) / gunSay;

            recGunler = new Rectangle[gunSay];
            for(int i=0;i<gunSay;i++)
                recGunler[i]=new Rectangle(new Point(0,secYuk*i+BTNYUK),new Size(BTNGEN,secYuk));

            recSaatler = new Rectangle[saatSay];
            for (int i = 0; i < saatSay; i++)
                recSaatler[i] = new Rectangle(new Point(BTNGEN + secGen * i, 0), new Size(secGen, BTNYUK));


                recSecimler = new Rectangle[gunSay, saatSay];
            for (int i = 0; i < gunSay; i++)
                for (int j = 0; j < saatSay; j++)
                    recSecimler[i, j] = new Rectangle(new Point(j * secGen+BTNGEN, i * secYuk+BTNYUK), new Size(secGen, secYuk));

            this.Width = GEN+1;
            this.Height = YUK+1;

            formatDikeyOrtali.LineAlignment = StringAlignment.Center;
            formatYatayDikeyOrtali.LineAlignment = StringAlignment.Center;
            formatYatayDikeyOrtali.Alignment = StringAlignment.Center;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            for (int i = 0; i < gunSay; i++)
                for (int j = 0; j < saatSay; j++)
                {
                    Rectangle recSecIc = new Rectangle(new Point(recSecimler[i, j].X + 3, recSecimler[i, j].Y + 3), new Size(secGen - 6, secYuk - 6));
                    
                    if (kosullar[i, j])
                        e.Graphics.FillRectangle(renkYesil, recSecIc);
                    else
                        e.Graphics.FillRectangle(renkKirmizi, recSecIc);
                    
                    e.Graphics.DrawRectangle(KalinCizgi, recSecimler[i, j]);
                }

            for (int i = 0; i < gunSay; i++)
            {
                if (i == aktifSatir)
                    e.Graphics.FillRectangle(renkSari, recGunler[i]);
                e.Graphics.DrawRectangle(KalinCizgi, recGunler[i]);
                e.Graphics.DrawString(tanim.program.gunler[i], fontYazilar, renkSiyah , recGunler[i], formatDikeyOrtali);
            }

            for (int i = 0; i < saatSay; i++)
            {
                if (aktifSutun == i)
                    e.Graphics.FillRectangle(renkSari, recSaatler[i]);
                e.Graphics.DrawRectangle(KalinCizgi, recSaatler[i]);
                e.Graphics.DrawString((i+1).ToString(), fontYazilar, renkSiyah , recSaatler[i], formatYatayDikeyOrtali);
            }

            base.OnPaint(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            aktifSatir = (e.Y - BTNYUK) / secYuk;
            aktifSutun = (e.X - BTNGEN) / secGen;

            if (e.Y > BTNYUK && e.X > BTNGEN && e.Y<(BTNYUK+secYuk*gunSay)&&e.X<(BTNGEN+secGen*saatSay))
            {
                kosullar[aktifSatir, aktifSutun] = !kosullar[aktifSatir, aktifSutun];
            }

            if (e.Y < BTNYUK && e.X > BTNGEN)
            {
                bool degili=!kosullar[0,aktifSutun];
                for (int i = 0; i < gunSay; i++)
                    kosullar[i, aktifSutun] = degili;
            }

            if (e.X < BTNGEN && e.Y > BTNYUK)
            {
                bool degili = !kosullar[aktifSatir, 0];
                for (int i = 0; i < saatSay; i++)
                    kosullar[aktifSatir, i] = degili;
            }

            this.Invalidate();
            base.OnMouseClick(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            aktifSatir = (e.Y - BTNYUK) / secYuk;
            aktifSutun = (e.X - BTNGEN) / secGen;

            this.Invalidate();
            base.OnMouseMove(e);
        }

    }

}