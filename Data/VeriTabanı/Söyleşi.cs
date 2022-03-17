using System;
using System.IO;
using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;
using Kilnevüg;

namespace Esas.VeriTabanı
{
    class Söyleşi
    {
        public static void Söyle(yeni_söz söz)
        {
            string komut_metni = $"INSERT INTO {TabloAdı()} (Söz, Söyleyen, " +
                                "Tarih, Söyleşi, Başlatan_Paylaşım, Bu_İlk) VALUES " +
                                "(@söz, @söyleyen, @tarih, @söyleşi, " +
                                "@başlatan_paylaşım, @bu_ilk);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@söz", söz.SÖZ);
            komut.Parameters.AddWithValue("@söyleyen", söz.SÖYLEYEN);
            komut.Parameters.AddWithValue("@tarih", söz.TARİH);
            komut.Parameters.AddWithValue("@söyleşi", söz.SÖYLEŞİ);
            komut.Parameters.AddWithValue("@başlatan_paylaşım", söz.BAŞLATAN_PAYLAŞIM);
            komut.Parameters.AddWithValue("@bu_ilk", söz.BU_İLK);
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
        }
        public static söz[] Yorumlar(string paylaşım_kimliği)
        {
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            string komut_metni = $"SELECT COUNT(Söz) FROM {TabloAdı()} WHERE " +
                                "Başlatan_Paylaşım = @paylaşım AND Bu_İlk = @bu_ilk;";
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşım", paylaşım_kimliği);
            komut.Parameters.AddWithValue("@bu_ilk", true);
            long yorum_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Başlatan_Paylaşım = @paylaşım " +
                        "AND Bu_İlk = @bu_ilk ORDER BY Tarih DESC, Genel_Sıra DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@paylaşım", paylaşım_kimliği);
            komut.Parameters.AddWithValue("@bu_ilk", true);

            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            söz[] yorumlar = new söz[yorum_niceliği];
            long döngü_turu = 0;
            CultureInfo TR = new CultureInfo("tr-TR");
            while (veri_okuyucu.Read())
            {
                yorumlar[döngü_turu].SÖZ = veri_okuyucu["Söz"].ToString();
                yorumlar[döngü_turu].SÖYLEYEN = veri_okuyucu["Söyleyen"].ToString();
                yorumlar[döngü_turu].TARİH = veri_okuyucu["Tarih"].ToString();
                yorumlar[döngü_turu].SÖYLEŞİ = veri_okuyucu["Söyleşi"].ToString();
                yorumlar[döngü_turu].BAŞLATAN_PAYLAŞIM = veri_okuyucu["Başlatan_Paylaşım"].ToString();
                yorumlar[döngü_turu].GENEL_SIRA = long.Parse(veri_okuyucu["Genel_Sıra"].ToString());
                yorumlar[döngü_turu].BU_İLK = bool.Parse(veri_okuyucu["Bu_İlk"].ToString());

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return yorumlar;
        }
        public static string[,][] TümBilgileriyleSöyleşi(string söyleşi_kimliği)
        {
            string komut_metni = $"SELECT COUNT(Söz) FROM {Söyleşi.TabloAdı()} INNER JOIN {Üyelik.TabloAdı()} " +
                                $"ON {Söyleşi.TabloAdı()}.Söyleyen = {Üyelik.TabloAdı()}.Kimlik " +
                                "WHERE Söyleşi = @söyleşi_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@söyleşi_kimliği", söyleşi_kimliği);
            long sonuç_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            string[,][] söyleşi;
            
            if (sonuç_niceliği < 1)
            {
                bağlantı.Close(); bağlantı.Dispose();
                söyleşi = new string[0,0][];
                return söyleşi;
            }
            sonuç_niceliği++; // Başlatan paylaşımı da eklemek için

