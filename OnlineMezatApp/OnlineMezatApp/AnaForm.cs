using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Npgsql;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Collections.Generic; 
using System.Linq; 

namespace OnlineMezatApp
{
    public partial class AnaForm : Form
    {
        public string kullaniciRolu = string.Empty;
        public int GirisYapanId;

        private System.Windows.Forms.Timer timerYenile;
        private bool hataGosterildi = false;

        
        private Button btnYeniKucult, btnYeniBuyut, btnYeniKapat;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public AnaForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            timerYenile = new System.Windows.Forms.Timer();
            timerYenile.Interval = 1000;

            timerYenile.Tick += (s, e) => {
                SuresiBitenleriIsleVeSat();
                if (!flpVitrin.ContainsFocus && (txtArama == null || !txtArama.Focused))
                {
                    VitrinYukle();
                }
            };
        }

        public AnaForm(string rol, int id) : this()
        {
            this.kullaniciRolu = rol ?? string.Empty;
            this.GirisYapanId = id;
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {
            if (this.kullaniciRolu == "admin") btnUrunYonetimi.Visible = true;
            else btnUrunYonetimi.Visible = false;

            VeritabaniniZorlaDuzelt();

            if (flpVitrin != null)
            {
                
                typeof(Panel).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null, flpVitrin, new object[] { true });

                flpVitrin.Width = this.ClientSize.Width - flpVitrin.Left - 20;
                flpVitrin.Height = this.ClientSize.Height - flpVitrin.Top - 20;
                flpVitrin.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }

            if (btnHesapDegistir != null)
            {
                btnHesapDegistir.Location = new Point(btnHesapDegistir.Location.X, this.ClientSize.Height - 70);
                btnHesapDegistir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            }

            if (btnExit != null) btnExit.Visible = false;

            PencereKontrolleriniOlustur();

            pnlTitleBar.MouseDown += pnlTitleBar_MouseDown;
            if (txtArama != null) txtArama.TextChanged += txtArama_TextChanged;

            VitrinYukle();
            timerYenile.Start();
        }

