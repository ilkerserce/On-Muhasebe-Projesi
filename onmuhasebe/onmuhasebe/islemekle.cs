using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace onmuhasebe
{
    public partial class islemekle : Form
    {
        public islemekle()
        {
            InitializeComponent();
        }
        public SqlConnection con = new SqlConnection("Data Source=ILKER\\SQLEXPRESS;Initial Catalog=onmuhasebe;Integrated Security=True");
        public DataSet dsbankaislem = new DataSet();
        string starih;

        public void Islemekle()
        {
            starih = dateTimePicker1.Value.ToShortDateString();
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO bankaislem (tarih,tip,odeme_sekli,tutar,aciklama,unvan,evrak_no) VALUES ('" + starih + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox4.Text + "','" + textBox3.Text + "' ,'" + textBox1.Text + "' , '" + textBox2.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Tamamlandı");
            dsbankaislem.Clear();
        }

        private void islemekle_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Giriş");
            comboBox1.Items.Add("Çıkış");
            comboBox1.Text = "Giriş";
            comboBox2.Items.Add("Havale");
            comboBox2.Items.Add("Kredi Kartı");
            comboBox2.Text = "Havale";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Islemekle();
        }
    }
}
