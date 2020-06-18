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
    public partial class kitapkirala : Form
    {
        public anaform frmanaform;
        public kitapkirala()
        {
            InitializeComponent();
        }
        public void Stok()
        {
            frmanaform.baglanti.Open();
            frmanaform.adaptor = new OleDbDataAdapter("select * from kitaplar where barkot='"+frmanaform.datakitap.CurrentRow.Cells[0].Value.ToString()+"'",frmanaform.baglanti);
            frmanaform.adaptor.Fill(frmanaform.dataset,"stok");
            textBox8.Text = frmanaform.dataset.Tables["stok"].Rows[0][5].ToString();
            frmanaform.dataset.Tables["stok"].Clear();
            frmanaform.adaptor.Dispose();
            frmanaform.baglanti.Close();
        }
        public void Stok_Guncelle()
        {
            int stoksayisi = int.Parse(textBox8.Text);
            stoksayisi = stoksayisi - 1;
            frmanaform.baglanti.Open();
            frmanaform.komut.Connection = frmanaform.baglanti;
            frmanaform.komut.CommandText = "update kitaplar set stok=@stok where barkot=@kosul";
            frmanaform.komut.Parameters.AddWithValue("@stok", stoksayisi.ToString());
            frmanaform.komut.Parameters.AddWithValue("@kosul", frmanaform.datakitap.CurrentRow.Cells[0].Value.ToString());
            frmanaform.komut.ExecuteNonQuery();
            frmanaform.komut.Dispose();
            frmanaform.baglanti.Close();
        }
        public decimal Uye_Ceza_Hesapla()
        {
            decimal ceza = 0;
            frmanaform.baglanti.Open();
            if (frmanaform.dataset.Tables["uyeceza"] != null)
            {
                frmanaform.dataset.Tables["uyeceza"].Clear();
            }
            frmanaform.adaptor = new OleDbDataAdapter("select ceza from kitapkirala where tc='" + frmanaform.datauye.CurrentRow.Cells[0].Value.ToString() + "'", frmanaform.baglanti);
            frmanaform.adaptor.Fill(frmanaform.dataset, "uyeceza");
            for (int i = 0; i < frmanaform.dataset.Tables["uyeceza"].Rows.Count; i++)
            {
                ceza = ceza + Convert.ToDecimal(frmanaform.dataset.Tables["uyeceza"].Rows[i][0].ToString());
            }
            frmanaform.adaptor.Dispose();
            frmanaform.baglanti.Close();
            return ceza;
        }
        public void KisiVarmi()
        {
            if (frmanaform.dataset.Tables["kisivarmi"] != null)
            {
                frmanaform.dataset.Tables["kisivarmi"].Clear();
            }
            frmanaform.baglanti.Open();
            frmanaform.adaptor = new OleDbDataAdapter("select * from kitapkirala where tc='" + textBox1.Text + "' and teslimatdurumu='HAYIR'", frmanaform.baglanti);
            frmanaform.adaptor.Fill(frmanaform.dataset, "kisivarmi");
            frmanaform.baglanti.Close();
        }
        private void kitapkirala_Load(object sender, EventArgs e)
        {
            textBox1.Text = frmanaform.datauye.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = frmanaform.datauye.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = frmanaform.datauye.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = frmanaform.datakitap.CurrentRow.Cells[0].Value.ToString();
            textBox5.Text = frmanaform.datakitap.CurrentRow.Cells[1].Value.ToString();
            textBox6.Text = DateTime.Today.ToShortDateString();
            textBox7.Text = DateTime.Today.AddDays(15).ToShortDateString();
            Stok();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Uye_Ceza_Hesapla()>0)
            {
                MessageBox.Show("Üyenin cezası var.Kitap kiralayamaz !"); 
            }
            else
            {
                if (int.Parse(textBox8.Text)==0)
                {
                    MessageBox.Show("Kitap stoğu bitti.Başka kitap kiralayabilirsiniz !");
                }
                else
                {
                    KisiVarmi();
                    if (frmanaform.dataset.Tables["kisivarmi"].Rows.Count<2)
                    {
                        frmanaform.baglanti.Open();
                        frmanaform.komut.Connection = frmanaform.baglanti;
                        frmanaform.komut.CommandText = "insert into kitapkirala(tc,adi,soyadi,barkod,kitap,kiratarihi,iadetarihi,teslimatdurumu,ceza) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + "HAYIR" + "','" + 0 + "')";
                        frmanaform.komut.ExecuteNonQuery();
                        frmanaform.baglanti.Close();
                        Stok_Guncelle();
                        frmanaform.dataset.Tables["kitapkirala_liste"].Clear();
                        frmanaform.kitap_kirala_listele();
                        MessageBox.Show("Kiralama işlemi başarılı.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcılar aynı anda 2 den fazla kitap kiralayamaz...");
                    }
                    
                }
            }
        }
    }
}
