using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace görsel_programlama_proje_en_son
{
    public partial class MuhasebeForm: Form
    {
        public MuhasebeForm()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
            GuncelleTarihSaat();
        }
        private void GuncelleTarihSaat()
        {
            lblTarih.Text = DateTime.Now.ToLongDateString();
            lblSaat.Text = DateTime.Now.ToLongTimeString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminGirisForm form = new AdminGirisForm();
            form.Show();
            this.Hide();
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RadyoDinleForm form = new RadyoDinleForm();
            form.Show();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SifreForm form = new SifreForm();
            form.Show();
            this.Hide();
        }

        private void MuhasebeForm_Load(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            GuncelleTarihSaat();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FaturaForm form = new FaturaForm();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GelirGiderForm form = new GelirGiderForm();
            form.Show();
            this.Hide();
        }
    }
}
