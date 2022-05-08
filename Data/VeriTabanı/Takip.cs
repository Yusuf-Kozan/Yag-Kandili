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
    internal class Takip
    {
        internal static void TakipEt(Esas.Takip takip)
        {
            if (!TakipEdiliyor(takip.TAKİP_EDEN, takip.TAKİP_EDİLEN))
            {
                string komut_metni = $"INSERT INTO {TabloAdı()} (Takip_Eden, Takip_Edilen, " +
                                    "Takip_Düzeyi, Tarih) VALUES (@takip_eden, " +
                                    "@takip_edilen, @takip_düzeyi, @tarih);";
                MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
                bağlantı.Open();
                MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
                komut.Parameters.AddWithValue("@takip_eden", takip.TAKİP_EDEN);
                komut.Parameters.AddWithValue("@takip_edilen", takip.TAKİP_EDİLEN);
                komut.Parameters.AddWithValue("@takip_düzeyi", takip.TAKİP_DÜZEYİ);
                komut.Parameters.AddWithValue("@tarih", takip.TarihMetni());
                komut.ExecuteNonQuery();
                komut.Dispose();
                bağlantı.Close(); bağlantı.Dispose();
            }
        }
        internal static void TakibiBırak(string takip_eden, string takip_edilen)
        {
            if (TakipEdiliyor(takip_eden, takip_edilen))
            {
                string komut_metni = $"DELETE FROM {TabloAdı()} WHERE " +
                                    "Takip_Eden = @takip_eden AND " +
                                    "Takip_Edilen = @takip_edilen;";
                MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
                bağlantı.Open();
                MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
                komut.Parameters.AddWithValue("@takip_eden", takip_eden);
                komut.Parameters.AddWithValue("@takip_edilen", takip_edilen);
                komut.ExecuteNonQuery();
                komut.Dispose();
                bağlantı.Close(); bağlantı.Dispose();
            }
        }
        internal static string[,] TakipEdilenKullanıcılarınKimlikleri(string takip_eden)
        {
            // a sıra sayısı olursa [a, 0] = kullanıcı kimliği, [a, 1] = takip düzeyi
            string komut_metni = $"SELECT COUNT(Takip_Edilen) FROM {TabloAdı()}" +
                                " WHERE Takip_Eden = @takip_eden AND " +
                                "Takip_Edilen NOT LIKE '%₺%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            long takip_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            if (takip_niceliği < 1)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return new string[0,0];
            }

            komut_metni = $"SELECT Takip_Edilen, Takip_Düzeyi FROM {TabloAdı()} " +
                        "WHERE Takip_Eden = @takip_eden AND " +
                        "Takip_Edilen NOT LIKE '%₺%';";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            string[,] kullanıcılar = new string[takip_niceliği, 2];
            long döngü_turu = 0;

            while (veri_okuyucu.Read())
            {
                kullanıcılar[döngü_turu, 0] = veri_okuyucu["Takip_Edilen"].ToString();
                kullanıcılar[döngü_turu, 1] = veri_okuyucu["Takip_Düzeyi"].ToString();
                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return kullanıcılar;
        }
        internal static takip[] TakipEdilenKullanıcılar(string takip_eden)
        {
            string komut_metni = $"SELECT COUNT(Takip_Edilen) FROM {TabloAdı()} " +
                                "WHERE Takip_Eden = @takip_eden AND " +
                                "Takip_Edilen NOT LIKE '%₺%';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            long takip_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            if (takip_niceliği < 1)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return new takip[0];
            }

            komut_metni = $"SELECT * FROM {TabloAdı()} " +
                        "WHERE Takip_Eden = @takip_eden AND " +
                        "Takip_Edilen NOT LIKE '%₺%' " +
                        "ORDER BY Tarih DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            long tur = 0;
            takip[] takip_edilenler = new takip[takip_niceliği];
            CultureInfo TR = new CultureInfo("tr-TR");
            while (veri_okuyucu.Read())
            {
                takip_edilenler[tur].TAKİP_EDEN = veri_okuyucu["Takip_Eden"].ToString();
                takip_edilenler[tur].TAKİP_EDİLEN = veri_okuyucu["Takip_Edilen"].ToString();
                takip_edilenler[tur].TAKİP_DÜZEYİ = short.Parse(veri_okuyucu["Takip_Düzeyi"].ToString());
                takip_edilenler[tur].TARİH = DateTime.ParseExact(veri_okuyucu["Tarih"].ToString(),
                                                                "yyyyMMddHHmmss",
                                                                TR);

                tur++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return takip_edilenler;
        }
        internal static string[,] TakipEdilenSöyleşilerinKimlikleri(string takip_eden)
        {
            // a sıra sayısı olursa [a, 0] = söyleşi kimliği, [a, 1] = takip düzeyi
            string komut_metni = $"SELECT COUNT(Takip_Edilen) FROM {TabloAdı()}" +
                                " WHERE Takip_Eden = @takip_eden AND " +
                                "Takip_Edilen LIKE '%₺Y';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            long takip_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            if (takip_niceliği < 1)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return new string[0,0];
            }

            komut_metni = $"SELECT Takip_Edilen, Takip_Düzeyi FROM {TabloAdı()} " +
                        "WHERE Takip_Eden = @takip_eden AND " +
                        "Takip_Edilen LIKE '%₺Y';";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            string[,] söyleşiler = new string[takip_niceliği, 2];
            long döngü_turu = 0;

            while (veri_okuyucu.Read())
            {
                söyleşiler[döngü_turu, 0] = veri_okuyucu["Takip_Edilen"].ToString();
                söyleşiler[döngü_turu, 1] = veri_okuyucu["Takip_Düzeyi"].ToString();
                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return söyleşiler;
        }
        internal static takip[] TakipEdilenSöyleşiler(string takip_eden)
        {
            string komut_metni = $"SELECT COUNT(Takip_Edilen) FROM {TabloAdı()} " +
                                "WHERE Takip_Eden = @takip_eden AND " +
                                "Takip_Edilen LIKE '%₺Y';";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            long takip_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            if (takip_niceliği < 1)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return new takip[0];
            }

            komut_metni = $"SELECT * FROM {TabloAdı()} " +
                        "WHERE Takip_Eden = @takip_eden AND " +
                        "Takip_Edilen LIKE '%₺Y' " +
                        "ORDER BY Tarih DESC;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            long tur = 0;
            takip[] takip_edilenler = new takip[takip_niceliği];
            CultureInfo TR = new CultureInfo("tr-TR");
            while (veri_okuyucu.Read())
            {
                takip_edilenler[tur].TAKİP_EDEN = veri_okuyucu["Takip_Eden"].ToString();
                takip_edilenler[tur].TAKİP_EDİLEN = veri_okuyucu["Takip_Edilen"].ToString();
                takip_edilenler[tur].TAKİP_DÜZEYİ = short.Parse(veri_okuyucu["Takip_Düzeyi"].ToString());
                takip_edilenler[tur].TARİH = DateTime.ParseExact(veri_okuyucu["Tarih"].ToString(),
                                                                "yyyyMMddHHmmss",
                                                                TR);

                tur++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return takip_edilenler;
        }
        internal static long TakipçiNiceliği(string takip_edilen)
        {
            string komut_metni = $"SELECT COUNT(Takip_Eden) FROM {TabloAdı()} " +
                                "WHERE Takip_Edilen = @takip_edilen;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_edilen", takip_edilen);
            long nicelik = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return nicelik;
        }
        internal static bool TakipEdiliyor(string takip_eden, string takip_edilen)
        {
            string komut_metni = $"SELECT COUNT(Takip_Edilen) FROM {TabloAdı()} " +
                                "WHERE Takip_Eden = @takip_eden AND " +
                                "Takip_Edilen = @takip_edilen;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@takip_eden", takip_eden);
            komut.Parameters.AddWithValue("@takip_edilen", takip_edilen);
            int nicelik = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            if (nicelik == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        internal static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 7 && belge_içeriği[6].StartsWith("-"))
            {
                return belge_içeriği[6].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}