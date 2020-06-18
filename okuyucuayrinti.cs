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
    public partial class okuyucuayrinti : Form
    {
        public anaform frmanaform;
        public okuyucuayrinti()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okuyucuayrinti_Load(object sender, EventArgs e)
        {
            frmanaform.baglanti.Open();
            frmanaform.adaptor = new OleDbDataAdapter("select * from uyeler where tc='"+frmanaform.datauye.CurrentRow.Cells[0].Value.ToString()+"'",frmanaform.baglanti);
            frmanaform.adaptor.Fill(frmanaform.dataset,"okuyucu_ayrıntı");
            labeltc.Text=frmanaform.dataset.Tables["okuyucu_ayrıntı"].Rows[0][1].ToString();
            labelad.Text = frmanaform.dataset.Tables["okuyucu_ayrıntı"].Rows[0][2].ToString();
            labelsoyad.Text = frmanaform.dataset.Tables["okuyucu_ayrıntı"].Rows[0][3].ToString();
            labelemail.Text = frmanaform.dataset.Tables["okuyucu_ayrıntı"].Rows[0][4].ToString();
            labeltel.Text = frmanaform.dataset.Tables["okuyucu_ayrıntı"].Rows[0][5].ToString();
            labeladres.Text = frmanaform.dataset.Tables["okuyucu_ayrıntı"].Rows[0][6].ToString();
            frmanaform.baglanti.Close();
            frmanaform.dataset.Tables["okuyucu_ayrıntı"].Clear();
        }
    }
}
