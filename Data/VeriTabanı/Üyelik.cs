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
    internal class Üyelik
    {
        internal static void ÜyeEkle(Üye üye, string kullanıcı_kimliği)
        {
            // Üye türü bir YAPIDIR, sınıf değildir.

            //INSERT INTO tablo_adı (Kullanıcı_Adı, Ad, Parola, Üstünlük, E_Posta, Başlangıç, Resim, Kimlik)
            //VALUES (@kullanıcı_adı, @ad, @parola, @üstünlük, @e_posta, @başlangıç, @resim, @kimlik);
            string komut_metni = $"INSERT INTO {TabloAdı()} (Kullanıcı_Adı, Ad, Parola, Üstünlük, " + 
                                "E_Posta, Başlangıç, Resim, Kimlik) VALUES (@kullanıcı_adı, @ad, " +
                                "@parola, @üstünlük, @e_posta, @başlangıç, @resim, @kimlik);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_adı", üye.KULLANICI_ADI);
            komut.Parameters.AddWithValue("@ad", üye.AD);
            komut.Parameters.AddWithValue("@parola", üye.PAROLA);
            komut.Parameters.AddWithValue("@üstünlük", üye.ÜSTÜNLÜK);
            komut.Parameters.AddWithValue("@e_posta", üye.E_POSTA);
            komut.Parameters.AddWithValue("@başlangıç", üye.BAŞLANGIÇ.ToString("yyyyMMddHHmmss"));
            komut.Parameters.AddWithValue("@resim", üye.RESİM);
            komut.Parameters.AddWithValue("@kimlik", kullanıcı_kimliği);
            komut.ExecuteNonQuery();
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;

            if (!Directory.Exists(üye.DizinYolu()))
            {
                Directory.CreateDirectory(üye.DizinYolu());
            }
        }
        internal static bool BilinenParolayıDeğiştir(string kullanıcı_kimliği, string eski_parola, string yeni_parola)
        {
            // Eski parola doğruysa parola değiştirilip True değeri döndürülür.
            // Eski parola yanlışsa işlem yapılmadan False değeri döndürülür.
            string ŞimdikiKarılmışParola = KullanıcınınParolaBilgileri(kullanıcı_kimliği);
            bool parola_doğru = Parolalar.ParolaDoğru(ŞimdikiKarılmışParola, eski_parola);
            if (parola_doğru)
            {
                byte[] tuz = Parolalar.YeniTuz();
                string YeniKarılmışParola = Parolalar.argon2id8_32_2_32(yeni_parola, tuz);
                
                string komut_metni = $"UPDATE {TabloAdı()} SET Parola = @yeni_parola " +
                                        "WHERE Kimlik = @kullanıcı_kimliği;";
                MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
                bağlantı.Open();
                MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
                komut.Parameters.AddWithValue("@yeni_parola", YeniKarılmışParola);
                komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
                komut.ExecuteNonQuery();
                komut.Dispose();
                bağlantı.Close(); bağlantı.Dispose();
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static void BilinmeyenParolayıDeğiştir(string kullanıcı_kimliği, string e_posta, string yeni_parola)
        {
            // Kullanıcı kimliği ile e-posta eşleşirse yeni parola atanır.

            byte[] tuz = Parolalar.YeniTuz();
            string karılmış_parola = Parolalar.argon2id8_32_2_32(yeni_parola, tuz);

            string komut_metni = $"UPDATE {TabloAdı()} SET Parola = @yeni_parola " +
                                "WHERE Kimlik = @kimlik AND " +
                                "E_Posta = @e_posta;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@yeni_parola", karılmış_parola);
            komut.Parameters.AddWithValue("@kimlik", kullanıcı_kimliği);
            komut.Parameters.AddWithValue("@e_posta", e_posta);
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
        }

        internal static ÜyeBil ÜyeBilgileri(string kullanıcı_kimliği)
        {
            string komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Kimlik = @kullanıcı_kimliği LIMIT 1;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            CultureInfo TR = new CultureInfo("tr-TR");
            ÜyeBil üye = new ÜyeBil();
            while (veri_okuyucu.Read())
            {
                üye.KULLANICI_ADI = veri_okuyucu["Kullanıcı_Adı"].ToString();
                üye.AD = veri_okuyucu["Ad"].ToString();
                üye.PAROLA = veri_okuyucu["Parola"].ToString();
                üye.ÜSTÜNLÜK = veri_okuyucu["Üstünlük"].ToString();
                üye.E_POSTA = veri_okuyucu["E_Posta"].ToString();
                üye.BAŞLANGIÇ = DateTime.ParseExact(veri_okuyucu["Başlangıç"].ToString(), "yyyyMMddHHmmss", TR);
                üye.RESİM = veri_okuyucu["Resim"].ToString();
                üye.KİMLİK = veri_okuyucu["Kimlik"].ToString();
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            komut.Dispose();
            return üye;
        }
        internal static parolasız_üye ParolasızÜyeBilgileri(string kullanıcı_kimliği)
        {
            string komut_metni = $"SELECT * FROM {TabloAdı()} " +
                                "WHERE Kimlik = @kullanıcı_kimliği " +
                                "LIMIT 1;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            CultureInfo TR = new CultureInfo("tr-TR");
            parolasız_üye üye = new parolasız_üye();
            while (veri_okuyucu.Read())
            {
                üye.KULLANICI_ADI = veri_okuyucu["Kullanıcı_Adı"].ToString();
                üye.AD = veri_okuyucu["Ad"].ToString();
                üye.ÜSTÜNLÜK = veri_okuyucu["Üstünlük"].ToString();
                üye.E_POSTA = veri_okuyucu["E_Posta"].ToString();
                üye.BAŞLANGIÇ = DateTime.ParseExact(veri_okuyucu["Başlangıç"].ToString(), "yyyyMMddHHmmss", TR);
                üye.RESİM = veri_okuyucu["Resim"].ToString();
                üye.KİMLİK = veri_okuyucu["Kimlik"].ToString();
            }
            veri_okuyucu.Close(); veri_okuyucu.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            komut.Dispose();
            return üye;
        }
        internal static ÜyeBil Kimliğinİyesi(string kullanıcı_kimliği)
        {
            //SELECT * FROM tablo_adı WHERE Kimlik=@kullanıcı_kimliği;
            string komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Kimlik = @kullanıcı_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            ÜyeBil üye = new ÜyeBil();
            CultureInfo TR = new CultureInfo("tr-TR");
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            while (veri_okuyucu.Read())
            {
                üye = new ÜyeBil();
                üye.KULLANICI_ADI = veri_okuyucu["Kullanıcı_Adı"].ToString();
                üye.AD = veri_okuyucu["Ad"].ToString();
                üye.ÜSTÜNLÜK = veri_okuyucu["Üstünlük"].ToString();
                üye.E_POSTA = veri_okuyucu["E_Posta"].ToString();
                üye.BAŞLANGIÇ = DateTime.ParseExact(veri_okuyucu["Başlangıç"].ToString(), "yyyyMMddHHmmss", TR);
                üye.RESİM = veri_okuyucu["Resim"].ToString();
                üye.KİMLİK = veri_okuyucu["Kimlik"].ToString();
            }
            veri_okuyucu.Close(); veri_okuyucu = null;
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;
            return üye;
        }
        internal static string KullanıcınınKimliği(string kullanıcı_adı)
        {
            string komut_metni = $"SELECT Kimlik FROM {TabloAdı()} WHERE Kullanıcı_Adı = @kullanıcı_adı";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_adı", kullanıcı_adı);
            string kullanıcı_kimliği = komut.ExecuteScalar().ToString();
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;
            return kullanıcı_kimliği;
        }
        internal static string KullanıcınınParolaBilgileri(string kullanıcı_kimliği)
        {
            string komut_metni = $"SELECT Parola FROM {TabloAdı()} WHERE Kimlik = @kimlik";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kimlik", kullanıcı_kimliği);
            string parola_bilgileri = komut.ExecuteScalar().ToString();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            return parola_bilgileri;
        }

        internal static bool KullanıcıAdıKullanımda(string kullanıcı_adı)
        {
            //SELECT COUNT(Kimlik) FROM tablo_adı WHERE Kullanıcı_Adı = @kullanıcı_adı;
            string komut_metni = $"SELECT COUNT(Kimlik) FROM {TabloAdı()} WHERE Kullanıcı_Adı = @kullanıcı_adı";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_adı", kullanıcı_adı);
            int kullanıcı_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            
            if (kullanıcı_niceliği == 0)
            {
                return false;
            }
            return true;
        }
        internal static bool EPostaKullanımda(string e_posta)
        {
            //SELECT COUNT(Kimlik) FROM tablo_adı WHERE E_Posta = @e_posta;
            string komut_metni = $"SELECT COUNT(Kimlik) FROM {TabloAdı()} WHERE E_Posta = @e_posta";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@e_posta", e_posta);
            int kullanıcı_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            
            if (kullanıcı_niceliği == 0)
            {
                return false;
            }
            return true;
        }
        internal static bool EPostaBuKullanıcının(string kullanıcı_adı, string e_posta)
        {
            string komut_metni = $"SELECT COUNT(Kimlik) FROM {TabloAdı()} WHERE E_Posta = @e_posta " +
                                "AND Kullanıcı_Adı = @kullanıcı_adı;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@e_posta", e_posta);
            komut.Parameters.AddWithValue("@kullanıcı_adı", kullanıcı_adı);
            int kullanıcı_niceliği = int.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();

            if (kullanıcı_niceliği == 1)
                return true;

            return false;
        }
        
        internal static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 5 && belge_içeriği[1].StartsWith("-"))
            {
                return belge_içeriği[1].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}