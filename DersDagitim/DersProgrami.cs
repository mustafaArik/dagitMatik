using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Collections;


namespace DersDagitim
{
    public class DersProgrami
    {
        #region GENEL BİLGİLER

        public string okulAdi;
        public string okulMuduru;
        public string okulMudurYrd;
        public bool mudurYrdBas;
        public string ogretimYili;
        public byte gunlukDersSaatiSayisi;
        public byte haftalikGunSayisi;
        public string[] gunler;
        public string[] derssaatleri;
        public bool[,] kosullar;

        #endregion

        public string dosyaAdi;

        #region BİLEŞENLER

        //Bileşen bilgileri
        public ushort idDerslikSon, idOgretmenSon, idSinifSon, idDersSon, idTanimliDersSon;

        //Bileşenler
        public List<bilesenDers> dersler = new List<bilesenDers>();
        public List<bilesenOgretmen> ogretmenler = new List<bilesenOgretmen>();
        public List<bilesenSinif> siniflar = new List<bilesenSinif>();
        public List<bilesenDerslik> derslikler = new List<bilesenDerslik>();
        public List<bilesenTanimliDers> tanimliDersler = new List<bilesenTanimliDers>();

        public bilesenDers dersGetir(ushort id)
        {
            bilesenDers b=null;
            for (int i = 0; i < dersler.Count; i++)
                if ((dersler[i] as bilesenDers).id == id)
                    b = dersler[i] as bilesenDers;
            return b;
        }
        public bilesenOgretmen ogretmenGetir(ushort id)
        {
            bilesenOgretmen b=null;
            for (int i = 0; i < ogretmenler.Count; i++)
                if ((ogretmenler[i] as bilesenOgretmen).id == id)
                    b = ogretmenler[i] as bilesenOgretmen;
            return b;
        }
        public bilesenDerslik derslikGetir(ushort id)
        {
            bilesenDerslik b=null;
            for (int i = 0; i < derslikler.Count; i++)
                if ((derslikler[i] as bilesenDerslik).id == id)
                    b = derslikler[i] as bilesenDerslik;
            return b;
        }
        public bilesenSinif sinifGetir(ushort id)
        {
            bilesenSinif b=null;
            for (int i = 0; i < siniflar.Count; i++)
                if ((siniflar[i] as bilesenSinif).id == id)
                    b = siniflar[i] as bilesenSinif;
            return b;
        }
        public bilesenTanimliDers tanimliDersGetir(ushort id)
        {
            bilesenTanimliDers b = null;
            for (int i = 0; i < tanimliDersler.Count; i++)
                if ((tanimliDersler[i] as bilesenTanimliDers).id == id)
                    b = tanimliDersler[i] as bilesenTanimliDers;
            return b;
        }


        public bool tumuYerlesmis()
        {
            foreach (bilesenTanimliDers tanimliDers in this.tanimliDersler)
                if (tanimliDers.aktifYerlesim == null)
                    return false;
            return true;
        }


        public ushort bilesenDersSayisi(bilesenTaban bilesen, bilesenTaban grupSay = null)
        {
            ushort toplam = 0;

            if (bilesen is bilesenDers)
            {
                bilesenDers ders = bilesen as bilesenDers;
                ushort id = ders.id;
                for (int i = 0; i < tanimliDersler.Count; i++)
                {
                    bilesenTanimliDers tanimliders = tanimliDersler[i] as bilesenTanimliDers;
                    if (id == tanimliders.ders.id)
                        toplam += tanimliders.toplamSaat;
                }
            }

            if (bilesen is bilesenDerslik)
            {
                bilesenDerslik derslik = bilesen as bilesenDerslik;
                ushort id = derslik.id;
                for (int i = 0; i < tanimliDersler.Count; i++)
                {
                    bilesenTanimliDers tanimliders = tanimliDersler[i] as bilesenTanimliDers;
                    foreach (bilesenDerslik d in tanimliders.derslikler)
                    {
                        if (d.id == id) toplam += tanimliders.toplamSaat;
                    }

                }
            }

            if (bilesen is bilesenSinif)
            {
                ushort toplamDondur = 0;
                bilesenSinif sinif=bilesen as bilesenSinif;
                ushort id = sinif.id;

                ushort[,] grupToplamlari = new ushort[2,sinif.gruplar.Count];
                for (int c = 0; c < sinif.gruplar.Count; c++)
                {
                    bilesenGrup grp = sinif.gruplar[c] as bilesenGrup;
                    grupToplamlari[0, c] = grp.id;
                }

                for (int i = 0; i < tanimliDersler.Count; i++)
                {
                    bilesenTanimliDers tanimliders = tanimliDersler[i] as bilesenTanimliDers;

                    foreach (bilesenSinifGrup d in tanimliders.sinifGruplar)
                    {
                        if(d.sinif.id==id)
                            for (int r = 0; r < sinif.gruplar.Count; r++)
                            {
                                ushort grpid = (sinif.gruplar[r] as bilesenGrup).id;
                                if (d.grup.id == grpid)
                                {
                                    grupToplamlari[1, r] += tanimliders.toplamSaat;
                                }
                            }
                    }
                }

                if (grupSay != null)
                {
                    ushort idd = (grupSay as bilesenGrup).id;
                    for (int i = 0; i < grupToplamlari.GetLength(1); i++)
                        if (idd == grupToplamlari[0, i])
                            toplamDondur = grupToplamlari[1, i];
                }
                else
                {
                    toplamDondur = grupToplamlari[1, 0];
                    ushort enbuyuk=0;
                    for (int i = 1; i < grupToplamlari.GetLength(1); i++)
                        if (grupToplamlari[1, i] > enbuyuk)
                            enbuyuk = grupToplamlari[1, i];
                    toplamDondur += enbuyuk;
                }

                return toplamDondur;


            }

            if (bilesen is bilesenOgretmen)
            {
                ushort id = (bilesen as bilesenOgretmen).id;
                for (int i = 0; i < tanimliDersler.Count; i++)
                {
                    bilesenTanimliDers tanimliders = tanimliDersler[i] as bilesenTanimliDers;
                    foreach (bilesenOgretmen d in tanimliders.ogretmenler)
                    {
                        if (d.id == id) toplam += tanimliders.toplamSaat;
                    }
                }
            }




            return toplam;
        }

