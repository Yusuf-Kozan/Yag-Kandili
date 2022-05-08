/*
Copyright (C) 2022 Yusuf Kozan

---

Bu belge Yağ Kandili'nin bir parçasıdır.

Yağ Kandili bir özgür yazılımdır: GNU Affero Genel Kamu Lisansı'nın 3.
ya da (isteğinize göre) sonraki bir sürümünün Free Software Foundation
tarafından yayınlandığı durumunun koşulları altında Yağ Kandili'ni
dağıtabilir veya Yağ Kandili üzerinde değişiklik yapabilirsiniz.

Yağ Kandili kullanışlı olması umuduyla dağıtılmaktadır ancak HİÇBİR
GARANTİ VERMEMEKTEDİR, zımni PAZARLANABİLİRLİK veya BELİRLİ BİR
AMACA UYGUNLUK garantisi bile. Daha çok ayrıntı için GNU Affero
Genel Kamu Lisansı'na bakın.

Yağ Kandili'nin yanında GNU Affero Genel Kamu Lisansı'nın bir kopyasını
almış olmalısınız. Almadıysanız, <https://www.gnu.org/licenses/>
adresine bakın.

Yağ Kandili'nin lisansıyla ilgili daha çok bilgi için /Lisans
dizinine bakın.

---

This file is part of Yağ Kandili.

Yağ Kandili is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

Yağ Kandili is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with Yağ Kandili. If not, see <https://www.gnu.org/licenses/>.

For more information about the license of Yağ Kandili, see
/Lisans directory.
*/
using System;
using System.IO;
using System.Globalization;
using MySql.Data.MySqlClient;
using Esas;
using Kilnevüg;

