using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace OnlineMezatApp
{
    public partial class AciklamaForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public AciklamaForm(string baslikText, string aciklamaMetni)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(450, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            
            Panel pnlHeader = new Panel();
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 50;
            pnlHeader.BackColor = Color.FromArgb(155, 89, 182);
            pnlHeader.MouseDown += (s, e) => { ReleaseCapture(); SendMessage(this.Handle, 0x112, 0xf012, 0); };
            this.Controls.Add(pnlHeader);

            Label lblBaslik = new Label();
            lblBaslik.Text = baslikText;
            lblBaslik.ForeColor = Color.White;
            lblBaslik.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblBaslik.Location = new Point(15, 13);
            lblBaslik.AutoSize = true;
            pnlHeader.Controls.Add(lblBaslik);

            Label lblKapat = new Label();
            lblKapat.Text = "X";
            lblKapat.ForeColor = Color.White;
            lblKapat.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblKapat.Location = new Point(this.Width - 40, 13);
            lblKapat.Cursor = Cursors.Hand;
            lblKapat.Click += (s, e) => this.Close();
            pnlHeader.Controls.Add(lblKapat);

            
            TextBox txtIcerik = new TextBox();
            txtIcerik.Multiline = true;
            txtIcerik.ReadOnly = true;
            txtIcerik.ScrollBars = ScrollBars.Vertical;
            txtIcerik.BackColor = Color.White;
            txtIcerik.BorderStyle = BorderStyle.None;
            txtIcerik.Font = new Font("Segoe UI", 11);
            txtIcerik.Text = aciklamaMetni;
            txtIcerik.Location = new Point(20, 70);
            txtIcerik.Size = new Size(410, 210);
            this.Controls.Add(txtIcerik);

            
            Button btnTamam = new Button();
            btnTamam.Text = "TAMAM";
            btnTamam.Size = new Size(120, 40);
            btnTamam.Location = new Point(165, 295);
            btnTamam.BackColor = Color.FromArgb(155, 89, 182);
            btnTamam.ForeColor = Color.White;
            btnTamam.FlatStyle = FlatStyle.Flat;
            btnTamam.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnTamam.Cursor = Cursors.Hand;
            btnTamam.Click += (s, e) => this.Close();
            this.Controls.Add(btnTamam);
        }

        private void AciklamaForm_Load(object sender, EventArgs e)
        {
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x20000;
                return cp;
            }
        }
    }
}