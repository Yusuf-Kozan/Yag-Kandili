using System;
using System.IO;
using Newtonsoft.Json;

namespace Esas.KişiselVeriler
{
    internal class BelgeyeYaz
    {
        internal void JSON_BelgeleriOluştur(string kullanıcı_kimliği)
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
        }
    }
}