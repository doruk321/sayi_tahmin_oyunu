using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace görsel_programlama_proje_en_son
{
    public partial class SifreForm: Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-ID73HVR;Initial Catalog=Otel_Otomasyonu;Integrated Security=True");
        public SifreForm()
        {
            InitializeComponent();
        }

        private void btnAnaSayfa4_Click(object sender, EventArgs e)
        {
            ResepsiyonForm form = new ResepsiyonForm();
            form.Show();
            this.Hide();
        }

        private void SifreForm_Load(object sender, EventArgs e)
        {

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text;
            string eskiSifre = txtEskiSifre.Text;
            string yeniSifre = txtYeniSifre.Text;

            try
            {
                baglanti.Open();

                // Kullanıcı adı (department) ve eski şifre (password) kontrolü
                SqlCommand kontrolCmd = new SqlCommand("SELECT COUNT(*) FROM [Users] WHERE department = @kadi AND password = @eskiSifre", baglanti);
                kontrolCmd.Parameters.AddWithValue("@kadi", kullaniciAdi);
                kontrolCmd.Parameters.AddWithValue("@eskiSifre", eskiSifre);

                int kullaniciVarMi = (int)kontrolCmd.ExecuteScalar();

                if (kullaniciVarMi > 0)
                {
                    // Şifreyi güncelle (password sütununa)
                    SqlCommand guncelleCmd = new SqlCommand("UPDATE [Users] SET password = @yeniSifre WHERE department = @kadi", baglanti);
                    guncelleCmd.Parameters.AddWithValue("@yeniSifre", yeniSifre);
                    guncelleCmd.Parameters.AddWithValue("@kadi", kullaniciAdi);

                    guncelleCmd.ExecuteNonQuery();

                    MessageBox.Show("Şifre başarıyla güncellendi.");
                }
                else
                {
                    MessageBox.Show("Bölüm (department) adı veya eski şifre hatalı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }






    }
}
