namespace OnlineMezatApp
{
    partial class AnaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnaForm));
            panel1 = new Panel();
            btnHesapDegistir = new Button();
            button1 = new Button();
            btnUrunYonetimi = new Button();
            btnProfil = new Button();
            btnTekliflerim = new Button();
            btnVitrin = new Button();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            txtArama = new TextBox();
            flpVitrin = new FlowLayoutPanel();
            pnlTitleBar = new Panel();
            btnExit = new Button();
            lblFormTitle = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlTitleBar.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(btnHesapDegistir);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(btnUrunYonetimi);
            panel1.Controls.Add(btnProfil);
            panel1.Controls.Add(btnTekliflerim);
            panel1.Controls.Add(btnVitrin);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(240, 805);
            panel1.TabIndex = 0;
            // 
            // btnHesapDegistir
            // 
            btnHesapDegistir.Cursor = Cursors.Hand;
            btnHesapDegistir.FlatAppearance.BorderSize = 0;
            btnHesapDegistir.FlatStyle = FlatStyle.Flat;
            btnHesapDegistir.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            btnHesapDegistir.Image = (Image)resources.GetObject("btnHesapDegistir.Image");
            btnHesapDegistir.ImageAlign = ContentAlignment.MiddleLeft;
            btnHesapDegistir.Location = new Point(3, 743);
            btnHesapDegistir.Name = "btnHesapDegistir";
            btnHesapDegistir.Padding = new Padding(5, 0, 0, 0);
            btnHesapDegistir.Size = new Size(230, 50);
            btnHesapDegistir.TabIndex = 7;
            btnHesapDegistir.Text = "Çıkış Yap";
            btnHesapDegistir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnHesapDegistir.UseVisualStyleBackColor = true;
            btnHesapDegistir.Click += btnHesapDegistir_Click;
            // 
            // button1
            // 
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(3, 359);
            button1.Name = "button1";
            button1.Padding = new Padding(5, 0, 0, 0);
            button1.Size = new Size(230, 50);
            button1.TabIndex = 6;
            button1.Text = "Envanter";
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnUrunYonetimi
            // 
            btnUrunYonetimi.Cursor = Cursors.Hand;
            btnUrunYonetimi.FlatAppearance.BorderSize = 0;
            btnUrunYonetimi.FlatStyle = FlatStyle.Flat;
            btnUrunYonetimi.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            btnUrunYonetimi.Image = (Image)resources.GetObject("btnUrunYonetimi.Image");
            btnUrunYonetimi.ImageAlign = ContentAlignment.MiddleLeft;
            btnUrunYonetimi.Location = new Point(3, 430);
            btnUrunYonetimi.Name = "btnUrunYonetimi";
            btnUrunYonetimi.Padding = new Padding(5, 0, 0, 0);
            btnUrunYonetimi.Size = new Size(230, 50);
            btnUrunYonetimi.TabIndex = 5;
            btnUrunYonetimi.Text = "Ürün Yönetimi";
            btnUrunYonetimi.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUrunYonetimi.UseVisualStyleBackColor = true;
            btnUrunYonetimi.Click += btnUrunYonetimi_Click_1;
            // 
            // btnProfil
            // 
            btnProfil.Cursor = Cursors.Hand;
            btnProfil.FlatAppearance.BorderSize = 0;
            btnProfil.FlatStyle = FlatStyle.Flat;
            btnProfil.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            btnProfil.Image = (Image)resources.GetObject("btnProfil.Image");
            btnProfil.ImageAlign = ContentAlignment.MiddleLeft;
            btnProfil.Location = new Point(3, 282);
            btnProfil.Name = "btnProfil";
            btnProfil.Padding = new Padding(5, 0, 0, 0);
            btnProfil.Size = new Size(230, 50);
            btnProfil.TabIndex = 4;
            btnProfil.Text = "Profil";
            btnProfil.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnProfil.UseVisualStyleBackColor = true;
            btnProfil.Click += btnProfil_Click;
            // 
            // btnTekliflerim
            // 
            btnTekliflerim.Cursor = Cursors.Hand;
            btnTekliflerim.FlatAppearance.BorderSize = 0;
            btnTekliflerim.FlatStyle = FlatStyle.Flat;
            btnTekliflerim.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            btnTekliflerim.Image = (Image)resources.GetObject("btnTekliflerim.Image");
            btnTekliflerim.ImageAlign = ContentAlignment.MiddleLeft;
            btnTekliflerim.Location = new Point(3, 207);
            btnTekliflerim.Name = "btnTekliflerim";
            btnTekliflerim.Padding = new Padding(5, 0, 0, 0);
            btnTekliflerim.Size = new Size(230, 50);
            btnTekliflerim.TabIndex = 3;
            btnTekliflerim.Text = "Verilen Teklifler";
            btnTekliflerim.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnTekliflerim.UseVisualStyleBackColor = true;
            btnTekliflerim.Click += btnTekliflerim_Click;
            // 
            // btnVitrin
            // 
            btnVitrin.Cursor = Cursors.Hand;
            btnVitrin.FlatAppearance.BorderSize = 0;
            btnVitrin.FlatStyle = FlatStyle.Flat;
            btnVitrin.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            btnVitrin.Image = (Image)resources.GetObject("btnVitrin.Image");
            btnVitrin.ImageAlign = ContentAlignment.MiddleLeft;
            btnVitrin.Location = new Point(3, 131);
            btnVitrin.Name = "btnVitrin";
            btnVitrin.Padding = new Padding(5, 0, 0, 0);
            btnVitrin.Size = new Size(230, 50);
            btnVitrin.TabIndex = 2;
            btnVitrin.Text = "Vitrin";
            btnVitrin.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnVitrin.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(152, 97);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(192, 0, 192);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(239, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1, 805);
            panel2.TabIndex = 0;
            // 
            // txtArama
            // 
            txtArama.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            txtArama.Location = new Point(246, 39);
            txtArama.Name = "txtArama";
            txtArama.PlaceholderText = "🔍 Ürün ara...";
            txtArama.Size = new Size(350, 31);
            txtArama.TabIndex = 1;
            txtArama.TextChanged += txtArama_TextChanged;
            // 
            // flpVitrin
            // 
            flpVitrin.AutoScroll = true;
            flpVitrin.Location = new Point(246, 92);
            flpVitrin.Name = "flpVitrin";
            flpVitrin.Padding = new Padding(20);
            flpVitrin.Size = new Size(980, 515);
            flpVitrin.TabIndex = 2;
            // 
            // pnlTitleBar
            // 
            pnlTitleBar.BackColor = SystemColors.ButtonFace;
            pnlTitleBar.Controls.Add(btnExit);
            pnlTitleBar.Controls.Add(lblFormTitle);
            pnlTitleBar.Dock = DockStyle.Top;
            pnlTitleBar.Location = new Point(240, 0);
            pnlTitleBar.Name = "pnlTitleBar";
            pnlTitleBar.Size = new Size(998, 30);
            pnlTitleBar.TabIndex = 3;
            // 
            // btnExit
            // 
            btnExit.Dock = DockStyle.Right;
            btnExit.Location = new Point(972, 0);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(26, 30);
            btnExit.TabIndex = 1;
            btnExit.Text = "X";
            btnExit.UseVisualStyleBackColor = true;
            // 
            // lblFormTitle
            // 
            lblFormTitle.AutoSize = true;
            lblFormTitle.Dock = DockStyle.Left;
            lblFormTitle.Location = new Point(0, 0);
            lblFormTitle.Name = "lblFormTitle";
            lblFormTitle.Size = new Size(46, 23);
            lblFormTitle.TabIndex = 0;
            lblFormTitle.Text = "Bidly";
            lblFormTitle.Click += lblFormTitle_Click;
            // 
            // AnaForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1238, 805);
            Controls.Add(pnlTitleBar);
            Controls.Add(flpVitrin);
            Controls.Add(txtArama);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AnaForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AnaForm";
            Load += AnaForm_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlTitleBar.ResumeLayout(false);
            pnlTitleBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox1;
        private TextBox txtArama;
        private FlowLayoutPanel flpVitrin;
        private Button btnVitrin;
        private Button btnUrunYonetimi;
        private Button btnProfil;
        private Button btnTekliflerim;
        private Panel pnlTitleBar;
        private Label lblFormTitle;
        private Button btnExit;
        private Button button1;
        private Button btnHesapDegistir;
    }
}