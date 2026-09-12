using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace görsel_programlama_proje_en_son
{
    public partial class YeniMusteriForm : Form
    {
        string connectionString = "Server=DESKTOP-ID73HVR;Database=Otel_Otomasyonu;Trusted_Connection=True;";

        public YeniMusteriForm()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.YeniMusteriForm_Load);
        }

        private void YeniMusteriForm_Load(object sender, EventArgs e)
        {
            dtpGiris.ValueChanged += dtpGiris_ValueChanged;
            dtpCikis.ValueChanged += dtpCikis_ValueChanged;

            OdaDurumlariniKontrolEt(); // 🔴 Form açıldığında dolu odaları kırmızı yap
        }

        private void MusteriVerileriniYukle()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Musteriler";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (!TcKimlikDogrula(txtTC.Text.Trim()))
            {
                MessageBox.Show("Geçersiz TC Kimlik Numarası!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!MailDogrula(txtMail.Text.Trim()))
            {
                MessageBox.Show("Geçersiz e-posta adresi! Lütfen doğru formatta e-posta girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"INSERT INTO Musteriler 
                (MusteriAdi, MusteriSoyadi, MusteriCinsiyeti, MusteriTelefonu, MusteriMaili, MusteriTC, MusteriUcret, MusteriGiris, MusteriCikis)
                VALUES 
                (@Adi, @Soyadi, @Cinsiyet, @Telefon, @Mail, @TC, @Ucret, @Giris, @Cikis)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Adi", txtAd.Text);
                    command.Parameters.AddWithValue("@Soyadi", txtSoyadi.Text);
                    command.Parameters.AddWithValue("@Cinsiyet", cmbCinsiyet.Text);
                    command.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
                    command.Parameters.AddWithValue("@Mail", txtMail.Text);
                    command.Parameters.AddWithValue("@TC", txtTC.Text.Trim());
                    command.Parameters.AddWithValue("@Ucret", txtUcret.Text);
                    command.Parameters.AddWithValue("@Giris", dtpGiris.Value.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Cikis", dtpCikis.Value.ToString("yyyy-MM-dd"));

                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Müşteri başarıyla kaydedildi.");
                        MusteriVerileriniYukle();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt başarısız.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void btnOdeme_Click(object sender, EventArgs e)
        {
            string ucret = txtUcret.Text;
            OdemeForm odemeForm = new OdemeForm();
            odemeForm.OdemeYaz(ucret);
            odemeForm.Show();
        }

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            ResepsiyonForm form = new ResepsiyonForm();
            form.Show();
            this.Hide();
        }

        private void dtpGiris_ValueChanged(object sender, EventArgs e)
        {
            UcretHesapla();
        }

        private void dtpCikis_ValueChanged(object sender, EventArgs e)
        {
            UcretHesapla();
        }

        private void UcretHesapla()
        {
            DateTime giris = dtpGiris.Value.Date;
            DateTime cikis = dtpCikis.Value.Date;
            int gun = (cikis - giris).Days;

            if (gun <= 0)
            {
                txtUcret.Text = "0";
                MessageBox.Show("Çıkış tarihi giriş tarihinden sonra olmalıdır.");
                return;
            }

            txtUcret.Text = (gun * 500).ToString();
        }

        private bool TcKimlikDogrula(string tc)
        {
            tc = tc.Trim();

            if (tc.Length != 11 || !tc.All(char.IsDigit) || tc.StartsWith("0")) return false;

            int[] d = tc.Select(c => int.Parse(c.ToString())).ToArray();
            int t10 = ((d[0] + d[2] + d[4] + d[6] + d[8]) * 7 - (d[1] + d[3] + d[5] + d[7])) % 10;
            int t11 = d.Take(10).Sum() % 10;

            return d[9] == t10 && d[10] == t11;
        }

        private bool MailDogrula(string email)
        {
            string desen = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|edu|mil)$";
            return Regex.IsMatch(email, desen);
        }

        // Oda butonları
        private void btnOda1_Click(object sender, EventArgs e) { new OdayaEkleForm(1, this).Show(); }
        private void btnOda2_Click(object sender, EventArgs e) { new OdayaEkleForm(2, this).Show(); }
        private void btnOda3_Click(object sender, EventArgs e) { new OdayaEkleForm(3, this).Show(); }
        private void btnOda4_Click(object sender, EventArgs e) { new OdayaEkleForm(4, this).Show(); }
        private void btnOda5_Click(object sender, EventArgs e) { new OdayaEkleForm(5, this).Show(); }
        private void btnOda6_Click(object sender, EventArgs e) { new OdayaEkleForm(6, this).Show(); }
        private void btnOda7_Click(object sender, EventArgs e) { new OdayaEkleForm(7, this).Show(); }
        private void button8_Click(object sender, EventArgs e) { new OdayaEkleForm(8, this).Show(); }
        private void btnOda9_Click(object sender, EventArgs e) { new OdayaEkleForm(9, this).Show(); }

        public void OdayiKirmiziYap(int odaNo)
        {
            switch (odaNo)
            {
                case 1:
                    btnOda1.BackColor = Color.Red;
                    btnOda1.Enabled = false;
                    break;
                case 2:
                    btnOda2.BackColor = Color.Red;
                    btnOda2.Enabled = false;
                    break;
                case 3:
                    btnOda3.BackColor = Color.Red;
                    btnOda3.Enabled = false;
                    break;
                case 4:
                    btnOda4.BackColor = Color.Red;
                    btnOda4.Enabled = false;
                    break;
                case 5:
                    btnOda5.BackColor = Color.Red;
                    btnOda5.Enabled = false;
                    break;
                case 6:
                    btnOda6.BackColor = Color.Red;
                    btnOda6.Enabled = false;
                    break;
                case 7:
                    btnOda7.BackColor = Color.Red;
                    btnOda7.Enabled = false;
                    break;
                case 8:
                    btnOda8.BackColor = Color.Red;
                    btnOda8.Enabled = false;
                    break;
                case 9:
                    btnOda9.BackColor = Color.Red;
                    btnOda9.Enabled = false;
                    break;
            }
        }

        // 🔴 Yeni eklenen metod: Uygulama açıldığında dolu odaları kırmızı yap
        private void OdaDurumlariniKontrolEt()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT DISTINCT odaID FROM Musteriler WHERE odaID IS NOT NULL";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int doluOda = Convert.ToInt32(reader["odaID"]);
                        OdayiKirmiziYap(doluOda);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Oda durumu alınamadı: " + ex.Message);
                }
            }
        }
    }
}