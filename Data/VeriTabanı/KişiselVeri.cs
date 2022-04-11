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
using Esas.KişiselVeriler;

namespace Esas.VeriTabanı
{
    internal class KişiselVeri
    {
        internal static void İşlemYaz(İşlemKaydı işlem)
        {
            string komut_metni = $"INSERT INTO {TabloAdı()} " +
                    "(Kullanıcı_Kimliği, İşlem, Tarih, İşlem_Kimliği) " +
                    "VALUES (@kullanıcı_kimliği, @işlem, @tarih, @işlem_kimliği);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", işlem.KULLANICI_KİMLİĞİ);
            komut.Parameters.AddWithValue("@işlem", işlem.İŞLEM);
            komut.Parameters.AddWithValue("@tarih", işlem.TARİH.ToString("yyyyMMddHHmmss"));
            komut.Parameters.AddWithValue("@işlem_kimliği", işlem.İŞLEM_KİMLİĞİ);
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
        }
        internal static bool BöyleİşlemVar(string işlem_kimliği)
        {
            string komut_metni = $"SELECT COUNT(İşlem_Kimliği) FROM {TabloAdı()} " +
                                "WHERE İşlem_Kimliği = @işlem_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@işlem_kimliği", işlem_kimliği);
            long kayıt_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            if (kayıt_niceliği == 0)
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
            if (belge_içeriği.Length >= 8 && belge_içeriği[7].StartsWith("-"))
            {
                return belge_içeriği[7].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}