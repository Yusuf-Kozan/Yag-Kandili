using System;
using System.IO;
using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;
using Kilnevüg;

namespace Esas.VeriTabanı
{
    class Söyleşi
    {
        public static void Söyle(yeni_söz söz)
        {
            string komut_metni = $"INSERT INTO {TabloAdı()} (Söz, Söyleyen, Oturum, " +
                                "Tarih, Söyleşi, Başlatan_Paylaşım, Bu_İlk) VALUES " +
                                "(@söz, @söyleyen, @oturum, @tarih, @söyleşi, " +
                                "@başlatan_paylaşım, @bu_ilk);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@söz", söz.SÖZ);
            komut.Parameters.AddWithValue("@söyleyen", söz.SÖYLEYEN);
            komut.Parameters.AddWithValue("@oturum", söz.OTURUM);
            komut.Parameters.AddWithValue("@tarih", söz.TARİH);
            komut.Parameters.AddWithValue("@söyleşi", söz.SÖYLEŞİ);
            komut.Parameters.AddWithValue("@başlatan_paylaşım", söz.BAŞLATAN_PAYLAŞIM);
            komut.Parameters.AddWithValue("@bu_ilk", söz.BU_İLK);
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
        }
        private static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 6 && belge_içeriği[4].StartsWith("-"))
            {
                return belge_içeriği[4].Substring(1);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}