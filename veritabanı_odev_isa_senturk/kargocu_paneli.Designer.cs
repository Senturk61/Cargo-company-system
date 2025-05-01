namespace veritabanı_odev_isa_senturk
{
    partial class kargocu_paneli
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
            this.button8 = new System.Windows.Forms.Button();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.guncelle3_Click = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(808, 115);
            this.button8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(163, 41);
            this.button8.TabIndex = 35;
            this.button8.Text = "Listele Kargo";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // dataGridView4
            // 
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(21, 115);
            this.dataGridView4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowHeadersWidth = 51;
            this.dataGridView4.RowTemplate.Height = 24;
            this.dataGridView4.Size = new System.Drawing.Size(734, 229);
            this.dataGridView4.TabIndex = 39;
            // 
            // guncelle3_Click
            // 
            this.guncelle3_Click.Location = new System.Drawing.Point(808, 206);
            this.guncelle3_Click.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guncelle3_Click.Name = "guncelle3_Click";
            this.guncelle3_Click.Size = new System.Drawing.Size(163, 41);
            this.guncelle3_Click.TabIndex = 44;
            this.guncelle3_Click.Text = "gucelle";
            this.guncelle3_Click.UseVisualStyleBackColor = true;
            this.guncelle3_Click.Click += new System.EventHandler(this.guncelle3_Click_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(808, 299);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(163, 45);
            this.button3.TabIndex = 45;
            this.button3.Text = "Veriyi sil1";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // kargocu_paneli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1018, 378);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.guncelle3_Click);
            this.Controls.Add(this.dataGridView4);
            this.Controls.Add(this.button8);
            this.Name = "kargocu_paneli";
            this.Text = "kargocu_paneli";
            this.Load += new System.EventHandler(this.kargocu_paneli_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Button guncelle3_Click;
        private System.Windows.Forms.Button button3;
    }
}