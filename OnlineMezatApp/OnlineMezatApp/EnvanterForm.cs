using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
using System.Runtime.InteropServices;

namespace OnlineMezatApp
{
    public partial class EnvanterForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private int aktifKullaniciId;

        public EnvanterForm(int id)
        {
            InitializeComponent();
            this.aktifKullaniciId = id;

            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(850, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.BackColor = Color.White;

            ArayuzOlustur();
            EnvanteriYukle();
        }

        
        private void EnvanterForm_Load(object sender, EventArgs e) { }

        private DataGridView dgvEnvanter;
        private Label lblBakiye;

        private void ArayuzOlustur()
        {
            
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(155, 89, 182) };
            pnlHeader.MouseDown += (s, e) => { ReleaseCapture(); SendMessage(this.Handle, 0x112, 0xf012, 0); };
            this.Controls.Add(pnlHeader);

            Label lblBaslik = new Label { Text = "ENVANTERİM (KAZANDIKLARIM)", ForeColor = Color.White, Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 18), AutoSize = true };
            pnlHeader.Controls.Add(lblBaslik);

            lblBakiye = new Label { Text = "Bakiye: ... ₺", ForeColor = Color.White, Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(500, 20), AutoSize = true };
            pnlHeader.Controls.Add(lblBakiye);

            Label lblKapat = new Label { Text = "X", ForeColor = Color.White, Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(810, 18), Cursor = Cursors.Hand };
            lblKapat.Click += (s, e) => this.Close();
            pnlHeader.Controls.Add(lblKapat);

            dgvEnvanter = new DataGridView
            {
                Location = new Point(20, 80),
                Size = new Size(810, 400),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            
            dgvEnvanter.EnableHeadersVisualStyles = false;
            dgvEnvanter.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(155, 89, 182);
            dgvEnvanter.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEnvanter.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvEnvanter.ColumnHeadersHeight = 35;

            
            dgvEnvanter.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvEnvanter.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 230, 250);
            dgvEnvanter.DefaultCellStyle.SelectionForeColor = Color.Black;

            this.Controls.Add(dgvEnvanter);
        }

        private void EnvanteriYukle()
        {
            try
            {
                using (var baglanti = Veritabani.BaglantiGetir())
                {
                    if (baglanti.State != ConnectionState.Open) baglanti.Open();

                    using (var cmdBakiye = new NpgsqlCommand("SELECT bakiye FROM kullanicilar WHERE kullanici_id = @id", baglanti))
                    {
                        cmdBakiye.Parameters.AddWithValue("@id", aktifKullaniciId);
                        object sonuc = cmdBakiye.ExecuteScalar();
                        if (sonuc != null) lblBakiye.Text = $"Güncel Bakiye: {Convert.ToDecimal(sonuc):N2} ₺";
                    }

                    string sql = @"
                        SELECT 
                            u.urun_adi AS ""Ürün Adı"",
                            s.satis_fiyati AS ""Satın Alma Fiyatı (₺)"",
                            s.satis_tarihi AS ""Kazanılma Tarihi""
                        FROM satislar s
                        JOIN urunler u ON s.urun_id = u.urun_id
                        WHERE s.kullanici_id = @id
                        ORDER BY s.satis_tarihi DESC";

                    using (var da = new NpgsqlDataAdapter(sql, baglanti))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@id", aktifKullaniciId);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvEnvanter.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }
    }
}