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

namespace Book_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtAra.TextChanged += txtAra_TextChanged;
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-H6SIL9M\\SQLEXPRESS;Initial Catalog=Book_Project;Integrated Security=True;");

        void listele()
        {
            // datagird e verilerimizi listeledik
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Books",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void ara()
        {
            int sayi = 0;
            sayi++;
            lblTest.Text = sayi.ToString();
            // Like komutu ile içinde yazılan harfleri bulunduran tüm kitapları aratma işlemi yaptık
            SqlCommand komut = new SqlCommand("Select * From Tbl_Books where Book_Name like '%" + txtAra.Text + "%'", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            listele();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TxtKitapAd.Text == "" || txtSayfa.Text == "" || txtYazar.Text == "" || cmbTur.Text == "")
            {
                MessageBox.Show("Lütfen tüm bilgileri girdiğinize emin olun","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                baglanti.Open();

                SqlCommand komut = new SqlCommand("insert into Tbl_Books (Book_Name,Book_Writer,Book_Page,Book_Type,Book_Status) values (@b1,@b2,@b3,@b4,@b5)",baglanti);
                komut.Parameters.AddWithValue("@b1",TxtKitapAd.Text);
                komut.Parameters.AddWithValue("@b2",txtYazar.Text);
                komut.Parameters.AddWithValue("@b3",txtSayfa.Text);
                komut.Parameters.AddWithValue("@b4",cmbTur.Text);
                komut.Parameters.AddWithValue("@b5",lblDeger0.Text);

                komut.ExecuteNonQuery();

                baglanti.Close();

                MessageBox.Show("Yeni kitap kaydedilmiştir","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (TxtKitapAd.Text == "" || txtSayfa.Text == "" || txtYazar.Text == "" || cmbTur.Text == "")
            {
                MessageBox.Show("Girilen değerler eksik lütfen kontrol edin","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else if (txtID.Text == "")
            {
                MessageBox.Show("Id olmadan güncelleme işlemi yapamazsınız","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                baglanti.Open();

                SqlCommand komut2 = new SqlCommand("update Tbl_Books set Book_Name=@p1,Book_Writer=@p2,Book_Page=@p3,Book_Type=@p4,Book_Status=@p5 where Book_Id=@p",baglanti);

                komut2.Parameters.AddWithValue("@p",txtID.Text);
                komut2.Parameters.AddWithValue("@p1",TxtKitapAd.Text);
                komut2.Parameters.AddWithValue("@p2",txtYazar.Text);
                komut2.Parameters.AddWithValue("@p3",txtSayfa.Text);
                komut2.Parameters.AddWithValue("@p4",cmbTur.Text);
                komut2.Parameters.AddWithValue("@p5",lblDeger0.Text);

                komut2.ExecuteNonQuery();

                baglanti.Close();

                MessageBox.Show("Kitap güncellenmiştir","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(txtID.Text == "")
            {
                MessageBox.Show("Id olamdan kitap silme işlemi yapamazsınız","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                baglanti.Open();

                SqlCommand komut4 = new SqlCommand("Delete from Tbl_Books where Book_Id=@p1",baglanti);
                komut4.Parameters.AddWithValue("@p1",txtID.Text);

                komut4.ExecuteNonQuery();

                baglanti.Close();

                MessageBox.Show("Kitap silindi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

        }

        private void rdbSifir_CheckedChanged(object sender, EventArgs e)
        {
            lblDeger0.Text = "true";
        }

        private void rdbKullanilmis_CheckedChanged(object sender, EventArgs e)
        {
            lblDeger0.Text = "false";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtKitapAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSayfa.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            cmbTur.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            lblDeger0.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();

            if (lblDeger0.Text == "False")
            {
                rdbKullanilmis.Checked = true;
            }
            else if (lblDeger0.Text == "True")
            {
                rdbSifir.Checked = true;
            }
            


        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Like komutu ile içinde yazılan harfleri bulunduran tüm kitapları aratma işlemi yaptık
            SqlCommand komut = new SqlCommand("Select * From Tbl_Books where Book_Name like '%"+ txtAra.Text +"%'",baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {

            lblTest.Text = txtAra.Text;

            SqlCommand komut = new SqlCommand("Select * From Tbl_Books where Book_Name like '%" + lblTest.Text + "%'", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }
    }
}
