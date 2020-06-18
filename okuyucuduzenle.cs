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
    public partial class okuyucuduzenle : Form
    {
        public anaform frmanaform;
        public okuyucuduzenle()
        {
            InitializeComponent();
        }

        private void okuyucuduzenle_Load(object sender, EventArgs e)
        {
            frmanaform.baglanti.Open();
            frmanaform.adaptor = new OleDbDataAdapter("select * from uyeler where tc='"+frmanaform.datauye.CurrentRow.Cells[0].Value.ToString()+"'",frmanaform.baglanti);
            frmanaform.adaptor.Fill(frmanaform.dataset,"okuyucuduzenle");
            textBox1.Text = frmanaform.dataset.Tables["okuyucuduzenle"].Rows[0][1].ToString();
            textBox2.Text = frmanaform.dataset.Tables["okuyucuduzenle"].Rows[0][2].ToString();
            textBox3.Text = frmanaform.dataset.Tables["okuyucuduzenle"].Rows[0][3].ToString();
            textBox4.Text = frmanaform.dataset.Tables["okuyucuduzenle"].Rows[0][4].ToString();
            textBox5.Text = frmanaform.dataset.Tables["okuyucuduzenle"].Rows[0][5].ToString();
            textBox6.Text = frmanaform.dataset.Tables["okuyucuduzenle"].Rows[0][6].ToString();
            frmanaform.baglanti.Close();
            frmanaform.dataset.Tables["okuyucuduzenle"].Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmanaform.baglanti.Open();
            frmanaform.komut = new OleDbCommand("update uyeler set tc=@tc,ad=@ad,soyad=@soyad,email=@email,telefon=@telefon,adres=@adres where tc=@kosul", frmanaform.baglanti);
            frmanaform.komut.Parameters.AddWithValue("@tc", textBox1.Text);
            frmanaform.komut.Parameters.AddWithValue("@ad", textBox2.Text);
            frmanaform.komut.Parameters.AddWithValue("@soyad", textBox3.Text);
            frmanaform.komut.Parameters.AddWithValue("@email", textBox4.Text);
            frmanaform.komut.Parameters.AddWithValue("@telefon", textBox5.Text);
            frmanaform.komut.Parameters.AddWithValue("@adres", textBox6.Text);
            frmanaform.komut.Parameters.AddWithValue("@kosul", frmanaform.datauye.CurrentRow.Cells[0].Value.ToString());
            frmanaform.komut.ExecuteNonQuery();
            frmanaform.dataset.Tables["uye_liste"].Clear();
            frmanaform.baglanti.Close();
            frmanaform.uye_listele();
            MessageBox.Show("Düzenleme işlemi başarılı...");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
