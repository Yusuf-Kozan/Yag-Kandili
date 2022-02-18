using System;
using System.IO;
using Esas.VeriTabanı;

namespace Esas.KişiselVeriler
{
    internal struct veri_derlemesi
    {
        parolasız_üye ÜYELİK_BİLGİLERİ;
        paylaşım[] YAPTIĞI_PAYLAŞIMLAR;
        değerli_paylaşım[] DEĞERLENDİRDİĞİ_PAYLAŞIMLAR;
        köklü_söz[] SÖZLERİ;
        takip[] TAKİP_ETTİĞİ_KİŞİLER;
        takip[] TAKİP_ETTİĞİ_SÖYLEŞİLER;

        internal veri_derlemesi(string kullanıcı_kimliği)
        {
            ÜYELİK_BİLGİLERİ = Üyelik.ParolasızÜyeBilgileri(kullanıcı_kimliği);
            YAPTIĞI_PAYLAŞIMLAR = VeriTabanı.Paylaşım.KişininTümPaylaşımları(kullanıcı_kimliği);
            DEĞERLENDİRDİĞİ_PAYLAŞIMLAR = 
                        VeriTabanı.Beğeni.KişininDeğerlendirdiğiPaylaşımlar(kullanıcı_kimliği);
            SÖZLERİ = Söyleşi.KişininSöyledikleri(kullanıcı_kimliği);
            TAKİP_ETTİĞİ_KİŞİLER = VeriTabanı.Takip.TakipEdilenKullanıcılar(kullanıcı_kimliği);
            TAKİP_ETTİĞİ_SÖYLEŞİLER = VeriTabanı.Takip.TakipEdilenSöyleşiler(kullanıcı_kimliği);
        }
    }
}