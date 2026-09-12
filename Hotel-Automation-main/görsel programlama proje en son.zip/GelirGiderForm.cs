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
    public partial class GelirGiderForm: Form
    {
        public static GelirGiderForm Instance;
        public GelirGiderForm()
        {
            InitializeComponent();
            
        }
        public void KasaToplaminiGoster()
        {
            string connectionString = @"Data Source=DESKTOP-ID73HVR;Initial Catalog=Otel_Otomasyonu;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT SUM(CAST(MusteriUcret AS DECIMAL(18,2))) FROM musteriler";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                        lblKasa.Text = result.ToString() + " ₺";
                    else
                        lblKasa.Text = "0 ₺";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kasa bilgisi alınırken hata: " + ex.Message);
                }
            }
        }





        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            MuhasebeForm form = new MuhasebeForm();
            form.Show();
            this.Hide();

        }

        private void GelirGiderForm_Load(object sender, EventArgs e)
        {
            Instance = this; // Form yüklendiğinde static referans oluştur
            KasaToplaminiGoster(); // Form açılırken kasa toplamını göster
        }
    }
}
