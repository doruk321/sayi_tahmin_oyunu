using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace görsel_programlama_proje_en_son
{
    public partial class OdayaEkleForm : Form
    {
        private int secilenOdaNo;
        private YeniMusteriForm anaForm;
        private string connectionString = @"Data Source=DESKTOP-ID73HVR;Initial Catalog=Otel_Otomasyonu;Integrated Security=True;";

        public OdayaEkleForm(int odaNo, YeniMusteriForm formRef)
        {
            InitializeComponent();
            secilenOdaNo = odaNo;
            anaForm = formRef;
        }

        private void OdayaEkleForm_Load(object sender, EventArgs e)
        {
            MusterileriListele();
        }

        private void MusterileriListele()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM musteriler";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dtpListele.DataSource = dt;

                    dtpListele.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dtpListele.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dtpListele.ReadOnly = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Müşteri listesi alınırken hata oluştu: " + ex.Message);
                }
            }
        }

        private void btnEkle_Click_1(object sender, EventArgs e)
        {
            if (dtpListele.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir müşteri seçin.");
                return;
            }

            string musteriID = dtpListele.SelectedRows[0].Cells["musteriID"].Value.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE musteriler SET odaID = @odaID WHERE musteriID = @musteriID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@odaID", secilenOdaNo);
                    cmd.Parameters.AddWithValue("@musteriID", musteriID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Odaya ekleme işlemi başarısız: " + ex.Message);
                    return;
                }
            }

            anaForm.OdayiKirmiziYap(secilenOdaNo);
            MessageBox.Show("Müşteri odaya başarıyla eklendi.");
            this.Close();
        }
    }
}