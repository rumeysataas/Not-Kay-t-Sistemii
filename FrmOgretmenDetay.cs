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

namespace NotKayitSistemi
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'dbNotKayıtDataSet.Tbl_ders' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tbl_dersTableAdapter.Fill(this.dbNotKayıtDataSet.Tbl_ders);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=RUMEYSA-W10\SQLEXPRESS;Initial Catalog=DbNotKayıt;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_ders(OGRNUMARA,OGRAD,OGRSOYAD) values (@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", MskNumara.Text);
            komut.Parameters.AddWithValue("@p2", TxtAd.Text);
            komut.Parameters.AddWithValue("@p3", TxtSoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Kaydı Yapıldı.");
            this.tbl_dersTableAdapter.Fill(this.dbNotKayıtDataSet.Tbl_ders);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            MskNumara.Text= dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtAd.Text= dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSoyad.Text= dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSınav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSınav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtSınav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();

        }
        string durum;

        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            s1 = Convert.ToDouble(TxtSınav1.Text);
            s2 = Convert.ToDouble(TxtSınav2.Text);
            s3 = Convert.ToDouble(TxtSınav3.Text);
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
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Tbl_ders set OGRS1=@P1,OGRS2=@P2,OGRS3=@P3,ORTALAMA=@P4,DURUM=@P5 WHERE OGRNUMARA=@P6", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtSınav1.Text);
            komut.Parameters.AddWithValue("@p2", TxtSınav2.Text);
            komut.Parameters.AddWithValue("@p3", TxtSınav3.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(LblOrtalama.Text));
            komut.Parameters.AddWithValue("@p5", durum);
            komut.Parameters.AddWithValue("@p6", MskNumara.Text);
            komut.ExecuteNonQuery();

            SqlCommand cmd = new SqlCommand("select count(*) from Tbl_ders where durum='TRUE'", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LblGecenSayısı.Text = dr[0].ToString();
            }
            dr.Close();

            SqlCommand cmd1 = new SqlCommand("select count(*) from TBLDERS where durum='FALSE'", baglanti);

            SqlDataReader drdr = cmd1.ExecuteReader();



            while (drdr.Read())

            {

                LblKalanSayısı.Text = drdr[0].ToString();

            }
            baglanti.Close();
            MessageBox.Show("Sınav Notları Güncellendi.");
            this.tbl_dersTableAdapter.Fill(this.dbNotKayıtDataSet.Tbl_ders);
        }
    }
}
