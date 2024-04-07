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

namespace ÖğrenciNotKayıt
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'dbNotKayıtDataSet.Tbl_ders' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tbl_dersTableAdapter.Fill(this.dbNotKayıtDataSet.Tbl_ders);

        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-5VIVL0L;Initial Catalog=DbNotKayıt;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Insert into Tbl_ders (OGRNUMARA,OGRAD,OGRSOYAD) values (@p1,@p2,@p3)",baglanti);
            komut.Parameters.AddWithValue("@p1", mskNumara.Text);
            komut.Parameters.AddWithValue("@p2", txtad.Text);
            komut.Parameters.AddWithValue("@p3", txtsoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ogrenci Sisteme Eklendi.");
            this.tbl_dersTableAdapter.Fill(this.dbNotKayıtDataSet.Tbl_ders);
        }

        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            mskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtsinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtsinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtsinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;
            
            s1 = Convert.ToDouble(txtsinav1.Text);
            s2 = Convert.ToDouble(txtsinav2.Text);
            s3 = Convert.ToDouble(txtsinav3.Text);
            

            ortalama = (s1 + s2 + s3) / 3;
            lblortalama.Text = ortalama.ToString();

            if (ortalama >= 50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }
                   

            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Tbl_ders set OGRS1=@P1,OGRS2=@P2,OGRS3=@P3,ORTALAMA=@P4,DURUM=@P5 WHERE OGRNUMARA=@P6",baglanti);
            komut.Parameters.AddWithValue("@P1", txtsinav1.Text);
            komut.Parameters.AddWithValue("@P2", txtsinav2.Text);
            komut.Parameters.AddWithValue("@P3", txtsinav3.Text);
            komut.Parameters.AddWithValue("@P4", Convert.ToDecimal(lblortalama.Text));
            komut.Parameters.AddWithValue("@p5", durum);
            komut.Parameters.AddWithValue("@p6", mskNumara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ogrenci Notlari Güncellendi.");
            this.tbl_dersTableAdapter.Fill(this.dbNotKayıtDataSet.Tbl_ders);
        }
    }
}
