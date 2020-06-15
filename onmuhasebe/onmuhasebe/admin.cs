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
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }
        string id, pw;
        public SqlConnection con = new SqlConnection("Data Source=ILKER\\SQLEXPRESS;Initial Catalog=onmuhasebe;Integrated Security=True");

        public void AdminSorgu()
        {
            con.Open();
            id = textBox1.Text;
            pw = textBox2.Text;
            SqlCommand cmd = new SqlCommand("SELECT * FROM ustyonetici WHERE id='" + id + "' AND pw='" + pw + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Form1 frm1 = new Form1();
                MessageBox.Show("Giriş başarılı.");
            }
            else
            {
                MessageBox.Show("Giriş başarısız. Kullanıcı adı veya şifreyi kontrol ediniz.");

            }
            con.Close();
        }

        private void admin_Load(object sender, EventArgs e)
        {
            textBox1.Text = "ilker";
            textBox2.Text = "1234";

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminSorgu();
        }
    }
}