            string tablo_söyleşi = Söyleşi.TabloAdı();
            string tablo_üyelik = Üyelik.TabloAdı();
            string tablo_paylaşım = Paylaşım.TabloAdı();
            komut_metni = $"SELECT * FROM {tablo_söyleşi} INNER JOIN {tablo_üyelik} " +
                        $"ON {tablo_söyleşi}.Söyleyen = {tablo_üyelik}.Kimlik " +
                        $"WHERE Söyleşi = @söyleşi_kimliği ORDER BY {tablo_söyleşi}.Tarih ASC, " +
                        $"{tablo_söyleşi}.Genel_Sıra ASC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@söyleşi_kimliği", söyleşi_kimliği);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            int döngü_turu = 1; // Sıfırıncı tur başlatan paylaşım için ayrıldı.
            söyleşi = new string[sonuç_niceliği, 2][];
            while (veri_okuyucu.Read())
            {
                söyleşi[döngü_turu, 0] = new string[7]; // söz bilgileri
                söyleşi[döngü_turu, 1] = new string[7]; // söyleyen bilgileri

                söyleşi[döngü_turu, 0][0] = veri_okuyucu["Söz"].ToString();
                söyleşi[döngü_turu, 0][1] = veri_okuyucu["Söyleyen"].ToString();
                söyleşi[döngü_turu, 0][2] = veri_okuyucu["Tarih"].ToString();
                söyleşi[döngü_turu, 0][3] = veri_okuyucu["Söyleşi"].ToString();
                söyleşi[döngü_turu, 0][4] = veri_okuyucu["Başlatan_Paylaşım"].ToString();
                söyleşi[döngü_turu, 0][5] = veri_okuyucu["Genel_Sıra"].ToString();
                söyleşi[döngü_turu, 0][6] = veri_okuyucu["Bu_İlk"].ToString();

                söyleşi[döngü_turu, 1][0] = veri_okuyucu["Ad"].ToString();
                söyleşi[döngü_turu, 1][1] = veri_okuyucu["Kullanıcı_Adı"].ToString();
                söyleşi[döngü_turu, 1][2] = veri_okuyucu["E_Posta"].ToString();
                söyleşi[döngü_turu, 1][3] = veri_okuyucu["Üstünlük"].ToString();
                söyleşi[döngü_turu, 1][4] = veri_okuyucu["Başlangıç"].ToString();
                söyleşi[döngü_turu, 1][5] = veri_okuyucu["Resim"].ToString();
                söyleşi[döngü_turu, 1][6] = veri_okuyucu["Kimlik"].ToString();

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();

            komut_metni = $"SELECT * FROM {tablo_paylaşım} INNER JOIN {tablo_üyelik} " +
                        $"ON {tablo_paylaşım}.Paylaşan = {tablo_üyelik}.Kimlik " +
                        $"WHERE {tablo_paylaşım}.Kimlik2 = @başlatan_paylaşım;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@başlatan_paylaşım", söyleşi[1,0][4]);
            veri_okuyucu = komut.ExecuteReader();
            int tur = 0;
            while(veri_okuyucu.Read())
            {
                if (tur > 0)
                {
                    break;
                }
                söyleşi[0, 0] = new string[8]; // paylaşım bilgileri
                söyleşi[0, 1] = new string[7]; // paylaşan bilgileri

                söyleşi[0, 0][0] = veri_okuyucu["Kimlik1"].ToString();
                söyleşi[0, 0][1] = veri_okuyucu["Kimlik2"].ToString();
                söyleşi[0, 0][2] = veri_okuyucu["Başlık"].ToString();
                söyleşi[0, 0][3] = veri_okuyucu["İçerik"].ToString();
                söyleşi[0, 0][4] = veri_okuyucu["Eklenti"].ToString();
                söyleşi[0, 0][5] = veri_okuyucu["Paylaşan"].ToString();
                söyleşi[0, 0][6] = veri_okuyucu["Tarih"].ToString();
                söyleşi[0, 0][7] = veri_okuyucu["Lisans"].ToString();

                söyleşi[0, 1][0] = veri_okuyucu["Ad"].ToString();
                söyleşi[0, 1][1] = veri_okuyucu["Kullanıcı_Adı"].ToString();
                söyleşi[0, 1][2] = veri_okuyucu["E_Posta"].ToString();
                söyleşi[0, 1][3] = veri_okuyucu["Üstünlük"].ToString();
                söyleşi[0, 1][4] = veri_okuyucu["Başlangıç"].ToString();
                söyleşi[0, 1][5] = veri_okuyucu["Resim"].ToString();
                söyleşi[0, 1][6] = veri_okuyucu["Kimlik"].ToString();
                
                tur++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return söyleşi;
        }
        public static köklü_söz[] KişininSöyledikleri(string kullanıcı_kimliği)
        {
            string komut_metni = $"SELECT COUNT(Söz) FROM {Söyleşi.TabloAdı()} " +
                                $"INNER JOIN {Paylaşım.TabloAdı()} ON " +
                                $"{Söyleşi.TabloAdı()}.Başlatan_Paylaşım = {Paylaşım.TabloAdı()}.Kimlik2 " +
                                "WHERE Söyleyen = @kullanıcı_kimliği AND " +
                                "Eklenti NOT LIKE '%>gizli%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            long söz_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            if (söz_niceliği < 1)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return new köklü_söz[0];
            }

            string sütunlar = "SÖZ.Söz AS asıl_söz, SÖZ.Söyleyen AS asıl_söyleyen, " +
                            "SÖZ.Tarih AS asıl_tarih, SÖZ.Söyleşi AS asıl_söyleşi, " +
                            "SÖZ.Başlatan_Paylaşım AS asıl_başlatan_paylaşım, " +
                            "SÖZ.Genel_Sıra AS asıl_genel_sıra, " +
                            "SÖZ.Bu_İlk AS asıl_bu_ilk, " +
            
                        "İLK.Söz AS ilk_söz, İLK.Söyleyen AS ilk_söyleyen, " +
                        "İLK.Tarih AS ilk_tarih, İLK.Söyleşi AS ilk_söyleşi, " +
                        "İLK.Başlatan_Paylaşım AS ilk_başlatan_paylaşım, " +
                        "İLK.Genel_Sıra AS ilk_genel_sıra, " +
                        "İLK.Bu_İlk AS ilk_bu_ilk, " +

                        $"{Paylaşım.TabloAdı()}.*";

            komut_metni = $"SELECT {sütunlar} FROM {Söyleşi.TabloAdı()} SÖZ " +
                        $"INNER JOIN {Söyleşi.TabloAdı()} İLK ON " +
                        "SÖZ.Söyleşi = İLK.Söyleşi " +
                        $"INNER JOIN {Paylaşım.TabloAdı()} ON " +
                        $"SÖZ.Başlatan_Paylaşım = {Paylaşım.TabloAdı()}.Kimlik2 " +
                        "WHERE SÖZ.Söyleyen = @kullanıcı_kimliği AND " +
                        "İLK.Bu_İlk = TRUE AND Eklenti NOT LIKE '%>gizli%' " +
                        "ORDER BY SÖZ.Tarih DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            long döngü_turu = 0;
            köklü_söz[] söylenenler = new köklü_söz[söz_niceliği];
            paylaşım başlatan; söz ilk_söz;
            CultureInfo TR = new CultureInfo("tr-TR");
            while (veri_okuyucu.Read())
            {
                
                başlatan.KİMLİK_1 = long.Parse(veri_okuyucu["Kimlik1"].ToString());
                başlatan.KİMLİK_2 = veri_okuyucu["Kimlik2"].ToString();
                başlatan.BAŞLIK = veri_okuyucu["Başlık"].ToString();
                başlatan.İÇERİK = veri_okuyucu["İçerik"].ToString();
                başlatan.EKLENTİ = veri_okuyucu["Eklenti"].ToString();
                başlatan.PAYLAŞAN = veri_okuyucu["Paylaşan"].ToString();
                başlatan.LİSANS = veri_okuyucu["Lisans"].ToString();
                başlatan.TARİH = DateTime.ParseExact(veri_okuyucu["Tarih"].ToString(),
                                                    "yyyyMMddHHmmss",
                                                    TR);
                
                söylenenler[döngü_turu].BAŞLATAN_PAYLAŞIM = başlatan;

                ilk_söz.SÖZ = veri_okuyucu["ilk_söz"].ToString();
                ilk_söz.SÖYLEYEN = veri_okuyucu["ilk_söyleyen"].ToString();
                ilk_söz.SÖYLEŞİ = veri_okuyucu["ilk_söyleşi"].ToString();
                ilk_söz.TARİH = veri_okuyucu["ilk_tarih"].ToString();
                ilk_söz.BAŞLATAN_PAYLAŞIM = veri_okuyucu["ilk_başlatan_paylaşım"].ToString();
                ilk_söz.BU_İLK = bool.Parse(veri_okuyucu["ilk_bu_ilk"].ToString());
                ilk_söz.GENEL_SIRA = long.Parse(veri_okuyucu["ilk_genel_sıra"].ToString());

                söylenenler[döngü_turu].İLK_SÖZ = ilk_söz;

                söylenenler[döngü_turu].BU_İLK = bool.Parse(veri_okuyucu["asıl_bu_ilk"].ToString());
                söylenenler[döngü_turu].SÖZ = veri_okuyucu["asıl_söz"].ToString();
                söylenenler[döngü_turu].SÖYLEYEN = veri_okuyucu["asıl_söyleyen"].ToString();
                söylenenler[döngü_turu].SÖYLEŞİ = veri_okuyucu["asıl_söyleşi"].ToString();
                söylenenler[döngü_turu].GENEL_SIRA = long.Parse(veri_okuyucu["asıl_genel_sıra"].ToString());
                söylenenler[döngü_turu].TARİH = DateTime.ParseExact(veri_okuyucu["asıl_tarih"].ToString(),
                                                                    "yyyyMMddHHmmss",
                                                                    TR);

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return söylenenler;
        }
        public static string[,] TakipEdilenSöyleşiler(string[,] takip_edilenler)
        {
            // [sıra] [söyleşi kimliği, başlık] > iki boyutlu dizi
            if (takip_edilenler.GetLength(0) < 1)
            {
                return new string[0,0];
            }
            string SorguİçinKimlikler = "";
            for (long i = 0; i < takip_edilenler.GetLongLength(0); i++)
            {
                SorguİçinKimlikler += $"@takip{i.ToString()}, ";
            }
            SorguİçinKimlikler = $"({SorguİçinKimlikler.Substring(0,(SorguİçinKimlikler.Length-2))})";
            string söyleşi_tablosu = TabloAdı();
            string paylaşım_tablosu = Paylaşım.TabloAdı();
            string takip_tablosu = Takip.TabloAdı();
            string komut_metni = $"SELECT COUNT(DISTINCT Söyleşi) FROM {söyleşi_tablosu} " +
                                $"INNER JOIN {paylaşım_tablosu} ON " +
                                $"{paylaşım_tablosu}.Kimlik2 = {söyleşi_tablosu}.Başlatan_Paylaşım " +
                                "WHERE Eklenti NOT LIKE '%>gizli%' AND " +
                                $"Söyleşi IN {SorguİçinKimlikler};";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            for (int j = 0; j < takip_edilenler.GetLongLength(0); j++)
            {
                komut.Parameters.AddWithValue($"@takip{j.ToString()}", takip_edilenler[j,0]);
            }
            long söyleşi_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            if (söyleşi_niceliği < 1)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return new string[0,0];
            }

            komut_metni = $"SELECT DISTINCT Başlık, Söyleşi FROM {söyleşi_tablosu} " +
                        $"INNER JOIN {paylaşım_tablosu} ON " +
                        $"{paylaşım_tablosu}.Kimlik2 = {söyleşi_tablosu}.Başlatan_Paylaşım " +
                        $"WHERE Eklenti NOT LIKE '%>gizli%' AND Söyleşi IN {SorguİçinKimlikler};";
            komut = new MySqlCommand(komut_metni, bağlantı);
            for (int j = 0; j < takip_edilenler.GetLongLength(0); j++)
            {
                komut.Parameters.AddWithValue($"@takip{j.ToString()}", takip_edilenler[j,0]);
            }
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            int döngü_turu = 0;
            string[,] söyleşiler = new string[söyleşi_niceliği,2];
            while (veri_okuyucu.Read())
            {
                söyleşiler[döngü_turu, 0] = veri_okuyucu["Söyleşi"].ToString();
                söyleşiler[döngü_turu, 1] = veri_okuyucu["Başlık"].ToString();

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return söyleşiler;
        }
        private static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 6 && belge_içeriği[4].StartsWith("-"))
            {
                return belge_içeriği[4].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}