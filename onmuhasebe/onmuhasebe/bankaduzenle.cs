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
    public partial class bankaduzenle : Form
    {
        internal static int islem;

        public bankaduzenle()
        {
            InitializeComponent();
        }

        int aramaislem;

        public SqlConnection con = new SqlConnection("Data Source=ILKER\\SQLEXPRESS;Initial Catalog=onmuhasebe;Integrated Security=True");
        public DataSet dsbanka = new DataSet();
        Form1 frm1 = new Form1();

        public void SatirBul()
        {
            if (textBox6.Text != "")
            {
                label6.Text="Banka bulundu.";
            }
        }
        public void IslemBul()
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT banka_adi,banka_subesi,hesap_numarasi,sube_kodu,iban FROM banka WHERE banka_id='" + Convert.ToInt32(textBox6.Text) + "'", con);
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr["banka_adi"].ToString();
                textBox2.Text = dr["banka_subesi"].ToString();
                textBox3.Text = dr["hesap_numarasi"].ToString();
                textBox4.Text = dr["sube_kodu"].ToString();
                textBox5.Text = dr["iban"].ToString();
            }
            else
            {
                label7.Text = "Böyle bir banka numarası yok.";
            }
            con.Close();
        }

        public void BankaDuzenle()
        {
            con.Open();
            //islem = Convert.ToInt32(textBox5.Text);
            SqlCommand up = new SqlCommand("UPDATE banka SET banka_adi = '" + textBox1.Text + "', banka_subesi = '" + textBox2.Text + "', hesap_numarasi = '" + textBox3.Text + "', sube_kodu = '" + textBox4.Text + "', iban ='" + textBox5.Text + "' WHERE banka_id ='" + aramaislem + "'", con);
            up.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Girdi başarıyla güncellendi.");
            dsbanka.Clear();
        }

        private void bankaduzenle_Load(object sender, EventArgs e)
        {
            SatirBul();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                aramaislem = Convert.ToInt32(textBox6.Text);
                IslemBul();
            }
            else
            {
                label7.Text = "Banka Numarası Giriniz.";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BankaDuzenle();
        }
    }
}
