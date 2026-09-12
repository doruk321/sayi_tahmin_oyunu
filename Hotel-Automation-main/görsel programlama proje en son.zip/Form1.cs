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
    public partial class GirisForm: Form
    {
        public GirisForm()
        {
            InitializeComponent();
        }

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            GirisForm form = new GirisForm();
            form.Show();
            this.Hide();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnStok_Click(object sender, EventArgs e)
        {
            StokForm form = new StokForm();
            form.Show();
            this.Hide();
        }

        private void GirisForm_Load(object sender, EventArgs e)
        {

        }
    }
}
