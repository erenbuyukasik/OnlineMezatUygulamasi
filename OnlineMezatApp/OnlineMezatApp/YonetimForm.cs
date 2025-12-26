using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Npgsql;
using System.Runtime.InteropServices;

namespace OnlineMezatApp
{
    public partial class YonetimForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private DataGridView dgvUrunler;
        private TextBox txtUrunAd, txtFiyat, txtSure, txtArama, txtAciklama;
        private Button btnEkle, btnGuncelle, btnSil, btnResimSec;
        private PictureBox pbxResim;
        private Label lblBaslik;

        private byte[] secilenResimBytes = null;

        public YonetimForm()
        {
            TasarimiElleKur();
        }

        private void TasarimiElleKur()
        {
            this.Text = "Bidly - Admin Ürün Yönetim Paneli";
            this.Size = new Size(950, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.White };
            pnlHeader.MouseDown += (s, e) => { ReleaseCapture(); SendMessage(this.Handle, 0x112, 0xf012, 0); };
            this.Controls.Add(pnlHeader);

            lblBaslik = new Label { Text = "Ürün Yönetim Merkezi", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(155, 89, 182), Location = new Point(20, 15), AutoSize = true };
            pnlHeader.Controls.Add(lblBaslik);

            Label lblKapat = new Label { Text = "X", ForeColor = Color.Gray, Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(910, 15), Cursor = Cursors.Hand };
            lblKapat.Click += (s, e) => this.Close();
            pnlHeader.Controls.Add(lblKapat);

            txtArama = new TextBox { Location = new Point(600, 18), Size = new Size(250, 30), Font = new Font("Segoe UI", 11), PlaceholderText = "Ürün ara..." };
            txtArama.TextChanged += TxtArama_TextChanged;
            pnlHeader.Controls.Add(txtArama);

            Panel pnlContent = new Panel { Location = new Point(0, 60), Size = new Size(950, 690) };
            this.Controls.Add(pnlContent);

            // --- GİRİŞ ALANLARI ---
            // 1. Ad
            pnlContent.Controls.Add(new Label { Text = "Ürün Adı:", Location = new Point(30, 20), AutoSize = true });
            txtUrunAd = new TextBox { Location = new Point(30, 40), Size = new Size(250, 30), Font = new Font("Segoe UI", 10) };
            pnlContent.Controls.Add(txtUrunAd);

            // 2. Fiyat
            pnlContent.Controls.Add(new Label { Text = "Fiyat (₺):", Location = new Point(30, 80), AutoSize = true });
            txtFiyat = new TextBox { Location = new Point(30, 100), Size = new Size(250, 30), Font = new Font("Segoe UI", 10) };
            pnlContent.Controls.Add(txtFiyat);

            // 3. Süre
            pnlContent.Controls.Add(new Label { Text = "Süre (Dakika):", Location = new Point(30, 140), AutoSize = true });
            txtSure = new TextBox { Location = new Point(30, 160), Size = new Size(250, 30), Font = new Font("Segoe UI", 10) };
            pnlContent.Controls.Add(txtSure);

            // 4. Açıklama
            pnlContent.Controls.Add(new Label { Text = "Açıklama:", Location = new Point(30, 200), AutoSize = true });
            txtAciklama = new TextBox { Location = new Point(30, 220), Size = new Size(250, 60), Font = new Font("Segoe UI", 10), Multiline = true };
            pnlContent.Controls.Add(txtAciklama);

            // --- RESİM ALANI ---
            pnlContent.Controls.Add(new Label { Text = "Ürün Görseli:", Location = new Point(320, 20), AutoSize = true });
            pbxResim = new PictureBox();
            pbxResim.Location = new Point(320, 40);
            pbxResim.Size = new Size(150, 150);
            pbxResim.BorderStyle = BorderStyle.FixedSingle;
            pbxResim.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxResim.BackColor = Color.WhiteSmoke;
            pnlContent.Controls.Add(pbxResim);

            btnResimSec = new Button { Text = "📷 Resim Seç", Location = new Point(320, 195), Size = new Size(150, 35), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnResimSec.Click += btnResimSec_Click;
            pnlContent.Controls.Add(btnResimSec);

            // --- BUTONLAR ---
            btnEkle = OlusturButon("➕ ÜRÜN EKLE", new Point(550, 40), Color.FromArgb(155, 89, 182));
            btnEkle.Click += btnEkle_Click;
            pnlContent.Controls.Add(btnEkle);

            btnGuncelle = OlusturButon("🔄 GÜNCELLE", new Point(550, 100), Color.FromArgb(41, 128, 185));
            btnGuncelle.Click += btnGuncelle_Click;
            pnlContent.Controls.Add(btnGuncelle);

            btnSil = OlusturButon("🗑️ ÜRÜNÜ SİL", new Point(550, 160), Color.FromArgb(231, 76, 60));
            btnSil.Click += btnSil_Click;
            pnlContent.Controls.Add(btnSil);

            // --- TABLO ---
            dgvUrunler = new DataGridView
            {
                Location = new Point(30, 300),
                Size = new Size(890, 350),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                RowHeadersVisible = false
            };
            pnlContent.Controls.Add(dgvUrunler);

            // Tablo Tıklama Olayı
            dgvUrunler.CellClick += (s, e) => {
                if (e.RowIndex >= 0)
                {
                    txtUrunAd.Text = dgvUrunler.Rows[e.RowIndex].Cells["Ürün"].Value.ToString();
                    txtFiyat.Text = dgvUrunler.Rows[e.RowIndex].Cells["Fiyat"].Value.ToString();

                    if (dgvUrunler.Rows[e.RowIndex].Cells["Açıklama"].Value != null)
                        txtAciklama.Text = dgvUrunler.Rows[e.RowIndex].Cells["Açıklama"].Value.ToString();
                    else
                        txtAciklama.Text = "";

                    try
                    {
                        var resimData = dgvUrunler.Rows[e.RowIndex].Cells["ResimData"].Value;
                        if (resimData != DBNull.Value)
                        {
                            byte[] resimBytes = (byte[])resimData;
                            using (MemoryStream ms = new MemoryStream(resimBytes))
                            {
                                pbxResim.Image = Image.FromStream(ms);
                            }
                            secilenResimBytes = resimBytes;
                        }
                        else
                        {
                            pbxResim.Image = null;
                            secilenResimBytes = null;
                        }
                    }
                    catch { pbxResim.Image = null; secilenResimBytes = null; }
                }
            };

            this.Load += (s, e) => Listele();
        }

        private Button OlusturButon(string text, Point loc, Color backColor)
        {
            return new Button { Text = text, Location = loc, Size = new Size(180, 45), BackColor = backColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
        }

        private void btnResimSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbxResim.Image = Image.FromFile(ofd.FileName);
                using (MemoryStream ms = new MemoryStream())
                {
                    pbxResim.Image.Save(ms, pbxResim.Image.RawFormat);
                    secilenResimBytes = ms.ToArray();
                }
            }
        }