        public void temizle()
        {
            for(int i=0;i<tanimliDersler.Count;i++)
            {
                bilesenTanimliDers tanimliders = tanimliDersler[i] as bilesenTanimliDers;

                if (this.dersGetir(tanimliders.ders.id) == null)
                {
                    this.tanimliDersler.RemoveAt(i--);
                    continue;
                }

                for (int j = 0; j < tanimliders.sinifGruplar.Count; j++)
                {
                    bilesenSinifGrup sinifgrup=tanimliders.sinifGruplar[j] as bilesenSinifGrup;
                    if (this.sinifGetir(sinifgrup.sinif.id) == null)
                        tanimliders.sinifGruplar.RemoveAt(j--);
                    else
                        if (this.sinifGetir(sinifgrup.sinif.id).grupGetir(sinifgrup.grup.id) == null)
                            tanimliders.sinifGruplar.RemoveAt(j--);
                }

                if (tanimliders.sinifGruplar.Count == 0)
                {
                    this.tanimliDersler.RemoveAt(i--);
                    continue;
                }

                for (int j = 0; j < tanimliders.ogretmenler.Count; j++)
                {
                    bilesenOgretmen ogretmen = tanimliders.ogretmenler[j] as bilesenOgretmen;
                    if (this.ogretmenGetir(ogretmen.id) == null)
                        tanimliders.ogretmenler.RemoveAt(j--);
                }

                if (tanimliders.ogretmenler.Count == 0)
                {
                    this.tanimliDersler.RemoveAt(i--);
                    continue;
                }

                for (int j = 0; j < tanimliders.derslikler.Count; j++)
                {
                    bilesenDerslik derslik = tanimliders.derslikler[j] as bilesenDerslik;
                    if (this.derslikGetir(derslik.id) == null)
                        tanimliders.derslikler.RemoveAt(j--);
                }
                /*
                if (tanimliders.derslikler.Count == 0)
                {
                    this.tanimliDersler.RemoveAt(i--);
                    continue;
                }

                */

            }

        }

        public int uygunDersSaatiSay(bilesenTaban bilesen)
        {
            int toplam = 0;
            bool[,] kosulBirlesim = araclar.diziBirlestir(this.kosullar, bilesen.kosul);
            for (int i = 0; i < kosulBirlesim.GetLength(0); i++)
                for (int j = 0; j < kosulBirlesim.GetLength(1); j++)
                    if (kosulBirlesim[i, j]) toplam++;
            return toplam;
        }

        #endregion

        public double yerlesimYuzde
        {
            get
            {
                double yerlesenSayisi = 0;
                foreach (bilesenTanimliDers ders in this.tanimliDersler)
                {
                    if (ders.aktifYerlesim != null)
                        yerlesenSayisi++;
                }
                return (100 * yerlesenSayisi) / (double)this.tanimliDersler.Count;
            }
        }

