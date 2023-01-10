/*
Copyright (C) 2022-2023 Yusuf Kozan

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
namespace Esas.VeriTabanı
{
    internal class Bağlantı
    {
        internal static string bağlantı_dizesi = $"Server={MySQL_SunucuAdresi()[0]};Port={MySQL_SunucuAdresi()[1]};" + 
                                        $"Database={VeriTabanınınAdı()};User ID={MySQL_KullanıcıAdıİleParola()[0]};" + 
                                        $"Password={MySQL_KullanıcıAdıİleParola()[1]};Pooling=false;";
        private static string[] MySQL_SunucuAdresi()
        {
            // Sunucu ve Port
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt1");
            if (belge_içeriği.Length >= 4 &&
                belge_içeriği[0].StartsWith(">") && belge_içeriği[1].StartsWith(">"))
            {
                string[] adres_bilgileri = new string[2];
                adres_bilgileri[0] = belge_içeriği[0].Substring(1);//sunucu
                adres_bilgileri[1] = belge_içeriği[1].Substring(1);//port
                return adres_bilgileri;
            }
            else
            {
                return new string[]{String.Empty, String.Empty};
            }
        }
        private static string[] MySQL_KullanıcıAdıİleParola()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt1");
            if (belge_içeriği[2].StartsWith(">") && belge_içeriği[3].StartsWith(">"))
            {
                string[] kullanıcıAdıİleParola = new string[2];
                kullanıcıAdıİleParola[0] = belge_içeriği[2].Substring(1);//kullanıcı adı
                if (belge_içeriği[3].Length > 1) //parola girilmiş
                {
                    kullanıcıAdıİleParola[1] = belge_içeriği[3].Substring(1);//parola
                }
                else //parola yok
                {
                    kullanıcıAdıİleParola[1] = String.Empty;
                }
                return kullanıcıAdıİleParola;
            }
            else
            {
                return new string[]{String.Empty, String.Empty};
            }
        }
        private static string VeriTabanınınAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği[0].StartsWith(">"))
            {
                return belge_içeriği[0].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}