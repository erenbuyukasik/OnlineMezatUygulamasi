using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
using System.Runtime.InteropServices;

namespace OnlineMezatApp
{
    public partial class TekliflerimForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        // Artık ID'ye göre filtreleme yapmadığımız için bu değişkeni sadece form yapısını bozmamak adına tutuyoruz
        private int aktifKullaniciId;

        // Yapıcı Metot
        public TekliflerimForm(int id)
        {
            InitializeComponent();
            this.aktifKullaniciId = id;

            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(900, 500); // İsim sütunu sığsın diye genişliği biraz artırdım
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.BackColor = Color.White;

            ArayuzOlustur();
            VerileriYukle();
        }

        private DataGridView dgvTeklifler;

        private void ArayuzOlustur()
        {
            Panel pnlHeader = new Panel();
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 50;
            pnlHeader.BackColor = Color.FromArgb(155, 89, 182); // Mor Tema
            pnlHeader.MouseDown += (s, e) => { ReleaseCapture(); SendMessage(this.Handle, 0x112, 0xf012, 0); };
            this.Controls.Add(pnlHeader);

            Label lblBaslik = new Label();
            lblBaslik.Text = "TÜM TEKLİFLER GEÇMİŞİ"; // Başlığı içeriğe uygun güncelledim
            lblBaslik.ForeColor = Color.White;
            lblBaslik.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblBaslik.AutoSize = true;
            lblBaslik.Location = new Point(20, 13);
            pnlHeader.Controls.Add(lblBaslik);

            Label lblKapat = new Label();
            lblKapat.Text = "X";
            lblKapat.ForeColor = Color.White;
            lblKapat.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblKapat.Location = new Point(860, 15);
            lblKapat.Cursor = Cursors.Hand;
            lblKapat.Click += (s, e) => this.Close();
            pnlHeader.Controls.Add(lblKapat);

            dgvTeklifler = new DataGridView();
            dgvTeklifler.Location = new Point(20, 70);
            dgvTeklifler.Size = new Size(860, 400);
            dgvTeklifler.BackgroundColor = Color.White;
            dgvTeklifler.BorderStyle = BorderStyle.None;
            dgvTeklifler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTeklifler.RowHeadersVisible = false;
            dgvTeklifler.AllowUserToAddRows = false;
            dgvTeklifler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTeklifler.ReadOnly = true;

            // Tasarım Ayarları (Aynı Kaldı)
            dgvTeklifler.EnableHeadersVisualStyles = false;
            dgvTeklifler.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(155, 89, 182);
            dgvTeklifler.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTeklifler.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvTeklifler.ColumnHeadersHeight = 35;

            dgvTeklifler.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvTeklifler.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 230, 250);
            dgvTeklifler.DefaultCellStyle.SelectionForeColor = Color.Black;

            this.Controls.Add(dgvTeklifler);
        }

        private void VerileriYukle()
        {
            try
            {
                using (var baglanti = Veritabani.BaglantiGetir())
                {
                    if (baglanti.State != ConnectionState.Open) baglanti.Open();

                    // --- KRİTİK DEĞİŞİKLİK ---
                    // 1. "WHERE t.kullanici_id = @kid" KISMINI SİLDİM (Böylece herkesin teklifi görünür)
                    // 2. "k.ad_soyad" EKLENDİ (Teklifi kimin verdiği görünür)
                    string sql = @"
                        SELECT 
                            k.ad_soyad AS ""Teklif Veren"",
                            u.urun_adi AS ""Ürün Adı"",
                            t.teklif_miktari AS ""Teklif (₺)"",
                            t.teklif_zamani AS ""Tarih""
                        FROM teklifler t
                        JOIN urunler u ON t.urun_id = u.urun_id
                        JOIN kullanicilar k ON t.kullanici_id = k.kullanici_id
                        ORDER BY t.teklif_zamani DESC";

                    using (var da = new NpgsqlDataAdapter(sql, baglanti))
                    {
                        // Parametreye gerek kalmadı çünkü tüm listeyi çekiyoruz
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvTeklifler.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}