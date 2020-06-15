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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public SqlConnection con = new SqlConnection("Data Source=ILKER\\SQLEXPRESS;Initial Catalog=onmuhasebe;Integrated Security=True");
        public DataSet dskasa = new DataSet();
        public DataSet dsbanka = new DataSet();
        public DataSet dsbankaislem = new DataSet();
        public static int secilisatirno1, secilisatirno2, secilisatirno3, islem, islem2, islem3;
        public static float tplmgiris, tplmcikis, toplam, min, max, ilk, son, say;
        public int olusumsecim;
        /*Yapılacaklar: Raporlama, Arama Fonksiyonu, Ufak Tefek Rütüşlar, Süper Admin Paneli(Veri Tabanı dahil), Log Sistemi, Giriş Paneli Çalışmaları*/
        public string[] tarih = new string[] { "02.02.2020", "03.02.2020", "04.02.2020", "05.02.2020", "06.02.2020" };
        public string[] tip = new string[] { "Giriş", "Çıkış" };
        public string[] unvan = new string[] { "Göksel Oto", "Ege Kampüs", "Kodak İsmail", "Şölen Pastanesi", "Öztürk Sigorta" };
        public string[] evrak_no = new string[] { "1", "2", "3", "4", "5" };
        public string kategori,aramabilgi, aramatip;

        public SqlConnection Con { get => con; set => con = value; }
        public void BankaSil()
        {
            DialogResult dialog = MessageBox.Show("Bankayı silmek istediginizden emin misiniz?", "Kayıtlı Bankayı Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                con.Open();
                int secilisatirno2 = dataGridView2.SelectedCells[0].RowIndex;
                DataGridViewRow secilisatir = dataGridView2.Rows[secilisatirno2];
                int islem = Convert.ToInt32(secilisatir.Cells["Banka Numarası"].Value);
                SqlCommand del = new SqlCommand("DELETE FROM banka WHERE banka_id='" + islem + "'", con);
                del.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Veri silindi", "Silme İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dsbanka.Clear();
                BankaListele();
            }
        }

        public void BankaIslemSil()
        {
            DialogResult dialog = MessageBox.Show("Banka işlemini silmek istediginizden emin misiniz?", "Kayıtlı Banka İşlemini Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                con.Open();
                int secilisatirno3 = dataGridView3.SelectedCells[0].RowIndex;
                DataGridViewRow secilisatir = dataGridView3.Rows[secilisatirno3];
                int islem = Convert.ToInt32(secilisatir.Cells["İşlem Numarası"].Value);
                SqlCommand del = new SqlCommand("DELETE FROM bankaislem WHERE banka_islem_no='" + islem + "'", con);
                del.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Veri silindi."," Silme İşlemi Başarılı" , MessageBoxButtons.OK, MessageBoxIcon.Information);
                dsbankaislem.Clear();
                BankaIslemListele();
            }

        }

        public void BankaSil1()
        {
            DialogResult dialog = MessageBox.Show("Bankayı silmek istediginizden emin misiniz?", "Kayıtlı Bankayı Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                con.Open();
                int secilisatirno = dataGridView2.SelectedCells[0].RowIndex;
                DataGridViewRow secilisatir = dataGridView2.Rows[secilisatirno];
                int islem = Convert.ToInt32(secilisatir.Cells["banka_id"].Value);

                SqlCommand del = new SqlCommand("DELETE FROM banka WHERE banka_id='" + islem + "'", con);
                del.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Silme Tamamlandı");
                dsbanka.Clear();
                BankaListele();
            }
        }

        public void KayitSil()
        {
            DialogResult dialog = MessageBox.Show("Veriyi silmek istediginizden emin misiniz?", "Kasa Kaydı Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                con.Open();
                int secilisatirno = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow secilisatir = dataGridView1.Rows[secilisatirno];
                int islem = Convert.ToInt32(secilisatir.Cells["İşlem Numarası"].Value);
                SqlCommand del = new SqlCommand("DELETE FROM kasa WHERE islem_no='" + islem + "'", con);
                del.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Veri silindi.", "Silme İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dskasa.Clear();
                KasaListele();
            }
        }

        public void BankaIslemListele()
        {
            con.Open();
            SqlDataAdapter adaptr3 = new SqlDataAdapter("SELECT banka_islem_no AS [İşlem Numarası], tarih Tarih, tip Tip, odeme_sekli [Ödeme Şekli], tutar Tutar, aciklama Açıklama, unvan Ünvan FROM bankaislem", con);
            dsbankaislem.Clear();
            adaptr3.Fill(dsbankaislem, "bankaislem");
            dataGridView3.DataSource = dsbankaislem.Tables[0];
            adaptr3.Dispose();
            con.Close();
        }

        private void BankaListele()
        {
            con.Open();
            SqlDataAdapter adaptr2 = new SqlDataAdapter("SELECT banka_id AS [Banka Numarası], banka_adi [Banka Adı], banka_subesi [Banka Şubesi], hesap_numarasi [Hesap Numarası], sube_kodu [Şube Kodu], iban IBAN FROM banka", con);
            dsbanka.Clear();
            adaptr2.Fill(dsbanka, "banka");
            dataGridView2.DataSource = dsbanka.Tables[0];
            adaptr2.Dispose();
            con.Close();
        }

        public void KasaCikisListele()
        {
            con.Open();
            SqlDataAdapter adaptr = new SqlDataAdapter("SELECT islem_no AS [İşlem Numarası], tarih AS Tarih, tip AS [Girdi Tipi], unvan AS Ünvan, aciklama AS Açıklama, evrak_no AS [Evrak Numarası], tutar AS Tutar FROM kasa kasa WHERE tip='Çıkış'", con);
            //SqlDataAdapter adaptr = new SqlDataAdapter("SELECT islem_no AS [İşlem Numarası], tarih AS Tarih, tip AS [Girdi Tipi], unvan AS Ünvan, aciklama AS Açıklama, evrak_no AS [Evrak Numarası], tutar AS Tutar WHERE tip ='Çıkış'", con);
            dskasa.Clear();
            adaptr.Fill(dskasa, "kasa");
            dataGridView1.DataSource = dskasa.Tables[0];
            adaptr.Dispose();
            con.Close();
        }

        public void KasaGirisListele()
        {
            con.Open();
            SqlDataAdapter adaptr = new SqlDataAdapter("SELECT islem_no AS [İşlem Numarası], tarih AS Tarih, tip AS [Girdi Tipi], unvan AS Ünvan, aciklama AS Açıklama, evrak_no AS [Evrak Numarası], tutar AS Tutar FROM kasa kasa WHERE tip ='Giriş'", con);
            dskasa.Clear();
            adaptr.Fill(dskasa, "kasa");
            dataGridView1.DataSource = dskasa.Tables[0];
            adaptr.Dispose();
            con.Close();
        }

        public void MinAl()
        {
            con.Open();
            /*SqlCommand cmd = new SqlCommand(@" DECLARE @min string;
                Select MIN(tutar) FROM kasa", con);*/
            SqlCommand cmd = new SqlCommand(@" DECLARE @id int;
                SELECT islem_no FROM kasa WHERE tutar = ( SELECT MIN(tutar) FROM kasa );", con);
            var id = Convert.ToInt32(cmd.ExecuteScalar());
            SqlCommand cmd2 = new SqlCommand(@" DECLARE @tutar float;
                SELECT tutar FROM kasa WHERE islem_no = '" + id + "' ", con);
            var tutar = Convert.ToInt32(cmd2.ExecuteScalar());
            SqlCommand cmd3 = new SqlCommand(@" DECLARE @tarih date;
                SELECT tarih FROM kasa WHERE islem_no = '" + id + "' ", con);
            var tarih = Convert.ToString(cmd3.ExecuteScalar());
            //var min = Convert.ToString(cmd.ExecuteScalar());
            label17.Text = Convert.ToString(tutar) + " ₺ Tarih: " + Convert.ToString(tarih);
            con.Close();
        }


        public void MaxAl()
        {
            /*con.Open();
            SqlCommand cmd = new SqlCommand(@" DECLARE @max float;
                Select MAX(tutar) FROM kasa", con);
            var max = Convert.ToString(cmd.ExecuteScalar());
            label15.Text = max;
            con.Close();*/
            con.Open();
            SqlCommand cmd = new SqlCommand(@" DECLARE @id int;
                SELECT islem_no FROM kasa WHERE tutar = ( SELECT MAX(tutar) FROM kasa );", con);
            var id = Convert.ToInt32(cmd.ExecuteScalar());
            SqlCommand cmd2 = new SqlCommand(@" DECLARE @tutar float;
                SELECT tutar FROM kasa WHERE islem_no = '" + id + "' ", con);
            var tutar = Convert.ToInt32(cmd2.ExecuteScalar());
            SqlCommand cmd3 = new SqlCommand(@" DECLARE @tarih date;
                SELECT tarih FROM kasa WHERE islem_no = '" + id + "' ", con);
            var tarih = Convert.ToString(cmd3.ExecuteScalar());
            label15.Text = Convert.ToString(tutar) + " ₺ Tarih: " + Convert.ToString(tarih);
            con.Close();
        }

        public void SayiAl()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@" DECLARE @say float;
                Select COUNT(tutar) FROM kasa", con);
            var say = Convert.ToString(cmd.ExecuteScalar());
            label9.Text = say;
            con.Close();
        }

        public void ToplamAl()
        {
            double giris2, cikis2 = 0;
            if (label5.Text == "" || label11.Text == "")
            {
                cikis2 = 0;
                giris2 = 0;
                label7.Text = Convert.ToString(giris2 - cikis2) + " ₺";
            }
            else
            {
                giris2 = Convert.ToDouble(label5.Text);
                cikis2 = Convert.ToDouble(label11.Text);
                label7.Text = Convert.ToString((giris2 - cikis2)) + " ₺";
            }
        }

        public void ToplamCikis()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@" DECLARE @tplmcikis float;
                Select SUM(tutar) FROM kasa WHERE tip='Çıkış' ", con);
            var tplmcikis = Convert.ToString(cmd.ExecuteScalar());
            label11.Text = tplmcikis;
            con.Close();
        }

        public void ToplamGiris()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@" DECLARE @tplmgiris float;
                Select SUM(tutar) FROM kasa WHERE tip='Giriş' ", con);
            var tplmgiris = Convert.ToString(cmd.ExecuteScalar());
            label5.Text = tplmgiris;
            con.Close();
        }

        public void KasaListele()
        {
            con.Open();
            SqlDataAdapter adaptr = new SqlDataAdapter("SELECT islem_no AS [İşlem Numarası], tarih AS Tarih, tip AS [Girdi Tipi], unvan AS Ünvan, aciklama AS Açıklama, evrak_no AS [Evrak Numarası], tutar AS Tutar FROM kasa", con);
            //SqlDataAdapter adaptr = new SqlDataAdapter("SELECT islem_no AS [İşlem Numarası], tarih AS Tarih, tip AS [Girdi Tipi], unvan AS Ünvan, aciklama AS Açıklama, evrak_no AS [Evrak Numarası], tutar AS Tutar FROM kasa", con);
            dskasa.Clear();
            adaptr.Fill(dskasa, "kasa");
            dataGridView1.DataSource = dskasa.Tables[0];
            //dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            adaptr.Dispose();
            con.Close();
        }

        public void Ara()
        {
            con.Open();
            string selectCommandText = "SELECT islem_no AS [İşlem Numarası], tarih AS Tarih, tip AS [Girdi Tipi], unvan AS Ünvan, aciklama AS Açıklama, evrak_no AS [Evrak Numarası], tutar AS Tutar FROM kasa WHERE "
                                                       + aramatip
                                                       + kategori 
                                                       + " LIKE '"
                                                       + aramabilgi 
                                                       + "'";
            SqlDataAdapter adaptr = new SqlDataAdapter(selectCommandText, con);
            label18.Text = selectCommandText;
            dskasa.Clear();
            adaptr.Fill(dskasa, "kasa");
            dataGridView1.DataSource = dskasa.Tables[0];
            adaptr.Dispose();
            con.Close();
        }

        public void TariheGore()
        {
            string ilktarih = Convert.ToString(dateTimePicker1.Value.ToShortDateString());
            string sontarih = Convert.ToString(dateTimePicker2.Value.ToShortDateString());
            con.Open();
            SqlDataAdapter sirala = new SqlDataAdapter("SELECT * FROM kasa WHERE tarih>= '" + ilktarih + "' AND tarih<= '" + sontarih + "'", con);
            dskasa.Clear();
            sirala.Fill(dskasa, "kasa");
            dataGridView1.DataSource = dskasa.Tables[0];
            sirala.Dispose();
            con.Close();
        }

        public void Temizle()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM kasa", con);
            cmd.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("DBCC CHECKIDENT('[kasa]', RESEED, 0);", con);
            cmd2.ExecuteNonQuery();
            con.Close();
            label5.Text = "Boş";
            label11.Text = "Boş";
            label7.Text = "Boş";
            label9.Text = "Boş";
            label15.Text = "Boş";
            label17.Text = "Boş";
            Listele();
        }

        public void Olustur()
        {
            olusumsecim = Convert.ToInt32(comboBox3.SelectedItem);
            string tarih2 = "";
            Random r = new Random();
            int i = r.Next(0, 4);
            int j = r.Next(0, 2);
            int k = r.Next(0, 4);
            int l = r.Next(0, 4);
            int m = r.Next(0, 1000);
            for (int p = 1; p <= olusumsecim; p++)
            {
                i = r.Next(0, 4);
                tarih2 = tarih[i];
                r.Next();
                j = r.Next(0, 2);
                string tip2 = tip[j];
                r.Next();
                k = r.Next(0, 4);
                string unvan2 = unvan[k];
                r.Next();
                l = r.Next(0, 4);
                string evrak_no2 = evrak_no[l];
                r.Next();
                int tutar = r.Next(500, 1000);
                r.Next();
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO kasa(yonetici_id,tarih,tip,unvan,aciklama,evrak_no,tutar) VALUES('" + 1 + "', '" + tarih2 + "', '" + tip2 + "', '" + unvan2 + "', '" + "deneme" + "' , '" + evrak_no2 + "', '" + tutar + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                r.Next();
                Listele();
            }
        }

        public void Listele()
        {
            KasaListele();
            BankaListele();
            BankaIslemListele();
            ToplamGiris();
            ToplamCikis();
            ToplamAl();
            SayiAl();
            MaxAl();
            MinAl();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //SqlCommand sirala = new SqlCommand("SELECT * FROM kasa WHERE tarih>= '"+ ilktarih +"' AND tarih<= '"+ sontarih +"'", con);
            TariheGore();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Giriş")
            {
                KasaGirisListele();
                aramatip = " tip ='Giriş' AND ";
            }
            else if (comboBox1.Text == "Çıkış")
            {
                KasaCikisListele();
                aramatip = " tip ='Çıkış' AND ";
            }
            else if (comboBox1.Text == "Hepsi")
            {
                KasaListele();
                aramatip = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Hepsi");
            comboBox1.Items.Add("Giriş");
            comboBox1.Items.Add("Çıkış");
            comboBox1.Text = "Hepsi";
            Listele();
            comboBox2.Items.Add("İşlem Numarası");
            comboBox2.Items.Add("Tarih");
            comboBox2.Items.Add("Ünvan");
            comboBox2.Items.Add("Açıklama");
            comboBox2.Items.Add("Evrak Numarası");
            comboBox2.Items.Add("Tutar");
            comboBox3.Items.Add("5");
            comboBox3.Items.Add("10");
            comboBox3.Items.Add("25");
            comboBox3.Text = "5";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form kasagiris = new kasagiris();
            kasagiris.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form kasacikis = new kasacikis();
            kasacikis.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form virman = new virman();
            virman.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form kasaduzelt = new kasaduzelt();
            kasaduzelt.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            KayitSil();
        }

        /*private void button8_Click(object sender, EventArgs e)
        {
            Olustur();
        }*/

        private void button9_Click(object sender, EventArgs e)
        {
            Form bankaekle = new bankaekle();
            bankaekle.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form bankaduzenle = new bankaduzenle();
            bankaduzenle.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            BankaSil();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "işlem Numarası")
            {
                Convert.ToInt32(aramabilgi);
            }
            Ara();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Form admin = new admin();
            admin.Show();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            Form kasaduzelt = new kasaduzelt();
            kasaduzelt.Show();
        }

        public void TextBox1_TextChanged(object sender, EventArgs e)
        {
            aramabilgi = "%" + textBox1.Text + "%";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "İşlem Numarası")
            {
                kategori = "islem_no";
            }
            else if (comboBox2.Text == "Tarih")
            {
                kategori = "tarih";
            }
            else if (comboBox2.Text == "Ünvan")
            {
                kategori = "unvan";
            }
            else if (comboBox2.Text == "Açıklama")
            {
                kategori = "aciklama";
            }
            else if (comboBox2.Text == "Evrak Numarası")
            {
                kategori = "evrak_no";
            }
            else if (comboBox2.Text == "Tutar")
            {
                kategori = "tutar";
            }
            label18.Text = kategori;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Olustur();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*int secilisatirno2 = dataGridView2.SelectedCells[0].RowIndex;
            DataGridViewRow secilisatir = dataGridView2.Rows[secilisatirno2];
            islem2 = Convert.ToInt32(secilisatir.Cells["banka_id"].Value);*/
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            olusumsecim = Convert.ToInt32(comboBox3.SelectedItem);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            BankaIslemSil();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            //label18.Text = Convert.ToString(m);
        }

        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*secilisatirno = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow secilisatir = dataGridView1.Rows[secilisatirno];
            kasaduzelt.islem = Convert.ToInt32(secilisatir.Cells["islem_no"].Value);*/
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Form islemekle = new islemekle();
            islemekle.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form islemyatirma = new islemcekme();
            islemyatirma.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form islemvirman = new islemvirman();
            islemvirman.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Form islembankavirman = new islembankavirman();
            islembankavirman.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Form islemduzenle = new islemduzenle();
            islemduzenle.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }
    }
}
