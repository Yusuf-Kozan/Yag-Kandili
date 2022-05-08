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
    internal class Arayış
    {
        internal static string[] KullanıcıAdınıAra(string aranan_söz)
        {
            string komut_metni = $"SELECT COUNT(Kullanıcı_Adı) FROM {Üyelik.TabloAdı()} " +
                                "WHERE Kullanıcı_Adı LIKE @aranan_söz;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@aranan_söz", $"%{aranan_söz}%");
            long sonuç_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();

            if (sonuç_niceliği < 1)
            {
                bağlantı.Close(); bağlantı.Dispose();
                return new string[0];
            }

            string[] sonuçlar = new string[sonuç_niceliği];

            komut_metni = $"SELECT Kullanıcı_Adı FROM {Üyelik.TabloAdı()} " +
                            "WHERE Kullanıcı_Adı LIKE @hepsi GROUP BY Kullanıcı_Adı " +
                            "ORDER BY (CASE " +
                            "WHEN Kullanıcı_Adı = @aranan_söz THEN 0 " + 
                            "WHEN Kullanıcı_Adı LIKE @başta THEN 1 " +
                            "WHEN Kullanıcı_Adı LIKE @ortada THEN 2 " +
                            "WHEN Kullanıcı_Adı LIKE @sonda THEN 3 " +
                            "ELSE 4 END), Kullanıcı_Adı;";
            komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@hepsi", $"%{aranan_söz}%");
            komut.Parameters.AddWithValue("@aranan_söz", aranan_söz);
            komut.Parameters.AddWithValue("@başta", $"{aranan_söz}_%");
            komut.Parameters.AddWithValue("@ortada", $"%_{aranan_söz}_%");
            komut.Parameters.AddWithValue("@sonda", $"%_{aranan_söz}");
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            long döngü_turu = 0;
            while (veri_okuyucu.Read())
            {
                sonuçlar[döngü_turu] = veri_okuyucu["Kullanıcı_Adı"].ToString();
                döngü_turu++;
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            return sonuçlar;
        }
    }
}