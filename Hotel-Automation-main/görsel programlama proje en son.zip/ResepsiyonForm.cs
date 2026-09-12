using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace görsel_programlama_proje_en_son
{
    public partial class ResepsiyonForm: Form
    {
        string connectionString = "Server=DESKTOP-ID73HVR;Database=Otel_Otomasyonu;Trusted_Connection=True;";

        public ResepsiyonForm()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Start();
            GuncelleTarihSaat();
        }

        private void GuncelleTarihSaat()
        {
            lblTarih.Text = DateTime.Now.ToLongDateString();
            lblSaat.Text = DateTime.Now.ToLongTimeString();
        }

        private void BtnAdminGiris_Click(object sender, EventArgs e)
        {
            AdminGirisForm form = new AdminGirisForm();
            form.Show();
            this.Hide();
            
        }

        private void BtnStok_Click(object sender, EventArgs e)
        {
            StokForm form = new StokForm();
            form.Show();
            this.Hide();
        }

        private void BtnRadyo_Click(object sender, EventArgs e)
        {
            RadyoDinleForm form = new RadyoDinleForm();
            form.Show();
            
        }

        private void BtnSifre_Click(object sender, EventArgs e)
        {
            SifreForm form = new SifreForm();
            form.Show();
            this.Hide();
        }

        private void BtnHakkimizda_Click(object sender, EventArgs e)
        {
            HakkımızdaForm form = new HakkımızdaForm();
            form.Show();
            this.Hide();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GuncelleTarihSaat();
        }

        private void lblTarih_Click(object sender, EventArgs e)
        {

        }

        private void btnYeniMusteri_Click(object sender, EventArgs e)
        {
            YeniMusteriForm form = new YeniMusteriForm();
            form.Show();
            this.Hide();
        }

        private void ResepsiyonForm_Load(object sender, EventArgs e)
        {

        }

        private void btnOdalar_Click(object sender, EventArgs e)
        {
            OdalarForm form = new OdalarForm();
            form.Show();
            this.Hide();
        }

        private void btnMusteriler_Click(object sender, EventArgs e)
        {
            MusterilerForm form = new MusterilerForm();
            form.ShowDialog();
            this.Close();
        }
    }
}
