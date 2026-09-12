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
    public partial class MusterilerForm: Form
    {
        SqlConnection baglanti = new SqlConnection(@"Server=DESKTOP-ID73HVR;Database=Otel_Otomasyonu;Integrated Security=True");
        string connectionString = @"Data Source=DESKTOP-ID73HVR;Initial Catalog=Otel_Otomasyonu;Integrated Security=True";

        public void MusteriListesiYukle(DataTable dt)
        {
            dtpMusterilerForm.DataSource = dt;
        }
        public MusterilerForm()
        {
            InitializeComponent();
        }

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            ResepsiyonForm form = new ResepsiyonForm();
            form.Show();
            this.Hide();
        }

       

        private void btnOdayıTemizle_Click(object sender, EventArgs e)
        {

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMusteriID.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen güncellenecek müşteriyi seçin!");
                    return;
                }

                int musteriID = Convert.ToInt32(cmbMusteriID.SelectedItem);
                

                baglanti.Open();

                SqlCommand komut = new SqlCommand(@"
            UPDATE dbo.Musteriler 
            SET 
                MusteriAdi = @MusteriAdi,
                MusteriSoyadi = @MusteriSoyadi,
                MusteriCinsiyeti = @MusteriCinsiyeti,
                MusteriTelefonu = @MusteriTelefonu,
                MusteriMaili = @MusteriMaili,
                MusteriTC = @MusteriTC,
                odaID = @odaID,
                MusteriUcret = @MusteriUcret,
                MusteriGiris = @MusteriGiris,
                MusteriCikis = @MusteriCikis
            WHERE musteriID = @musteriID", baglanti);

                // Parametreleri ekle
                komut.Parameters.AddWithValue("@MusteriAdi", txtAdi.Text);
                komut.Parameters.AddWithValue("@MusteriSoyadi", txtSoyadi.Text);
                komut.Parameters.AddWithValue("@MusteriCinsiyeti", cmbCinsiyet.Text);
                komut.Parameters.AddWithValue("@MusteriTelefonu", mskdTxtTelefon.Text);
                komut.Parameters.AddWithValue("@MusteriMaili", txtMail.Text);
                komut.Parameters.AddWithValue("@MusteriTC", txtTC.Text);
                komut.Parameters.AddWithValue("@odaID", txtOdaNumarasi.Text);
                komut.Parameters.AddWithValue("@MusteriUcret", txtUcret.Text);
                komut.Parameters.AddWithValue("@MusteriGiris", dtpGiris.Value);
                komut.Parameters.AddWithValue("@MusteriCikis", dtpCikis.Value);
                komut.Parameters.AddWithValue("@musteriID", musteriID);

                int sonuc = komut.ExecuteNonQuery();
                baglanti.Close();

                if (sonuc > 0)
                {
                    MessageBox.Show("Müşteri bilgileri başarıyla güncellendi.");
                    MusteriListele(); // DataGridView güncelle
                }
                else
                {
                    MessageBox.Show("Güncelleme başarısız. Böyle bir müşteri bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            }
        }

        private void btnVerileriGoster_Click(object sender, EventArgs e)
        {

        }
        private void VeriGetir()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM dbo.Musteriler";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dtpMusterilerForm.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void dtpMusterilerForm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MusterilerForm_Load(object sender, EventArgs e)
        {  MusteriListele();
            VeriGetir();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT MusteriID FROM dbo.Musteriler";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cmbMusteriID.Items.Clear();
                    while (reader.Read())
                    {
                        cmbMusteriID.Items.Add(reader["MusteriID"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void cmbMusteriID_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                string secilenID = cmbMusteriID.SelectedItem.ToString();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT * FROM dbo.Musteriler WHERE MusteriID = @MusteriID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@MusteriID", secilenID);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            txtAdi.Text = reader["MusteriAdi"].ToString();
                            txtSoyadi.Text = reader["MusteriSoyadi"].ToString();
                            cmbCinsiyet.Text = reader["MusteriCinsiyeti"].ToString();
                            mskdTxtTelefon.Text = reader["MusteriTelefonu"].ToString();
                            txtMail.Text = reader["MusteriMaili"].ToString();
                            txtTC.Text = reader["MusteriTC"].ToString();
                            txtUcret.Text = reader["MusteriUcret"].ToString();
                            txtOdaNumarasi.Text = reader["odaID"].ToString();

                            // DateTime verilerini DateTimePicker'a set et
                            if (DateTime.TryParse(reader["MusteriGiris"].ToString(), out DateTime giris))
                                dtpGiris.Value = giris;

                            if (DateTime.TryParse(reader["MusteriCikis"].ToString(), out DateTime cikis))
                                dtpCikis.Value = cikis;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
                cmbMusteriID.SelectedIndexChanged += cmbMusteriID_SelectedIndexChanged;

            }
        }
        private void MusteriListele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM dbo.Musteriler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dtpMusterilerForm.DataSource = dt;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                // Seçilen müşteri ID'yi al
                if (cmbMusteriID.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen silinecek müşteriyi seçin!");
                    return;
                }

                string musteriID = cmbMusteriID.SelectedItem.ToString();

                // Bağlantıyı aç
                baglanti.Open();

                // SQL silme komutu
                SqlCommand komut = new SqlCommand("DELETE FROM dbo.Musteriler WHERE MusteriID = @id", baglanti);
                komut.Parameters.AddWithValue("@id", musteriID);

                int sonuc = komut.ExecuteNonQuery();

                // Bağlantıyı kapat
                baglanti.Close();

                if (sonuc > 0)
                {
                    MessageBox.Show("Müşteri başarıyla silindi.");
                    MusteriListele(); // DataGridView güncelle
                }
                else
                {
                    MessageBox.Show("Silme işlemi başarısız. Böyle bir müşteri bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            }
        }
    }
}
