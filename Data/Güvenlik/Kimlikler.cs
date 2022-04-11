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
using System.Security.Cryptography;
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
        public static string Kimlik(string kullanıcı_adı, string ad,
                                    string e_posta, DateTime başlangıç_tarihi)
        {
            CryptHash.Net.Hash.Hash.SHA512 sha512 = new CryptHash.Net.Hash.Hash.SHA512();
            string tarih = başlangıç_tarihi.ToString("yyyyMMddHHmmss");
            string karılmamış = kullanıcı_adı + ad + tarih + e_posta;
            return sha512.ComputeHash(karılmamış).HashString;
        }
        public static string Kimlik(Üye üye)
        {
            CryptHash.Net.Hash.Hash.SHA512 sha512 = new CryptHash.Net.Hash.Hash.SHA512();
            string tarih = üye.BAŞLANGIÇ.ToString("yyyyMMddHHmmss");
            string karılmamış = üye.KULLANICI_ADI + üye.AD + tarih + üye.E_POSTA;
            return sha512.ComputeHash(karılmamış).HashString;
        }
    }
    public class OturumKimliği
    {
        public static string YeniKimlik()
        {
            string gelişigüzel = GelişigüzelYazı();
            bool kimlik_kullanımda = Esas.VeriTabanı.Oturum.KimlikKullanımda(gelişigüzel);

            while (kimlik_kullanımda)
            {
                gelişigüzel = GelişigüzelYazı();
                kimlik_kullanımda = Esas.VeriTabanı.Oturum.KimlikKullanımda(gelişigüzel);
            }
            
            return gelişigüzel;
        }
        public static string GelişigüzelYazı()
        {
            RandomNumberGenerator üreteç = RandomNumberGenerator.Create();
            byte[] ikili_yazı = new byte[32];
            üreteç.GetBytes(ikili_yazı);
            string yazı = Convert.ToBase64String(ikili_yazı);
            return yazı;
        }
    }
    internal class KV_İşlemKimliği
    {
        public static string YeniKimlik()
        {
            string gelişigüzel = GelişigüzelYazı();
            bool kimlik_kullanımda = Esas.VeriTabanı.KişiselVeri.BöyleİşlemVar(gelişigüzel);

            while (kimlik_kullanımda)
            {
                gelişigüzel = GelişigüzelYazı();
                kimlik_kullanımda = Esas.VeriTabanı.KişiselVeri.BöyleİşlemVar(gelişigüzel);
            }
            
            return gelişigüzel;
        }
        public static string GelişigüzelYazı()
        {
            RandomNumberGenerator üreteç = RandomNumberGenerator.Create();
            byte[] ikili_yazı = new byte[32];
            üreteç.GetBytes(ikili_yazı);
            string yazı = Convert.ToBase64String(ikili_yazı);
            return yazı;
        }
    }
}