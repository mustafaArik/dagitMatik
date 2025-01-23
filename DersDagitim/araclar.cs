using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DersDagitim
{
    public static class araclar
    {
        public static bool diziKesisiyormu(bool[,] dizi1, bool[,] dizi2)
        {
            bool kesisiyor = false;
            for(int i=0;i<dizi1.GetLength(0);i++)
                for (int j = 0; j < dizi1.GetLength(1); j++)
                {
                    if (!dizi1[i, j] && !dizi2[i, j])
                    {
                        kesisiyor = true;
                        break;
                    }

                    if (kesisiyor)
                        break;
                }


            return kesisiyor;

        }

        public static bool[,] diziKopyala(bool[,] kopyalanacak)
        {
            int gen=kopyalanacak.GetLength(0);
            int yuk=kopyalanacak.GetLength(1);

            bool[,] kopya = new bool[gen,yuk];

            for (int i = 0; i < gen; i++)
                for (int j = 0; j < yuk; j++)
                    kopya[i, j] = kopyalanacak[i, j];
            return kopya;
        }

        public static bool[,] diziBirlestir(bool[,] dizi1,bool[,] dizi2)
        {
            bool[,] gonder = diziOlustur();

            for(int i=0;i<dizi2.GetLength(0);i++)
                for (int j = 0; j < dizi2.GetLength(1); j++)
                {
                    gonder[i, j] = dizi2[i, j] && dizi1[i, j];
                }

            return gonder;
        }

        public static bool[,] diziEkle(bool[,] dizi1, bool[,] dizi2)
        {
            bool[,] gonder = diziOlustur();

            for (int i = 0; i < dizi2.GetLength(0); i++)
                for (int j = 0; j < dizi2.GetLength(1); j++)
                {
                    gonder[i, j] = dizi2[i, j] || dizi1[i, j];
                }

            return gonder;
        }

        public static void diziKopyala(ref bool[,] hedef, bool[,] kaynak)
        {
            int gen = kaynak.GetLength(0);
            int yuk = kaynak.GetLength(1);
            for (int i = 0; i < gen; i++)
                for (int j = 0; j < yuk; j++)
                    if (i < hedef.GetLength(0) && j < hedef.GetLength(1))
                        hedef[i, j] = kaynak[i, j];

        }

        public static string diziKodla(bool[,] dizi)
        {
            int gen = dizi.GetLength(0);
            int yuk = dizi.GetLength(1);
            string kod="";
            for (int i = 0; i < gen; i++)
                for (int j = 0; j < yuk; j++)
                    if (dizi[i, j])
                        kod += "1";
                    else
                        kod += "0";
            return kod;
        }

        public static bool[,] diziKodCoz(string str,int gen,int yuk)
        {
            bool[,] cozum = new bool[gen, yuk];
            int say=0;
            for(int i=0;i<gen;i++)
                for (int j = 0; j < yuk; j++)
                {
                    if (str[say++] == '1')
                        cozum[i, j] = true;
                    else
                        cozum[i, j] = false;
                }
            return cozum;


        }

        public static bool[,] diziOlustur(bool durum=true)
        {
            bool[,] dizi = new bool[tanim.program.haftalikGunSayisi, tanim.program.gunlukDersSaatiSayisi];

            for (int i = 0; i < dizi.GetLength(0); i++)
                for (int j = 0; j < dizi.GetLength(1); j++)
                    dizi[i, j] = durum;
            return dizi;
        }

        public static Bitmap kosulResim(bool[,] dizi, bool buyukResim=false)
        {
            const int GEN=100; const int YUK=40;
            double hGen = (GEN-1) / dizi.GetLength(1);
            double hYuk = (YUK-1) / dizi.GetLength(0);

            Bitmap bitmap = new Bitmap(GEN,YUK);
            Graphics gr = Graphics.FromImage(bitmap);

            for (int i = 0; i < dizi.GetLength(1); i++)
                for (int j = 0; j < dizi.GetLength(0); j++)
                {
                    if (dizi[j, i])
                        gr.FillRectangle(new SolidBrush(Color.Green), new Rectangle((int)(i * hGen), (int)(j * hYuk), (int)hGen, (int)hYuk));
                    else
                        gr.FillRectangle(new SolidBrush(Color.Red), new Rectangle((int)(i * hGen), (int)(j * hYuk), (int)hGen, (int)hYuk));
                    gr.DrawRectangle(new Pen(Color.White), new Rectangle((int)(i * hGen), (int)(j * hYuk), (int)hGen, (int)hYuk));
                }
            if (buyukResim)
                bitmap = resimBuyult(bitmap);
            return bitmap;
        }

        private static Bitmap resimBuyult(Bitmap sourceBMP)
        {
            int width = sourceBMP.Width * 2;
            int height = sourceBMP.Height * 2;
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }

        public static void marioMelodiCal()
        {
            Console.Beep(2250, 200);
        }

        public static Bitmap dersProgramiCizelgesi(bilesenTaban bilesen)
        {
            const int SAATLERGEN = 30;
            const int GUNLERYUK = 30;
            const int GUNLERGEN = 120;
            const int SAATLERYUK = 70;

            const int BAS_X = SAATLERGEN;
            const int BAS_Y = GUNLERYUK;

            int gunSay = tanim.program.haftalikGunSayisi;
            int saatSay = tanim.program.gunlukDersSaatiSayisi;

            
            Bitmap bitmap = new Bitmap(BAS_X + GUNLERGEN * gunSay + 1, BAS_Y + SAATLERYUK * saatSay + 1);
            Graphics gr = Graphics.FromImage(bitmap);

            #region GRAPHİCS NESNELERİ
            Pen penKalin = new Pen(new SolidBrush(Color.Black), 1.5f);
            Pen penInceNoktali = new Pen(new SolidBrush(Color.Gray), 1.5f); penInceNoktali.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            Font fontNormal7P = new Font("Arial", 7, FontStyle.Regular);
            Font fontKalin10P = new Font("Arial", 10, FontStyle.Bold);
            Font fontNormal9P = new Font("Arial", 9, FontStyle.Regular);
            Font fontKalin9P = new Font("Arial", 9, FontStyle.Bold);
            Font fontKalin8P=new Font("Arial", 8.5f, FontStyle.Bold);
           

            SolidBrush brushNormal=new SolidBrush(Color.Black);
            StringFormat strFormatOrtaOrta=new StringFormat();  strFormatOrtaOrta.Alignment=StringAlignment.Center; strFormatOrtaOrta.LineAlignment=StringAlignment.Center;
            StringFormat strFormatSolUst = new StringFormat();
            StringFormat strFormatSagAlt = new StringFormat();  strFormatSagAlt.LineAlignment = StringAlignment.Far; strFormatSagAlt.Alignment = StringAlignment.Far;
            StringFormat strFormatSolAlt = new StringFormat();  strFormatSolAlt.LineAlignment = StringAlignment.Far; strFormatSolAlt.Alignment = StringAlignment.Near;

            StringFormat strFormatOrtaOrtaDik = new StringFormat(); strFormatOrtaOrtaDik.Alignment = StringAlignment.Center; strFormatOrtaOrtaDik.LineAlignment = StringAlignment.Center; strFormatOrtaOrtaDik.FormatFlags= StringFormatFlags.DirectionVertical;

            #endregion

            //GÜNLER VE SAATLER DİKDÖRTGENLERİ
            Rectangle recBitmap=new Rectangle(0,0,bitmap.Width,bitmap.Height);

            Rectangle[] recGunler = new Rectangle[gunSay];
            Rectangle[] recSaatler = new Rectangle[saatSay];
            Rectangle[,] recProgram = new Rectangle[gunSay, saatSay];

            List<Rectangle> recTumu = new List<Rectangle>();
            for (int i = 0; i < gunSay; i++)
                recGunler[i] = new Rectangle(BAS_X + i * GUNLERGEN, 0,GUNLERGEN , GUNLERYUK);

            for (int i = 0; i < saatSay; i++)
                recSaatler[i] = new Rectangle(0, BAS_Y + i * SAATLERYUK, SAATLERGEN, SAATLERYUK);

            for (int i = 0; i < gunSay; i++)
                for (int j = 0; j < saatSay; j++)
                {
                    recProgram[i, j] = new Rectangle(BAS_X + i * GUNLERGEN, BAS_Y + j * SAATLERYUK, GUNLERGEN, SAATLERYUK);
                    recTumu.Add(recProgram[i, j]);
                }


            recTumu.AddRange(recGunler);
            recTumu.AddRange(recSaatler);

            gr.FillRectangle(new SolidBrush(Color.White), recBitmap);

            foreach (Rectangle rec in recTumu)
                gr.DrawRectangle(new Pen(Color.Black), rec);
            
            //DOLDURMA İŞLEMLERİ


            for (int i = 0; i < recSaatler.Length; i++)
            {
                gr.TranslateTransform(recSaatler[i].X,recSaatler[i].Height+recSaatler[i].Y); 
                gr.RotateTransform(270);

                Rectangle recDon = new Rectangle(0, 0, recSaatler[i].Height, recSaatler[i].Width);
                string yazilacak=(i+1).ToString()+"\n"+tanim.program.derssaatleri[i];
                //var textSize = TextRenderer.MeasureText(yazilacak, fontKalin8P);

                gr.DrawString(yazilacak, fontKalin8P, brushNormal, recDon, strFormatOrtaOrta);
                
                gr.ResetTransform();
            }
            

            for (int i = 0; i < recGunler.Length; i++)
                gr.DrawString(tanim.program.gunler[i], fontKalin10P, brushNormal, recGunler[i], strFormatOrtaOrta);


            #region ÖĞRETMEN DERS PROGRAMI
            if (bilesen is bilesenOgretmen)
            {
                bilesenOgretmen ogretmen = bilesen as bilesenOgretmen;
                List<bilesenTanimliDers> tumTanimiDersler = new List<bilesenTanimliDers>();

                List<dersYerlesim> yerlesimler = new List<dersYerlesim>();

                foreach (bilesenTanimliDers tanimliDers in tanim.program.tanimliDersler)
                {
                    if (tanimliDers.aktifYerlesim == null)
                    {
                        continue;
                    }
                    foreach (bilesenOgretmen ogretmenTD in tanimliDers.ogretmenler)
                    {
                        if (ogretmenTD == ogretmen)
                        {
                            for (int i = 0; i < tanimliDers.nodes.Length; i++)
                            {
                                for (int j = 0; j < tanimliDers.nodes[i].tSaat; j++)
                                {
                                    dersYerlesim yerlesim = new dersYerlesim();
                                    yerlesim.gun = tanimliDers.nodes[i].yerlesimGun;
                                    yerlesim.saat = tanimliDers.nodes[i].yerlesimSaat + j;

                                    yerlesim.dersAdi = tanimliDers.ders.adi;
                                    foreach (bilesenDerslik derslik in tanimliDers.derslikler)
                                        yerlesim.derslikler += derslik.kisaAdi + " ";

                                    foreach (bilesenSinifGrup sinifGrup in tanimliDers.sinifGruplar)
                                    {
                                        if (sinifGrup.grup.id != 0)
                                            yerlesim.sinifGruplar += sinifGrup.sinif.kisaAdi + "-" + sinifGrup.grup.kisaAdi + " ";
                                        else
                                            yerlesim.sinifGruplar += sinifGrup.sinif.kisaAdi + " ";
                                    }
                                    yerlesimler.Add(yerlesim);
                                }

                            }
                        }
                    }
                }

                for (int i = 0; i < tanim.program.haftalikGunSayisi; i++)
                    for (int j = 0; j < tanim.program.gunlukDersSaatiSayisi; j++)
                    {
                        List<dersYerlesim> topYerlesim = new List<dersYerlesim>();
                        foreach (dersYerlesim yerlesim in yerlesimler)
                            if (yerlesim.gun == i && yerlesim.saat == j)
                                topYerlesim.Add(yerlesim);

                        int toplam = topYerlesim.Count;
                        Rectangle rec = recProgram[i, j];
                        Rectangle[] grupRec = new Rectangle[toplam];


                        Font fontNormal = new Font("Arial", 8 - toplam, FontStyle.Regular);
                        Font fontKalin = new Font("Arial", 10 - toplam, FontStyle.Bold);



                        for (int ii = 0; ii < topYerlesim.Count; ii++)
                        {
                            grupRec[ii] = new Rectangle(rec.X, rec.Y + ii * rec.Height / toplam, rec.Width, rec.Height / toplam);
                            gr.DrawRectangle(penInceNoktali, grupRec[ii]);
                            gr.DrawString(topYerlesim[ii].dersAdi, fontNormal, brushNormal, grupRec[ii], strFormatSolUst);
                            gr.DrawString(topYerlesim[ii].sinifGruplar, fontKalin, brushNormal, grupRec[ii], strFormatOrtaOrta);
                            //gr.DrawString(topYerlesim[ii].ogretmenler, fontNormal, brushNormal, grupRec[ii], strFormatSolAlt);
                            gr.DrawString(topYerlesim[ii].derslikler, fontNormal, brushNormal, grupRec[ii], strFormatSagAlt);
                        }
                    }
            }

            #endregion









            #region SINIF DERS PROGRAMI
            if (bilesen is bilesenSinif)
            {
                bilesenSinif sinif = bilesen as bilesenSinif;
                List<bilesenTanimliDers> tumTanimiDersler = new List<bilesenTanimliDers>();

                List<dersYerlesim> yerlesimler = new List<dersYerlesim>();

                foreach (bilesenTanimliDers tanimliDers in tanim.program.tanimliDersler)
                {
                    if (tanimliDers.aktifYerlesim == null)
                    {
                        continue;
                    }
                    foreach (bilesenSinifGrup sinifGrup in tanimliDers.sinifGruplar)
                    {
                        if (sinifGrup.sinif == sinif)
                        {
                            for (int i = 0; i < tanimliDers.nodes.Length; i++)
                            {
                                for (int j = 0; j < tanimliDers.nodes[i].tSaat; j++)
                                {
                                    dersYerlesim yerlesim = new dersYerlesim();
                                    yerlesim.gun = tanimliDers.nodes[i].yerlesimGun;
                                    yerlesim.saat = tanimliDers.nodes[i].yerlesimSaat + j;
                                    if (sinifGrup.grup.id != 0)
                                    {
                                        yerlesim.grupAdi = sinifGrup.grup.kisaAdi;
                                        yerlesim.dersAdi = tanimliDers.ders.kisaAdi;
                                    }
                                    else
                                        yerlesim.dersAdi = tanimliDers.ders.adi;
                                    foreach (bilesenOgretmen ogr in tanimliDers.ogretmenler)
                                        yerlesim.ogretmenler += ogr.kisaAdi + " ";
                                    foreach (bilesenDerslik derslik in tanimliDers.derslikler)
                                        yerlesim.derslikler += derslik.kisaAdi + " ";
                                    yerlesimler.Add(yerlesim);
                                }

                            }
                        }
                    }
                }

                for(int i=0;i<tanim.program.haftalikGunSayisi;i++)
                    for (int j = 0; j < tanim.program.gunlukDersSaatiSayisi; j++)
                    {
                        List<dersYerlesim> topYerlesim = new List<dersYerlesim>();
                        foreach (dersYerlesim yerlesim in yerlesimler)
                            if (yerlesim.gun == i && yerlesim.saat == j)
                                topYerlesim.Add(yerlesim);

                        int toplam = topYerlesim.Count;
                        Rectangle rec = recProgram[i, j];
                        Rectangle[] grupRec = new Rectangle[toplam];


                        Font fontNormal = new Font("Arial", 8-toplam, FontStyle.Regular);
                        Font fontKalin = new Font("Arial", 10-toplam, FontStyle.Bold);
                        
                        

                        for (int ii = 0; ii < topYerlesim.Count; ii++)
                        {
                            grupRec[ii] = new Rectangle(rec.X, rec.Y + ii * rec.Height / toplam, rec.Width, rec.Height / toplam);
                            gr.DrawRectangle(penInceNoktali, grupRec[ii]);
                            gr.DrawString(topYerlesim[ii].grupAdi, fontNormal, brushNormal, grupRec[ii], strFormatSolUst);
                            gr.DrawString(topYerlesim[ii].dersAdi, fontKalin, brushNormal, grupRec[ii], strFormatOrtaOrta);
                            gr.DrawString(topYerlesim[ii].ogretmenler, fontNormal, brushNormal, grupRec[ii], strFormatSolAlt);
                            gr.DrawString(topYerlesim[ii].derslikler, fontNormal, brushNormal, grupRec[ii], strFormatSagAlt);
                        }
                    }
            }
            #endregion


            #region DERSLİK DERS PROGRAMI
            if (bilesen is bilesenDerslik)
            {
                bilesenDerslik derslik = bilesen as bilesenDerslik;
                List<bilesenTanimliDers> tumTanimiDersler = new List<bilesenTanimliDers>();

                List<dersYerlesim> yerlesimler = new List<dersYerlesim>();

                foreach (bilesenTanimliDers tanimliDers in tanim.program.tanimliDersler)
                {
                    if (tanimliDers.aktifYerlesim == null)
                    {
                        continue;
                    }
                    foreach (bilesenDerslik derslikTD in tanimliDers.derslikler)
                    {
                        if (derslikTD == derslik)
                        {
                            for (int i = 0; i < tanimliDers.nodes.Length; i++)
                            {
                                for (int j = 0; j < tanimliDers.nodes[i].tSaat; j++)
                                {
                                    dersYerlesim yerlesim = new dersYerlesim();
                                    yerlesim.gun = tanimliDers.nodes[i].yerlesimGun;
                                    yerlesim.saat = tanimliDers.nodes[i].yerlesimSaat + j;

                                    yerlesim.dersAdi = tanimliDers.ders.adi;
                                    foreach (bilesenOgretmen ogr in tanimliDers.ogretmenler)
                                        yerlesim.ogretmenler += ogr.kisaAdi + " ";

                                    foreach (bilesenSinifGrup sinifGrup in tanimliDers.sinifGruplar)
                                    {

                                        if (sinifGrup.grup.id != 0)
                                            yerlesim.sinifGruplar += sinifGrup.sinif.kisaAdi + "-" + sinifGrup.grup.kisaAdi + " ";
                                        else
                                            yerlesim.sinifGruplar += sinifGrup.sinif.kisaAdi + " ";

                                    }

                                    yerlesimler.Add(yerlesim);
                                }

                            }
                        }
                    }
                }

                for (int i = 0; i < tanim.program.haftalikGunSayisi; i++)
                    for (int j = 0; j < tanim.program.gunlukDersSaatiSayisi; j++)
                    {
                        List<dersYerlesim> topYerlesim = new List<dersYerlesim>();
                        foreach (dersYerlesim yerlesim in yerlesimler)
                            if (yerlesim.gun == i && yerlesim.saat == j)
                                topYerlesim.Add(yerlesim);

                        int toplam = topYerlesim.Count;
                        Rectangle rec = recProgram[i, j];
                        Rectangle[] grupRec = new Rectangle[toplam];


                        Font fontNormal = new Font("Arial", 8 - toplam, FontStyle.Regular);
                        Font fontKalin = new Font("Arial", 10 - toplam, FontStyle.Bold);



                        for (int ii = 0; ii < topYerlesim.Count; ii++)
                        {
                            grupRec[ii] = new Rectangle(rec.X, rec.Y + ii * rec.Height / toplam, rec.Width, rec.Height / toplam);
                            gr.DrawRectangle(penInceNoktali, grupRec[ii]);
                            gr.DrawString(topYerlesim[ii].sinifGruplar, fontNormal, brushNormal, grupRec[ii], strFormatSolUst);
                            gr.DrawString(topYerlesim[ii].dersAdi, fontKalin, brushNormal, grupRec[ii], strFormatOrtaOrta);
                            gr.DrawString(topYerlesim[ii].ogretmenler, fontNormal, brushNormal, grupRec[ii], strFormatSolAlt);
                            //gr.DrawString(topYerlesim[ii].derslikler, fontNormal, brushNormal, grupRec[ii], strFormatSagAlt);
                        }
                    }
            }

            #endregion


            string yerlesmeyenDersListesi = "";
            foreach (bilesenTanimliDers tanimliDers in tanim.program.tanimliDersler)
            {
                if (tanimliDers.aktifYerlesim == null)
                {
                    yerlesmeyenDersListesi += "["+tanimliDers.aciklama + "]";
                    continue;
                }
            }


            if (yerlesmeyenDersListesi != "")
                MessageBox.Show("Aşağıdaki Dersler Yerleşmemiştir!!\n" + yerlesmeyenDersListesi);

            foreach (Rectangle rec in recTumu)
                gr.DrawRectangle(new Pen(Color.Black), rec);

            return bitmap;

        }

        public static void dersProgramiCizelgesi(bilesenTaban bilesen, Graphics gr,int xBOS,int YBOS)
        {
            const int SAATLERGEN = 30;
            const int GUNLERYUK = 30;
            const int GUNLERGEN = 120;
            const int SAATLERYUK = 60;

            const int BAS_X = SAATLERGEN;
            const int BAS_Y = GUNLERYUK;

            int gunSay = tanim.program.haftalikGunSayisi;
            int saatSay = tanim.program.gunlukDersSaatiSayisi;

            gr.TranslateTransform(xBOS,YBOS);

            //Bitmap bitmap = new Bitmap(BAS_X + GUNLERGEN * gunSay + 1, BAS_Y + SAATLERYUK * saatSay + 1);
            //Graphics gr = Graphics.FromImage(bitmap);

            #region GRAPHİCS NESNELERİ
            Pen penKalin = new Pen(new SolidBrush(Color.Black), 1.5f);
            Pen penInceNoktali = new Pen(new SolidBrush(Color.Gray), 0.7f); penInceNoktali.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            Font fontNormal7P = new Font("Arial", 7, FontStyle.Regular);
            Font fontKalin10P = new Font("Arial", 10, FontStyle.Bold);
            Font fontNormal9P = new Font("Arial", 9, FontStyle.Regular);
            Font fontKalin9P = new Font("Arial", 9, FontStyle.Bold);
            Font fontKalin8P = new Font("Arial", 7f, FontStyle.Bold);


            SolidBrush brushNormal = new SolidBrush(Color.Black);
            StringFormat strFormatOrtaOrta = new StringFormat(); strFormatOrtaOrta.Alignment = StringAlignment.Center; strFormatOrtaOrta.LineAlignment = StringAlignment.Center;
            StringFormat strFormatSolUst = new StringFormat();
            StringFormat strFormatSagAlt = new StringFormat(); strFormatSagAlt.LineAlignment = StringAlignment.Far; strFormatSagAlt.Alignment = StringAlignment.Far;
            StringFormat strFormatSolAlt = new StringFormat(); strFormatSolAlt.LineAlignment = StringAlignment.Far; strFormatSolAlt.Alignment = StringAlignment.Near;

            StringFormat strFormatOrtaOrtaDik = new StringFormat(); strFormatOrtaOrtaDik.Alignment = StringAlignment.Center; strFormatOrtaOrtaDik.LineAlignment = StringAlignment.Center; strFormatOrtaOrtaDik.FormatFlags = StringFormatFlags.DirectionVertical;


            #endregion

            //GÜNLER VE SAATLER DİKDÖRTGENLERİ
            Rectangle recBitmap = new Rectangle(0, 0, BAS_X + GUNLERGEN * gunSay + 1, BAS_Y + SAATLERYUK * saatSay + 1);

            Rectangle[] recGunler = new Rectangle[gunSay];
            Rectangle[] recSaatler = new Rectangle[saatSay];
            Rectangle[,] recProgram = new Rectangle[gunSay, saatSay];

            List<Rectangle> recTumu = new List<Rectangle>();
            for (int i = 0; i < gunSay; i++)
                recGunler[i] = new Rectangle(BAS_X + i * GUNLERGEN, 0, GUNLERGEN, GUNLERYUK);

            for (int i = 0; i < saatSay; i++)
                recSaatler[i] = new Rectangle(0, BAS_Y + i * SAATLERYUK, SAATLERGEN, SAATLERYUK);

            for (int i = 0; i < gunSay; i++)
                for (int j = 0; j < saatSay; j++)
                {
                    recProgram[i, j] = new Rectangle(BAS_X + i * GUNLERGEN, BAS_Y + j * SAATLERYUK, GUNLERGEN, SAATLERYUK);
                    recTumu.Add(recProgram[i, j]);
                }


            recTumu.AddRange(recGunler);
            recTumu.AddRange(recSaatler);

            gr.FillRectangle(new SolidBrush(Color.White), recBitmap);

            foreach (Rectangle rec in recTumu)
                gr.DrawRectangle(new Pen(Color.Black), rec);

            //DOLDURMA İŞLEMLERİ


            for (int i = 0; i < recSaatler.Length; i++)
            {
                gr.TranslateTransform(recSaatler[i].X, recSaatler[i].Height + recSaatler[i].Y);
                gr.RotateTransform(270);

                Rectangle recDon = new Rectangle(0, 0, recSaatler[i].Height, recSaatler[i].Width);
                string yazilacak = (i + 1).ToString() + "\n" + tanim.program.derssaatleri[i];
                gr.DrawString(yazilacak, fontKalin8P, brushNormal, recDon, strFormatOrtaOrta);
                gr.ResetTransform();
                gr.TranslateTransform(xBOS, YBOS);
            }




            for (int i = 0; i < recGunler.Length; i++)
                gr.DrawString(tanim.program.gunler[i], fontKalin10P, brushNormal, recGunler[i], strFormatOrtaOrta);


            #region ÖĞRETMEN DERS PROGRAMI
            if (bilesen is bilesenOgretmen)
            {
                bilesenOgretmen ogretmen = bilesen as bilesenOgretmen;
                List<bilesenTanimliDers> tumTanimiDersler = new List<bilesenTanimliDers>();

                List<dersYerlesim> yerlesimler = new List<dersYerlesim>();

                foreach (bilesenTanimliDers tanimliDers in tanim.program.tanimliDersler)
                {
                    if (tanimliDers.aktifYerlesim == null)
                    {
                        continue;
                    }
                    foreach (bilesenOgretmen ogretmenTD in tanimliDers.ogretmenler)
                    {
                        if (ogretmenTD == ogretmen)
                        {
                            for (int i = 0; i < tanimliDers.nodes.Length; i++)
                            {
                                for (int j = 0; j < tanimliDers.nodes[i].tSaat; j++)
                                {
                                    dersYerlesim yerlesim = new dersYerlesim();
                                    yerlesim.gun = tanimliDers.nodes[i].yerlesimGun;
                                    yerlesim.saat = tanimliDers.nodes[i].yerlesimSaat + j;

                                    yerlesim.dersAdi = tanimliDers.ders.adi;
                                    foreach (bilesenDerslik derslik in tanimliDers.derslikler)
                                        yerlesim.derslikler += derslik.kisaAdi + " ";

                                    foreach (bilesenSinifGrup sinifGrup in tanimliDers.sinifGruplar)
                                    {
                                        if (sinifGrup.grup.id != 0)
                                            yerlesim.sinifGruplar += sinifGrup.sinif.kisaAdi + "-" + sinifGrup.grup.kisaAdi + " ";
                                        else
                                            yerlesim.sinifGruplar += sinifGrup.sinif.kisaAdi + " ";
                                    }
                                    yerlesimler.Add(yerlesim);
                                }

                            }
                        }
                    }
                }

                for (int i = 0; i < tanim.program.haftalikGunSayisi; i++)
                    for (int j = 0; j < tanim.program.gunlukDersSaatiSayisi; j++)
                    {
                        List<dersYerlesim> topYerlesim = new List<dersYerlesim>();
                        foreach (dersYerlesim yerlesim in yerlesimler)
                            if (yerlesim.gun == i && yerlesim.saat == j)
                                topYerlesim.Add(yerlesim);

                        int toplam = topYerlesim.Count;
                        Rectangle rec = recProgram[i, j];
                        Rectangle[] grupRec = new Rectangle[toplam];


                        Font fontNormal = new Font("Arial", 8 - toplam, FontStyle.Regular);
                        Font fontKalin = new Font("Arial", 10 - toplam, FontStyle.Bold);



                        for (int ii = 0; ii < topYerlesim.Count; ii++)
                        {
                            grupRec[ii] = new Rectangle(rec.X, rec.Y + ii * rec.Height / toplam, rec.Width, rec.Height / toplam);
                            gr.DrawRectangle(penInceNoktali, grupRec[ii]);
                            gr.DrawString(topYerlesim[ii].dersAdi, fontNormal, brushNormal, grupRec[ii], strFormatSolUst);
                            gr.DrawString(topYerlesim[ii].sinifGruplar, fontKalin, brushNormal, grupRec[ii], strFormatOrtaOrta);
                            //gr.DrawString(topYerlesim[ii].ogretmenler, fontNormal, brushNormal, grupRec[ii], strFormatSolAlt);
                            gr.DrawString(topYerlesim[ii].derslikler, fontNormal, brushNormal, grupRec[ii], strFormatSagAlt);
                        }
                    }
            }

            #endregion









            #region SINIF DERS PROGRAMI
            if (bilesen is bilesenSinif)
            {
                bilesenSinif sinif = bilesen as bilesenSinif;
                List<bilesenTanimliDers> tumTanimiDersler = new List<bilesenTanimliDers>();

                List<dersYerlesim> yerlesimler = new List<dersYerlesim>();

                foreach (bilesenTanimliDers tanimliDers in tanim.program.tanimliDersler)
                {
                    if (tanimliDers.aktifYerlesim == null)
                    {
                        continue;
                    }
                    foreach (bilesenSinifGrup sinifGrup in tanimliDers.sinifGruplar)
                    {
                        if (sinifGrup.sinif == sinif)
                        {
                            for (int i = 0; i < tanimliDers.nodes.Length; i++)
                            {
                                for (int j = 0; j < tanimliDers.nodes[i].tSaat; j++)
                                {
                                    dersYerlesim yerlesim = new dersYerlesim();
                                    yerlesim.gun = tanimliDers.nodes[i].yerlesimGun;
                                    yerlesim.saat = tanimliDers.nodes[i].yerlesimSaat + j;
                                    if (sinifGrup.grup.id != 0)
                                    {
                                        yerlesim.grupAdi = sinifGrup.grup.kisaAdi;
                                        yerlesim.dersAdi = tanimliDers.ders.kisaAdi;
                                    }
                                    else
                                        yerlesim.dersAdi = tanimliDers.ders.adi;
                                    foreach (bilesenOgretmen ogr in tanimliDers.ogretmenler)
                                        yerlesim.ogretmenler += ogr.kisaAdi + " ";
                                    foreach (bilesenDerslik derslik in tanimliDers.derslikler)
                                        yerlesim.derslikler += derslik.kisaAdi + " ";
                                    yerlesimler.Add(yerlesim);
                                }

                            }
                        }
                    }
                }

                for (int i = 0; i < tanim.program.haftalikGunSayisi; i++)
                    for (int j = 0; j < tanim.program.gunlukDersSaatiSayisi; j++)
                    {
                        List<dersYerlesim> topYerlesim = new List<dersYerlesim>();
                        foreach (dersYerlesim yerlesim in yerlesimler)
                            if (yerlesim.gun == i && yerlesim.saat == j)
                                topYerlesim.Add(yerlesim);

                        int toplam = topYerlesim.Count;
                        Rectangle rec = recProgram[i, j];
                        Rectangle[] grupRec = new Rectangle[toplam];


                        Font fontNormal = new Font("Arial", 8 - toplam, FontStyle.Regular);
                        Font fontKalin = new Font("Arial", 10 - toplam, FontStyle.Bold);



                        for (int ii = 0; ii < topYerlesim.Count; ii++)
                        {
                            grupRec[ii] = new Rectangle(rec.X, rec.Y + ii * rec.Height / toplam, rec.Width, rec.Height / toplam);
                            gr.DrawRectangle(penInceNoktali, grupRec[ii]);
                            gr.DrawString(topYerlesim[ii].grupAdi, fontNormal, brushNormal, grupRec[ii], strFormatSolUst);
                            gr.DrawString(topYerlesim[ii].dersAdi, fontKalin, brushNormal, grupRec[ii], strFormatOrtaOrta);
                            gr.DrawString(topYerlesim[ii].ogretmenler, fontNormal, brushNormal, grupRec[ii], strFormatSolAlt);
                            gr.DrawString(topYerlesim[ii].derslikler, fontNormal, brushNormal, grupRec[ii], strFormatSagAlt);
                        }
                    }
            }
            #endregion


            #region DERSLİK DERS PROGRAMI
            if (bilesen is bilesenDerslik)
            {
                bilesenDerslik derslik = bilesen as bilesenDerslik;
                List<bilesenTanimliDers> tumTanimiDersler = new List<bilesenTanimliDers>();

                List<dersYerlesim> yerlesimler = new List<dersYerlesim>();

                foreach (bilesenTanimliDers tanimliDers in tanim.program.tanimliDersler)
                {
                    if (tanimliDers.aktifYerlesim == null)
                    {
                        continue;
                    }
                    foreach (bilesenDerslik derslikTD in tanimliDers.derslikler)
                    {
                        if (derslikTD == derslik)
                        {
                            for (int i = 0; i < tanimliDers.nodes.Length; i++)
                            {
                                for (int j = 0; j < tanimliDers.nodes[i].tSaat; j++)
                                {
                                    dersYerlesim yerlesim = new dersYerlesim();
                                    yerlesim.gun = tanimliDers.nodes[i].yerlesimGun;
                                    yerlesim.saat = tanimliDers.nodes[i].yerlesimSaat + j;

                                    yerlesim.dersAdi = tanimliDers.ders.adi;
                                    foreach (bilesenOgretmen ogr in tanimliDers.ogretmenler)
                                        yerlesim.ogretmenler += ogr.kisaAdi + " ";

                                    foreach (bilesenSinifGrup sinifGrup in tanimliDers.sinifGruplar)
                                    {

                                        if (sinifGrup.grup.id != 0)
                                            yerlesim.sinifGruplar += sinifGrup.sinif.kisaAdi + "-" + sinifGrup.grup.kisaAdi + " ";
                                        else
                                            yerlesim.sinifGruplar += sinifGrup.sinif.kisaAdi + " ";

                                    }

                                    yerlesimler.Add(yerlesim);
                                }

                            }
                        }
                    }
                }

                for (int i = 0; i < tanim.program.haftalikGunSayisi; i++)
                    for (int j = 0; j < tanim.program.gunlukDersSaatiSayisi; j++)
                    {
                        List<dersYerlesim> topYerlesim = new List<dersYerlesim>();
                        foreach (dersYerlesim yerlesim in yerlesimler)
                            if (yerlesim.gun == i && yerlesim.saat == j)
                                topYerlesim.Add(yerlesim);

                        int toplam = topYerlesim.Count;
                        Rectangle rec = recProgram[i, j];
                        Rectangle[] grupRec = new Rectangle[toplam];


                        Font fontNormal = new Font("Arial", 8 - toplam, FontStyle.Regular);
                        Font fontKalin = new Font("Arial", 10 - toplam, FontStyle.Bold);



                        for (int ii = 0; ii < topYerlesim.Count; ii++)
                        {
                            grupRec[ii] = new Rectangle(rec.X, rec.Y + ii * rec.Height / toplam, rec.Width, rec.Height / toplam);
                            gr.DrawRectangle(penInceNoktali, grupRec[ii]);
                            gr.DrawString(topYerlesim[ii].sinifGruplar, fontNormal, brushNormal, grupRec[ii], strFormatSolUst);
                            gr.DrawString(topYerlesim[ii].dersAdi, fontKalin, brushNormal, grupRec[ii], strFormatOrtaOrta);
                            gr.DrawString(topYerlesim[ii].ogretmenler, fontNormal, brushNormal, grupRec[ii], strFormatSolAlt);
                            //gr.DrawString(topYerlesim[ii].derslikler, fontNormal, brushNormal, grupRec[ii], strFormatSagAlt);
                        }
                    }
            }

            #endregion


            string yerlesmeyenDersListesi = "";
            foreach (bilesenTanimliDers tanimliDers in tanim.program.tanimliDersler)
            {
                if (tanimliDers.aktifYerlesim == null)
                {
                    yerlesmeyenDersListesi += "[" + tanimliDers.aciklama + "]";
                    continue;
                }
            }

            foreach (Rectangle rec in recTumu)
                gr.DrawRectangle(new Pen(Color.Black), rec);


            gr.ResetTransform();



        }









        class dersYerlesim
        {
            public int gun;
            public int saat;
            public string dersAdi="";
            public string ogretmenler="";
            public string derslikler="";
            public string sinifGruplar = "";
            public string grupAdi="";
        }


        public static bool catalmi(bilesenTanimliDers d1, bilesenTanimliDers d2)
        {
            foreach (bilesenOgretmen ogr1 in d1.ogretmenler)
                foreach (bilesenOgretmen ogr2 in d2.ogretmenler)
                    if (ogr1 == ogr2)
                        return false;
            foreach (bilesenDerslik derslik1 in d1.derslikler)
                foreach (bilesenDerslik derslik2 in d2.derslikler)
                    if (derslik1 == derslik2)
                        return false;

            foreach (bilesenSinifGrup sinifgrup1 in d1.sinifGruplar)
                foreach (bilesenSinifGrup sinifgrup2 in d2.sinifGruplar)
                    if (sinifgrup1.sinif == sinifgrup2.sinif && sinifgrup1.grup == sinifgrup2.grup)
                        return false;
            return true;
        }


        # region DOSYA SIKIŞTIRMA

        public static byte[] Zip(string str)
        {
            byte[] raw = GetBytes(str);
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }
                return memory.ToArray();
            }
        }

        public static string unZip(byte[] gzip)
        {
            // Create a GZIP stream with decompression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return GetString(memory.ToArray());
                }
            }
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        #endregion

    }


}