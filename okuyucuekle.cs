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
    public partial class okuyucuekle : Form
    {
        public anaform frmanaform;
        public okuyucuekle()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 11 && Convert.ToInt32(textBox1.Text.Substring(10,1)) % 2 == 0)
            {
                if (textBox4.Text.IndexOf('@')!=-1)
                {
                    if (textBox5.TextLength==10 || textBox5.TextLength==11)
                    {
                        frmanaform.baglanti.Open();
                        frmanaform.komut.Connection = frmanaform.baglanti;
                        frmanaform.komut.CommandText = "insert into uyeler (tc,ad,soyad,email,telefon,adres) values(@tc,@ad,@soyad,@email,@telefon,@adres)";
                        frmanaform.komut.Parameters.AddWithValue("@tc", textBox1.Text);
                        frmanaform.komut.Parameters.AddWithValue("@ad", textBox2.Text);
                        frmanaform.komut.Parameters.AddWithValue("@soyad", textBox3.Text);
                        frmanaform.komut.Parameters.AddWithValue("@email", textBox4.Text);
                        frmanaform.komut.Parameters.AddWithValue("@telefon", textBox5.Text);
                        frmanaform.komut.Parameters.AddWithValue("@adres", textBox6.Text);
                        frmanaform.komut.ExecuteNonQuery();
                        frmanaform.komut.Dispose();
                        frmanaform.dataset.Tables["uye_liste"].Clear();
                        frmanaform.baglanti.Close();
                        frmanaform.uye_listele();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Telefon doğru olmalıdır !", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("E-Mail adresi doğru olmalıdır !", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TC Kimlik Numarası doğru olmalıdır !","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
