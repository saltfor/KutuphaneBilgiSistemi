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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("provider=microsoft.ace.oledb.12.0;data source=veritabani.accdb;Jet OLEDB:Database Password=mhtarsln;");
        DataSet dataset = new DataSet();
        OleDbDataAdapter adaptor = new OleDbDataAdapter();
        OleDbCommand komut = new OleDbCommand();

        private void button1_Click(object sender, EventArgs e)
        {
            string kadi = textBox1.Text;
            string sifre = textBox2.Text;
            baglanti.Open();
            dataset.Clear();
            adaptor = new OleDbDataAdapter("select * from giris where kadi='" + kadi + "' and sifre='" + sifre + "'", baglanti);
            adaptor.Fill(dataset, "giris");
            if (dataset.Tables["giris"].Rows.Count == 0)
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre yanlış !");
                adaptor.Dispose();
                baglanti.Close();
            }
            else
            {
                adaptor.Dispose();
                baglanti.Close();
                this.Hide();
                anaform anaformm = new anaform();
                anaformm.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
