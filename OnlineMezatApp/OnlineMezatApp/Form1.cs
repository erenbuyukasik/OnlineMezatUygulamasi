using Npgsql;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace OnlineMezatApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Şifre karakterini gizli yap (İsteğe bağlı, güvenlik için iyi olur)
            if (txtSifre != null) txtSifre.PasswordChar = '●';
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string sifre = txtSifre.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Lütfen E-Posta ve Şifre alanlarını doldurunuz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var baglanti = Veritabani.BaglantiGetir())
                {
                    // SORGUMUZU GÜNCELLEDİK: Rol ve Ad'ın yanına ID bilgisini de ekledik
                    string sorgu = "SELECT kullanici_id, ad_soyad, rol FROM kullanicilar WHERE email=@p1 AND sifre=@p2";

                    using (var komut = new Npgsql.NpgsqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@p1", email);
                        komut.Parameters.AddWithValue("@p2", sifre);

                        if (baglanti.State != ConnectionState.Open)
                            baglanti.Open();

                        using (var okuyucu = komut.ExecuteReader())
                        {
                            if (okuyucu.Read())
                            {
                                string adSoyad = okuyucu["ad_soyad"].ToString();
                                string rol = okuyucu["rol"].ToString();

                                // --- YENİ EKLENEN KISIM: ID'Yİ ALIYORUZ ---
                                int id = Convert.ToInt32(okuyucu["kullanici_id"]);
                                // ------------------------------------------

                                MessageBox.Show($"Hoşgeldiniz, {adSoyad}!\nSistemdeki Rolünüz: {rol}", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // --- DEĞİŞİKLİK BURADA: ID'Yİ ANA FORMA GÖNDERİYORUZ ---
                                AnaForm anaMenü = new AnaForm(rol, id);
                                // -------------------------------------------------------

                                anaMenü.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Hatalı E-Posta veya Şifre girdiniz!", "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı bağlantı hatası:\n" + ex.Message, "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}