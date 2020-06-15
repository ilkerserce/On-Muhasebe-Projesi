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
    public partial class virman : Form
    {
        public virman()
        {
            InitializeComponent();
        }

        public SqlConnection con = new SqlConnection("Data Source=ILKER\\SQLEXPRESS;Initial Catalog=onmuhasebe;Integrated Security=True");
        public DataSet dskasa = new DataSet();
        string starih;

        public void Kasaekle()
        {
            starih = dateTimePicker1.Value.ToShortDateString();
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO kasa (yonetici_id,tarih,tip,unvan,aciklama,evrak_no,tutar) VALUES ('" + 1 + "','" + starih + "','" + comboBox1.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "' ,'" + textBox4.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Tamamlandı");
            dskasa.Clear();
        }

        private void virman_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "Çıkış";
            textBox2.Text = "Kasadan Bankaya Virman";
            comboBox1.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = textBox2.Text;
        }
    }
}
