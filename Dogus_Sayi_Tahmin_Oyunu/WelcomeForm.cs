using System;
using System.Windows.Forms;

namespace SayiTahminOyunu
{
    // Proje Kuralları: Sınıf ve Nesne Kullanımı + Metot Kullanımı
    // Bu form, kullanıcı karşılama kurallarını açıklar ve form geçişini tamamlar.
    public partial class WelcomeForm : Form
    {
        // Oturum açan kullanıcının adını tutan alan (Encapsulation örneği)
        private string loggedInUser;

        // Parametreli Kurucu Metot (Constructor) - LoginForm'dan gelen kullanıcı adını alır
        public WelcomeForm(string username)
        {
            InitializeComponent();
            loggedInUser = username;
            
            // Kullanıcıyı dinamik olarak selamla
            lblWelcome.Text = $"Hoş Geldiniz, {loggedInUser}!";
        }

        // Oyuna başla butonu olay tetikleyicisi
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            // Form1 (Ana Oyun Formu) nesnesini oluştur ve kullanıcı adını kurucuya aktar (Form Geçişi - 2. Aşama)
            Form1 gameForm = new Form1(loggedInUser);
            
            // Oyunu göster
            gameForm.Show();

            // Hoş geldin formunu gizle
            this.Hide();
        }

        // Çıkış yap butonu olay tetikleyicisi
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Tüm uygulamayı kapat
            Application.Exit();
        }
    }
}
