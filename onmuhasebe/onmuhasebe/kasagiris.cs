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
    public partial class kasagiris : Form
    {
        public kasagiris()
        {
            InitializeComponent();
        }

        public SqlConnection con = new SqlConnection("Data Source=ILKER\\SQLEXPRESS;Initial Catalog=onmuhasebe;Integrated Security=True");
        public DataSet dskasa = new DataSet();
        string starih;
        Form1 frm1 = new Form1();
        public void Kasaekle()
        {
            con.Open();
            starih = dateTimePicker1.Value.ToShortDateString();
            SqlCommand cmd = new SqlCommand("INSERT INTO kasa (yonetici_id,tarih,tip,unvan,aciklama,evrak_no,tutar) VALUES ('" + 1 + "','" + starih + "','" + comboBox1.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "' ,'" + textBox4.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Tamamlandı");
            dskasa.Clear();
        }

        private void kasagiris_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Giriş");
            comboBox1.Items.Add("Çıkış");
            comboBox1.Text = "Giriş";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kasaekle();
        }
    }
}
