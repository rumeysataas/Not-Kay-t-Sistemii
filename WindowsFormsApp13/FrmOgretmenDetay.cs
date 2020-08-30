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


namespace WindowsFormsApp13
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        SqlConnection baglantı = new SqlConnection(@"Data Source=RUMEYSA-W10\SQLEXPRESS;Initial Catalog=DbNotKayıt;Integrated Security=True
");


        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_ders(OGRNUMARA,OGRAD,OGRSOYAD) values (@p1,@p2,@p3)", baglantı);
            komut.Parameters.AddWithValue("@p1", msknumara.Text);
            komut.Parameters.AddWithValue("@p2", LblAd.Text);
            komut.Parameters.AddWithValue("@p3", LblSoyad.Text);
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi.");
            this.tbl_dersTableAdapter.Fill(this.dbNotKayıtDataSet.Tbl_ders);
        }

       
        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'dbNotKayıtDataSet.Tbl_ders' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tbl_dersTableAdapter.Fill(this.dbNotKayıtDataSet.Tbl_ders);

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double s1, s2, s3;
            double ortalama;
            String durum;
            s1 = Convert.ToDouble(LblSınav1.Text);
            s2 = Convert.ToDouble(LblSınav2.Text);
            s3 = Convert.ToDouble(LblSınav3.Text);
            ortalama = (s1 + s2 + s3) / 3;
            LblOrtalama.Text = ortalama.ToString();

            if (ortalama >= 50)
            {
                durum = "TRUE";
            }
            else
            {
                durum = "FALSE";
            }

            baglantı.Open();
            SqlCommand komut2 = new SqlCommand("update Tbl_ders set OGRS1=@p1,OGRS2=@p2,OGRS3=@p3,ORTALAMA=@p4,DURUM=@p5 where OGRNUMARA=@p6", baglantı);
            komut2.Parameters.AddWithValue("@p1", LblSınav1.Text);
            komut2.Parameters.AddWithValue("@p2", LblSınav2.Text);
            komut2.Parameters.AddWithValue("@p3", LblSınav3.Text);
            komut2.Parameters.AddWithValue("@p4", decimal.Parse(LblOrtalama.Text));
            komut2.Parameters.AddWithValue("@p5", durum);
            komut2.Parameters.AddWithValue("@p6", msknumara.Text);
            komut2.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Öğrenci Sınavları Güncellendi.");
            this.tbl_dersTableAdapter.Fill(this.dbNotKayıtDataSet.Tbl_ders);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            msknumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            LblAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            LblSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            LblSınav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            LblSınav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            LblSınav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }
    }
}
