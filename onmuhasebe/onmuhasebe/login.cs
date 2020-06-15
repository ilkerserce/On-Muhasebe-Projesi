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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        public SqlConnection con = new SqlConnection("Data Source=ILKER\\SQLEXPRESS;Initial Catalog=onmuhasebe;Integrated Security=True");
        static string kno;
        string kadi, sifre;
        private const bool verkadi = false;
        private const bool versifre = false;

        public void GirisSorgu()
        {
            con.Open();
            kadi = textBox1.Text;            sifre = textBox2.Text;
            SqlCommand cmd = new SqlCommand("SELECT * FROM yonetici WHERE kullanici_adi='" + kadi + "' AND sifre='" + sifre + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            /*SqlCommand cmd2 = new SqlCommand("SELECT yonetici_id FROM yonetici WHERE kullanici_adi='" + kadi + "' AND sifre='" + sifre + "'", con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            kno = Convert.ToString(dr2.Read());
            label3.Text = kno.*/
            if (dr.Read())
            {
                Form1 frm1 = new Form1();
                frm1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Giriş başarısız. Kullanıcı adı veya şifreyi kontrol ediniz.");
            }
            con.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void login_Load(object sender, EventArgs e)
        {
            textBox1.Text = "ilker";
            textBox2.Text = "1234";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GirisSorgu();
        }
    }
}
