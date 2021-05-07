using System;
using System.IO;
using System.Globalization;
using MySql.Data.MySqlClient;
using Esas;
using Kilnevüg;

namespace Esas.VeriTabanı
{
    public class Üyelik
    {
        public static ÜyeBil KimlikKimin(string kullanıcı_kimliği)
        {
            //SELECT * FROM tablo_adı WHERE Kimlik=@kullanıcı_kimliği;
            string komut_metni = $"SELECT * FROM {TabloAdı()} WHERE Kimlik = @kullanıcı_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", kullanıcı_kimliği);
            ÜyeBil üye = new ÜyeBil();
            //CultureInfo TR = new CultureInfo("tr-TR");
            MySqlDataReader veri_okuyucu = komut.ExecuteReader();
            while (veri_okuyucu.Read())
            {
                üye = new ÜyeBil();
                üye.KULLANICI_ADI = veri_okuyucu["Kullanıcı_Adı"].ToString();
                üye.AD = veri_okuyucu["Ad"].ToString();
                üye.SOYADI = veri_okuyucu["Soyadı"].ToString();
                üye.ÜSTÜNLÜK = veri_okuyucu["Üstünlük"].ToString();
                üye.E_POSTA = veri_okuyucu["E_Posta"].ToString();
                üye.BAŞLANGIÇ = DateTime.Parse(veri_okuyucu["Başlangıç"].ToString());
                //DateTime.ParseExact(veri_okuyucu["Ne_Zaman"].ToString(), "yyyyMMddHHmmss", TR)
                üye.RESİM = veri_okuyucu["Resim"].ToString();
                üye.KİMLİK = veri_okuyucu["Kimlik"].ToString();
            }
            veri_okuyucu.Close(); veri_okuyucu = null;
            bağlantı.Close(); bağlantı = null;
            komut.Dispose(); komut = null;
            return üye;
        }
        private static string TabloAdı()
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