namespace Esas.VeriTabanı
{
    internal class Paylaşım
    {
        internal static void PaylaşımYap(Esas.Paylaşım paylaşım)
        {
            string komut_metni = $"INSERT INTO {TabloAdı()} (Kimlik2, Başlık, İçerik, Eklenti, " +
                                "Paylaşan, Tarih, Lisans) VALUES (@kimlik2, @başlık, " +
                                "@içerik, @eklenti, @paylaşan, @tarih, @lisans);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kimlik2", paylaşım.KİMLİK_2);
            komut.Parameters.AddWithValue("@başlık", paylaşım.BAŞLIK);
            komut.Parameters.AddWithValue("@içerik", paylaşım.İÇERİK);
            komut.Parameters.AddWithValue("@eklenti", paylaşım.EKLENTİ);
            komut.Parameters.AddWithValue("@paylaşan", paylaşım.PAYLAŞAN);
            komut.Parameters.AddWithValue("@tarih", paylaşım.TARİH.ToString("yyyyMMddHHmmss"));
            komut.Parameters.AddWithValue("@lisans", paylaşım.LİSANS);
            komut.ExecuteNonQuery();
            bağlantı.Close(); bağlantı.Dispose();
            komut.Dispose();
        }
        internal static void PaylaşımYap(Esas.yeni_paylaşım paylaşım)
        {
            string komut_metni = $"INSERT INTO {TabloAdı()} (Kimlik2, Başlık, İçerik, Eklenti, " +
                                "Paylaşan, Tarih, Lisans) VALUES (@kimlik2, @başlık, " +
                                "@içerik, @eklenti, @paylaşan, @tarih, @lisans);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kimlik2", paylaşım.KİMLİK_2);
            komut.Parameters.AddWithValue("@başlık", paylaşım.BAŞLIK);
            komut.Parameters.AddWithValue("@içerik", paylaşım.İÇERİK);
            komut.Parameters.AddWithValue("@eklenti", paylaşım.EKLENTİ);
            komut.Parameters.AddWithValue("@paylaşan", paylaşım.PAYLAŞAN);
            komut.Parameters.AddWithValue("@tarih", paylaşım.TARİH.ToString("yyyyMMddHHmmss"));
            komut.Parameters.AddWithValue("@lisans", paylaşım.LİSANS);
            komut.ExecuteNonQuery();
            bağlantı.Close(); bağlantı.Dispose();
            komut.Dispose();
        }
        internal static void PaylaşımıGizle(string kimlik2)
        {
            string gizleme_tarihi = DateTime.Now.ToString("yyyyMMddHHmmss");
            string komut_metni = $"SELECT Eklenti FROM {TabloAdı()} " +
                                "WHERE Kimlik2 = @kimlik2;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kimlik2", kimlik2);
            string eklenti = komut.ExecuteScalar().ToString();
            komut.Dispose();
            string[] eklentiler = eklenti.Split('>');
            int gizlilik_eki = 0;
            int sayaç = 0;
            for (int i = 0; i < eklentiler.Length; i++)
            {
                if (eklentiler[i].StartsWith("gizli"))
                {
                    gizlilik_eki = i;
                    sayaç++;
                }
            }
            if (sayaç != 0)
            {
                eklentiler[gizlilik_eki] = $"gizli/{gizleme_tarihi}";
                
                eklenti = String.Empty;
                for (int i = 0; i < eklentiler.Length; i++)
                {
                    eklenti += $">{eklentiler[i]}";
                }
            }
            else
            {
                eklenti = String.Empty;
                for (int i = 0; i < eklentiler.Length; i++)
                {
                    eklenti += $">{eklentiler[i]}";
                }
                eklenti += $">gizli/{gizleme_tarihi}";
            }

            komut_metni = $"UPDATE {TabloAdı()} SET Eklenti = @eklenti " +
                        "WHERE Kimlik2 = @kimlik2;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kimlik2", kimlik2);
            komut.Parameters.AddWithValue("@eklenti", eklenti);
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
        }
        internal static void GizliPaylaşımıAç(string kimlik2)
        {
            string komut_metni = $"SELECT COUNT(Eklenti) FROM {TabloAdı()} " +
                                "WHERE Kimlik2 = @kimlik2 AND " +
                                "Eklenti LIKE '%>gizli%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kimlik2", kimlik2);
            short nicelik = short.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            
            if (nicelik == 0)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return;
            }
            
            komut_metni = $"SELECT Eklenti FROM {TabloAdı()} " +
                        "WHERE Kimlik2 = @kimlik2;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kimlik2", kimlik2);
            string eklenti = komut.ExecuteScalar().ToString();
            komut.Dispose();
            string[] eklentiler = eklenti.Split('>');
            int sayaç = 0;
            for (int i = 0; i < eklentiler.Length; i++)
            {
                if (eklentiler[i].StartsWith("gizli"))
                {
                    sayaç++;
                }
            }
            if (sayaç != 0)
            {
                if (eklentiler.Length == 1)
                {
                    eklenti = "y";
                }
                else
                {
                    eklenti = String.Empty;
                    for (int i = 0; i < eklentiler.Length; i++)
                    {
                        if (!(eklentiler[i].StartsWith("gizli")))
                        {
                            eklenti += $">{eklentiler[i]}";
                        }
                    }
                }

                komut_metni = $"UPDATE {TabloAdı()} SET Eklenti = @eklenti " +
                            "WHERE Kimlik2 = @kimlik2;";
                komut = new MySqlCommand(komut_metni, bağlantı);
                komut.Parameters.AddWithValue("@kimlik2", kimlik2);
                komut.Parameters.AddWithValue("@eklenti", eklenti);
                komut.ExecuteNonQuery();
                komut.Dispose();
                bağlantı.Close(); bağlantı.Dispose();
                return;
            }
            else
            {
                bağlantı.Close(); bağlantı.Dispose();
                return;
            }
        }
        internal static void PaylaşımıSil(string sildiren, string kimlik2)
        {
            // UYARI!
            // Bu fonksiyon, belirtilen paylaşımı neredeyse yok edecektir.
            // Çok dikkatli kullan.

            string komut_metni = $"DELETE FROM {TabloAdı()} WHERE " +
                                "Paylaşan = @sildiren AND Kimlik2 = @kimlik2;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@sildiren", sildiren);
            komut.Parameters.AddWithValue("@kimlik2", kimlik2);
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
        }
        internal static string PaylaşanınKimliği(string kimlik2)
        {
            string komut_metni = $"SELECT Paylaşan FROM {TabloAdı()} " +
                                "WHERE Kimlik2 = @kimlik2;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kimlik2", kimlik2);
            string paylaşan = komut.ExecuteScalar().ToString();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return paylaşan;
        }
        internal static Esas.Paylaşım TekPaylaşım(string paylaşım_kimliği)
        {
            string komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Kimlik2 = @paylaşım_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşım_kimliği", paylaşım_kimliği);
            CultureInfo TR = new CultureInfo("tr-TR");
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            int döngü_turu = 0;
            Esas.Paylaşım paylaşım = new Esas.Paylaşım();
            while (veri_okuyucu.Read())
            {
                if (döngü_turu > 0)
                {
                    break;
                }
                paylaşım.KİMLİK_1 = long.Parse(veri_okuyucu["Kimlik1"].ToString());
                paylaşım.KİMLİK_2 = veri_okuyucu["Kimlik2"].ToString();
                paylaşım.BAŞLIK = veri_okuyucu["Başlık"].ToString();
                paylaşım.İÇERİK = veri_okuyucu["İçerik"].ToString();
                paylaşım.EKLENTİ = veri_okuyucu["Eklenti"].ToString();
                paylaşım.PAYLAŞAN = veri_okuyucu["Paylaşan"].ToString();
                paylaşım.TARİH = DateTime.ParseExact(veri_okuyucu["Tarih"].ToString(), "yyyyMMddHHmmss", TR);
                paylaşım.LİSANS = veri_okuyucu["Lisans"].ToString();
                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return paylaşım;
        }
        internal static Esas.Paylaşım[] TümPaylaşımlar()
        {
            string komut_metni = $"SELECT COUNT(Kimlik1) FROM {TabloAdı()} WHERE Eklenti NOT LIKE '%>gizli%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            int paylaşım_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            Esas.Paylaşım[] paylaşımlar;

            if (paylaşım_niceliği < 1)
            {
                paylaşımlar = new Esas.Paylaşım[0];
                bağlantı.Close(); bağlantı.Dispose();
                return paylaşımlar;
            }

            komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Eklenti NOT LIKE '%>gizli%' ORDER BY Tarih DESC, Kimlik1 DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            CultureInfo TR = new CultureInfo("tr-TR");
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            int döngü_turu = 0;
            paylaşımlar = new Esas.Paylaşım[paylaşım_niceliği];
            while (veri_okuyucu.Read())
            {
                paylaşımlar[döngü_turu] = new Esas.Paylaşım();
                paylaşımlar[döngü_turu].KİMLİK_1 = long.Parse(veri_okuyucu["Kimlik1"].ToString());
                paylaşımlar[döngü_turu].KİMLİK_2 = veri_okuyucu["Kimlik2"].ToString();
                paylaşımlar[döngü_turu].BAŞLIK = veri_okuyucu["Başlık"].ToString();
                paylaşımlar[döngü_turu].İÇERİK = veri_okuyucu["İçerik"].ToString();
                paylaşımlar[döngü_turu].EKLENTİ = veri_okuyucu["Eklenti"].ToString();
                paylaşımlar[döngü_turu].PAYLAŞAN = veri_okuyucu["Paylaşan"].ToString();
                string tarih = veri_okuyucu["Tarih"].ToString();
                paylaşımlar[döngü_turu].TARİH = DateTime.ParseExact(tarih, "yyyyMMddHHmmss", TR);
                paylaşımlar[döngü_turu].LİSANS = veri_okuyucu["Lisans"].ToString();

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return paylaşımlar;
        }
        internal static string[,][] KullanıcıBilgileriyleTümPaylaşımlar()
        {
            string komut_metni = $"SELECT COUNT(Kimlik2) FROM {Paylaşım.TabloAdı()} INNER JOIN {Üyelik.TabloAdı()} " +
                                $"ON {Paylaşım.TabloAdı()}.Paylaşan = {Üyelik.TabloAdı()}.Kimlik " +
                                "WHERE Eklenti NOT LIKE '%>gizli%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            int paylaşım_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            string[,][] paylaşımlar;

            if (paylaşım_niceliği < 1)
            {
                paylaşımlar = new string[0,0][];
                bağlantı.Close(); bağlantı.Dispose();
                return paylaşımlar;
            }

            komut_metni = $"SELECT * FROM {Paylaşım.TabloAdı()} INNER JOIN {Üyelik.TabloAdı()} " +
                        $" ON {Paylaşım.TabloAdı()}.Paylaşan = {Üyelik.TabloAdı()}.Kimlik " +
                        "WHERE Eklenti NOT LIKE '%>gizli%' ORDER BY Tarih DESC, Kimlik1 DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            int döngü_turu = 0;
            paylaşımlar = new string[paylaşım_niceliği, 2][];
            while (veri_okuyucu.Read())
            {
                paylaşımlar[döngü_turu, 0] = new string[8]; //paylaşım bilgileri
                paylaşımlar[döngü_turu, 1] = new string[7]; //paylaşan bilgileri

                paylaşımlar[döngü_turu, 0][0] = veri_okuyucu["Kimlik1"].ToString();
                paylaşımlar[döngü_turu, 0][1] = veri_okuyucu["Kimlik2"].ToString();
                paylaşımlar[döngü_turu, 0][2] = veri_okuyucu["Başlık"].ToString();
                paylaşımlar[döngü_turu, 0][3] = veri_okuyucu["İçerik"].ToString();
                paylaşımlar[döngü_turu, 0][4] = veri_okuyucu["Eklenti"].ToString();
                paylaşımlar[döngü_turu, 0][5] = veri_okuyucu["Paylaşan"].ToString();
                paylaşımlar[döngü_turu, 0][6] = veri_okuyucu["Tarih"].ToString();
                paylaşımlar[döngü_turu, 0][7] = veri_okuyucu["Lisans"].ToString();

                paylaşımlar[döngü_turu, 1][0] = veri_okuyucu["Ad"].ToString();
                paylaşımlar[döngü_turu, 1][1] = veri_okuyucu["Kullanıcı_Adı"].ToString();
                paylaşımlar[döngü_turu, 1][2] = veri_okuyucu["E_Posta"].ToString();
                paylaşımlar[döngü_turu, 1][3] = veri_okuyucu["Üstünlük"].ToString();
                paylaşımlar[döngü_turu, 1][4] = veri_okuyucu["Başlangıç"].ToString();
                paylaşımlar[döngü_turu, 1][5] = veri_okuyucu["Resim"].ToString();
                paylaşımlar[döngü_turu, 1][6] = veri_okuyucu["Kimlik"].ToString();

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return paylaşımlar;
        }
        internal static string[,][] TakipEdilenlerinBilgileriylePaylaşımları(string[,] takip_edilenler)
        {
            // a sıra sayısı olursa
            // takip_edilenler[a, 0] = kullanıcı kimliği, takip_edilenler[a, 1] = takip düzeyi
            
            // [a, 0][] paylaşım bilgileri, [a, 1][] paylaşan bilgileri
            string SorguİçinKimlikler = "";
            for (long i = 0; i < takip_edilenler.GetLongLength(0); i++)
            {
                SorguİçinKimlikler += $"@takip{i.ToString()}, ";
            }
            SorguİçinKimlikler = $"({SorguİçinKimlikler.Substring(0,(SorguİçinKimlikler.Length-2))})";
            string komut_metni = $"SELECT COUNT(Kimlik2) FROM {Paylaşım.TabloAdı()} " +
                                $"INNER JOIN {Üyelik.TabloAdı()} " +
                                $"ON {Paylaşım.TabloAdı()}.Paylaşan = {Üyelik.TabloAdı()}.Kimlik " +
                                "WHERE Eklenti NOT LIKE '%>gizli%' AND" + 
                                $" Paylaşan IN {SorguİçinKimlikler};";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            for (int j = 0; j < takip_edilenler.GetLongLength(0); j++)
            {
                komut.Parameters.AddWithValue($"@takip{j.ToString()}", takip_edilenler[j,0]);
            }
            int paylaşım_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            string[,][] paylaşımlar;

            if (paylaşım_niceliği < 1)
            {
                paylaşımlar = new string[0,0][];
                bağlantı.Close(); bağlantı.Dispose();
                return paylaşımlar;
            }

            komut_metni = $"SELECT * FROM {Paylaşım.TabloAdı()} " +
                        $"INNER JOIN {Üyelik.TabloAdı()} " +
                        $"ON {Paylaşım.TabloAdı()}.Paylaşan = {Üyelik.TabloAdı()}.Kimlik " +
                        "WHERE Eklenti NOT LIKE '%>gizli%' AND" + 
                        $" Paylaşan IN {SorguİçinKimlikler} " +
                        "ORDER BY Tarih DESC, Kimlik1 DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            for (int j = 0; j < takip_edilenler.GetLongLength(0); j++)
            {
                komut.Parameters.AddWithValue($"@takip{j.ToString()}", takip_edilenler[j,0]);
            }
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            int döngü_turu = 0;
            paylaşımlar = new string[paylaşım_niceliği, 2][];
            while (veri_okuyucu.Read())
            {
                paylaşımlar[döngü_turu, 0] = new string[9]; //paylaşım bilgileri
                paylaşımlar[döngü_turu, 1] = new string[7]; //paylaşan bilgileri

                paylaşımlar[döngü_turu, 0][0] = veri_okuyucu["Kimlik1"].ToString();
                paylaşımlar[döngü_turu, 0][1] = veri_okuyucu["Kimlik2"].ToString();
                paylaşımlar[döngü_turu, 0][2] = veri_okuyucu["Başlık"].ToString();
                paylaşımlar[döngü_turu, 0][3] = veri_okuyucu["İçerik"].ToString();
                paylaşımlar[döngü_turu, 0][4] = veri_okuyucu["Eklenti"].ToString();
                paylaşımlar[döngü_turu, 0][5] = veri_okuyucu["Paylaşan"].ToString();
                paylaşımlar[döngü_turu, 0][6] = veri_okuyucu["Tarih"].ToString();
                paylaşımlar[döngü_turu, 0][7] = veri_okuyucu["Lisans"].ToString();

                paylaşımlar[döngü_turu, 1][0] = veri_okuyucu["Ad"].ToString();
                paylaşımlar[döngü_turu, 1][1] = veri_okuyucu["Kullanıcı_Adı"].ToString();
                paylaşımlar[döngü_turu, 1][2] = veri_okuyucu["E_Posta"].ToString();
                paylaşımlar[döngü_turu, 1][3] = veri_okuyucu["Üstünlük"].ToString();
                paylaşımlar[döngü_turu, 1][4] = veri_okuyucu["Başlangıç"].ToString();
                paylaşımlar[döngü_turu, 1][5] = veri_okuyucu["Resim"].ToString();
                paylaşımlar[döngü_turu, 1][6] = veri_okuyucu["Kimlik"].ToString();

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return paylaşımlar;
        }
        internal static paylaşım[] KişininTümAçıkPaylaşımları(string kullanıcı_kimliği)
        {
            string komut_metni = $"SELECT COUNT(Kimlik1) FROM {TabloAdı()} " +
                                "WHERE Paylaşan = @paylaşan AND Eklenti NOT LIKE '%>gizli%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşan", kullanıcı_kimliği);
            int paylaşım_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            paylaşım[] paylaşımlar;

            if (paylaşım_niceliği < 1)
            {
                paylaşımlar = new paylaşım[0];
                bağlantı.Close(); bağlantı.Dispose();
                return paylaşımlar;
            }

            komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Paylaşan = @paylaşan AND " +
                        "Eklenti NOT LIKE '%>gizli%' ORDER BY Tarih DESC, Kimlik1 DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşan", kullanıcı_kimliği);
            CultureInfo TR = new CultureInfo("tr-TR");
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            int döngü_turu = 0;
            paylaşımlar = new paylaşım[paylaşım_niceliği];
            while (veri_okuyucu.Read())
            {
                paylaşımlar[döngü_turu] = new paylaşım();
                paylaşımlar[döngü_turu].KİMLİK_1 = long.Parse(veri_okuyucu["Kimlik1"].ToString());
                paylaşımlar[döngü_turu].KİMLİK_2 = veri_okuyucu["Kimlik2"].ToString();
                paylaşımlar[döngü_turu].BAŞLIK = veri_okuyucu["Başlık"].ToString();
                paylaşımlar[döngü_turu].İÇERİK = veri_okuyucu["İçerik"].ToString();
                paylaşımlar[döngü_turu].EKLENTİ = veri_okuyucu["Eklenti"].ToString();
                paylaşımlar[döngü_turu].PAYLAŞAN = veri_okuyucu["Paylaşan"].ToString();
                string tarih = veri_okuyucu["Tarih"].ToString();
                paylaşımlar[döngü_turu].TARİH = DateTime.ParseExact(tarih, "yyyyMMddHHmmss", TR);
                paylaşımlar[döngü_turu].LİSANS = veri_okuyucu["Lisans"].ToString();

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return paylaşımlar;
        }
        internal static paylaşım[] KişininTümGizliPaylaşımları(string kullanıcı_kimliği)
        {
            string komut_metni = $"SELECT COUNT(Kimlik1) FROM {TabloAdı()} " +
                                "WHERE Paylaşan = @paylaşan AND Eklenti LIKE '%>gizli%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşan", kullanıcı_kimliği);
            int paylaşım_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            paylaşım[] paylaşımlar;

            if (paylaşım_niceliği < 1)
            {
                paylaşımlar = new paylaşım[0];
                bağlantı.Close(); bağlantı.Dispose();
                return paylaşımlar;
            }

            komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Paylaşan = @paylaşan AND " +
                        "Eklenti LIKE '%>gizli%' ORDER BY Tarih DESC, Kimlik1 DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşan", kullanıcı_kimliği);
            CultureInfo TR = new CultureInfo("tr-TR");
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            int döngü_turu = 0;
            paylaşımlar = new paylaşım[paylaşım_niceliği];
            while (veri_okuyucu.Read())
            {
                paylaşımlar[döngü_turu] = new paylaşım();
                paylaşımlar[döngü_turu].KİMLİK_1 = long.Parse(veri_okuyucu["Kimlik1"].ToString());
                paylaşımlar[döngü_turu].KİMLİK_2 = veri_okuyucu["Kimlik2"].ToString();
                paylaşımlar[döngü_turu].BAŞLIK = veri_okuyucu["Başlık"].ToString();
                paylaşımlar[döngü_turu].İÇERİK = veri_okuyucu["İçerik"].ToString();
                paylaşımlar[döngü_turu].EKLENTİ = veri_okuyucu["Eklenti"].ToString();
                paylaşımlar[döngü_turu].PAYLAŞAN = veri_okuyucu["Paylaşan"].ToString();
                string tarih = veri_okuyucu["Tarih"].ToString();
                paylaşımlar[döngü_turu].TARİH = DateTime.ParseExact(tarih, "yyyyMMddHHmmss", TR);
                paylaşımlar[döngü_turu].LİSANS = veri_okuyucu["Lisans"].ToString();

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return paylaşımlar;
        }
        internal static paylaşım[] KişininTümPaylaşımları(string kullanıcı_kimliği)
        {
            string komut_metni = $"SELECT COUNT(Kimlik1) FROM {TabloAdı()} " +
                                "WHERE Paylaşan = @paylaşan;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşan", kullanıcı_kimliği);
            int paylaşım_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            paylaşım[] paylaşımlar;

            if (paylaşım_niceliği < 1)
            {
                paylaşımlar = new paylaşım[0];
                bağlantı.Close(); bağlantı.Dispose();
                return paylaşımlar;
            }

            komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Paylaşan = @paylaşan " +
                        "ORDER BY Tarih DESC, Kimlik1 DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşan", kullanıcı_kimliği);
            CultureInfo TR = new CultureInfo("tr-TR");
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            int döngü_turu = 0;
            paylaşımlar = new paylaşım[paylaşım_niceliği];
            while (veri_okuyucu.Read())
            {
                paylaşımlar[döngü_turu] = new paylaşım();
                paylaşımlar[döngü_turu].KİMLİK_1 = long.Parse(veri_okuyucu["Kimlik1"].ToString());
                paylaşımlar[döngü_turu].KİMLİK_2 = veri_okuyucu["Kimlik2"].ToString();
                paylaşımlar[döngü_turu].BAŞLIK = veri_okuyucu["Başlık"].ToString();
                paylaşımlar[döngü_turu].İÇERİK = veri_okuyucu["İçerik"].ToString();
                paylaşımlar[döngü_turu].EKLENTİ = veri_okuyucu["Eklenti"].ToString();
                paylaşımlar[döngü_turu].PAYLAŞAN = veri_okuyucu["Paylaşan"].ToString();
                string tarih = veri_okuyucu["Tarih"].ToString();
                paylaşımlar[döngü_turu].TARİH = DateTime.ParseExact(tarih, "yyyyMMddHHmmss", TR);
                paylaşımlar[döngü_turu].LİSANS = veri_okuyucu["Lisans"].ToString();

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return paylaşımlar;
        }
        internal static int TümPaylaşımlarınNiceliği()
        {
            string komut_metni = $"SELECT COUNT(Kimlik1) FROM {TabloAdı()} WHERE Eklenti NOT LIKE '%>gizli%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            int paylaşım_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            bağlantı.Close(); bağlantı.Dispose();
            komut.Dispose();
            return paylaşım_niceliği;
        }
        internal static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 5 && belge_içeriği[3].StartsWith("-"))
            {
                return belge_içeriği[3].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}