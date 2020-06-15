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
    public partial class bankaekle : Form
    {
        public bankaekle()
        {
            InitializeComponent();
        }
        public SqlConnection con = new SqlConnection("Data Source=ILKER\\SQLEXPRESS;Initial Catalog=onmuhasebe;Integrated Security=True");
        public DataSet dsbanka = new DataSet();

        public void Bankaekle()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO banka (banka_adi,banka_subesi,hesap_numarasi,sube_kodu,iban) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "' ,'" + textBox4.Text + "','" + textBox5.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Banka kaydı tamamlandı.");
            dsbanka.Clear();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bankaekle_Load(object sender, EventArgs e)
        {
            textBox1.Text = "asd";
            textBox2.Text = "asd";
            textBox3.Text = "asd";
            textBox4.Text = "as";
            textBox5.Text = "asd";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bankaekle();
        }
    }
}
