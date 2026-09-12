using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace görsel_programlama_proje_en_son
{
    public partial class AdminGirisForm: Form
    {
        public AdminGirisForm()
        {
            InitializeComponent();
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text.Trim();
            string sifre = txtSifre.Text.Trim();
            string departman = cmbDepartman.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre) || string.IsNullOrEmpty(departman))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            string connectionString = @"Data Source=DESKTOP-ID73HVR;Initial Catalog=Otel_Otomasyonu;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE username = @username AND password = @password AND department = @department";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@username", kullaniciAdi);
                cmd.Parameters.AddWithValue("@password", sifre);
                cmd.Parameters.AddWithValue("@department", departman);

                try
                {
                    con.Open();
                    int sonuc = (int)cmd.ExecuteScalar();

                    if (sonuc > 0)
                    {
                        MessageBox.Show("Giriş başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Örnek: departmana göre farklı form aç
                        if (departman == "Resepsiyon")
                        {
                            new ResepsiyonForm().Show();
                        }
                        else if (departman == "Muhasebe")
                        {
                           MuhasebeForm form = new MuhasebeForm();
                            form.Show();
                            this.Hide();
                        }

                        // Mevcut formu gizle
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Hatalı kullanıcı adı, şifre veya departman.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAnaSayfa5_Click(object sender, EventArgs e)
        {
            ResepsiyonForm form = new ResepsiyonForm();
            form.Show();
            this.Hide();

            
        }

        private void AdminGirisForm_Load(object sender, EventArgs e)
        {

        }
    }
}
