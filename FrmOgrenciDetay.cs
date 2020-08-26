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
    public partial class FrmOgrenciDetay : Form
    {
        public FrmOgrenciDetay()
        {
            InitializeComponent();
        }

        private void LblDurum_Click(object sender, EventArgs e)
        {

        }

        public string numara;
        SqlConnection baglanti = new SqlConnection(@"Data Source=RUMEYSA-W10\SQLEXPRESS;Initial Catalog=DbNotKayıt;Integrated Security=True");

        private void FrmOgrenciDetay_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_ders where OGRNUMARA=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[2].ToString() + " " + dr[3].ToString();
                LblSınav1.Text = dr[4].ToString();
                LblSınav2.Text = dr[5].ToString();
                LblSınav3.Text = dr[6].ToString();
                LblOrtalama.Text = dr[7].ToString();
                if (dr[8].ToString()== "True")
                {
                    LblDurum.Text = "GEÇTİ";
                }
                else
                {
                    LblDurum.Text = "KALDI";
                }

            }
            baglanti.Close();
        }
    }
}
