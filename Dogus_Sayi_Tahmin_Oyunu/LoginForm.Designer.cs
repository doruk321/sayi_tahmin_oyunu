namespace SayiTahminOyunu
{
    partial class LoginForm
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
            lblUsername = new Label();
            txtUsername = new TextBox();
            btnLogin = new Button();
            btnReset = new Button();
            lblError = new Label();
            grpLoginPanel = new GroupBox();
            grpLoginPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTitle.Location = new Point(12, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(360, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Sayı Tahmin Oyunu - Giriş";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpLoginPanel
            // 
            grpLoginPanel.Controls.Add(lblUsername);
            grpLoginPanel.Controls.Add(txtUsername);
            grpLoginPanel.Controls.Add(btnLogin);
            grpLoginPanel.Controls.Add(btnReset);
            grpLoginPanel.Controls.Add(lblError);
            grpLoginPanel.Font = new Font("Segoe UI", 10F);
            grpLoginPanel.Location = new Point(25, 70);
            grpLoginPanel.Name = "grpLoginPanel";
            grpLoginPanel.Size = new Size(330, 195);
            grpLoginPanel.TabIndex = 1;
            grpLoginPanel.TabStop = false;
            grpLoginPanel.Text = "Kullanıcı Doğrulama";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsername.Location = new Point(25, 35);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(96, 19);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Kullanıcı Adı:";
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Location = new Point(25, 57);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(280, 25);
            txtUsername.TabIndex = 1;

            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(79, 70, 229);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(25, 95);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(130, 35);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Giriş Yap";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(100, 116, 139);
            btnReset.Cursor = Cursors.Hand;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(175, 95);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(130, 35);
            btnReset.TabIndex = 6;
            btnReset.Text = "Temizle";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            // 
            // lblError
            // 
            lblError.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblError.ForeColor = Color.FromArgb(220, 38, 38);
            lblError.Location = new Point(25, 143);
            lblError.Name = "lblError";
            lblError.Size = new Size(280, 40);
            lblError.TabIndex = 7;
            lblError.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LoginForm
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(384, 286);
            Controls.Add(grpLoginPanel);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kullanıcı Girişi";
            grpLoginPanel.ResumeLayout(false);
            grpLoginPanel.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpLoginPanel;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblError;
    }
}
