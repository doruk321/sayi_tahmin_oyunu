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
    public partial class OdemeForm: Form
    {
        
        public OdemeForm()
        {
            InitializeComponent();
        }

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            ResepsiyonForm form = new ResepsiyonForm();
            form.Show();
            this.Hide();
        }

        private void btnOdemeYap_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ödemeniz başarıyla gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public void OdemeYaz(string tutar)
        {
            lblOdeme.Text = tutar;
        }
    }
}
