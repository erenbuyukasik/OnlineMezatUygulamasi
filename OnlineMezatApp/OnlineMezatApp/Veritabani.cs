using Npgsql; // PostgreSQL kütüphanesini çağırdık
using System;
using System.Data;
using System.Windows.Forms;

namespace OnlineMezatApp
{
    public class Veritabani
    {
        // Bağlantı cümlesi: Veritabanının adresi ve şifresi burada
        private static string baglantiAdresi = "Host=localhost;Username=postgres;Password=123-123.aEren;Database=OnlineMezatDB";

        public static NpgsqlConnection BaglantiGetir()
        {
            NpgsqlConnection baglanti = new NpgsqlConnection(baglantiAdresi);

            try
            {
                // Bağlantıyı açmayı dene
                baglanti.Open();
            }
            catch (Exception ex)
            {
                // Hata verirse mesaj göster
                MessageBox.Show("Veritabanı bağlantı hatası: " + ex.Message);
            }

            return baglanti;
        }
    }
}