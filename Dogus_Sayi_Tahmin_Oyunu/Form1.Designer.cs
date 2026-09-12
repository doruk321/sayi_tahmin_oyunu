namespace SayiTahminOyunu
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            txtTahmin = new TextBox();
            btnTahminEt = new Button();
            lblMesaj = new Label();
            lblDeneme = new Label();
            lstGecmis = new ListBox();
            btnYeniOyun = new Button();
            lblBasariOrani = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtTahmin
            // 
            txtTahmin.BorderStyle = BorderStyle.FixedSingle;
            txtTahmin.Font = new Font("Segoe UI", 14F);
            txtTahmin.ForeColor = Color.FromArgb(51, 65, 85);
            txtTahmin.Location = new Point(29, 101);
            txtTahmin.Margin = new Padding(3, 4, 3, 4);
            txtTahmin.Name = "txtTahmin";
            txtTahmin.Size = new Size(183, 39);
            txtTahmin.TabIndex = 0;
            txtTahmin.TextChanged += txtTahmin_TextChanged;
            // 
            // btnTahminEt
            // 
            btnTahminEt.BackColor = Color.FromArgb(79, 70, 229);
            btnTahminEt.Cursor = Cursors.Hand;
            btnTahminEt.FlatAppearance.BorderSize = 0;
            btnTahminEt.FlatStyle = FlatStyle.Flat;
            btnTahminEt.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnTahminEt.ForeColor = Color.White;
            btnTahminEt.Location = new Point(29, 168);
            btnTahminEt.Margin = new Padding(3, 4, 3, 4);
            btnTahminEt.Name = "btnTahminEt";
            btnTahminEt.Size = new Size(183, 45);
            btnTahminEt.TabIndex = 1;
            btnTahminEt.Text = "Tahmin Et";
            btnTahminEt.UseVisualStyleBackColor = false;
            btnTahminEt.Click += btnTahminEt_Click;
            // 
            // lblMesaj
            // 
            lblMesaj.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMesaj.ForeColor = Color.FromArgb(71, 85, 105);
            lblMesaj.Location = new Point(23, 240);
            lblMesaj.Name = "lblMesaj";
            lblMesaj.Size = new Size(434, 60);
            lblMesaj.TabIndex = 2;
            lblMesaj.Text = "1 ile 100 arasında bir sayı tahmin edin.";
            lblMesaj.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDeneme
            // 
            lblDeneme.AutoSize = true;
            lblDeneme.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDeneme.ForeColor = Color.FromArgb(100, 116, 139);
            lblDeneme.Location = new Point(29, 313);
            lblDeneme.Name = "lblDeneme";
            lblDeneme.Size = new Size(146, 23);
            lblDeneme.TabIndex = 3;
            lblDeneme.Text = "Deneme Sayısı: 0";
            // 
            // lstGecmis
            // 
            lstGecmis.BackColor = Color.FromArgb(248, 250, 252);
            lstGecmis.BorderStyle = BorderStyle.FixedSingle;
            lstGecmis.Font = new Font("Segoe UI", 10F);
            lstGecmis.ForeColor = Color.FromArgb(51, 65, 85);
            lstGecmis.FormattingEnabled = true;
            lstGecmis.Location = new Point(234, 53);
            lstGecmis.Margin = new Padding(3, 4, 3, 4);
            lstGecmis.Name = "lstGecmis";
            lstGecmis.Size = new Size(223, 140);
            lstGecmis.TabIndex = 4;
            lstGecmis.SelectedIndexChanged += lstGecmis_SelectedIndexChanged;
            // 
            // btnYeniOyun
            // 
            btnYeniOyun.BackColor = Color.FromArgb(16, 185, 129);
            btnYeniOyun.Cursor = Cursors.Hand;
            btnYeniOyun.FlatAppearance.BorderSize = 0;
            btnYeniOyun.FlatStyle = FlatStyle.Flat;
            btnYeniOyun.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnYeniOyun.ForeColor = Color.White;
            btnYeniOyun.Location = new Point(29, 360);
            btnYeniOyun.Margin = new Padding(3, 4, 3, 4);
            btnYeniOyun.Name = "btnYeniOyun";
            btnYeniOyun.Size = new Size(183, 40);
            btnYeniOyun.TabIndex = 5;
            btnYeniOyun.Text = "Yeni Oyun";
            btnYeniOyun.UseVisualStyleBackColor = false;
            btnYeniOyun.Click += btnYeniOyun_Click;
            // 
            // lblBasariOrani
            // 
            lblBasariOrani.AutoSize = true;
            lblBasariOrani.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBasariOrani.ForeColor = Color.FromArgb(100, 116, 139);
            lblBasariOrani.Location = new Point(234, 313);
            lblBasariOrani.Name = "lblBasariOrani";
            lblBasariOrani.Size = new Size(142, 23);
            lblBasariOrani.TabIndex = 6;
            lblBasariOrani.Text = "Başarı Oranı: %0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(30, 41, 59);
            label1.Location = new Point(234, 21);
            label1.Name = "label1";
            label1.Size = new Size(119, 23);
            label1.TabIndex = 7;
            label1.Text = "Oyun Geçmişi";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(486, 433);
            Controls.Add(label1);
            Controls.Add(lblBasariOrani);
            Controls.Add(btnYeniOyun);
            Controls.Add(lstGecmis);
            Controls.Add(lblDeneme);
            Controls.Add(lblMesaj);
            Controls.Add(btnTahminEt);
            Controls.Add(txtTahmin);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sayı Tahmin Oyunu Dashboard";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtTahmin;
        private System.Windows.Forms.Button btnTahminEt;
        private System.Windows.Forms.Label lblMesaj;
        private System.Windows.Forms.Label lblDeneme;
        private System.Windows.Forms.ListBox lstGecmis;
        private System.Windows.Forms.Button btnYeniOyun;
        private System.Windows.Forms.Label lblBasariOrani;
        private System.Windows.Forms.Label label1;

    }
}