        private void TxtArama_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = dgvUrunler.DataSource as DataTable;
            if (dt != null) dt.DefaultView.RowFilter = string.Format("Ürün LIKE '%{0}%'", txtArama.Text.Replace("'", "''"));
        }

        private void Listele()
        {
            try
            {
                using (var baglanti = Veritabani.BaglantiGetir())
                {
                    string sorgu = "SELECT urun_id as ID, urun_adi as Ürün, son_fiyat as Fiyat, durum as Durum, aciklama as Açıklama, resim as ResimData FROM urunler ORDER BY urun_id DESC";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvUrunler.DataSource = dt;
                    if (dgvUrunler.Columns["ResimData"] != null) dgvUrunler.Columns["ResimData"].Visible = false;
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                using (var baglanti = Veritabani.BaglantiGetir())
                {
                    if (baglanti.State != ConnectionState.Open) baglanti.Open();

                    string sorgu = "INSERT INTO urunler (urun_adi, baslangic_fiyati, son_fiyat, baslangic_tarihi, bitis_tarihi, durum, resim, aciklama) " +
                                   "VALUES (@p1, @p2, @p2, NOW(), NOW() + (@p3 || ' minutes')::interval, 'aktif', @p4, @p5)";

                    using (var komut = new NpgsqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@p1", txtUrunAd.Text);
                        komut.Parameters.AddWithValue("@p2", decimal.Parse(txtFiyat.Text));
                        komut.Parameters.AddWithValue("@p3", txtSure.Text);

                        if (secilenResimBytes != null) komut.Parameters.AddWithValue("@p4", secilenResimBytes);
                        else komut.Parameters.AddWithValue("@p4", DBNull.Value);

                        komut.Parameters.AddWithValue("@p5", txtAciklama.Text);

                        komut.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Ürün başarıyla eklendi!");
                Listele();
                Temizle();
            }
            catch (Exception ex) { MessageBox.Show("Ekleme Hatası: " + ex.Message); }
        }

        // --- DÜZELTİLEN KISIM: SÜRE GÜNCELLEME EKLENDİ ---
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dgvUrunler.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvUrunler.CurrentRow.Cells["ID"].Value);
            try
            {
                using (var baglanti = Veritabani.BaglantiGetir())
                {
                    if (baglanti.State != ConnectionState.Open) baglanti.Open();

                    // bitis_tarihi = NOW() + (@p6 || ' minutes')::interval EKLENDİ
                    string sorgu = "UPDATE urunler SET urun_adi=@p1, baslangic_fiyati=@p2, son_fiyat=@p2, resim=@p4, aciklama=@p5, bitis_tarihi = NOW() + (@p6 || ' minutes')::interval WHERE urun_id=@p3";

                    using (var komut = new NpgsqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@p1", txtUrunAd.Text);
                        komut.Parameters.AddWithValue("@p2", decimal.Parse(txtFiyat.Text));
                        komut.Parameters.AddWithValue("@p3", id);

                        if (secilenResimBytes != null) komut.Parameters.AddWithValue("@p4", secilenResimBytes);
                        else komut.Parameters.AddWithValue("@p4", DBNull.Value);

                        komut.Parameters.AddWithValue("@p5", txtAciklama.Text);

                        // SÜRE PARAMETRESİ (YENİ)
                        komut.Parameters.AddWithValue("@p6", txtSure.Text);

                        komut.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Ürün ve süresi başarıyla güncellendi!");
                Listele();
                Temizle();
            }
            catch (Exception ex) { MessageBox.Show("Güncelleme Hatası: " + ex.Message); }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvUrunler.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvUrunler.CurrentRow.Cells["ID"].Value);
            try
            {
                using (var baglanti = Veritabani.BaglantiGetir())
                {
                    if (baglanti.State != ConnectionState.Open) baglanti.Open();
                    string sorgu = "UPDATE urunler SET durum='pasif' WHERE urun_id = @p1";

                    using (var komut = new NpgsqlCommand(sorgu, baglanti))
                    {
                        komut.Parameters.AddWithValue("@p1", id);
                        komut.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Ürün silindi!");
                Listele();
                Temizle();
            }
            catch (Exception ex) { MessageBox.Show("Silme Hatası: " + ex.Message); }
        }

        private void Temizle()
        {
            txtUrunAd.Clear();
            txtFiyat.Clear();
            txtSure.Clear();
            txtAciklama.Clear();
            pbxResim.Image = null;
            secilenResimBytes = null;
        }
    }
}