        private void PencereKontrolleriniOlustur()
        {
            int btnWidth = 45;
            int btnHeight = 30;
            Font btnFont = new Font("Segoe UI", 10, FontStyle.Regular);

            
            btnYeniKapat = new Button();
            btnYeniKapat.Text = "✕";
            btnYeniKapat.Size = new Size(btnWidth, btnHeight);
            btnYeniKapat.Location = new Point(pnlTitleBar.Width - btnWidth, 0);
            btnYeniKapat.FlatStyle = FlatStyle.Flat;
            btnYeniKapat.FlatAppearance.BorderSize = 0;
            btnYeniKapat.BackColor = Color.Transparent;
            btnYeniKapat.ForeColor = Color.Black;
            btnYeniKapat.Font = btnFont;
            btnYeniKapat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnYeniKapat.MouseEnter += (s, e) => { btnYeniKapat.BackColor = Color.Red; btnYeniKapat.ForeColor = Color.White; };
            btnYeniKapat.MouseLeave += (s, e) => { btnYeniKapat.BackColor = Color.Transparent; btnYeniKapat.ForeColor = Color.Black; };
            btnYeniKapat.Click += (s, e) => Application.Exit();
            pnlTitleBar.Controls.Add(btnYeniKapat);

            
            btnYeniBuyut = new Button();
            btnYeniBuyut.Text = "⬜";
            btnYeniBuyut.Size = new Size(btnWidth, btnHeight);
            btnYeniBuyut.Location = new Point(pnlTitleBar.Width - (btnWidth * 2), 0);
            btnYeniBuyut.FlatStyle = FlatStyle.Flat;
            btnYeniBuyut.FlatAppearance.BorderSize = 0;
            btnYeniBuyut.BackColor = Color.Transparent;
            btnYeniBuyut.ForeColor = Color.Black;
            btnYeniBuyut.Font = new Font("Segoe UI", 11);
            btnYeniBuyut.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnYeniBuyut.MouseEnter += (s, e) => { btnYeniBuyut.BackColor = Color.LightGray; };
            btnYeniBuyut.MouseLeave += (s, e) => { btnYeniBuyut.BackColor = Color.Transparent; };
            btnYeniBuyut.Click += (s, e) =>
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.WindowState = FormWindowState.Maximized;
                    this.Region = null;
                    btnYeniBuyut.Text = "❐";
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                    this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
                    btnYeniBuyut.Text = "⬜";
                }
            };
            pnlTitleBar.Controls.Add(btnYeniBuyut);

            
            btnYeniKucult = new Button();
            btnYeniKucult.Text = "―";
            btnYeniKucult.Size = new Size(btnWidth, btnHeight);
            btnYeniKucult.Location = new Point(pnlTitleBar.Width - (btnWidth * 3), 0);
            btnYeniKucult.FlatStyle = FlatStyle.Flat;
            btnYeniKucult.FlatAppearance.BorderSize = 0;
            btnYeniKucult.BackColor = Color.Transparent;
            btnYeniKucult.ForeColor = Color.Black;
            btnYeniKucult.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnYeniKucult.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnYeniKucult.MouseEnter += (s, e) => { btnYeniKucult.BackColor = Color.LightGray; };
            btnYeniKucult.MouseLeave += (s, e) => { btnYeniKucult.BackColor = Color.Transparent; };
            btnYeniKucult.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
            pnlTitleBar.Controls.Add(btnYeniKucult);
        }

        private void SuresiBitenleriIsleVeSat()
        {
            try
            {
                using (var baglanti = Veritabani.BaglantiGetir())
                {
                    if (baglanti.State != ConnectionState.Open) baglanti.Open();
                    using (var cmd = new NpgsqlCommand("CALL ihale_sonlandir()", baglanti))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                if (!hataGosterildi)
                {
                    timerYenile.Stop();
                    MessageBox.Show("Prosedür Hatası: " + ex.Message);
                    hataGosterildi = true;
                }
            }
        }

        
        public void VitrinYukle()
        {
            if (flpVitrin.ContainsFocus) return;

            

            try
            {
                using (var baglanti = Veritabani.BaglantiGetir())
                {
                    if (baglanti.State != ConnectionState.Open) baglanti.Open();

                    string sorgu = "SELECT urun_id, urun_adi, son_fiyat, bitis_tarihi, resim, aciklama FROM urunler WHERE durum = 'aktif'";

                    if (!string.IsNullOrEmpty(txtArama.Text)) sorgu += " AND urun_adi ILIKE @aranan";
                    sorgu += " ORDER BY urun_id DESC";

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                    if (!string.IsNullOrEmpty(txtArama.Text)) da.SelectCommand.Parameters.AddWithValue("@aranan", "%" + txtArama.Text + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    
                    List<string> guncelIdler = new List<string>();

                    foreach (DataRow satir in dt.Rows)
                    {
                        string urunIdStr = satir["urun_id"].ToString();
                        string panelName = "pnl_" + urunIdStr;
                        guncelIdler.Add(panelName);

                        
                        Control[] bulunanlar = flpVitrin.Controls.Find(panelName, false);

                        if (bulunanlar.Length > 0)
                        {
                            
                            Panel mevcutKart = (Panel)bulunanlar[0];

                            
                            Control lblSure = mevcutKart.Controls["lblSure_" + urunIdStr];
                            Control lblFiyat = mevcutKart.Controls["lblFiyat_" + urunIdStr];

                            
                            DateTime bitis = Convert.ToDateTime(satir["bitis_tarihi"]);
                            TimeSpan kalan = bitis - DateTime.Now;
                            string kalanYazi = kalan.TotalSeconds > 0 ? $"⏱ {kalan.Hours}sa {kalan.Minutes}dk {kalan.Seconds}sn" : "Süre Doldu";
                            Color sureRenk = kalan.TotalSeconds > 0 ? Color.FromArgb(231, 76, 60) : Color.DarkRed;

                            if (lblSure != null)
                            {
                                lblSure.Text = kalanYazi;
                                lblSure.ForeColor = sureRenk;
                            }

                            
                            decimal guncelFiyat = Convert.ToDecimal(satir["son_fiyat"]);
                            if (lblFiyat != null)
                            {
                                lblFiyat.Text = guncelFiyat.ToString("N2") + " ₺";
                            }
                        }
                        else
                        {
                            
                            Panel kart = KartOlustur(satir);
                            flpVitrin.Controls.Add(kart);
                            
                            flpVitrin.Controls.SetChildIndex(kart, dt.Rows.IndexOf(satir));
                        }
                    }

                    
                    for (int i = flpVitrin.Controls.Count - 1; i >= 0; i--)
                    {
                        Control c = flpVitrin.Controls[i];
                        if (!guncelIdler.Contains(c.Name))
                        {
                            flpVitrin.Controls.Remove(c);
                            c.Dispose(); 
                        }
                    }
                }
            }
            catch { }
        }

        private Panel KartOlustur(DataRow satir)
        {
            string idStr = satir["urun_id"].ToString();
            int urunId = Convert.ToInt32(satir["urun_id"]);

            Panel kart = new Panel();
            kart.Name = "pnl_" + idStr; 
            kart.Size = new Size(240, 390);
            kart.BackColor = Color.White;
            kart.Margin = new Padding(15);
            kart.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 240, 390, 20, 20));

            
            PictureBox pbx = new PictureBox();
            pbx.Size = new Size(240, 160);
            pbx.Location = new Point(0, 0);
            pbx.SizeMode = PictureBoxSizeMode.Zoom;
            pbx.BackColor = Color.WhiteSmoke;

            if (satir["resim"] != DBNull.Value)
            {
                using (MemoryStream ms = new MemoryStream((byte[])satir["resim"]))
                {
                    pbx.Image = Image.FromStream(ms);
                }
            }
            kart.Controls.Add(pbx);

            
            Label lblAd = new Label
            {
                Text = satir["urun_adi"].ToString(),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),
                Location = new Point(10, 170),
                Size = new Size(220, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true
            };
            kart.Controls.Add(lblAd);

            
            DateTime bitis = Convert.ToDateTime(satir["bitis_tarihi"]);
            TimeSpan kalan = bitis - DateTime.Now;
            string kalanYazi = kalan.TotalSeconds > 0 ? $"⏱ {kalan.Hours}sa {kalan.Minutes}dk {kalan.Seconds}sn" : "Süre Doldu";
            Color sureRenk = kalan.TotalSeconds > 0 ? Color.FromArgb(231, 76, 60) : Color.DarkRed;

            Label lblSure = new Label
            {
                Name = "lblSure_" + idStr, 
                Text = kalanYazi,
                ForeColor = sureRenk,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, 200),
                Size = new Size(220, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };
            kart.Controls.Add(lblSure);

            
            decimal guncelFiyat = Convert.ToDecimal(satir["son_fiyat"]);
            Label lblFiyat = new Label
            {
                Name = "lblFiyat_" + idStr, 
                Text = guncelFiyat.ToString("N2") + " ₺",
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                ForeColor = Color.FromArgb(155, 89, 182),
                Location = new Point(10, 225),
                Size = new Size(220, 35),
                TextAlign = ContentAlignment.MiddleCenter
            };
            kart.Controls.Add(lblFiyat);

            
            string aciklamaMetni = satir["aciklama"] != DBNull.Value ? satir["aciklama"].ToString() : "Açıklama bulunmuyor.";
            string urunAdi = satir["urun_adi"].ToString();

            Button btnAciklama = new Button
            {
                Text = "Detayları Gör",
                Size = new Size(200, 35),
                Location = new Point(20, 275),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(155, 89, 182),
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnAciklama.FlatAppearance.BorderSize = 1;
            btnAciklama.FlatAppearance.BorderColor = Color.FromArgb(155, 89, 182);

            btnAciklama.Click += (s, ev) =>
            {
                AciklamaForm ozelMesaj = new AciklamaForm(urunAdi, aciklamaMetni);
                ozelMesaj.ShowDialog();
            };
            kart.Controls.Add(btnAciklama);

            
            Button btnTeklif = new Button
            {
                Text = "TEKLİF VER",
                Size = new Size(200, 45),
                Location = new Point(20, 325),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            btnTeklif.FlatAppearance.BorderSize = 0;

            btnTeklif.Click += (s, ev) =>
            {
                
                decimal anlikFiyat = decimal.Parse(lblFiyat.Text.Replace(" ₺", "").Replace(".", ""));
                TeklifForm tForm = new TeklifForm(urunAdi, anlikFiyat);
                if (tForm.ShowDialog() == DialogResult.OK)
                {
                    TeklifVer(urunId, tForm.GirilenTeklif);
                }
            };
            kart.Controls.Add(btnTeklif);

            return kart;
        }

        private void TeklifVer(int id, decimal teklifMiktari)
        {
            try
            {
                using (var baglanti = Veritabani.BaglantiGetir())
                {
                    if (baglanti.State != ConnectionState.Open) baglanti.Open();

                    string sqlBakiye = "SELECT bakiye FROM kullanicilar WHERE kullanici_id = @kid";
                    decimal mevcutBakiye = 0;
                    using (var cmdBakiye = new NpgsqlCommand(sqlBakiye, baglanti))
                    {
                        cmdBakiye.Parameters.AddWithValue("@kid", this.GirisYapanId);
                        object sonuc = cmdBakiye.ExecuteScalar();
                        if (sonuc != null) mevcutBakiye = Convert.ToDecimal(sonuc);
                    }

                    if (mevcutBakiye < teklifMiktari)
                    {
                        MessageBox.Show($"Yetersiz Bakiye!\nMevcut Bakiyeniz: {mevcutBakiye:N2} ₺", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string sqlSure = "SELECT bitis_tarihi FROM urunler WHERE urun_id = @id";
                    using (var cmdSure = new NpgsqlCommand(sqlSure, baglanti))
                    {
                        cmdSure.Parameters.AddWithValue("@id", id);
                        var bitisObj = cmdSure.ExecuteScalar();
                        if (bitisObj != null)
                        {
                            if (DateTime.Now > Convert.ToDateTime(bitisObj))
                            {
                                MessageBox.Show("Üzgünüz, bu ürünün süresi doldu!");
                                VitrinYukle();
                                return;
                            }
                        }
                    }

                    string sqlFiyat = "SELECT son_fiyat FROM urunler WHERE urun_id = @id";
                    decimal dbFiyat = 0;
                    using (var cmd = new NpgsqlCommand(sqlFiyat, baglanti))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        object result = cmd.ExecuteScalar();
                        if (result != null) dbFiyat = Convert.ToDecimal(result);
                    }

                    if (teklifMiktari <= dbFiyat)
                    {
                        MessageBox.Show($"Fiyat değişti! En az {(dbFiyat + 1):N2} TL vermelisiniz.");
                        VitrinYukle();
                        return;
                    }

                    string sqlUpdate = "UPDATE urunler SET son_fiyat = @fiyat WHERE urun_id = @id";
                    using (var cmdUpdate = new NpgsqlCommand(sqlUpdate, baglanti))
                    {
                        cmdUpdate.Parameters.AddWithValue("@id", id);
                        cmdUpdate.Parameters.AddWithValue("@fiyat", teklifMiktari);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    string sqlInsert = "INSERT INTO teklifler (teklif_miktari, teklif_zamani, urun_id, kullanici_id) VALUES (@fiyat, NOW(), @id, @kullaniciId)";
                    using (var cmdInsert = new NpgsqlCommand(sqlInsert, baglanti))
                    {
                        cmdInsert.Parameters.AddWithValue("@fiyat", teklifMiktari);
                        cmdInsert.Parameters.AddWithValue("@id", id);
                        cmdInsert.Parameters.AddWithValue("@kullaniciId", this.GirisYapanId);
                        cmdInsert.ExecuteNonQuery();
                    }

                    MessageBox.Show("Teklifiniz başarıyla alındı!");
                    VitrinYukle();
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void VeritabaniniZorlaDuzelt()
        {
            try
            {
                using (var baglanti = Veritabani.BaglantiGetir())
                {
                    if (baglanti.State != ConnectionState.Open) baglanti.Open();
                    string sqlTamir = @"
                        CREATE TABLE IF NOT EXISTS fiyat_loglari (
                            log_id SERIAL PRIMARY KEY, urun_id INT, eski_fiyat DECIMAL(10,2), yeni_fiyat DECIMAL(10,2), degisim_tarihi TIMESTAMP DEFAULT NOW()
                        );
                        CREATE OR REPLACE FUNCTION fiyat_degisim_logla() RETURNS TRIGGER AS $$
                        BEGIN
                            INSERT INTO fiyat_loglari (urun_id, eski_fiyat, yeni_fiyat) VALUES (OLD.urun_id, OLD.son_fiyat, NEW.son_fiyat);
                            RETURN NEW;
                        END;
                        $$ LANGUAGE plpgsql;
                        DROP TRIGGER IF EXISTS trg_fiyat_logla ON urunler;
                        CREATE TRIGGER trg_fiyat_logla AFTER UPDATE ON urunler FOR EACH ROW EXECUTE FUNCTION fiyat_degisim_logla();
                    ";
                    using (var komut = new NpgsqlCommand(sqlTamir, baglanti)) { komut.ExecuteNonQuery(); }
                }
            }
            catch { }
        }

        private void btnHesapDegistir_Click(object sender, EventArgs e) { Application.Restart(); }
        private void button1_Click(object sender, EventArgs e) { EnvanterForm frm = new EnvanterForm(this.GirisYapanId); frm.ShowDialog(); }
        private void btnUrunYonetimi_Click_1(object sender, EventArgs e) { new YonetimForm().ShowDialog(); VitrinYukle(); }

        private void btnUrunYonetimi_Click(object sender, EventArgs e) { btnUrunYonetimi_Click_1(sender, e); }
        private void lblFormTitle_Click(object sender, EventArgs e) { }
        private void btnEnvanter_Click(object sender, EventArgs e) { }
        private void btnExit_Click(object sender, EventArgs e) { Application.Exit(); }
        private void pnlTitleBar_MouseDown(object sender, MouseEventArgs e) { ReleaseCapture(); SendMessage(this.Handle, 0x112, 0xf012, 0); }
        private void btnVitrin_Click(object sender, EventArgs e) { VitrinYukle(); }
        private void btnTekliflerim_Click(object sender, EventArgs e) { TekliflerimForm form = new TekliflerimForm(this.GirisYapanId); form.ShowDialog(); }
        private void btnProfil_Click(object sender, EventArgs e) { ProfilForm frm = new ProfilForm(this.GirisYapanId); frm.ShowDialog(); }
        private void txtArama_TextChanged(object sender, EventArgs e) { VitrinYukle(); }
    }
}