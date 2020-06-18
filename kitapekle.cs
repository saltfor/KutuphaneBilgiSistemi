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
    public partial class kitapekle : Form
    {
        public anaform frmanaform;
        public kitapekle()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmanaform.baglanti.Open();
            frmanaform.komut.Connection = frmanaform.baglanti;
            frmanaform.komut.CommandText = "insert into kitaplar (kitapadi,yazari,barkot,evi,stok) values(@ad,@yazarad,@barkod,@evi,@stok)";
            frmanaform.komut.Parameters.AddWithValue("@ad", textBox1.Text);
            frmanaform.komut.Parameters.AddWithValue("@yazarad", textBox2.Text);
            frmanaform.komut.Parameters.AddWithValue("@barkod", textBox3.Text);
            frmanaform.komut.Parameters.AddWithValue("@evi", textBox4.Text);
            frmanaform.komut.Parameters.AddWithValue("@stok", int.Parse(textBox5.Text));
            frmanaform.komut.ExecuteNonQuery();
            frmanaform.komut.Dispose();
            frmanaform.dataset.Tables["kitap_liste"].Clear();
            frmanaform.baglanti.Close();
            frmanaform.kitap_listele();
            this.Close();
        }
    }
}
