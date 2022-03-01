using System;
using System.IO;
using System.Globalization;
using MySql.Data.MySqlClient;
using Esas;
using Esas.KişiselVeriler;

namespace Esas.VeriTabanı
{
    internal class KişiselVeri
    {
        internal static void İşlemYaz(İşlemKaydı işlem)
        {
            string komut_metni = $"INSERT INTO {TabloAdı()} " +
                    "(Kullanıcı_Kimliği, İşlem, Tarih, İşlem_Kimliği) " +
                    "VALUES (@kullanıcı_kimliği, @işlem, @tarih, @işlem_kimliği);";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@kullanıcı_kimliği", işlem.KULLANICI_KİMLİĞİ);
            komut.Parameters.AddWithValue("@işlem", işlem.İŞLEM);
            komut.Parameters.AddWithValue("@tarih", işlem.TARİH.ToString("yyyyMMddHHmmss"));
            komut.Parameters.AddWithValue("@işlem_kimliği", işlem.İŞLEM_KİMLİĞİ);
            komut.ExecuteNonQuery();
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
        }
        internal static bool BöyleİşlemVar(string işlem_kimliği)
        {
            string komut_metni = $"SELECT COUNT(İşlem_Kimliği) FROM {TabloAdı()} " +
                                "WHERE İşlem_Kimliği = @işlem_kimliği;";
            MySqlConnection bağlantı = new MySqlConnection(Bağlantı.bağlantı_dizesi);
            bağlantı.Open();
            MySqlCommand komut = new MySqlCommand(komut_metni, bağlantı);
            komut.Parameters.AddWithValue("@işlem_kimliği", işlem_kimliği);
            long kayıt_niceliği = long.Parse(komut.ExecuteScalar().ToString());
            komut.Dispose();
            bağlantı.Close(); bağlantı.Dispose();
            if (kayıt_niceliği == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        internal static string TabloAdı()
        {
            string[] belge_içeriği = File.ReadAllLines("./.Ayarlar/vt2");
            if (belge_içeriği.Length >= 8 && belge_içeriği[7].StartsWith("-"))
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