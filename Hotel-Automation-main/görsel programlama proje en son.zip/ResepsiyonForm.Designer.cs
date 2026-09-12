namespace görsel_programlama_proje_en_son
{
    partial class ResepsiyonForm
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
            this.components = new System.ComponentModel.Container();
            this.lblSaat = new System.Windows.Forms.Label();
            this.lblTarih = new System.Windows.Forms.Label();
            this.BtnCikis = new System.Windows.Forms.Button();
            this.BtnHakkimizda = new System.Windows.Forms.Button();
            this.BtnSifre = new System.Windows.Forms.Button();
            this.BtnRadyo = new System.Windows.Forms.Button();
            this.BtnStok = new System.Windows.Forms.Button();
            this.BtnAdminGiris = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnYeniMusteri = new System.Windows.Forms.Button();
            this.btnOdalar = new System.Windows.Forms.Button();
            this.btnMusteriler = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSaat
            // 
            this.lblSaat.AutoSize = true;
            this.lblSaat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSaat.ForeColor = System.Drawing.Color.Transparent;
            this.lblSaat.Location = new System.Drawing.Point(602, 150);
            this.lblSaat.Name = "lblSaat";
            this.lblSaat.Size = new System.Drawing.Size(57, 20);
            this.lblSaat.TabIndex = 15;
            this.lblSaat.Text = "label2";
            // 
            // lblTarih
            // 
            this.lblTarih.AutoSize = true;
            this.lblTarih.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTarih.ForeColor = System.Drawing.Color.Transparent;
            this.lblTarih.Location = new System.Drawing.Point(546, 97);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(57, 20);
            this.lblTarih.TabIndex = 14;
            this.lblTarih.Text = "label1";
            this.lblTarih.Click += new System.EventHandler(this.lblTarih_Click);
            // 
            // BtnCikis
            // 
            this.BtnCikis.BackColor = System.Drawing.Color.Red;
            this.BtnCikis.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnCikis.Location = new System.Drawing.Point(254, 346);
            this.BtnCikis.Name = "BtnCikis";
            this.BtnCikis.Size = new System.Drawing.Size(90, 67);
            this.BtnCikis.TabIndex = 13;
            this.BtnCikis.Text = "Çıkış";
            this.BtnCikis.UseVisualStyleBackColor = false;
            this.BtnCikis.Click += new System.EventHandler(this.BtnCikis_Click);
            // 
            // BtnHakkimizda
            // 
            this.BtnHakkimizda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnHakkimizda.Location = new System.Drawing.Point(164, 419);
            this.BtnHakkimizda.Name = "BtnHakkimizda";
            this.BtnHakkimizda.Size = new System.Drawing.Size(135, 48);
            this.BtnHakkimizda.TabIndex = 12;
            this.BtnHakkimizda.Text = "Hakkımızda";
            this.BtnHakkimizda.UseVisualStyleBackColor = true;
            this.BtnHakkimizda.Click += new System.EventHandler(this.BtnHakkimizda_Click);
            // 
            // BtnSifre
            // 
            this.BtnSifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnSifre.Location = new System.Drawing.Point(254, 240);
            this.BtnSifre.Name = "BtnSifre";
            this.BtnSifre.Size = new System.Drawing.Size(90, 67);
            this.BtnSifre.TabIndex = 11;
            this.BtnSifre.Text = "Şifre Güncelle";
            this.BtnSifre.UseVisualStyleBackColor = true;
            this.BtnSifre.Click += new System.EventHandler(this.BtnSifre_Click);
            // 
            // BtnRadyo
            // 
            this.BtnRadyo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnRadyo.Location = new System.Drawing.Point(115, 346);
            this.BtnRadyo.Name = "BtnRadyo";
            this.BtnRadyo.Size = new System.Drawing.Size(90, 67);
            this.BtnRadyo.TabIndex = 10;
            this.BtnRadyo.Text = "Radyo Dinle ";
            this.BtnRadyo.UseVisualStyleBackColor = true;
            this.BtnRadyo.Click += new System.EventHandler(this.BtnRadyo_Click);
            // 
            // BtnStok
            // 
            this.BtnStok.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnStok.Location = new System.Drawing.Point(254, 24);
            this.BtnStok.Name = "BtnStok";
            this.BtnStok.Size = new System.Drawing.Size(90, 67);
            this.BtnStok.TabIndex = 9;
            this.BtnStok.Text = "Stoklar";
            this.BtnStok.UseVisualStyleBackColor = true;
            this.BtnStok.Click += new System.EventHandler(this.BtnStok_Click);
            // 
            // BtnAdminGiris
            // 
            this.BtnAdminGiris.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnAdminGiris.Location = new System.Drawing.Point(115, 24);
            this.BtnAdminGiris.Name = "BtnAdminGiris";
            this.BtnAdminGiris.Size = new System.Drawing.Size(90, 67);
            this.BtnAdminGiris.TabIndex = 8;
            this.BtnAdminGiris.Text = "Admin Giriş";
            this.BtnAdminGiris.UseVisualStyleBackColor = true;
            this.BtnAdminGiris.Click += new System.EventHandler(this.BtnAdminGiris_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnYeniMusteri
            // 
            this.btnYeniMusteri.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnYeniMusteri.Location = new System.Drawing.Point(115, 140);
            this.btnYeniMusteri.Name = "btnYeniMusteri";
            this.btnYeniMusteri.Size = new System.Drawing.Size(90, 67);
            this.btnYeniMusteri.TabIndex = 16;
            this.btnYeniMusteri.Text = "Yeni Müşteri";
            this.btnYeniMusteri.UseVisualStyleBackColor = true;
            this.btnYeniMusteri.Click += new System.EventHandler(this.btnYeniMusteri_Click);
            // 
            // btnOdalar
            // 
            this.btnOdalar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOdalar.Location = new System.Drawing.Point(254, 140);
            this.btnOdalar.Name = "btnOdalar";
            this.btnOdalar.Size = new System.Drawing.Size(90, 67);
            this.btnOdalar.TabIndex = 17;
            this.btnOdalar.Text = "Odalar";
            this.btnOdalar.UseVisualStyleBackColor = true;
            this.btnOdalar.Click += new System.EventHandler(this.btnOdalar_Click);
            // 
            // btnMusteriler
            // 
            this.btnMusteriler.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMusteriler.Location = new System.Drawing.Point(115, 240);
            this.btnMusteriler.Name = "btnMusteriler";
            this.btnMusteriler.Size = new System.Drawing.Size(90, 67);
            this.btnMusteriler.TabIndex = 18;
            this.btnMusteriler.Text = "Müşteriler";
            this.btnMusteriler.UseVisualStyleBackColor = true;
            this.btnMusteriler.Click += new System.EventHandler(this.btnMusteriler_Click);
            // 
            // ResepsiyonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(769, 479);
            this.Controls.Add(this.btnMusteriler);
            this.Controls.Add(this.btnOdalar);
            this.Controls.Add(this.btnYeniMusteri);
            this.Controls.Add(this.lblSaat);
            this.Controls.Add(this.lblTarih);
            this.Controls.Add(this.BtnCikis);
            this.Controls.Add(this.BtnHakkimizda);
            this.Controls.Add(this.BtnSifre);
            this.Controls.Add(this.BtnRadyo);
            this.Controls.Add(this.BtnStok);
            this.Controls.Add(this.BtnAdminGiris);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResepsiyonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ResepsiyonForm";
            this.Load += new System.EventHandler(this.ResepsiyonForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSaat;
        private System.Windows.Forms.Label lblTarih;
        private System.Windows.Forms.Button BtnCikis;
        private System.Windows.Forms.Button BtnHakkimizda;
        private System.Windows.Forms.Button BtnSifre;
        private System.Windows.Forms.Button BtnRadyo;
        private System.Windows.Forms.Button BtnStok;
        private System.Windows.Forms.Button BtnAdminGiris;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnYeniMusteri;
        private System.Windows.Forms.Button btnOdalar;
        private System.Windows.Forms.Button btnMusteriler;
    }
}