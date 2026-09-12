using System;
using System.Windows.Forms;

namespace SayiTahminOyunu
{

    public partial class LoginForm : Form
    {
        // Kurucu metot (Constructor)
        public LoginForm()
        {
            InitializeComponent();
        }

        // Giriş butonuna tıklama olayı (Event Handler)
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı boşluk kontrolü (Input Validation)
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                lblError.Text = "Kullanıcı adı boş bırakılamaz!";
                txtUsername.Focus();
                return;
            }

            // Giriş başarılı: Hata etiketini temizle
            lblError.Text = "";

            // Kullanıcı adını alarak WelcomeForm nesnesi oluşturuyoruz (Form Geçişi - 1. Aşama)
            string username = txtUsername.Text.Trim();
            WelcomeForm welcomeForm = new WelcomeForm(username);
            
            // Hoş geldin sayfasını göster
            welcomeForm.Show();

            // Giriş formunu gizle
            this.Hide();
        }

        // Temizle butonuna tıklama olayı
        private void btnReset_Click(object sender, EventArgs e)
        {
            // Tüm alanları varsayılan durumuna sıfırlar
            txtUsername.Clear();
            lblError.Text = "";
            txtUsername.Focus();
        }

        // Oturumu sıfırlar ve kullanıcı adı alanına odaklanır (Dışarıdan çağrılabilir)
        public void ResetAndFocus()
        {
            txtUsername.Clear();
            lblError.Text = "";
            txtUsername.Focus();
        }
    }
}
