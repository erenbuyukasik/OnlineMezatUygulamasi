namespace OnlineMezatApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            btnGiris = new Button();
            label5 = new Label();
            panel4 = new Panel();
            panel5 = new Panel();
            pictureBox3 = new PictureBox();
            txtSifre = new TextBox();
            label4 = new Label();
            panel2 = new Panel();
            panel3 = new Panel();
            pictureBox2 = new PictureBox();
            txtEmail = new TextBox();
            label3 = new Label();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label6 = new Label();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(btnGiris);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(263, -1);
            panel1.Name = "panel1";
            panel1.Size = new Size(431, 652);
            panel1.TabIndex = 0;
            // 
            // btnGiris
            // 
            btnGiris.BackColor = Color.Purple;
            btnGiris.Cursor = Cursors.Hand;
            btnGiris.ForeColor = Color.GhostWhite;
            btnGiris.Location = new Point(51, 452);
            btnGiris.Name = "btnGiris";
            btnGiris.Size = new Size(329, 46);
            btnGiris.TabIndex = 8;
            btnGiris.Text = "GİRİŞ YAP";
            btnGiris.UseVisualStyleBackColor = false;
            btnGiris.Click += btnGiris_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 10.2F, FontStyle.Italic, GraphicsUnit.Point, 162);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(106, 131);
            label5.Name = "label5";
            label5.Size = new Size(224, 21);
            label5.TabIndex = 7;
            label5.Text = "\"Açık Artırmanın Yeni Yüzü\"";
            // 
            // panel4
            // 
            panel4.Controls.Add(panel5);
            panel4.Controls.Add(pictureBox3);
            panel4.Controls.Add(txtSifre);
            panel4.Location = new Point(51, 385);
            panel4.Name = "panel4";
            panel4.Size = new Size(329, 44);
            panel4.TabIndex = 6;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(64, 0, 64);
            panel5.Dock = DockStyle.Bottom;
            panel5.ForeColor = Color.White;
            panel5.Location = new Point(0, 43);
            panel5.Name = "panel5";
            panel5.Size = new Size(329, 1);
            panel5.TabIndex = 6;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(309, 11);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(20, 20);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 5;
            pictureBox3.TabStop = false;
            // 
            // txtSifre
            // 
            txtSifre.BorderStyle = BorderStyle.None;
            txtSifre.Font = new Font("Century Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 162);
            txtSifre.Location = new Point(12, 10);
            txtSifre.Name = "txtSifre";
            txtSifre.PasswordChar = '*';
            txtSifre.Size = new Size(300, 21);
            txtSifre.TabIndex = 0;
            txtSifre.TextChanged += textBox2_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(51, 361);
            label4.Name = "label4";
            label4.Size = new Size(42, 21);
            label4.TabIndex = 5;
            label4.Text = "Şifre";
            // 
            // panel2
            // 
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(txtEmail);
            panel2.Location = new Point(51, 283);
            panel2.Name = "panel2";
            panel2.Size = new Size(329, 44);
            panel2.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(64, 0, 64);
            panel3.Dock = DockStyle.Bottom;
            panel3.ForeColor = Color.White;
            panel3.Location = new Point(0, 43);
            panel3.Name = "panel3";
            panel3.Size = new Size(329, 1);
            panel3.TabIndex = 6;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(309, 11);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(20, 20);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.None;
            txtEmail.Font = new Font("Century Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 162);
            txtEmail.Location = new Point(12, 10);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 21);
            txtEmail.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(51, 259);
            label3.Name = "label3";
            label3.Size = new Size(72, 21);
            label3.TabIndex = 3;
            label3.Text = "E-Posta";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(106, 194);
            label2.Name = "label2";
            label2.Size = new Size(220, 27);
            label2.TabIndex = 2;
            label2.Text = "Tekrar Hoşgeldiniz!";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(131, 42);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(76, 76);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(213, 65);
            label1.Name = "label1";
            label1.Size = new Size(92, 40);
            label1.TabIndex = 1;
            label1.Text = "Bidly";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Cursor = Cursors.Hand;
            label6.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label6.ForeColor = Color.FromArgb(64, 0, 64);
            label6.Location = new Point(920, 9);
            label6.Name = "label6";
            label6.Size = new Size(21, 19);
            label6.TabIndex = 1;
            label6.Text = "X";
            label6.Click += label6_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            ClientSize = new Size(950, 650);
            Controls.Add(label6);
            Controls.Add(panel1);
            Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 162);
            ForeColor = SystemColors.ControlLightLight;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label2;
        private PictureBox pictureBox1;
        private Label label1;
        private Panel panel2;
        private Label label3;
        private PictureBox pictureBox2;
        private TextBox txtEmail;
        private Panel panel4;
        private Panel panel5;
        private PictureBox pictureBox3;
        private TextBox txtSifre;
        private Label label4;
        private Panel panel3;
        private Label label5;
        private Button btnGiris;
        private Label label6;
    }
}
