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
    public partial class kitapteslim : Form
    {
        public anaform frmanaform;
        public kitapteslim()
        {
            InitializeComponent();
        }
        public decimal UyeCezaHesapla()
        {
            decimal ceza = 0;
            frmanaform.baglanti.Open();
            if (frmanaform.dataset.Tables["uyelerceza"] != null)
            {
                frmanaform.dataset.Tables["uyelerceza"].Clear();
            }
            frmanaform.adaptor = new OleDbDataAdapter("select ceza from kitapkirala where tc='" + frmanaform.datakitapkirala.CurrentRow.Cells[0].Value.ToString() + "'", frmanaform.baglanti);
            frmanaform.adaptor.Fill(frmanaform.dataset, "uyelerceza");
            for (int i = 0; i < frmanaform.dataset.Tables["uyelerceza"].Rows.Count; i++)
            {
                ceza = ceza + Convert.ToDecimal(frmanaform.dataset.Tables["uyelerceza"].Rows[i][0].ToString());
            }

            frmanaform.adaptor.Dispose();
            frmanaform.baglanti.Close();
            return ceza;


        }

        private void kitapteslim_Load(object sender, EventArgs e)
        {
            textBox1.Text = frmanaform.datakitapkirala.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = frmanaform.datakitapkirala.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = frmanaform.datakitapkirala.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = frmanaform.datakitapkirala.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = frmanaform.datakitapkirala.CurrentRow.Cells[4].Value.ToString();
            textBox7.Text = DateTime.Now.ToShortDateString();
            textBox8.Text = Convert.ToString(UyeCezaHesapla());
            StokKontrol();
            if (UyeCezaHesapla() > 0)
            {
                button2.Enabled = true;
                button1.Enabled = false;
            }
            else
            {
                button2.Enabled = false;
                button1.Enabled = true;
            }
        }
        public void StokKontrol()
        {
            frmanaform.baglanti.Open();
            frmanaform.adaptor = new OleDbDataAdapter("select * from kitaplar where barkot='" + frmanaform.datakitapkirala.CurrentRow.Cells[3].Value.ToString() + "'", frmanaform.baglanti);
            frmanaform.adaptor.Fill(frmanaform.dataset, "kitapstok");
            textBox6.Text = frmanaform.dataset.Tables["kitapstok"].Rows[0][5].ToString();
            frmanaform.dataset.Tables["kitapstok"].Clear();
            frmanaform.baglanti.Close();
        }
        private void StokGuncelle()
        {
            int sayı = Convert.ToInt32(textBox6.Text);
            sayı = sayı + 1;
            frmanaform.baglanti.Open();
            frmanaform.komut.Connection = frmanaform.baglanti;
            frmanaform.komut.CommandText = "update kitaplar set stok=@stok where barkot=@kosul";
            frmanaform.komut.Parameters.AddWithValue("@stok", sayı.ToString());
            frmanaform.komut.Parameters.AddWithValue("@kosul", frmanaform.datakitap.CurrentRow.Cells[0].Value.ToString());
            frmanaform.komut.ExecuteNonQuery();
            frmanaform.komut.Dispose();
            frmanaform.baglanti.Close();
        }
        private void TarihGuncelle()
        {
            frmanaform.baglanti.Open();
            frmanaform.komut.Connection = frmanaform.baglanti;
            frmanaform.komut.CommandText = "update kitapkirala set iadetarihi=@iade where tc='" + frmanaform.datakitapkirala.CurrentRow.Cells[0].Value.ToString() + "' and barkod='" + frmanaform.datakitapkirala.CurrentRow.Cells[3].Value.ToString() + "'";
            frmanaform.komut.Parameters.AddWithValue("@iade", textBox7.Text);
            frmanaform.komut.ExecuteNonQuery();
            frmanaform.komut.Dispose();
            frmanaform.baglanti.Close();
        }
        private void CezaGuncelle()
        {
            int ceza = 0;
            frmanaform.baglanti.Open();
            frmanaform.komut.Connection = frmanaform.baglanti;
            frmanaform.komut.CommandText = "update kitapkirala set ceza=@ceza where barkod=@kosul and tc=@kosull";
            frmanaform.komut.Parameters.AddWithValue("@ceza", ceza.ToString());
            frmanaform.komut.Parameters.AddWithValue("@kosul", frmanaform.datakitapkirala.CurrentRow.Cells[3].Value.ToString());
            frmanaform.komut.Parameters.AddWithValue("@kosull", frmanaform.datakitapkirala.CurrentRow.Cells[0].Value.ToString());
            frmanaform.komut.ExecuteNonQuery();
            frmanaform.komut.Dispose();
            frmanaform.baglanti.Close();
        }
        public void UyeCezaİsle()
        {
            decimal sayı = UyeCezaHesapla();
            frmanaform.baglanti.Open();
            frmanaform.komut.Connection = frmanaform.baglanti;
            frmanaform.komut.CommandText = "insert into cezaode (tc,adi,soyadi,barkod,kitap,cezatarihi,ceza) values (@tc,@adi,@soyadi,@barkod,@kitapadi,@ceza,@ceza1)";
            frmanaform.komut.Parameters.AddWithValue("@tc", textBox1.Text.ToString());
            frmanaform.komut.Parameters.AddWithValue("@adi", textBox2.Text.ToString());
            frmanaform.komut.Parameters.AddWithValue("@soyadi", textBox3.Text.ToString());
            frmanaform.komut.Parameters.AddWithValue("@barkod", textBox4.Text.ToString());
            frmanaform.komut.Parameters.AddWithValue("@kitapadi", textBox5.Text.ToString());
            frmanaform.komut.Parameters.AddWithValue("@ceza", textBox7.Text.ToString());
            frmanaform.komut.Parameters.AddWithValue("@ceza1", sayı.ToString());
            frmanaform.komut.ExecuteNonQuery();
            frmanaform.komut.Dispose();
            frmanaform.baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("İade işlemi gerçekleşecek.Devam edilsin mi ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                frmanaform.baglanti.Open();
                frmanaform.adaptor = new OleDbDataAdapter("select * from kitapkirala where tc='" + frmanaform.datakitapkirala.CurrentRow.Cells[0].Value.ToString() + "' and barkod='" + frmanaform.datakitapkirala.CurrentRow.Cells[3].Value.ToString() + "'", frmanaform.baglanti);
                frmanaform.adaptor.Fill(frmanaform.dataset, "kitapteslim");
                string teslimatdurumu = frmanaform.dataset.Tables["kitapteslim"].Rows[0][7].ToString();
                frmanaform.dataset.Tables["kitapteslim"].Clear();
                frmanaform.baglanti.Close();
                teslimatdurumu = "EVET";
                frmanaform.baglanti.Open();
                frmanaform.komut.Connection = frmanaform.baglanti;
                frmanaform.komut.CommandText = "update kitapkirala set teslimatdurumu=@teslim where barkod=@kosul and tc=@kosull";
                frmanaform.komut.Parameters.AddWithValue("@teslim", teslimatdurumu.ToString());
                frmanaform.komut.Parameters.AddWithValue("@kosul", frmanaform.datakitapkirala.CurrentRow.Cells[3].Value.ToString());
                frmanaform.komut.Parameters.AddWithValue("@kosull", frmanaform.datakitapkirala.CurrentRow.Cells[0].Value.ToString());
                frmanaform.komut.ExecuteNonQuery();
                frmanaform.komut.Dispose();
                frmanaform.baglanti.Close();
                StokGuncelle();
                StokKontrol();
                TarihGuncelle();
                frmanaform.dataset.Tables["kitapkirala_liste"].Clear();
                this.Close();
                frmanaform.kitap_kirala_listele();
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UyeCezaİsle();
            CezaGuncelle();
            UyeCezaHesapla();
            textBox8.Text = Convert.ToString(UyeCezaHesapla());
            button1.Enabled = true;
            button2.Enabled = false;
        }
    }
}
