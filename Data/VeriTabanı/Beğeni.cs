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
    public class Beğeni
    {
        public static void Beğen(Esas.Beğeni beğeni)
        {
            string komut_metni = $"INSERT INTO {TabloAdı()} (Kim, Neyi, Ne_Kadar, Ne_Zaman) " +
                                "VALUES (@kim, @neyi, @ne_kadar, @ne_zaman);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kim", beğeni.KİM);
            komut.Parameters.AddWithValue("@neyi", beğeni.NEYİ);
            komut.Parameters.AddWithValue("@ne_kadar", beğeni.NE_KADAR);
            komut.Parameters.AddWithValue("@ne_zaman", beğeni.NE_ZAMAN.ToString("yyyyMMddHHmmss"));
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
        }
        public static void BeğeniyiSil(string beğenen, string beğenilen)
        {
            string komut_metni = $"DELETE FROM {TabloAdı()} WHERE Kim = @beğenen AND Neyi = @beğenilen;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@beğenen", beğenen);
            komut.Parameters.AddWithValue("@beğenilen", beğenilen);
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
        }
        public static double PaylaşımınBeğeniOrtalaması(string beğenilen)
        {
            string komut_metni = $"SELECT COUNT(Ne_Kadar) FROM {TabloAdı()} WHERE Neyi = @beğenilen;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@beğenilen", beğenilen);
            long beğeni_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            if (beğeni_niceliği < 1)
            {
                komut.Dispose();
                bağlantı.Close(); bağlantı.Dispose();
                return 0;
            }

            komut_metni = $"SELECT AVG(Ne_Kadar) FROM {TabloAdı()} WHERE Neyi = @beğenilen;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@beğenilen", beğenilen);
            double ortalama = double.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return ortalama;
        }
        public static double KişininBeğeniOrtalaması(string kullanıcı_kimliği)
        {
            string komut_metni = $"SELECT COUNT(Ne_Kadar) FROM {TabloAdı()} INNER JOIN {Paylaşım.TabloAdı()} " +
                                $"ON {Beğeni.TabloAdı()}.Neyi = {Paylaşım.TabloAdı()}.Kimlik2 " +
                                $"WHERE Paylaşan = @beğenilen_kişi AND Eklenti NOT LIKE '%>gizli%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@beğenilen_kişi", kullanıcı_kimliği);
            long beğeni_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            if (beğeni_niceliği < 1)
            {
                komut.Dispose();
                bağlantı.Close(); bağlantı.Dispose();
                return 0;
            }

            komut_metni = $"SELECT AVG(Ne_Kadar) FROM {TabloAdı()} INNER JOIN {Paylaşım.TabloAdı()} " +
                                $"ON {Beğeni.TabloAdı()}.Neyi = {Paylaşım.TabloAdı()}.Kimlik2 " +
                                $"WHERE Paylaşan = @beğenilen_kişi AND Eklenti NOT LIKE '%>gizli%';";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@beğenilen_kişi", kullanıcı_kimliği);
            double ortalama = double.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return ortalama;
        }
        public static int DeğerlendirmeNiceliği(string beğenilen)
        {
            string komut_metni = $"SELECT COUNT(Neyi) FROM {TabloAdı()} WHERE Neyi = @beğenilen;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@beğenilen", beğenilen);
            int nicelik = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return nicelik;
        }
        public static int[] BakanKişininBeğenisi(string bakan, string beğenilen)
        {
            // [0] == 0 ise hiç değerlendirme yapılmamıştır.
            // [0] == 1 ise [1] beğenidir.
            string komut_metni = $"SELECT COUNT(Ne_Kadar) FROM {TabloAdı()} WHERE Kim = @bakan AND Neyi = @beğenilen;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@bakan", bakan);
            komut.Parameters.AddWithValue("@beğenilen", beğenilen);
            int nicelik = int.Parse(komut.ExecuteScalar().ToString());

            if (nicelik != 1)
            {
                komut.Dispose();
                bağlantı.Close(); bağlantı.Dispose();
                if (nicelik > 1)
                {
                    BeğeniyiSil(bakan, beğenilen);
                }
                return new int[]{0, 0};
            }

            komut_metni = $"SELECT Ne_Kadar FROM {TabloAdı()} WHERE Kim = @bakan AND Neyi = @beğenilen;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@bakan", bakan);
            komut.Parameters.AddWithValue("@beğenilen", beğenilen);
            int beğeni = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            int[] sonuç = new int[]{1, beğeni};
            return sonuç;
        }

        public static değerli_paylaşım[] KişininDeğerlendirdiğiPaylaşımlar(string kullanıcı_kimliği)
        {
            // Sonuçlar değerlendirme tarihine göre
            // yeniden eskiye doğru sıralanır.

            string komut_metni = $"SELECT COUNT(Neyi) FROM {Beğeni.TabloAdı()} " +
                                $"INNER JOIN {Paylaşım.TabloAdı()} " +
                                $"ON {Beğeni.TabloAdı()}.Neyi = {Paylaşım.TabloAdı()}.Kimlik2 " +
                                "WHERE Eklenti NOT LIKE '%>gizli%' AND " +
                                "Kim = @kullanıcı_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            long paylaşım_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            if (paylaşım_niceliği < 1)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return new değerli_paylaşım[0];
            }

            komut_metni = $"SELECT * FROM {Beğeni.TabloAdı()} " +
                        $"INNER JOIN {Paylaşım.TabloAdı()} " +
                        $"ON {Beğeni.TabloAdı()}.Neyi = {Paylaşım.TabloAdı()}.Kimlik2 " +
                        "WHERE Eklenti NOT LIKE '%>gizli%' AND " +
                        "Kim = @kullanıcı_kimliği " +
                        "ORDER BY Ne_Zaman DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            long döngü_turu = 0;
            CultureInfo TR = new CultureInfo("tr-TR");
            değerli_paylaşım[] sonuç = new değerli_paylaşım[paylaşım_niceliği];
            while (veri_okuyucu.Read())
            {

                sonuç[döngü_turu].KİMLİK_1 = long.Parse(
                                            veri_okuyucu["Kimlik1"].ToString());
                sonuç[döngü_turu].KİMLİK_2 = veri_okuyucu["Kimlik2"].ToString();
                sonuç[döngü_turu].BAŞLIK = veri_okuyucu["Başlık"].ToString();
                sonuç[döngü_turu].İÇERİK = veri_okuyucu["İçerik"].ToString();
                sonuç[döngü_turu].EKLENTİ = veri_okuyucu["Eklenti"].ToString();
                sonuç[döngü_turu].PAYLAŞAN = veri_okuyucu["Paylaşan"].ToString();
                sonuç[döngü_turu].PAYLAŞIM_TARİHİ = DateTime.ParseExact(
                                            veri_okuyucu["Tarih"].ToString(),
                                            "yyyyMMddHHmmss",
                                            TR );
                sonuç[döngü_turu].LİSANS = veri_okuyucu["Lisans"].ToString();
                
                sonuç[döngü_turu].DEĞERLENDİREN = veri_okuyucu["Kim"].ToString();
                sonuç[döngü_turu].DEĞER = int.Parse(
                                        veri_okuyucu["Ne_Kadar"].ToString());
                sonuç[döngü_turu].DEĞERLENDİRME_TARİHİ = DateTime.ParseExact(
                                            veri_okuyucu["Ne_Zaman"].ToString(),
                                            "yyyyMMddHHmmss",
                                            TR );

                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return sonuç;
        }

        private static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 6 && belge_içeriği[3].StartsWith("-"))
            {
                return belge_içeriği[5].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}