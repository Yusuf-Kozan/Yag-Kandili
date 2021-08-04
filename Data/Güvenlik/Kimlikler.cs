using System;
using Esas;

namespace Kilnevüg
{
    public class TY_Kimlikleri
    {
        public static string Kimlik(string tür, string kimden, string neye, string ne_zaman, string içerik, string oturum)
        {
            CryptHash.Net.Hash.Hash.SHA512 sha512 = new CryptHash.Net.Hash.Hash.SHA512();
            string karılmamış = kimden + neye + ne_zaman + içerik + oturum;
            string karılmış = sha512.ComputeHash(karılmamış).HashString;
            return $"{karılmış}₺{tür[0]}";
        }
    }
    public class PaylaşımKimliği
    {
        //Paylaşımların Kimlik2 değişkenini hesaplamak için
        public static string Kimlik2(Paylaşım paylaşım)
        {
            CryptHash.Net.Hash.Hash.SHA512 sha512 = new CryptHash.Net.Hash.Hash.SHA512();
            string zaman = paylaşım.TARİH.ToString("yyyyMMddHHmmss");
            string karılmamış = paylaşım.BAŞLIK + paylaşım.İÇERİK + paylaşım.PAYLAŞAN + paylaşım.OTURUM + zaman;
            return sha512.ComputeHash(karılmamış).HashString;
        }
        public static string Kimlik2(string başlık, string içerik, string paylaşan, string oturum, DateTime tarih)
        {
            CryptHash.Net.Hash.Hash.SHA512 sha512 = new CryptHash.Net.Hash.Hash.SHA512();
            string zaman = tarih.ToString("yyyyMMddHHmmss");
            string karılmamış = başlık + içerik + paylaşan + oturum + zaman;
            return sha512.ComputeHash(karılmamış).HashString;
        }
    }
    public class KullanıcıKimliği
    {
        public static string Kimlik(string kullanıcı_adı, string ad, string soyadı,
                                    string e_posta, DateTime başlangıç_tarihi)
        {
            CryptHash.Net.Hash.Hash.SHA512 sha512 = new CryptHash.Net.Hash.Hash.SHA512();
            string tarih = başlangıç_tarihi.ToString("yyyyMMddHHmmss");
            string karılmamış = kullanıcı_adı + ad + tarih + soyadı + e_posta;
            return sha512.ComputeHash(karılmamış).HashString;
        }
        public static string Kimlik(Üye üye)
        {
            CryptHash.Net.Hash.Hash.SHA512 sha512 = new CryptHash.Net.Hash.Hash.SHA512();
            string tarih = üye.BAŞLANGIÇ.ToString("yyyyMMddHHmmss");
            string karılmamış = üye.KULLANICI_ADI + üye.AD + tarih + üye.SOYADI + üye.E_POSTA;
            return sha512.ComputeHash(karılmamış).HashString;
        }
    }
}