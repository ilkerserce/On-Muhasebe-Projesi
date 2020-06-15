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
    public partial class islemduzenle : Form
    {
        public islemduzenle()
        {
            InitializeComponent();
        }

        public SqlConnection con = new SqlConnection("Data Source=ILKER\\SQLEXPRESS;Initial Catalog=onmuhasebe;Integrated Security=True");
        public DataSet dskasa = new DataSet();
        string starih;
        int islem;

        public void IslemBul()
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT tarih,tip,odeme_sekli,tutar,aciklama,unvan,evrak_no FROM bankaislem WHERE banka_islem_no='" + Convert.ToInt32(textBox5.Text) + "'", con);
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
            {
                textBox6.Text = dr["tarih"].ToString();
                comboBox1.Text = dr["tip"].ToString();
                comboBox2.Text = dr["odeme_sekli"].ToString();
                textBox4.Text = dr["tutar"].ToString();
                textBox3.Text = dr["aciklama"].ToString();
                textBox1.Text = dr["unvan"].ToString();
                textBox2.Text = dr["evrak_no"].ToString();
                label9.Text = "İşlem numarası bulundu.";
            }
            else
            {
                label9.Text = "Böyle bir işlem numarası yok.";
            }
            con.Close();
        }

        public void BIslemDuzenle()
        {
            con.Open();
            islem = Convert.ToInt32(textBox5.Text);
            SqlCommand up = new SqlCommand("UPDATE bankaislem SET tarih = '" + textBox6.Text + "', tip = '" + comboBox1.Text + "', odeme_sekli = '" + comboBox2.Text + "', tutar = '" + Convert.ToDouble(textBox4.Text) + "', aciklama ='" + textBox3.Text + "', unvan ='" + textBox1.Text + "', evrak_no ='" + textBox2.Text + "' WHERE banka_islem_no ='" + islem + "'", con);
            up.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Girdi başarıyla güncellendi.");
            dskasa.Clear();
        }

        private void islemduzenle_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Giriş");
            comboBox1.Items.Add("Çıkış");
            comboBox2.Items.Add("Havale");
            comboBox2.Items.Add("Kredi Kartı");
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            IslemBul();
            islem = Convert.ToInt32(textBox5.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BIslemDuzenle();
        }
    }
}
