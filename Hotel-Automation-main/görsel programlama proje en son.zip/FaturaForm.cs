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
    public partial class FaturaForm: Form
    {
        string connectionString = @"Data Source=DESKTOP-ID73HVR;Initial Catalog=Otel_Otomasyonu;Integrated Security=True;";

        public FaturaForm()
        {
            InitializeComponent();
        }

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            MuhasebeForm frm = new MuhasebeForm();
            frm.Show();
            this.Hide();
        }

        private void FaturaForm_Load(object sender, EventArgs e)
        {
            FaturalariGetir();
            FaturaIDleriGetir();
            cmbFaturaID.SelectedIndexChanged += cmbFaturaID_SelectedIndexChanged;
        }
        private void FaturaIDleriGetir()
        {
            string connectionString = @"Data Source=DESKTOP-ID73HVR;Initial Catalog=Otel_Otomasyonu;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT FaturaID FROM dbo.FaturaForm"; // Güncel sütun adı
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cmbFaturaID.Items.Clear();

                    while (reader.Read())
                    {
                        cmbFaturaID.Items.Add(reader["FaturaID"].ToString()); // Doğru sütun adı
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("FaturaID'leri getirirken hata: " + ex.Message);
                }
            }
        }
        private void FaturalariGetir()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM dbo.FaturaForm";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dtpFatura.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri getirme hatası: " + ex.Message);
                }
            }
        }
        private void cmbFaturaID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedID = cmbFaturaID.SelectedItem.ToString();
            string connectionString = @"Data Source=DESKTOP-ID73HVR;Initial Catalog=Otel_Otomasyonu;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM dbo.FaturaForm WHERE FaturaID = @FaturaID"; // Güncel sütun adı
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FaturaID", selectedID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtFaturaNo.Text = reader["MusteriFatura"].ToString();
                        cmbFaturaTarihi.Text = reader["MusteriFaturaTarihi"].ToString();
                        txtMusteriFirma.Text = reader["MusteriFirma"].ToString();
                        txtVergiDairesi.Text = reader["MusteriVergiDairesi"].ToString();
                        txtVergiNo.Text = reader["MusteriVergiNo"].ToString();
                        mskdtxtboxTelefon.Text = reader["MusteriTelefon"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fatura bilgisi getirme hatası: " + ex.Message);
                }
            }
        }


        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-ID73HVR;Initial Catalog=Otel_Otomasyonu;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO dbo.FaturaForm (MusteriFatura, MusteriFaturaTarihi, MusteriFirma, MusteriVergiDairesi, MusteriVergiNo, MusteriTelefon) " +
                                   "VALUES (@MusteriFatura, @MusteriFaturaTarihi, @MusteriFirma, @MusteriVergiDairesi, @MusteriVergiNo, @MusteriTelefon)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MusteriFatura", txtFaturaNo.Text);
                        cmd.Parameters.AddWithValue("@MusteriFaturaTarihi", cmbFaturaTarihi.Text); // Eğer tarih combobox ise
                        cmd.Parameters.AddWithValue("@MusteriFirma", txtMusteriFirma.Text);
                        cmd.Parameters.AddWithValue("@MusteriVergiDairesi", txtVergiDairesi.Text);
                        cmd.Parameters.AddWithValue("@MusteriVergiNo", txtVergiNo.Text);
                        cmd.Parameters.AddWithValue("@MusteriTelefon", mskdtxtboxTelefon.Text);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Fatura başarıyla kaydedildi.");
                    FaturalariGetir(); // GridView yenilensin
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                FaturaIDleriGetir();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (cmbFaturaID.SelectedItem == null)
            {
                MessageBox.Show("Lütfen silinecek bir FaturaID seçin.");
                return;
            }

            string selectedID = cmbFaturaID.SelectedItem.ToString();

            DialogResult result = MessageBox.Show("Seçilen faturayı silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM dbo.FaturaForm WHERE FaturaID = @FaturaID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FaturaID", selectedID);
                    int affected = cmd.ExecuteNonQuery();

                    if (affected > 0)
                    {
                        MessageBox.Show("Fatura başarıyla silindi.");

                        // Verileri güncelle
                        FaturalariGetir();
                        FaturaIDleriGetir();

                        // Form alanlarını temizle
                        txtFaturaNo.Clear();
                        cmbFaturaTarihi.Text = "";
                        txtMusteriFirma.Clear();
                        txtVergiDairesi.Clear();
                        txtVergiNo.Clear();
                        mskdtxtboxTelefon.Clear();
                        cmbFaturaID.SelectedItem = null;
                    }
                    else
                    {
                        MessageBox.Show("Silme işlemi başarısız. Kayıt bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Silme hatası: " + ex.Message);
                }
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (cmbFaturaID.SelectedItem == null)
            {
                MessageBox.Show("Lütfen güncellenecek bir FaturaID seçin.");
                return;
            }

            string selectedID = cmbFaturaID.SelectedItem.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE dbo.FaturaForm SET " +
                                   "MusteriFatura = @MusteriFatura, " +
                                   "MusteriFaturaTarihi = @MusteriFaturaTarihi, " +
                                   "MusteriFirma = @MusteriFirma, " +
                                   "MusteriVergiDairesi = @MusteriVergiDairesi, " +
                                   "MusteriVergiNo = @MusteriVergiNo, " +
                                   "MusteriTelefon = @MusteriTelefon " +
                                   "WHERE FaturaID = @FaturaID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MusteriFatura", txtFaturaNo.Text);
                    cmd.Parameters.AddWithValue("@MusteriFaturaTarihi", cmbFaturaTarihi.Text);
                    cmd.Parameters.AddWithValue("@MusteriFirma", txtMusteriFirma.Text);
                    cmd.Parameters.AddWithValue("@MusteriVergiDairesi", txtVergiDairesi.Text);
                    cmd.Parameters.AddWithValue("@MusteriVergiNo", txtVergiNo.Text);
                    cmd.Parameters.AddWithValue("@MusteriTelefon", mskdtxtboxTelefon.Text);
                    cmd.Parameters.AddWithValue("@FaturaID", selectedID);

                    int affected = cmd.ExecuteNonQuery();
                    if (affected > 0)
                    {
                        MessageBox.Show("Fatura başarıyla güncellendi.");
                        FaturalariGetir();
                        FaturaIDleriGetir();

                        // 🔽 Form alanlarını temizle
                        txtFaturaNo.Clear();
                        cmbFaturaTarihi.Text = "";
                        txtMusteriFirma.Clear();
                        txtVergiDairesi.Clear();
                        txtVergiNo.Clear();
                        mskdtxtboxTelefon.Clear();
                        cmbFaturaID.SelectedItem = null;
                    }
                    else
                    {
                        MessageBox.Show("Güncelleme başarısız. Belirtilen ID bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Güncelleme hatası: " + ex.Message);
                }
            }
        }

    }
}
