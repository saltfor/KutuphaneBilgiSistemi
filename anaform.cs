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
    public partial class anaform : Form
    {
        public okuyucuekle frmokuyucuekle;
        public okuyucuduzenle frmokuyucuduzenle;
        public okuyucuayrinti frmokuyucuayrinti;
        public kitapekle frmkitapekle;
        public kitapduzenle frmkitapduzenle;
        public kitapayrinti frmkitapayrinti;
        public kitapkirala frmkitapkirala;
        public kitapteslim frmkitapteslim;
        public anaform()
        {
            InitializeComponent();
            frmokuyucuekle = new okuyucuekle();
            frmokuyucuekle.frmanaform = this; 
            frmokuyucuduzenle = new okuyucuduzenle();
            frmokuyucuduzenle.frmanaform = this;
            frmokuyucuayrinti = new okuyucuayrinti();
            frmokuyucuayrinti.frmanaform = this;
            frmkitapekle = new kitapekle();
            frmkitapekle.frmanaform = this;
            frmkitapduzenle = new kitapduzenle();
            frmkitapduzenle.frmanaform = this;
            frmkitapayrinti = new kitapayrinti();
            frmkitapayrinti.frmanaform = this;
            frmkitapkirala = new kitapkirala();
            frmkitapkirala.frmanaform = this;
            frmkitapteslim = new kitapteslim();
            frmkitapteslim.frmanaform = this;
        }
        public OleDbConnection baglanti = new OleDbConnection("provider=microsoft.ace.oledb.12.0;data source=veritabani.accdb;Jet OLEDB:Database Password=mhtarsln;");
        public DataSet dataset = new DataSet();
        public OleDbDataAdapter adaptor = new OleDbDataAdapter();
        public OleDbCommand komut = new OleDbCommand();

        private void anaform_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void uye_listele()
        {
            baglanti.Open();
            adaptor = new OleDbDataAdapter("select tc,ad,soyad from uyeler",baglanti);
            adaptor.Fill(dataset,"uye_liste");
            datauye.DataSource = dataset.Tables["uye_liste"];
            baglanti.Close();
        }

        public void kitap_listele()
        {
            baglanti.Open();
            adaptor = new OleDbDataAdapter("select barkot,kitapadi,yazari from kitaplar",baglanti);
            adaptor.Fill(dataset,"kitap_liste");
            datakitap.DataSource = dataset.Tables["kitap_liste"];
            baglanti.Close();
        }
        public void kitap_kirala_listele()
        {
            baglanti.Open();
            adaptor = new OleDbDataAdapter("select tc,adi,soyadi,barkod,kitap,kiratarihi,iadetarihi,teslimatdurumu,ceza from kitapkirala",baglanti);
            adaptor.Fill(dataset,"kitapkirala_liste");
            datakitapkirala.DataSource = dataset.Tables["kitapkirala_liste"];
            baglanti.Close();
        }
        public void UyeCezaGuncelle()
        {
            TimeSpan fark = new TimeSpan();
            baglanti.Open();
            if (dataset.Tables["uyecezaguncelle"] != null)
            {
                dataset.Tables["uyecezaguncelle"].Clear();
            }
            adaptor = new OleDbDataAdapter("select id,kiratarihi,iadetarihi,ceza from kitapkirala where teslimatdurumu='HAYIR'", baglanti);
            adaptor.Fill(dataset, "uyecezaguncelle");
            for (int i = 0; i < dataset.Tables["uyecezaguncelle"].Rows.Count; i++)
            {
                fark = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()) - Convert.ToDateTime(dataset.Tables["uyecezaguncelle"].Rows[i][1]);
                if (Convert.ToInt32(fark.Days) > 15)
                {
                    dataset.Tables["uyecezaguncelle"].Rows[i][3] = ((Convert.ToDouble(fark.Days) - 15) * 0.10);
                    komut = new OleDbCommand("update kitapkirala set ceza='" + dataset.Tables["uyecezaguncelle"].Rows[i][3].ToString() + "' where id=" + Convert.ToInt32(dataset.Tables["uyecezaguncelle"].Rows[i][0]) + "", baglanti);
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                }
            }
            komut.Dispose();
            baglanti.Close();
        }

        private void anaform_Load(object sender, EventArgs e)
        {
            uye_listele();
            kitap_listele();
            kitap_kirala_listele();
            UyeCezaGuncelle();
            datauye.Columns[0].HeaderText = "TC";
            datauye.Columns[1].HeaderText = "AD";
            datauye.Columns[2].HeaderText = "SOYAD";
            datakitap.Columns[0].HeaderText = "BARKOD NO";
            datakitap.Columns[1].HeaderText = "KİTAP ADI";
            datakitap.Columns[2].HeaderText = "YAZARI";
            datakitapkirala.Columns[0].HeaderText = "TC";
            datakitapkirala.Columns[1].HeaderText = "AD";
            datakitapkirala.Columns[2].HeaderText = "SOYAD";
            datakitapkirala.Columns[3].HeaderText = "BARKOD NO";
            datakitapkirala.Columns[4].HeaderText = "KİTAP ADI";
            datakitapkirala.Columns[5].HeaderText = "KİRALAMA TARİHİ";
            datakitapkirala.Columns[6].HeaderText = "İADE TARİHİ";
            datakitapkirala.Columns[7].HeaderText = "TESLİMAT DURUMU";
            datakitapkirala.Columns[8].HeaderText = "CEZA";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmokuyucuekle.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmokuyucuduzenle.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Okuyucu silinecek.Onaylıyor musunuz?","UYARI",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (cevap==DialogResult.Yes)
            {
                baglanti.Open();
                komut = new OleDbCommand("delete from uyeler where tc='"+datauye.CurrentRow.Cells[0].Value.ToString()+"'",baglanti);
                komut.ExecuteNonQuery();
                dataset.Tables["uye_liste"].Clear();
                baglanti.Close();
                uye_listele();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmokuyucuayrinti.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmkitapekle.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmkitapduzenle.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Kitap silinecek.Onaylıyor musunuz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                baglanti.Open();
                komut = new OleDbCommand("delete from kitaplar where barkot='" + datakitap.CurrentRow.Cells[0].Value.ToString() + "'", baglanti);
                komut.ExecuteNonQuery();
                dataset.Tables["kitap_liste"].Clear();
                baglanti.Close();
                kitap_listele();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmkitapayrinti.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            frmkitapkirala.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (datakitapkirala.CurrentRow.Cells[7].Value.ToString() == "HAYIR")
            {
                frmkitapteslim.ShowDialog();
            }
            else
	        {
                MessageBox.Show("Bu kitap iade edilmiştir !", "Uyarı");
	        }
        }
    }
}
