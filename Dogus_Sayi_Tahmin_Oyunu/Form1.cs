using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SayiTahminOyunu
{
    // Proje Kuralları: Sınıf ve Nesne Kullanımı + Metot Kullanımı
    // Bu sınıf, ana tahmin oyunu arayüzünü, veritabanı işlemlerini (CRUD) ve dosya ihracını yönetir.
    public partial class Form1 : Form
    {
        // Sınıf seviyesindeki değişkenler (Encapsulation)
        private int gizliSayi;
        private int denemeSayisi;
        private Random rastgele = new Random();
        private int kazanmaSayisi = 0;
        private int toplamOyunSayisi = 0;
        private string aktifKullanici = "Misafir";
        
        // Orijinal DPI ölçekli boyutu saklamak için değişken
        private Size originalClientSize;
        
        // Oyuncu değiştirme durumunu takip eden değişken
        private bool isSwitchingPlayer = false;

        // Parametresiz Kurucu Metot (Constructor) - Geriye dönük uyumluluk için
        public Form1() : this("Misafir")
        {
        }

        // Parametreli Kurucu Metot (Constructor) - WelcomeForm'dan gelen aktif kullanıcı adını alır
        public Form1(string username)
        {
            InitializeComponent();
            aktifKullanici = username;
            
            // Orijinal (DPI ile ölçeklenmiş) boyutu sakla
            originalClientSize = this.ClientSize;
            
            // TextBox içeriğini merkeze hizalar
            txtTahmin.TextAlign = HorizontalAlignment.Center;
        }

        // Form yüklendiğinde tetiklenen olay (Load Event)
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // 3. ADIM: Tahmin oyununu başlat
                OyunuBaslat();

                // "Oyuncu Değiştir" butonu dinamik olarak ekleniyor
                Button btnOyuncuDegistir = new Button();
                btnOyuncuDegistir.Name = "btnOyuncuDegistir";
                btnOyuncuDegistir.Text = "Oyuncu Değiştir 👤🔄";
                btnOyuncuDegistir.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                btnOyuncuDegistir.BackColor = Color.FromArgb(100, 116, 139); // Slate Gray
                btnOyuncuDegistir.ForeColor = Color.White;
                btnOyuncuDegistir.Cursor = Cursors.Hand;
                btnOyuncuDegistir.FlatStyle = FlatStyle.Flat;
                btnOyuncuDegistir.FlatAppearance.BorderSize = 0;
                
                // DPI UYUMLU AKILLI HİZALAMA:
                // Tasarım zamanında otomatik ölçeklenen kontrollerin (lstGecmis ve btnYeniOyun)
                // konum ve boyut değerlerini birebir kopyalayarak yüksek çözünürlüklü ekranlarda mükemmel uyum sağlıyoruz!
                btnOyuncuDegistir.Location = new Point(lstGecmis.Left, btnYeniOyun.Top);
                btnOyuncuDegistir.Size = new Size(lstGecmis.Width, btnYeniOyun.Height);
                
                btnOyuncuDegistir.Click += btnOyuncuDegistir_Click;
                this.Controls.Add(btnOyuncuDegistir);

                // Aktif oyuncuyu belirten şık etiket dinamik olarak ekleniyor
                Label lblActivePlayer = new Label();
                lblActivePlayer.Name = "lblActivePlayer";
                lblActivePlayer.Text = $"👤 Oyuncu: {aktifKullanici}";
                lblActivePlayer.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                lblActivePlayer.ForeColor = Color.FromArgb(79, 70, 229); // Modern Indigo
                
                // Sol üstteki başlık ile mükemmel hizala (DPI uyumlu)
                lblActivePlayer.Location = new Point(txtTahmin.Left, label1.Top);
                lblActivePlayer.AutoSize = true;
                this.Controls.Add(lblActivePlayer);

                // Pencere başlığı aktif oyuncuya göre ayarlanıyor
                this.Text = $"Sayı Tahmin Oyunu - Aktif Oyuncu: {aktifKullanici}";

                // Başarı oranı etiketini oyuncuya özel başlat
                lblBasariOrani.Text = $"{aktifKullanici} Başarı Oranı: %0";

                // DPI UYUMLU DİNAMİK BOYUTLANDIRMA:
                // btnYeniOyun otomatik ölçeklendiği için, form yüksekliğini
                // bu ölçeklenmiş değere göre hesaplıyoruz. Böylece hiçbir eleman kesilmiyor!
                int targetWidth = lstGecmis.Right + 25;
                int targetHeight = btnYeniOyun.Bottom + 25;
                this.ClientSize = new Size(targetWidth, targetHeight);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Form başlatılırken hata oluştu: " + ex.Message, "Sistem Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Oyunu ilk durumuna getiren ve yeni sayı üreten metot
        private void OyunuBaslat()
        {
            // 0 ile 100 arasında rastgele sayı üretimi (Kural: 0 ile 100 arasında tahmin oyunu)
            gizliSayi = rastgele.Next(0, 101);
            denemeSayisi = 0;

            lblMesaj.Text = "0 ile 100 arasında bir sayı tahmin edin.";
            lblMesaj.ForeColor = Color.Black;
            lblDeneme.Text = "Deneme Sayısı: 0";
            txtTahmin.Text = "";
            txtTahmin.Enabled = true;
            btnTahminEt.Enabled = true;
            this.BackColor = Color.FromArgb(241, 245, 249); // Form arka plan rengi sıfırlanıyor
            btnTahminEt.BackColor = Color.FromArgb(79, 70, 229);

            txtTahmin.Focus();
        }

        // Tahmin Et butonuna tıklandığında çalışan olay (Tahmin akışı)
        private void btnTahminEt_Click(object sender, EventArgs e)
        {
            try
            {
                // Try-catch ile sayısal olmayan hatalı giriş yönetimi
                int tahmin = Convert.ToInt32(txtTahmin.Text);

                // 0 ile 100 arasında olup olmadığının kontrolü (Gereksinim doğrulaması)
                if (tahmin < 0 || tahmin > 100)
                {
                    MessageBox.Show("Lütfen sadece 0 ile 100 arasında bir sayı giriniz!", "Geçersiz Aralık", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTahmin.Text = "";
                    txtTahmin.Focus();
                    return;
                }

                denemeSayisi++;
                lblDeneme.Text = "Deneme Sayısı: " + denemeSayisi;

                // İç içe if-else yapıları ile tahmin kontrolü (Nested if-else)
                if (tahmin == gizliSayi)
                {
                    // Kazanma Durumu: Görsel geri bildirim (renk değişimleri)
                    lblMesaj.Text = $"Tebrikler! {denemeSayisi}. denemede bildiniz.";
                    lblMesaj.ForeColor = Color.Green;
                    this.BackColor = Color.LightGreen;
                    btnTahminEt.BackColor = Color.SeaGreen;

                    txtTahmin.Enabled = false;
                    btnTahminEt.Enabled = false;

                    // Oyun geçmişini liste kutusuna (ListBox) ekle
                    OyunGecmisiniEkle(true, denemeSayisi);
                }
                else
                {
                    if (tahmin < gizliSayi)
                    {
                        lblMesaj.Text = "Daha BÜYÜK bir sayı girin ↑";
                        lblMesaj.ForeColor = Color.Blue;
                    }
                    else
                    {
                        lblMesaj.Text = "Daha KÜÇÜK bir sayı girin ↓";
                        lblMesaj.ForeColor = Color.Red;
                    }

                    // 10 deneme hakkı bittiğinde kaybetme durumu
                    if (denemeSayisi >= 10)
                    {
                        lblMesaj.Text = $"Maalesef kaybettiniz! Sayı {gizliSayi} idi.";
                        lblMesaj.ForeColor = Color.White;
                        this.BackColor = Color.DarkRed;
                        btnTahminEt.BackColor = Color.IndianRed;

                        txtTahmin.Enabled = false;
                        btnTahminEt.Enabled = false;

                        // Oyun geçmişini ListBox'a ekleme
                        OyunGecmisiniEkle(false, denemeSayisi);
                    }
                }

                txtTahmin.Text = "";
                txtTahmin.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen geçerli bir tam sayı giriniz!", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTahmin.Text = "";
                txtTahmin.Focus();
            }
        }



        // ListBox tabanlı oturum geçmişine kayıt ekleyen metot
        private void OyunGecmisiniEkle(bool kazandiMi, int deneme)
        {
            toplamOyunSayisi++;
            if (kazandiMi)
            {
                kazanmaSayisi++;
                lstGecmis.Items.Add($"[{aktifKullanici}] Oyun {toplamOyunSayisi}: KAZANDI ({deneme} deneme)");
            }
            else
            {
                lstGecmis.Items.Add($"[{aktifKullanici}] Oyun {toplamOyunSayisi}: KAYBETTİ");
            }

            BasariOraniHesapla();
        }

        // While döngüsü ile başarı oranını hesaplayan metot
        private void BasariOraniHesapla()
        {
            // Windows Forms akışında while kullanımı örneği
            int index = 0;
            int hesaplananKazanma = 0;
            int hesaplananToplam = 0;

            while (index < lstGecmis.Items.Count)
            {
                var item = lstGecmis.Items[index]?.ToString();
                if (item != null && item.StartsWith($"[{aktifKullanici}]"))
                {
                    hesaplananToplam++;
                    if (item.Contains("KAZANDI"))
                    {
                        hesaplananKazanma++;
                    }
                }
                index++;
            }

            if (hesaplananToplam > 0)
            {
                double oran = (double)hesaplananKazanma / hesaplananToplam * 100;
                lblBasariOrani.Text = $"{aktifKullanici} Başarı Oranı: %{Math.Round(oran, 2)}";
            }
            else
            {
                lblBasariOrani.Text = $"{aktifKullanici} Başarı Oranı: %0";
            }
        }



        // Yeni oyun başlatma butonu olay tetikleyicisi
        private void btnYeniOyun_Click(object sender, EventArgs e)
        {
            OyunuBaslat();
        }

        // Kullanılmayan olay işleyicileri (boş bırakıldı)
        private void lstGecmis_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtTahmin_TextChanged(object sender, EventArgs e) { }

        // Oyuncu Değiştir butonu tıklama olayı
        private void btnOyuncuDegistir_Click(object sender, EventArgs e)
        {
            isSwitchingPlayer = true;
            
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is LoginForm loginForm)
                {
                    loginForm.ResetAndFocus();
                    loginForm.Show();
                    this.Close(); // Mevcut formu kapatarak kaynakları boşalt
                    return;
                }
            }

            LoginForm newLogin = new LoginForm();
            newLogin.Show();
            this.Close();
        }

        // Form kapatılırken tetiklenen olay override'ı
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            
            // Eğer oyuncu değiştirmiyorsa ve form kullanıcı tarafından kapatılıyorsa, uygulamayı tamamen sonlandır
            if (!isSwitchingPlayer && e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}
