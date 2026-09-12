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
    public partial class OdalarForm: Form
    {

        private string connectionString = @"Data Source=DESKTOP-ID73HVR;Initial Catalog=Otel_Otomasyonu;Integrated Security=True;";

        public OdalarForm()
        {
            InitializeComponent();
        }

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            ResepsiyonForm form = new ResepsiyonForm();
            form.Show();
            this.Hide();
        }

        private void OdalarForm_Load(object sender, EventArgs e)
        {
            OdaBilgileriniYukle();
        }

        private void OdaBilgileriniYukle()
        {
            for (int odaID = 1; odaID <= 9; odaID++)
            {
                string musteriBilgisi = MusteriGetir(odaID);
                Button hedefButon = ButonGetir(odaID);

                if (hedefButon != null)
                {
                    hedefButon.Text = musteriBilgisi;

                    // Eğer müşteri varsa, buton rengini kırmızı yap
                    if (musteriBilgisi != "Boş")
                    {
                        hedefButon.BackColor = Color.Red;
                    }
                    // Eğer müşteri yoksa, varsayılan rengini koru (yeşil vs.)
                }
            }
        }

        private string MusteriGetir(int odaID)
        {
            string bilgi = "Boş";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TOP 1 MusteriAdi, MusteriSoyadi FROM musteriler WHERE odaID = @odaID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@odaID", odaID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string ad = reader["MusteriAdi"].ToString();
                        string soyad = reader["MusteriSoyadi"].ToString();
                        bilgi = ad + " " + soyad;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Oda bilgisi alınamadı: " + ex.Message);
                }
            }

            return bilgi;
        }

        // odaID'ye karşılık gelen butonu döndür
        private Button ButonGetir(int odaID)
        {
            switch (odaID)
            {
                case 1: return btnOda1;
                case 2: return btnOda2;
                case 3: return btnOda3;
                case 4: return btnOda4;
                case 5: return btnOda5;
                case 6: return btnOda6;
                case 7: return btnOda7;
                case 8: return btnOda8;
                case 9: return btnOda9;
                default: return null;
            }
        }
    }
}
