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
    public partial class kitapduzenle : Form
    {
        public anaform frmanaform;
        public kitapduzenle()
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
            frmanaform.komut = new OleDbCommand("update kitaplar set kitapadi=@adi,yazari=@yazaradi,barkot=@barkot,evi=@evi,stok=@stok where barkot=@kosul", frmanaform.baglanti);
            frmanaform.komut.Parameters.AddWithValue("@adi", textBox1.Text);
            frmanaform.komut.Parameters.AddWithValue("@yazaradi", textBox2.Text);
            frmanaform.komut.Parameters.AddWithValue("@barkot", textBox3.Text);
            frmanaform.komut.Parameters.AddWithValue("@evi", textBox4.Text);
            frmanaform.komut.Parameters.AddWithValue("@stok", int.Parse(textBox5.Text));
            frmanaform.komut.Parameters.AddWithValue("@kosul", frmanaform.datakitap.CurrentRow.Cells[0].Value.ToString());
            frmanaform.komut.ExecuteNonQuery();
            frmanaform.dataset.Tables["kitap_liste"].Clear();
            frmanaform.baglanti.Close();
            frmanaform.kitap_listele();
            MessageBox.Show("Düzenleme işlemi başarılı...");
        }

        private void kitapduzenle_Load(object sender, EventArgs e)
        {
            frmanaform.baglanti.Open();
            frmanaform.adaptor = new OleDbDataAdapter("select * from kitaplar where kitapadi='" + frmanaform.datakitap.CurrentRow.Cells[1].Value.ToString() + "'", frmanaform.baglanti);
            frmanaform.adaptor.Fill(frmanaform.dataset, "kitapduzenle");
            textBox1.Text = frmanaform.dataset.Tables["kitapduzenle"].Rows[0][1].ToString();
            textBox2.Text = frmanaform.dataset.Tables["kitapduzenle"].Rows[0][2].ToString();
            textBox3.Text = frmanaform.dataset.Tables["kitapduzenle"].Rows[0][3].ToString();
            textBox4.Text = frmanaform.dataset.Tables["kitapduzenle"].Rows[0][4].ToString();
            textBox5.Text = frmanaform.dataset.Tables["kitapduzenle"].Rows[0][5].ToString();
            frmanaform.baglanti.Close();
            frmanaform.dataset.Tables["kitapduzenle"].Clear();
        }
    }
}
