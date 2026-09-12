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
    public partial class StokForm : Form
    {
        string connectionString = "Server=DESKTOP-ID73HVR;Database=Otel_Otomasyonu;Trusted_Connection=True;";


        public StokForm()
        {
            InitializeComponent();
        }

        private void StokForm_Load(object sender, EventArgs e)
        {
            UrunIDleriYenile();
            VerileriListele();

        }

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            ResepsiyonForm frm = new ResepsiyonForm();
            frm.Show();
            this.Hide();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Stok (urunADI, Kategori, Miktar, Birim, [Min Stok], Tedarikci, Tedarik, Aciklama) " +
                                   "VALUES (@urunADI, @Kategori, @Miktar, @Birim, @MinStok, @Tedarikci, @Tedarik, @Aciklama)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@urunADI", txtUrunAdi.Text);
                    command.Parameters.AddWithValue("@Kategori", cmbKategori.SelectedItem?.ToString() ?? "");
                    command.Parameters.AddWithValue("@Miktar", txtMiktar.Text);
                    command.Parameters.AddWithValue("@Birim", cmbBirim.SelectedItem?.ToString() ?? "");
                    command.Parameters.AddWithValue("@MinStok", txtMinStok.Text);
                    command.Parameters.AddWithValue("@Tedarikci", txtTedarikci.Text);
                    command.Parameters.AddWithValue("@Tedarik", cmbTedarik.SelectedItem?.ToString() ?? "");
                    command.Parameters.AddWithValue("@Aciklama", txtAciklama.Text);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Ürün başarıyla eklendi!");

                    // İsteğe bağlı: urunID ComboBox'ını güncelle
                    UrunIDleriYenile();
                    VerileriListele();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }

        }

        private void UrunIDleriYenile()
        {
            cmbUrunID.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT urunID FROM Stok", connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        cmbUrunID.Items.Add(reader["urunID"].ToString());
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("UrunID listesi alınamadı: " + ex.Message);
                }
            }
        }
        private void VerileriListele()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Stok";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Listeleme hatası: " + ex.Message);
                }
            }
        }
        private void cmbUrunID_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Stok WHERE urunID = @urunID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@urunID", cmbUrunID.SelectedItem?.ToString());

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtUrunAdi.Text = reader["urunADI"].ToString();
                        cmbKategori.SelectedItem = reader["Kategori"].ToString();
                        txtMiktar.Text = reader["Miktar"].ToString();
                        cmbBirim.SelectedItem = reader["Birim"].ToString();
                        txtMinStok.Text = reader["Min Stok"].ToString();
                        txtTedarikci.Text = reader["Tedarikci"].ToString();
                        cmbTedarik.SelectedItem = reader["Tedarik"].ToString();
                        txtAciklama.Text = reader["Aciklama"].ToString();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri getirme hatası: " + ex.Message);
                }
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            // ComboBox'tan seçim yapılmamışsa uyarı ver
            if (cmbUrunID.SelectedItem == null)
            {
                MessageBox.Show("Lütfen silinecek bir urunID seçin.");
                return;
            }

            // Seçilen urunID'yi al
            int urunID = Convert.ToInt32(cmbUrunID.SelectedItem.ToString());

            // Silme işlemi
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "DELETE FROM Stok WHERE urunID = @urunID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@urunID", urunID);

                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Ürün başarıyla silindi.");

                        // Arayüzü güncelle
                        VerileriListele();
                        UrunIDleriYenile();

                        // Alanları temizle (isteğe bağlı)
                        TemizleForm();
                    }
                    else
                    {
                        MessageBox.Show("Silme işlemi başarısız.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void TemizleForm()
        {
            cmbUrunID.SelectedIndex = -1;
            txtUrunAdi.Clear();
            cmbKategori.SelectedIndex = -1;
            txtMiktar.Clear();
            cmbBirim.SelectedIndex = -1;
            txtMinStok.Clear();
            txtTedarikci.Clear();
            cmbTedarik.SelectedIndex = -1;
            txtAciklama.Clear();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            // ComboBox'tan seçim yapılmış mı kontrol et
            if (cmbUrunID.SelectedItem == null)
            {
                MessageBox.Show("Lütfen güncellenecek bir urunID seçin.");
                return;
            }

            // Seçilen urunID'yi al
            int urunID = Convert.ToInt32(cmbUrunID.SelectedItem.ToString());

            // SQL bağlantısı ve güncelleme işlemi
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"UPDATE Stok SET 
                                urunADI = @urunADI, 
                                Kategori = @Kategori, 
                                Miktar = @Miktar, 
                                Birim = @Birim, 
                                [Min Stok] = @MinStok, 
                                Tedarikci = @Tedarikci, 
                                Tedarik = @Tedarik, 
                                Aciklama = @Aciklama
                            WHERE urunID = @urunID";

                    SqlCommand command = new SqlCommand(query, connection);

                    // Parametreleri aktar
                    command.Parameters.AddWithValue("@urunID", urunID);
                    command.Parameters.AddWithValue("@urunADI", txtUrunAdi.Text);
                    command.Parameters.AddWithValue("@Kategori", cmbKategori.Text);
                    command.Parameters.AddWithValue("@Miktar", txtMiktar.Text);
                    command.Parameters.AddWithValue("@Birim", cmbBirim.Text);
                    command.Parameters.AddWithValue("@MinStok", txtMinStok.Text);
                    command.Parameters.AddWithValue("@Tedarikci", txtTedarikci.Text);
                    command.Parameters.AddWithValue("@Tedarik", cmbTedarik.Text);
                    command.Parameters.AddWithValue("@Aciklama", txtAciklama.Text);

                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Güncelleme başarılı!");
                        VerileriListele();     // Tabloyu güncelle
                        UrunIDleriYenile();   // ComboBox'ı da güncelle
                    }
                    else
                    {
                        MessageBox.Show("Güncelleme başarısız!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
    }
}
