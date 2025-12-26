using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
using System.Runtime.InteropServices;

namespace OnlineMezatApp
{
    public partial class ProfilForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        
        private int aktifKullaniciId;

        
        private Label lblAd = null!;
        private Label lblEmail = null!;
        private Label lblRol = null!;
        private TextBox txtYeniSifre = null!;

        
        public ProfilForm(int id)
        {
            InitializeComponent();
            this.aktifKullaniciId = id; 

            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.BackColor = Color.White;

            ArayuzOlustur();
        }

        private void ProfilForm_Load(object sender, EventArgs e)
        {
            BilgileriGetir();
        }

        private void ArayuzOlustur()
        {
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(155, 89, 182) };
            pnlHeader.MouseDown += (s, e) => { ReleaseCapture(); SendMessage(this.Handle, 0x112, 0xf012, 0); };
            this.Controls.Add(pnlHeader);

            Label lblBaslik = new Label { Text = "PROFİLİM", ForeColor = Color.White, Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 13), AutoSize = true };
            pnlHeader.Controls.Add(lblBaslik);

            Label lblKapat = new Label { Text = "X", ForeColor = Color.White, Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(460, 15), Cursor = Cursors.Hand };
            lblKapat.Click += (s, e) => this.Close();
            pnlHeader.Controls.Add(lblKapat);

            this.Controls.Add(new Label { Text = "Ad Soyad:", Location = new Point(50, 80), Font = new Font("Segoe UI", 10, FontStyle.Bold), AutoSize = true });
            lblAd = new Label { Location = new Point(150, 80), Font = new Font("Segoe UI", 10), AutoSize = true, Text = "..." };
            this.Controls.Add(lblAd);

            this.Controls.Add(new Label { Text = "E-Posta:", Location = new Point(50, 120), Font = new Font("Segoe UI", 10, FontStyle.Bold), AutoSize = true });
            lblEmail = new Label { Location = new Point(150, 120), Font = new Font("Segoe UI", 10), AutoSize = true, Text = "..." };
            this.Controls.Add(lblEmail);

            this.Controls.Add(new Label { Text = "Rol:", Location = new Point(50, 160), Font = new Font("Segoe UI", 10, FontStyle.Bold), AutoSize = true });
            lblRol = new Label { Location = new Point(150, 160), Font = new Font("Segoe UI", 10), AutoSize = true, Text = "..." };
            this.Controls.Add(lblRol);

            GroupBox grpSifre = new GroupBox { Text = "Şifre Güncelle", Location = new Point(40, 220), Size = new Size(420, 150), Font = new Font("Segoe UI", 10) };
            this.Controls.Add(grpSifre);

            grpSifre.Controls.Add(new Label { Text = "Yeni Şifre:", Location = new Point(20, 40), AutoSize = true });
            txtYeniSifre = new TextBox { Location = new Point(120, 35), Size = new Size(200, 25) };
            grpSifre.Controls.Add(txtYeniSifre);

            Button btnKaydet = new Button { Text = "GÜNCELLE", Location = new Point(120, 80), Size = new Size(200, 40), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
            btnKaydet.Click += BtnKaydet_Click;
            grpSifre.Controls.Add(btnKaydet);
        }

        private void BilgileriGetir()
        {
            try
            {
                using (var baglanti = Veritabani.BaglantiGetir())
                {
                    if (baglanti.State != System.Data.ConnectionState.Open) baglanti.Open();
                    string sql = "SELECT ad_soyad, email, rol FROM kullanicilar WHERE kullanici_id = @id";
                    using (var cmd = new NpgsqlCommand(sql, baglanti))
                    {
                        cmd.Parameters.AddWithValue("@id", aktifKullaniciId);
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                lblAd.Text = dr["ad_soyad"].ToString();
                                lblEmail.Text = dr["email"].ToString();
                                lblRol.Text = dr["rol"].ToString()?.ToUpper();
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtYeniSifre.Text)) { MessageBox.Show("Şifre boş olamaz."); return; }

            try
            {
                using (var baglanti = Veritabani.BaglantiGetir())
                {
                    if (baglanti.State != System.Data.ConnectionState.Open) baglanti.Open();

                    using (var cmd = new NpgsqlCommand("CALL sp_sifre_degistir(@id, @sifre)", baglanti))
                    {
                        cmd.Parameters.AddWithValue("@id", aktifKullaniciId);
                        cmd.Parameters.AddWithValue("@sifre", txtYeniSifre.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Şifreniz güncellendi!");
                        txtYeniSifre.Clear();
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