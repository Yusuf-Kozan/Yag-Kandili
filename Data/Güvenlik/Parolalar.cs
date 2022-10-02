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
    public static class Parolalar
    {
        public static string argon2id8_32_2_32(string parola, byte[] tuz)
        {
            //Argon2id ile parola karma fonksiyonu
            // 8 tekrar, 32 MiB bellek, 2 eş anlı işlem, 32 B çıktı
            byte[] byte_parola = System.Text.Encoding.Unicode.GetBytes(parola);
            CryptHash.Net.Hash.Argon2id argon2id = new CryptHash.Net.Hash.Argon2id();
            byte[] karma = argon2id.ComputeHash(byte_parola, 8, (32*1024), 2, 32, tuz).HashBytes;

            string sonuç = Convert.ToBase64String(karma);

            string tuz_metni = Convert.ToBase64String(tuz);
            sonuç = $"{sonuç}₺8,{(32*1024).ToString()},2,32₺{tuz_metni}";
            return sonuç;
        }
        public static bool ParolaDoğru(string karma, string girilen_parola)
        {
            //Argon2id ile parola doğrulama fonksiyonu
            string[] karım_bilgileri = karma.Split('₺');
                // 0: karılmış parola, 1: değişkenler
                // 2: tuz

            string[] karım_değişkenleri = karım_bilgileri[1].Split(',');
                // 0: tekrar, 1: bellek(KB), 2: eş anlı işlem sayısı
                // 3: çıktı boyutu(B)
            int tekrar = int.Parse(karım_değişkenleri[0]);
            int bellek = int.Parse(karım_değişkenleri[1]);
            int eş_anlılık = int.Parse(karım_değişkenleri[2]);
            int çıktı_boyutu = int.Parse(karım_değişkenleri[3]);
            
            byte[] tuz = Convert.FromBase64String(karım_bilgileri[2]);
            byte[] bilinen_karma = Convert.FromBase64String(karım_bilgileri[0]);

            CryptHash.Net.Hash.Argon2id argon2id = new CryptHash.Net.Hash.Argon2id();
            bool sonuç = argon2id.VerifyHash(
                bilinen_karma,
                System.Text.Encoding.Unicode.GetBytes(girilen_parola),
                tekrar, bellek, eş_anlılık, çıktı_boyutu,
                tuz
            ).Success;

            return sonuç;
        }
        internal static bool UnutulanParolayıYenile(
                            string kullanıcı_adı, string kullanıcı_kimliği,
                            string e_posta, string yeni_parola)
        {
            ÜyeBil kullanıcı = Esas.VeriTabanı.Üyelik.Kimliğinİyesi(kullanıcı_kimliği);
            if (kullanıcı.KULLANICI_ADI == kullanıcı_adı)
            {
                if (Esas.VeriTabanı.Üyelik.EPostaBuKullanıcının(kullanıcı_adı, e_posta))
                {
                    try
                    {
                        Esas.VeriTabanı.Üyelik.BilinmeyenParolayıDeğiştir(kullanıcı_kimliği,
                                                e_posta, yeni_parola);
                        string tarih = DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss");
                        Esas.Posta.GönderenSMTPBilgisi gönderen_smtp = new Esas.Posta.GönderenSMTPBilgisi();
                        gönderen_smtp.AyarBelgesiniOku();
                        Esas.Posta.GönderenIMAPBilgisi gönderen_imap = new Esas.Posta.GönderenIMAPBilgisi();
                        gönderen_imap.AyarBelgesiniOku();
                        Esas.Posta.PostaGönder.TekKullanıcıyaGönder(
                            gönderen_smtp,
                            gönderen_imap,
                            kullanıcı,
                            "Yağ Kandili Güvenlik Uyarısı",
                            $"Parolanız {tarih} tarihinde " +
                            "\"Parolamı Unuttum\" bölümünden değiştirildi."
                        );
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            else
                return false;
        }

        public static byte[] YeniTuz()
        {
            RandomNumberGenerator rastgele_sayı = RandomNumberGenerator.Create();
            byte[] sonuç = new byte[32];
            rastgele_sayı.GetBytes(sonuç);
            return sonuç;
        }
    }
}