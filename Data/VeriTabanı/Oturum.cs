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
using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;
using Kilnevüg;

namespace Esas.VeriTabanı
{
    internal class Oturum
    {
        internal static void OturumAç(string oturum_kimliği, string kullanıcı_kimliği)
        {
            // Kullanıcı_Kimliği, Oturum_Kimliği, Başlangıç_Tarihi, Son_Tarih
            string komut_metni = $"INSERT INTO {TabloAdı()}(Kullanıcı_Kimliği, Oturum_Kimliği, Başlangıç_Tarihi, Son_Tarih) "+
                        $"VALUES (@kullanıcı_kimliği, @oturum_kimliği, @başlangıç_tarihi, @son_tarih);";
            DateTime başlangıç_tarihi = DateTime.Now;
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            komut.Parameters.AddWithValue("@oturum_kimliği", oturum_kimliği);
            komut.Parameters.AddWithValue("@başlangıç_tarihi", başlangıç_tarihi.ToString("yyyyMMddHHmmss"));
            komut.Parameters.AddWithValue("@son_tarih", başlangıç_tarihi.AddHours(4).ToString("yyyyMMddHHmmss"));
            komut.ExecuteNonQuery();
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;
        }
        internal static void OturumKapat(string oturum_kimliği)
        {
            string komut_metni = $"DELETE FROM {TabloAdı()} WHERE Oturum_Kimliği = @oturum_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@oturum_kimliği", oturum_kimliği);
            komut.ExecuteNonQuery();
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;
        }
        internal static bool BuOturumAçık(string oturum_kimliği, string kullanıcı_kimliği)
        {
            string komut_metni = $"SELECT COUNT(Oturum_Kimliği) FROM {TabloAdı()} WHERE Oturum_Kimliği = @oturum_kimliği AND " +
                                $"Kullanıcı_Kimliği = @kullanıcı_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@oturum_kimliği", oturum_kimliği);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            int oturum_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            if (oturum_niceliği == 0)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return false;
            }

            komut_metni = $"SELECT Son_Tarih FROM {TabloAdı()} WHERE " + 
                            "Oturum_Kimliği = @oturum_kimliği AND Kullanıcı_Kimliği = @kullanıcı_kimliği;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@oturum_kimliği", oturum_kimliği);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            CultureInfo TR = new CultureInfo("tr-TR");
            DateTime son_tarih = DateTime.ParseExact(komut.ExecuteScalar().ToString(),
                                                    "yyyyMMddHHmmss", TR);
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            if (DateTime.Now.CompareTo(son_tarih) < 0)
            {
                return true;
            }
            return false;
        }
        internal static string Oturumunİyesi(string oturum_kimliği)
        {
            // Oturum kimliği bilinen kullanıcının kullanıcı kimliğini döndürür.
            string komut_metni = $"SELECT Kullanıcı_Kimliği FROM {TabloAdı()} " + 
                                    "WHERE Oturum_Kimliği = @oturum_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@oturum_kimliği", oturum_kimliği);
            string kullanıcı_kimliği = komut.ExecuteScalar().ToString();
            bağlantı.Close(); bağlantı.Dispose();
            komut.Dispose();
            return kullanıcı_kimliği;
        }
        internal static bool KimlikKullanımda(string oturum_kimliği)
        {
            string komut_metni = $"SELECT COUNT(Oturum_Kimliği) FROM {TabloAdı()} " +
                                    "WHERE Oturum_Kimliği = @oturum_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@oturum_kimliği", oturum_kimliği);
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
        private static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 5 && belge_içeriği[2].StartsWith("-"))
            {
                return belge_içeriği[2].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}