namespace veritabanı_odev_isa_senturk
{
    partial class Kullanıcı_ekle
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
            this.Ad1 = new System.Windows.Forms.TextBox();
            this.soyadekle = new System.Windows.Forms.TextBox();
            this.epostaekle = new System.Windows.Forms.TextBox();
            this.sifreekle = new System.Windows.Forms.TextBox();
            this.Adekle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Soyad = new System.Windows.Forms.Label();
            this.Eposta = new System.Windows.Forms.Label();
            this.Sifre = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Ad1
            // 
            this.Ad1.Location = new System.Drawing.Point(188, 89);
            this.Ad1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Ad1.Multiline = true;
            this.Ad1.Name = "Ad1";
            this.Ad1.Size = new System.Drawing.Size(194, 29);
            this.Ad1.TabIndex = 18;
            this.Ad1.TextChanged += new System.EventHandler(this.Ad_TextChanged);
            // 
            // soyadekle
            // 
            this.soyadekle.Location = new System.Drawing.Point(188, 153);
            this.soyadekle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.soyadekle.Multiline = true;
            this.soyadekle.Name = "soyadekle";
            this.soyadekle.Size = new System.Drawing.Size(194, 29);
            this.soyadekle.TabIndex = 19;
            this.soyadekle.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // epostaekle
            // 
            this.epostaekle.Location = new System.Drawing.Point(188, 227);
            this.epostaekle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.epostaekle.Multiline = true;
            this.epostaekle.Name = "epostaekle";
            this.epostaekle.Size = new System.Drawing.Size(194, 29);
            this.epostaekle.TabIndex = 20;
            this.epostaekle.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // sifreekle
            // 
            this.sifreekle.Location = new System.Drawing.Point(188, 287);
            this.sifreekle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sifreekle.Multiline = true;
            this.sifreekle.Name = "sifreekle";
            this.sifreekle.Size = new System.Drawing.Size(194, 29);
            this.sifreekle.TabIndex = 21;
            this.sifreekle.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // Adekle
            // 
            this.Adekle.AutoSize = true;
            this.Adekle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Adekle.Location = new System.Drawing.Point(20, 89);
            this.Adekle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Adekle.Name = "Adekle";
            this.Adekle.Size = new System.Drawing.Size(36, 20);
            this.Adekle.TabIndex = 23;
            this.Adekle.Text = "Ad:";
            this.Adekle.Click += new System.EventHandler(this.label3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(270, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 37);
            this.label1.TabIndex = 24;
            this.label1.Text = "KULLANICI EKLEME";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(463, 89);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 115);
            this.button1.TabIndex = 25;
            this.button1.Text = "Kaydet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Soyad
            // 
            this.Soyad.AutoSize = true;
            this.Soyad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Soyad.Location = new System.Drawing.Point(20, 153);
            this.Soyad.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Soyad.Name = "Soyad";
            this.Soyad.Size = new System.Drawing.Size(64, 20);
            this.Soyad.TabIndex = 26;
            this.Soyad.Text = "Soyad:";
            // 
            // Eposta
            // 
            this.Eposta.AutoSize = true;
            this.Eposta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Eposta.Location = new System.Drawing.Point(20, 227);
            this.Eposta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Eposta.Name = "Eposta";
            this.Eposta.Size = new System.Drawing.Size(71, 20);
            this.Eposta.TabIndex = 27;
            this.Eposta.Text = "Eposta:";
            // 
            // Sifre
            // 
            this.Sifre.AutoSize = true;
            this.Sifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Sifre.Location = new System.Drawing.Point(20, 287);
            this.Sifre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Sifre.Name = "Sifre";
            this.Sifre.Size = new System.Drawing.Size(52, 20);
            this.Sifre.TabIndex = 28;
            this.Sifre.Text = "Sifre:";
            // 
            // Kullanıcı_ekle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(859, 450);
            this.Controls.Add(this.Sifre);
            this.Controls.Add(this.Eposta);
            this.Controls.Add(this.Soyad);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Adekle);
            this.Controls.Add(this.sifreekle);
            this.Controls.Add(this.epostaekle);
            this.Controls.Add(this.soyadekle);
            this.Controls.Add(this.Ad1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Kullanıcı_ekle";
            this.Text = "Kullanıcı_ekle";
            this.Load += new System.EventHandler(this.Kullanıcı_ekle_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Ad1;
        private System.Windows.Forms.TextBox soyadekle;
        private System.Windows.Forms.TextBox epostaekle;
        private System.Windows.Forms.TextBox sifreekle;
        private System.Windows.Forms.Label Adekle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label Soyad;
        private System.Windows.Forms.Label Eposta;
        private System.Windows.Forms.Label Sifre;
        private System.Windows.Forms.Label OdemeMiktarı;
        private System.Windows.Forms.Label UrunAdı;
    }
}