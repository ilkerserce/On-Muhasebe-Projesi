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
    public partial class kasaduzelt : Form
    {
        Form Form1 = new Form1();
        public kasaduzelt()
        {
            InitializeComponent();
        }

        public SqlConnection con = new SqlConnection("Data Source=ILKER\\SQLEXPRESS;Initial Catalog=onmuhasebe;Integrated Security=True");
        public DataSet dskasa = new DataSet();
        string starih;
        static internal int islem;
        int aramaislem;

        public void IslemBul()
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT tarih,tip,unvan,aciklama,evrak_no,tutar FROM kasa WHERE islem_no='" + Convert.ToInt32(textBox5.Text) + "'", con);
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
            {
                textBox6.Text = dr["tarih"].ToString();
                comboBox1.Text = dr["tip"].ToString();
                textBox1.Text = dr["unvan"].ToString();
                textBox2.Text = dr["aciklama"].ToString();
                textBox3.Text = dr["evrak_no"].ToString();
                textBox4.Text = dr["tutar"].ToString();
                label8.Text = "İşlem numarası bulundu.";
            }
            else
            {
                label8.Text = "Böyle bir işlem numarası yok.";
            }
            con.Close();
        }

        public void KasaDuzenle()
        {
            con.Open();
            islem = Convert.ToInt32(textBox5.Text);
            SqlCommand up = new SqlCommand("UPDATE kasa SET tarih = '" + textBox6.Text + "', tip = '" + comboBox1.Text +"', unvan = '" + textBox1.Text + "', aciklama = '" + textBox2.Text + "', evrak_no ='" + textBox3.Text + "', tutar ='" + Convert.ToDouble(textBox4.Text) + "' WHERE islem_no ='" + islem + "'", con);
            up.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Girdi başarıyla güncellendi.");
            dskasa.Clear();
        }

        private void kasaduzelt_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Giriş");
            comboBox1.Items.Add("Çıkış");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KasaDuzenle();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            label7.Text = Convert.ToString(islem);
        }

        public void textBox5_TextChanged(object sender, EventArgs e)
        {

            if(textBox5.Text != "")
            {
                aramaislem = Convert.ToInt32(textBox5.Text);
                IslemBul();
            }
            else
            {
                label8.Text = "İşlem Numarası Giriniz.";
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
