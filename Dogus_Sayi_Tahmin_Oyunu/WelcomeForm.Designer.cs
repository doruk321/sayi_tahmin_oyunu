namespace SayiTahminOyunu
{
    partial class WelcomeForm
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
            lblTitle = new Label();
            lblWelcome = new Label();
            grpRules = new GroupBox();
            lblRule1 = new Label();
            lblRule2 = new Label();
            lblRule3 = new Label();
            lblRule4 = new Label();
            btnStartGame = new Button();
            btnExit = new Button();
            grpRules.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(79, 70, 229);
            lblTitle.Location = new Point(12, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(460, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Doğuş Üniversitesi Programlama Final Projesi";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblWelcome
            // 
            lblWelcome.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(30, 41, 59);
            lblWelcome.Location = new Point(12, 55);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(460, 35);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "Hoş Geldiniz, [Kullanıcı]!";
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpRules
            // 
            grpRules.Controls.Add(lblRule1);
            grpRules.Controls.Add(lblRule2);
            grpRules.Controls.Add(lblRule3);
            grpRules.Controls.Add(lblRule4);
            grpRules.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpRules.ForeColor = Color.FromArgb(51, 65, 85);
            grpRules.Location = new Point(25, 105);
            grpRules.Name = "grpRules";
            grpRules.Size = new Size(430, 180);
            grpRules.TabIndex = 2;
            grpRules.TabStop = false;
            grpRules.Text = "Oyun Kuralları ve Özellikler";
            // 
            // lblRule1
            // 
            lblRule1.Font = new Font("Segoe UI", 10F);
            lblRule1.Location = new Point(20, 35);
            lblRule1.Name = "lblRule1";
            lblRule1.Size = new Size(390, 25);
            lblRule1.TabIndex = 0;
            lblRule1.Text = "• Bilgisayar 1 ile 100 arasında gizli bir sayı üretir.";
            // 
            // lblRule2
            // 
            lblRule2.Font = new Font("Segoe UI", 10F);
            lblRule2.Location = new Point(20, 65);
            lblRule2.Name = "lblRule2";
            lblRule2.Size = new Size(390, 25);
            lblRule2.TabIndex = 1;
            lblRule2.Text = "• Gizli sayıyı tahmin etmek için 10 adet deneme hakkınız bulunur.";
            // 
            // lblRule3
            // 
            lblRule3.Font = new Font("Segoe UI", 10F);
            lblRule3.Location = new Point(20, 95);
            lblRule3.Name = "lblRule3";
            lblRule3.Size = new Size(390, 25);
            lblRule3.TabIndex = 2;
            lblRule3.Text = "• Tahminlerinize göre 'Daha Büyük' veya 'Daha Küçük' uyarısı alırsınız.";
            // 
            // lblRule4
            // 
            lblRule4.Font = new Font("Segoe UI", 10F);
            lblRule4.Location = new Point(20, 125);
            lblRule4.Name = "lblRule4";
            lblRule4.Size = new Size(390, 45);
            lblRule4.TabIndex = 3;
            lblRule4.Text = "• Oyun geçmişiniz anlık olarak listelenir ve başarı oranınız tamamen size özel olarak dinamik şekilde hesaplanır.";
            // 
            // btnStartGame
            // 
            btnStartGame.BackColor = Color.FromArgb(16, 185, 129);
            btnStartGame.Cursor = Cursors.Hand;
            btnStartGame.FlatAppearance.BorderSize = 0;
            btnStartGame.FlatStyle = FlatStyle.Flat;
            btnStartGame.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnStartGame.ForeColor = Color.White;
            btnStartGame.Location = new Point(55, 305);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(160, 40);
            btnStartGame.TabIndex = 3;
            btnStartGame.Text = "Oyuna Başla 🎮";
            btnStartGame.UseVisualStyleBackColor = false;
            btnStartGame.Click += btnStartGame_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(239, 68, 68);
            btnExit.Cursor = Cursors.Hand;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(265, 305);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(160, 40);
            btnExit.TabIndex = 4;
            btnExit.Text = "Çıkış Yap ❌";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // WelcomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(484, 371);
            Controls.Add(btnExit);
            Controls.Add(btnStartGame);
            Controls.Add(grpRules);
            Controls.Add(lblWelcome);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "WelcomeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hoş Geldiniz";
            grpRules.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.GroupBox grpRules;
        private System.Windows.Forms.Label lblRule1;
        private System.Windows.Forms.Label lblRule2;
        private System.Windows.Forms.Label lblRule3;
        private System.Windows.Forms.Label lblRule4;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnExit;
    }
}
