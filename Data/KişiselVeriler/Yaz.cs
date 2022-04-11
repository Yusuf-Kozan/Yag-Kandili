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
using System.IO.Compression;
using Newtonsoft.Json;

namespace Esas.KişiselVeriler
{
    internal class BelgeyeYaz
    {
        internal static string VeriBelgeliğiOluştur(string kullanıcı_kimliği)
        {
            veri_derlemesi veriler = new veri_derlemesi(kullanıcı_kimliği);
            string kullanıcı_dizini = veriler.ÜYELİK_BİLGİLERİ.DizinYolu();
            string tarih = DateTime.Now.ToString("yyyyMMddHHmmss");
            string veri_dizini = Path.Combine(kullanıcı_dizini, $"kv{tarih}");
            Directory.CreateDirectory(veri_dizini);
            
            File.WriteAllText
            (
                Path.Combine(veri_dizini, "Üyelik_Bilgileri.json"),
                JsonConvert.SerializeObject(veriler.ÜYELİK_BİLGİLERİ,
                                            Formatting.Indented)
            );
            File.WriteAllText
            (
                Path.Combine(veri_dizini, "Yaptığı_Paylaşımlar.json"),
                JsonConvert.SerializeObject(veriler.YAPTIĞI_PAYLAŞIMLAR,
                                            Formatting.Indented)
            );
            File.WriteAllText
            (
                Path.Combine(veri_dizini, "Değerlendirdiği_Paylaşımlar.json"),
                JsonConvert.SerializeObject(veriler.DEĞERLENDİRDİĞİ_PAYLAŞIMLAR,
                                            Formatting.Indented)
            );
            File.WriteAllText
            (
                Path.Combine(veri_dizini, "Sözleri.json"),
                JsonConvert.SerializeObject(veriler.SÖZLERİ,
                                            Formatting.Indented)
            );
            File.WriteAllText
            (
                Path.Combine(veri_dizini, "Takip_Ettiği_Kişiler.json"),
                JsonConvert.SerializeObject(veriler.TAKİP_ETTİĞİ_KİŞİLER,
                                            Formatting.Indented)
            );
            File.WriteAllText
            (
                Path.Combine(veri_dizini, "Takip_Ettiği_Söyleşiler.json"),
                JsonConvert.SerializeObject(veriler.TAKİP_ETTİĞİ_SÖYLEŞİLER,
                                            Formatting.Indented)
            );

            string kullanıcı_adı = veriler.ÜYELİK_BİLGİLERİ.KULLANICI_ADI;
            string ZIP_konumu = Path.Combine(kullanıcı_dizini,
                            $"vd-{kullanıcı_adı}-{tarih}");
            ZipFile.CreateFromDirectory
            (
                veri_dizini,
                ZIP_konumu
            );
            Directory.Delete(veri_dizini, true);
            return ZIP_konumu;
        }
    }
}