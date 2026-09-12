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
    public partial class HakkımızdaForm: Form
    {
        public HakkımızdaForm()
        {
            InitializeComponent();
        }

        private void btnAnaSayfa2_Click(object sender, EventArgs e)
        {
            ResepsiyonForm form = new ResepsiyonForm();
            form.Show();
            this.Hide();
        }
    }
}
