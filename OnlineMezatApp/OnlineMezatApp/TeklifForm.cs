using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace OnlineMezatApp
{
    public partial class TeklifForm : Form
    {
        // Yuvarlak köşeler için DLL
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public decimal GirilenTeklif { get; private set; }
        private TextBox txtMiktar;

        public TeklifForm(string urunAdi, decimal guncelFiyat)
        {
            // Form Ayarları
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(400, 320);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // --- Başlık Paneli ---
            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(155, 89, 182) };
            pnlHeader.MouseDown += (s, e) => { ReleaseCapture(); SendMessage(this.Handle, 0x112, 0xf012, 0); };
            this.Controls.Add(pnlHeader);

            Label lblBaslik = new Label { Text = "TEKLİF VER", ForeColor = Color.White, Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(15, 13), AutoSize = true };
            pnlHeader.Controls.Add(lblBaslik);

            Label lblKapat = new Label { Text = "X", ForeColor = Color.White, Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(360, 13), Cursor = Cursors.Hand };
            lblKapat.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            pnlHeader.Controls.Add(lblKapat);

            // --- İçerik ---
            Label lblUrun = new Label { Text = urunAdi, Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.Black, Location = new Point(0, 70), Size = new Size(400, 30), TextAlign = ContentAlignment.MiddleCenter };
            this.Controls.Add(lblUrun);

            Label lblInfo = new Label { Text = "Şu Anki Fiyat:", Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, Location = new Point(0, 110), Size = new Size(400, 20), TextAlign = ContentAlignment.MiddleCenter };
            this.Controls.Add(lblInfo);

            Label lblFiyat = new Label { Text = guncelFiyat.ToString("N2") + " ₺", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(155, 89, 182), Location = new Point(0, 130), Size = new Size(400, 30), TextAlign = ContentAlignment.MiddleCenter };
            this.Controls.Add(lblFiyat);

            Label lblTeklifiniz = new Label { Text = "Teklifiniz (₺):", Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(50, 180), AutoSize = true };
            this.Controls.Add(lblTeklifiniz);

            txtMiktar = new TextBox { Location = new Point(50, 205), Size = new Size(300, 30), Font = new Font("Segoe UI", 12), TextAlign = HorizontalAlignment.Center };
            // Varsayılan olarak mevcut fiyatın biraz üstünü yazalım
            txtMiktar.Text = (guncelFiyat + 100).ToString("0");
            this.Controls.Add(txtMiktar);

            // --- Butonlar ---
            Button btnOnayla = new Button { Text = "ONAYLA", Location = new Point(50, 255), Size = new Size(140, 40), BackColor = Color.FromArgb(39, 174, 96), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
            btnOnayla.Click += BtnOnayla_Click;
            this.Controls.Add(btnOnayla);

            Button btnIptal = new Button { Text = "İPTAL", Location = new Point(210, 255), Size = new Size(140, 40), BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
            btnIptal.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnIptal);
        }

        private void BtnOnayla_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtMiktar.Text, out decimal miktar))
            {
                this.GirilenTeklif = miktar;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir tutar giriniz.");
            }
        }

        // Hata önleyici boş metod
        private void TeklifForm_Load(object sender, EventArgs e) { }

        protected override CreateParams CreateParams
        {
            get { CreateParams cp = base.CreateParams; cp.ClassStyle |= 0x20000; return cp; }
        }
    }
}