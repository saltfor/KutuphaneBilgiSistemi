using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace otomasyon_deneme
{
    public partial class kitapayrinti : Form
    {
        public anaform frmanaform;
        public kitapayrinti()
        {
            InitializeComponent();
        }

        private void kitapayrinti_Load(object sender, EventArgs e)
        {
            frmanaform.baglanti.Open();
            frmanaform.adaptor = new OleDbDataAdapter("select * from kitaplar where barkot='" + frmanaform.datakitap.CurrentRow.Cells[0].Value.ToString() + "'", frmanaform.baglanti);
            frmanaform.adaptor.Fill(frmanaform.dataset, "kitap_ayrıntı");
            labelkitapadi.Text = frmanaform.dataset.Tables["kitap_ayrıntı"].Rows[0][1].ToString();
            labelyazar.Text = frmanaform.dataset.Tables["kitap_ayrıntı"].Rows[0][2].ToString();
            labelbarkod.Text = frmanaform.dataset.Tables["kitap_ayrıntı"].Rows[0][3].ToString();
            labelyayınevi.Text = frmanaform.dataset.Tables["kitap_ayrıntı"].Rows[0][4].ToString();
            labelstok.Text = frmanaform.dataset.Tables["kitap_ayrıntı"].Rows[0][5].ToString();
            frmanaform.baglanti.Close();
            frmanaform.dataset.Tables["kitap_ayrıntı"].Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