        public DersProgrami(bool ornekVeri=true)
        {
            if (ornekVeri)
            {
                idDerslikSon = idOgretmenSon = idSinifSon = idDersSon = idTanimliDersSon = 0;
                haftalikGunSayisi = 5;
                mudurYrdBas = false;
                gunlukDersSaatiSayisi = 8;
                ogretimYili = string.Format("{0}-{1}", DateTime.Now.Year, DateTime.Now.Year + 1);
                gunler = new string[] { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma" };
                derssaatleri = new string[] { "08:30-09:10", "09:20-10:00", "10:10-10:50", "11:00-11:40", "11:50-12:30", "13:30-14:10", "14:20-15:00", "15:10-15:50" };
                kosullar = new bool[haftalikGunSayisi, gunlukDersSaatiSayisi];
                for (int i = 0; i < haftalikGunSayisi; i++)
                    for (int j = 0; j < gunlukDersSaatiSayisi; j++)
                        kosullar[i, j] = true;
            }
        }

        public void dagitimaHazirla()
        {
            ArrayList lstTumu = new ArrayList();
            lstTumu.AddRange(this.ogretmenler);
            lstTumu.AddRange(this.derslikler);

            //Öğretmenlerin yerleşmeme sayılarını sıfırla
            foreach (bilesenOgretmen ogretmen in this.ogretmenler)
                ogretmen.yerlesmemeSayisi = 0;

            for (int t = 0; t < this.tanimliDersler.Count; t++)
            {
                for (int i = 0; i < lstTumu.Count; i++)
                {
                    bilesenTaban bilesen = lstTumu[i] as bilesenTaban;
                    bilesen.yKosul = araclar.diziOlustur();
                }

                foreach (bilesenSinif sinif in siniflar)
                {
                    foreach (bilesenGrup grup in sinif.gruplar)
                        grup.yKosul = araclar.diziOlustur();
                }
            }
        }

        public void kaydet(bool farkliKaydet=false)
        {
            bool dosyaAcOk = true;
            if (dosyaAdi == null || farkliKaydet)
            {
                SaveFileDialog dosyaAcDialog = new SaveFileDialog();
                dosyaAcDialog.Filter = "(Ders Programı Dosyası)|*.dprg";
                if (dosyaAcDialog.ShowDialog() == DialogResult.OK)
                {
                    dosyaAdi = dosyaAcDialog.FileName;
                }
                else
                    dosyaAcOk = false;
                
            }
            if (dosyaAdi != "" && dosyaAcOk)
            {
                XmlDocument xmlDosya = new XmlDocument();
                XmlNode tanimNode = xmlDosya.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDosya.AppendChild(tanimNode);

                XmlNode nodeRoot = xmlDosya.CreateElement("DersProgrami");
                xmlDosya.AppendChild(nodeRoot);

                #region GENEL AYARLAR
                {
                    XmlNode nodeAyarlar = xmlDosya.CreateElement("GenelAyarlar");
                    nodeRoot.AppendChild(nodeAyarlar);

                    XmlNode nodeOkulAdi = xmlDosya.CreateElement("OkulAdi");
                    nodeOkulAdi.InnerText = okulAdi;
                    nodeAyarlar.AppendChild(nodeOkulAdi);

                    XmlNode nodeOkulMuduru = xmlDosya.CreateElement("OkulMuduru");
                    nodeOkulMuduru.InnerText = okulMuduru;
                    nodeAyarlar.AppendChild(nodeOkulMuduru);

                    XmlNode nodeOkulMudurYrd = xmlDosya.CreateElement("OkulMudurYrd");
                    XmlAttribute nodeBasYrd = xmlDosya.CreateAttribute("Bas");
                    if (this.mudurYrdBas) nodeBasYrd.Value = "1"; else nodeBasYrd.Value = "0";
                    nodeOkulMudurYrd.Attributes.Append(nodeBasYrd);
                    nodeOkulMudurYrd.InnerText = okulMudurYrd;
                    nodeAyarlar.AppendChild(nodeOkulMudurYrd);

                    XmlNode nodeOgretimYili = xmlDosya.CreateElement("OgretimYili");
                    nodeOgretimYili.InnerText = ogretimYili;
                    nodeAyarlar.AppendChild(nodeOgretimYili);

                    XmlNode nodeGunlukDersSaatiSayisi = xmlDosya.CreateElement("GunlukDersSaatiSayisi");
                    nodeGunlukDersSaatiSayisi.InnerText = gunlukDersSaatiSayisi.ToString();
                    nodeAyarlar.AppendChild(nodeGunlukDersSaatiSayisi);

                    XmlNode nodeHaftalikGunSayisi = xmlDosya.CreateElement("HaftalikGunSayisi");
                    nodeHaftalikGunSayisi.InnerText = haftalikGunSayisi.ToString();
                    nodeAyarlar.AppendChild(nodeHaftalikGunSayisi);

                    XmlNode nodeGunler = xmlDosya.CreateElement("Gunler");
                    nodeAyarlar.AppendChild(nodeGunler);

                    for (int i = 0; i < gunler.Length; i++)
                    {
                        XmlNode nodeGun = xmlDosya.CreateElement("Gun");
                        XmlAttribute nodeParam = xmlDosya.CreateAttribute("Sira");
                        nodeParam.Value = i.ToString();
                        nodeGun.Attributes.Append(nodeParam);
                        nodeGun.InnerText = gunler[i];
                        nodeGunler.AppendChild(nodeGun);
                    }


                    XmlNode nodeSaatler = xmlDosya.CreateElement("Saatler");
                    nodeAyarlar.AppendChild(nodeSaatler);
                    for (int i = 0; i < derssaatleri.Length; i++)
                    {
                        XmlNode nodeSaat = xmlDosya.CreateElement("Saat");
                        XmlAttribute nodeParam = xmlDosya.CreateAttribute("Sira");
                        nodeParam.Value = i.ToString();
                        nodeSaat.Attributes.Append(nodeParam);
                        nodeSaat.InnerText = derssaatleri[i];
                        nodeSaatler.AppendChild(nodeSaat);
                    }


                    XmlNode nodeKosullar = xmlDosya.CreateElement("Kosullar");
                    nodeKosullar.InnerText = araclar.diziKodla(kosullar);
                    nodeAyarlar.AppendChild(nodeKosullar);
                }
                #endregion


                #region DERS BİLGİLERİ
                {
                    XmlNode nodeDersler = xmlDosya.CreateElement("Dersler");
                    XmlAttribute nodeParamDerslerIdSon = xmlDosya.CreateAttribute("DerslerIdSon");
                    nodeParamDerslerIdSon.Value = this.idDersSon.ToString();
                    nodeDersler.Attributes.Append(nodeParamDerslerIdSon);
                    nodeRoot.AppendChild(nodeDersler);

                    for (int i = 0; i < this.dersler.Count; i++)
                    {
                        bilesenDers ders = this.dersler[i] as bilesenDers;
                        XmlNode nodeDers = xmlDosya.CreateElement("Ders");
                        nodeDersler.AppendChild(nodeDers);

                        XmlAttribute nodeParam = xmlDosya.CreateAttribute("Id");
                        nodeParam.Value = ders.id.ToString();
                        nodeDers.Attributes.Append(nodeParam);

                        XmlNode nodeDersAdi = xmlDosya.CreateElement("Adi");
                        nodeDers.AppendChild(nodeDersAdi);
                        nodeDersAdi.InnerText = ders.adi;

                        XmlNode nodeDersKisaAdi = xmlDosya.CreateElement("KisaAdi");
                        nodeDers.AppendChild(nodeDersKisaAdi);
                        nodeDersKisaAdi.InnerText = ders.kisaAdi;

                        XmlNode nodeDersKosul = xmlDosya.CreateElement("Kosul");
                        nodeDers.AppendChild(nodeDersKosul);
                        nodeDersKosul.InnerText = araclar.diziKodla(ders.kosul);
                    }
                }
                #endregion


                #region ÖĞRETMEN BİLGİLERİ
                {
                    XmlNode nodeOgretmenler = xmlDosya.CreateElement("Ogretmenler");

                    XmlAttribute nodeParamOgretmenlerIdSon = xmlDosya.CreateAttribute("OgretmenlerIdSon");
                    nodeParamOgretmenlerIdSon.Value = this.idOgretmenSon.ToString();
                    nodeOgretmenler.Attributes.Append(nodeParamOgretmenlerIdSon);

                    nodeRoot.AppendChild(nodeOgretmenler);

                    for (int i = 0; i < this.ogretmenler.Count; i++)
                    {
                        bilesenOgretmen ogretmen = this.ogretmenler[i] as bilesenOgretmen;
                        XmlNode nodeOgretmen = xmlDosya.CreateElement("Ogretmen");
                        nodeOgretmenler.AppendChild(nodeOgretmen);

                        XmlAttribute nodeParam = xmlDosya.CreateAttribute("Id");
                        nodeParam.Value = ogretmen.id.ToString();
                        nodeOgretmen.Attributes.Append(nodeParam);

                        XmlNode nodeOgretmenAdi = xmlDosya.CreateElement("Adi");
                        nodeOgretmen.AppendChild(nodeOgretmenAdi);
                        nodeOgretmenAdi.InnerText = ogretmen.adi;

                        XmlNode nodeOgretmenKisaAdi = xmlDosya.CreateElement("KisaAdi");
                        nodeOgretmen.AppendChild(nodeOgretmenKisaAdi);
                        nodeOgretmenKisaAdi.InnerText = ogretmen.kisaAdi;

                        XmlNode nodeOgretmenKosul = xmlDosya.CreateElement("Kosul");
                        nodeOgretmen.AppendChild(nodeOgretmenKosul);
                        nodeOgretmenKosul.InnerText = araclar.diziKodla(ogretmen.kosul);
                    }
                }
                #endregion


                #region DERSLİK BİLGİLERİ
                {
                    XmlNode nodeDerslikler = xmlDosya.CreateElement("Derslikler");

                    XmlAttribute nodeParamDersliklerIdSon = xmlDosya.CreateAttribute("DersliklerIdSon");
                    nodeParamDersliklerIdSon.Value = this.idDerslikSon.ToString();
                    nodeDerslikler.Attributes.Append(nodeParamDersliklerIdSon);

                    nodeRoot.AppendChild(nodeDerslikler);

                    for (int i = 0; i < this.derslikler.Count; i++)
                    {
                        bilesenDerslik derslik = this.derslikler[i] as bilesenDerslik;
                        XmlNode nodeDerslik = xmlDosya.CreateElement("Derslik");
                        nodeDerslikler.AppendChild(nodeDerslik);

                        XmlAttribute nodeParam = xmlDosya.CreateAttribute("Id");
                        nodeParam.Value = derslik.id.ToString();
                        nodeDerslik.Attributes.Append(nodeParam);

                        XmlNode nodeDerslikAdi = xmlDosya.CreateElement("Adi");
                        nodeDerslik.AppendChild(nodeDerslikAdi);
                        nodeDerslikAdi.InnerText = derslik.adi;

                        XmlNode nodeDerslikKisaAdi = xmlDosya.CreateElement("KisaAdi");
                        nodeDerslik.AppendChild(nodeDerslikKisaAdi);
                        nodeDerslikKisaAdi.InnerText = derslik.kisaAdi;

                        XmlNode nodeDerslikKosul = xmlDosya.CreateElement("Kosul");
                        nodeDerslik.AppendChild(nodeDerslikKosul);
                        nodeDerslikKosul.InnerText = araclar.diziKodla(derslik.kosul);
                    }
                }
                #endregion


                #region SINIF BİLGİLERİ
                {
                    XmlNode nodeSiniflar = xmlDosya.CreateElement("Siniflar");

                    XmlAttribute nodeParamSiniflarIdSon = xmlDosya.CreateAttribute("SiniflarIdSon");
                    nodeParamSiniflarIdSon.Value = this.idSinifSon.ToString();
                    nodeSiniflar.Attributes.Append(nodeParamSiniflarIdSon);

                    nodeRoot.AppendChild(nodeSiniflar);

                    for (int i = 0; i < this.siniflar.Count; i++)
                    {
                        bilesenSinif sinif = this.siniflar[i] as bilesenSinif;
                        XmlNode nodeSinif = xmlDosya.CreateElement("Sinif");
                        nodeSiniflar.AppendChild(nodeSinif);

                        XmlAttribute nodeParam = xmlDosya.CreateAttribute("Id");
                        nodeParam.Value = sinif.id.ToString();
                        nodeSinif.Attributes.Append(nodeParam);

                        XmlAttribute nodeParam2 = xmlDosya.CreateAttribute("GruplarIdSon");
                        nodeParam2.Value = sinif.grupIdSon.ToString();
                        nodeSinif.Attributes.Append(nodeParam2);

                        XmlNode nodeSinifAdi = xmlDosya.CreateElement("Adi");
                        nodeSinif.AppendChild(nodeSinifAdi);
                        nodeSinifAdi.InnerText = sinif.adi;

                        XmlNode nodeSinifKisaAdi = xmlDosya.CreateElement("KisaAdi");
                        nodeSinif.AppendChild(nodeSinifKisaAdi);
                        nodeSinifKisaAdi.InnerText = sinif.kisaAdi;

                        XmlNode nodeSinifKosul = xmlDosya.CreateElement("Kosul");
                        nodeSinif.AppendChild(nodeSinifKosul);
                        nodeSinifKosul.InnerText = araclar.diziKodla(sinif.kosul);

                        XmlNode nodeGruplar = xmlDosya.CreateElement("Gruplar");
                        nodeSinif.AppendChild(nodeGruplar);


                        //GRUPLARI KAYDET
                        for (int j = 0; j < sinif.gruplar.Count; j++)
                        {
                            bilesenGrup grup = sinif.gruplar[j] as bilesenGrup;
                            XmlNode nodeGrup = xmlDosya.CreateElement("Grup");
                            nodeGruplar.AppendChild(nodeGrup);

                            XmlAttribute nodeParam3 = xmlDosya.CreateAttribute("Id");
                            nodeParam3.Value = grup.id.ToString();
                            nodeGrup.Attributes.Append(nodeParam3);

                            XmlNode nodeGrupAdi = xmlDosya.CreateElement("Adi");
                            nodeGrupAdi.InnerText = grup.adi;
                            nodeGrup.AppendChild(nodeGrupAdi);

                            XmlNode nodeGrupKisaAdi = xmlDosya.CreateElement("KisaAdi");
                            nodeGrupKisaAdi.InnerText = grup.kisaAdi;
                            nodeGrup.AppendChild(nodeGrupKisaAdi);
                        }



                    }
                }
                #endregion


                #region TANIMLANMIŞ DERSLER
                {
                    XmlNode nodeTanimliDersler = xmlDosya.CreateElement("TanimliDersler");
                    XmlAttribute parametreSonTanimliDersId = xmlDosya.CreateAttribute("TanimliDersIdSon");
                    parametreSonTanimliDersId.Value = this.idTanimliDersSon.ToString();
                    nodeTanimliDersler.Attributes.Append(parametreSonTanimliDersId);
                    nodeRoot.AppendChild(nodeTanimliDersler);

                    for (int i = 0; i < this.tanimliDersler.Count; i++)
                    {
                        XmlNode nodeTanimliDers = xmlDosya.CreateElement("TanimliDers");
                        nodeTanimliDersler.AppendChild(nodeTanimliDers);

                        bilesenTanimliDers tanimliDers=this.tanimliDersler[i] as bilesenTanimliDers;

                        XmlAttribute parameterId = xmlDosya.CreateAttribute("Id");
                        parameterId.Value = tanimliDers.id.ToString();
                        nodeTanimliDers.Attributes.Append(parameterId);

                        XmlAttribute parameterDers = xmlDosya.CreateAttribute("DersId");
                        parameterDers.Value = tanimliDers.ders.id.ToString();
                        nodeTanimliDers.Attributes.Append(parameterDers);

                        XmlAttribute parametreYerlesim = xmlDosya.CreateAttribute("Yerlesim");
                        parametreYerlesim.Value = tanimliDers.yerlesimStr;
                        nodeTanimliDers.Attributes.Append(parametreYerlesim);

                        string ogretmenler = "";
                        for (int j = 0; j < tanimliDers.ogretmenler.Count; j++)
                        {
                            ogretmenler += (tanimliDers.ogretmenler[j] as bilesenOgretmen).id.ToString();
                            if (j < tanimliDers.ogretmenler.Count - 1)
                                ogretmenler += ",";
                        }
                        XmlAttribute parametreOgretmenler = xmlDosya.CreateAttribute("Ogretmenler");
                        parametreOgretmenler.Value = ogretmenler;
                        nodeTanimliDers.Attributes.Append(parametreOgretmenler);

                        string derslikler = "";
                        for (int j = 0; j < tanimliDers.derslikler.Count; j++)
                        {
                            derslikler += (tanimliDers.derslikler[j] as bilesenDerslik).id.ToString();
                            if (j < tanimliDers.derslikler.Count - 1)
                                derslikler += ",";
                        }
                        XmlAttribute parametrederslikler = xmlDosya.CreateAttribute("Derslikler");
                        parametrederslikler.Value = derslikler;
                        nodeTanimliDers.Attributes.Append(parametrederslikler);

                        string siniflar = "";
                        for (int j = 0; j < tanimliDers.sinifGruplar.Count; j++)
                        {
                            string sinifId=((tanimliDers.sinifGruplar[j] as bilesenSinifGrup).sinif as bilesenSinif).id.ToString();
                            string grupId;
                            bilesenGrup grup=(tanimliDers.sinifGruplar[j] as bilesenSinifGrup).grup as bilesenGrup;
                            grupId=grup.id.ToString();

                            siniflar += sinifId + ":" + grupId;
                            if (j < tanimliDers.sinifGruplar.Count - 1)
                                siniflar += ",";
                        }
                        XmlAttribute parametresiniflar = xmlDosya.CreateAttribute("SinifGruplar");
                        parametresiniflar.Value = siniflar;
                        nodeTanimliDers.Attributes.Append(parametresiniflar);

                        if (tanimliDers.aktifYerlesim != null)
                        {
                            XmlAttribute parametreYerlesimOl = xmlDosya.CreateAttribute("YerlesimStr");
                            parametreYerlesimOl.Value = tanimliDers.aktifYerlesim.yerlesimStr;
                            nodeTanimliDers.Attributes.Append(parametreYerlesimOl);
                        }

                    }


                }
                #endregion

                byte[] strZipped = araclar.Zip(xmlDosya.OuterXml);
                File.WriteAllBytes(this.dosyaAdi, strZipped);

                Clipboard.SetText(xmlDosya.OuterXml);
            }
        }

        public bool ac()
        {
            bool herSeyYolunda = true;
            OpenFileDialog dosyaAc = new OpenFileDialog();
            dosyaAc.Filter = " (Ders Programı Dosyası)|*.dprg";
            if (dosyaAc.ShowDialog() == DialogResult.OK)
            {
                this.dosyaAdi = dosyaAc.FileName;
                XmlDocument xmlDosya = new XmlDocument();
                try
                {
                    byte[] byteBuffer = File.ReadAllBytes(this.dosyaAdi);
                    xmlDosya.LoadXml(araclar.unZip(byteBuffer));

                    #region GENEL AYARLAR
                    {
                        this.okulAdi = xmlDosya.SelectSingleNode("DersProgrami/GenelAyarlar/OkulAdi").InnerText;
                        this.okulMuduru = xmlDosya.SelectSingleNode("DersProgrami/GenelAyarlar/OkulMuduru").InnerText;
                        this.okulMudurYrd = xmlDosya.SelectSingleNode("DersProgrami/GenelAyarlar/OkulMudurYrd").InnerText;
                        if (xmlDosya.SelectSingleNode("DersProgrami/GenelAyarlar/OkulMudurYrd").Attributes["Bas"].Value.ToString() == "1") this.mudurYrdBas = true; else this.mudurYrdBas = false;

                        this.ogretimYili = xmlDosya.SelectSingleNode("DersProgrami/GenelAyarlar/OgretimYili").InnerText;
                        this.gunlukDersSaatiSayisi = Convert.ToByte(xmlDosya.SelectSingleNode("DersProgrami/GenelAyarlar/GunlukDersSaatiSayisi").InnerText);
                        this.haftalikGunSayisi = Convert.ToByte(xmlDosya.SelectSingleNode("DersProgrami/GenelAyarlar/HaftalikGunSayisi").InnerText);
                        this.haftalikGunSayisi = Convert.ToByte(xmlDosya.SelectSingleNode("DersProgrami/GenelAyarlar/HaftalikGunSayisi").InnerText);

                        XmlNodeList nodeGunlerListe = xmlDosya.SelectNodes("DersProgrami/GenelAyarlar/Gunler/Gun");
                        if (nodeGunlerListe.Count != this.haftalikGunSayisi)
                            throw new Exception();
                        this.gunler = new string[this.haftalikGunSayisi];
                        for (int i = 0; i < this.haftalikGunSayisi; i++)
                        {
                            this.gunler[i] = nodeGunlerListe[i].InnerText;
                        }

                        XmlNodeList nodeSaatlerListe = xmlDosya.SelectNodes("DersProgrami/GenelAyarlar/Saatler/Saat");
                        if (nodeSaatlerListe.Count != this.gunlukDersSaatiSayisi)
                            throw new Exception();
                        this.derssaatleri = new string[this.gunlukDersSaatiSayisi];
                        for (int i = 0; i < this.gunlukDersSaatiSayisi; i++)
                        {
                            this.derssaatleri[i] = nodeSaatlerListe[i].InnerText;
                        }

                        string kosullarStr = xmlDosya.SelectSingleNode("DersProgrami/GenelAyarlar/Kosullar").InnerText;
                        if (kosullarStr.Length != this.haftalikGunSayisi * this.gunlukDersSaatiSayisi)
                            throw new Exception();
                        this.kosullar = araclar.diziKodCoz(kosullarStr, this.haftalikGunSayisi, this.gunlukDersSaatiSayisi);
                    }
                    #endregion

                    #region DERS BİLGİLERİ
                    {
                        this.idDersSon = Convert.ToUInt16(xmlDosya.SelectSingleNode("DersProgrami/Dersler").Attributes["DerslerIdSon"].Value);

                        XmlNodeList nodeDerslerListe = xmlDosya.SelectNodes("DersProgrami/Dersler/Ders");
                        for (int i = 0; i < nodeDerslerListe.Count; i++)
                        {
                            XmlNode nodeDers = nodeDerslerListe[i];
                            ushort id = Convert.ToUInt16(nodeDers.Attributes["Id"].Value);
                            string dersAdi = nodeDers.SelectSingleNode("Adi").InnerText;
                            string dersKisaAdi = nodeDers.SelectSingleNode("KisaAdi").InnerText;
                            bool[,] kosul = araclar.diziKodCoz(nodeDers.SelectSingleNode("Kosul").InnerText, haftalikGunSayisi, gunlukDersSaatiSayisi);
                            bilesenDers ders = new bilesenDers(id, kosul, dersAdi, dersKisaAdi);
                            this.dersler.Add(ders);
                        }
                    }
                    #endregion

                    #region OGRETMEN BİLGİLERİ
                    {
                        this.idOgretmenSon = Convert.ToUInt16(xmlDosya.SelectSingleNode("DersProgrami/Ogretmenler").Attributes["OgretmenlerIdSon"].Value);

                        XmlNodeList nodeOgretmenlerListe = xmlDosya.SelectNodes("DersProgrami/Ogretmenler/Ogretmen");
                        for (int i = 0; i < nodeOgretmenlerListe.Count; i++)
                        {
                            XmlNode nodeOgretmen = nodeOgretmenlerListe[i];
                            ushort id = Convert.ToUInt16(nodeOgretmen.Attributes["Id"].Value);
                            string ogretmenAdi = nodeOgretmen.SelectSingleNode("Adi").InnerText;
                            string ogretmenKisaAdi = nodeOgretmen.SelectSingleNode("KisaAdi").InnerText;
                            bool[,] kosul = araclar.diziKodCoz(nodeOgretmen.SelectSingleNode("Kosul").InnerText, haftalikGunSayisi, gunlukDersSaatiSayisi);
                            bilesenOgretmen ders = new bilesenOgretmen(id, kosul, ogretmenAdi, ogretmenKisaAdi);
                            this.ogretmenler.Add(ders);
                        }
                    }
                    #endregion

                    #region DERSLİK BİLGİLERİ
                    {
                        this.idDerslikSon = Convert.ToUInt16(xmlDosya.SelectSingleNode("DersProgrami/Derslikler").Attributes["DersliklerIdSon"].Value);

                        XmlNodeList nodeDersliklerListe = xmlDosya.SelectNodes("DersProgrami/Derslikler/Derslik");
                        for (int i = 0; i < nodeDersliklerListe.Count; i++)
                        {
                            XmlNode nodeDerslik = nodeDersliklerListe[i];
                            ushort id = Convert.ToUInt16(nodeDerslik.Attributes["Id"].Value);
                            string derslikAdi = nodeDerslik.SelectSingleNode("Adi").InnerText;
                            string derslikKisaAdi = nodeDerslik.SelectSingleNode("KisaAdi").InnerText;
                            bool[,] kosul = araclar.diziKodCoz(nodeDerslik.SelectSingleNode("Kosul").InnerText, haftalikGunSayisi, gunlukDersSaatiSayisi);
                            bilesenDerslik ders = new bilesenDerslik(id, kosul, derslikAdi, derslikKisaAdi);
                            this.derslikler.Add(ders);
                        }
                    }
                    #endregion

                    #region SINIF BİLGİLERİ
                    {
                        this.idSinifSon = Convert.ToUInt16(xmlDosya.SelectSingleNode("DersProgrami/Siniflar").Attributes["SiniflarIdSon"].Value);

                        XmlNodeList nodeSiniflarListe = xmlDosya.SelectNodes("DersProgrami/Siniflar/Sinif");
                        for (int i = 0; i < nodeSiniflarListe.Count; i++)
                        {
                            XmlNode nodeSinif = nodeSiniflarListe[i];
                            ushort id = Convert.ToUInt16(nodeSinif.Attributes["Id"].Value);
                            ushort grupIdSon = Convert.ToUInt16(nodeSinif.Attributes["GruplarIdSon"].Value);

                            string sinifAdi = nodeSinif.SelectSingleNode("Adi").InnerText;
                            string sinifKisaAdi = nodeSinif.SelectSingleNode("KisaAdi").InnerText;
                            bool[,] kosul = araclar.diziKodCoz(nodeSinif.SelectSingleNode("Kosul").InnerText, haftalikGunSayisi, gunlukDersSaatiSayisi);
                            ArrayList gruplar = new ArrayList();

                            XmlNodeList nodeGruplarListe = nodeSinif.SelectNodes("Gruplar/Grup");
                            for (int j = 0; j < nodeGruplarListe.Count; j++)
                            {
                                XmlNode nodeGrup = nodeGruplarListe[j];
                                ushort g_id = Convert.ToUInt16(nodeGrup.Attributes["Id"].Value);
                                string g_adi = nodeGrup.SelectSingleNode("Adi").InnerText;
                                string g_kisaAdi = nodeGrup.SelectSingleNode("KisaAdi").InnerText;
                                bilesenGrup grup = new bilesenGrup(g_id, g_adi, g_kisaAdi);
                                gruplar.Add(grup);
                            }

                            bilesenSinif sinif = new bilesenSinif(id, kosul, sinifAdi, sinifKisaAdi, gruplar, grupIdSon);
                            this.siniflar.Add(sinif);
                        }
                    }
                    #endregion

                    #region TANIMLANMIŞ DERSLER
                    {
                        this.idTanimliDersSon = Convert.ToUInt16(xmlDosya.SelectSingleNode("DersProgrami/TanimliDersler").Attributes["TanimliDersIdSon"].Value);
                        this.tanimliDersler = new List<bilesenTanimliDers>();
                        XmlNodeList nodeTanimliDersListe = xmlDosya.SelectNodes("DersProgrami/TanimliDersler/TanimliDers");
                        for (int i = 0; i < nodeTanimliDersListe.Count; i++)
                        {
                            XmlNode nodeTanimliDers = nodeTanimliDersListe[i];
                            ushort id=Convert.ToUInt16(nodeTanimliDers.Attributes["Id"].Value);
                            bilesenDers ders=this.dersGetir(Convert.ToUInt16(nodeTanimliDers.Attributes["DersId"].Value));
                            string yerlesim=nodeTanimliDers.Attributes["Yerlesim"].Value;
                            
                            string ogretmenlerStr=nodeTanimliDers.Attributes["Ogretmenler"].Value;
                            List<bilesenOgretmen> ogretmenler=new List<bilesenOgretmen>();
                            string[] ogretmenlerDizi=ogretmenlerStr.Split(',');
                            for (int j = 0; j < ogretmenlerDizi.Length; j++)
                            {
                                bilesenOgretmen ogretmen = this.ogretmenGetir(Convert.ToUInt16(ogretmenlerDizi[j]));
                                ogretmenler.Add(ogretmen);
                            }
                            
                            List<bilesenDerslik> derslikler = new List<bilesenDerslik>();
                            string dersliklerStr = nodeTanimliDers.Attributes["Derslikler"].Value;

                            string[] dersliklerDizi = dersliklerStr.Split(',');
                            for (int j = 0; j < dersliklerDizi.Length; j++)
                            {
                                if (dersliklerDizi[j] == "")
                                    continue;
                                bilesenDerslik derslik = this.derslikGetir(Convert.ToUInt16(dersliklerDizi[j]));
                                derslikler.Add(derslik);
                            }
                            
                            
                            string sinifGruplarStr = nodeTanimliDers.Attributes["SinifGruplar"].Value;
                            List<bilesenSinifGrup> sinifGruplar = new List<bilesenSinifGrup>();
                            string[] sinifGruplarDizi = sinifGruplarStr.Split(',');
                            for (int j = 0; j < sinifGruplarDizi.Length; j++)
                            {
                                string[] strDizi = sinifGruplarDizi[j].Split(':');
                                bilesenSinif sinif = this.sinifGetir(Convert.ToUInt16(strDizi[0]));
                                bilesenSinifGrup sinifGrup = new bilesenSinifGrup(sinif, Convert.ToUInt16(strDizi[1]));
                                sinifGruplar.Add(sinifGrup);
                            }

                            string yerlesimYeri = "";
                            if (nodeTanimliDers.Attributes["YerlesimStr"] != null)
                            {
                                yerlesimYeri = nodeTanimliDers.Attributes["YerlesimStr"].Value;
                            }

                            bilesenTanimliDers tanimliDers = new bilesenTanimliDers(id, ders, sinifGruplar, ogretmenler, derslikler, yerlesim, this);
                            this.tanimliDersler.Add(tanimliDers);
                            tanimliDers.baslangicYerlesimi = yerlesimYeri;

                        }

                    }
                    #endregion
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Dosya okumada hata oluştu!!\n"+ex.Message);
                    herSeyYolunda = false;
                }
            }
            else
                herSeyYolunda = false;

            return herSeyYolunda;

        }

    }

}
