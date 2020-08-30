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
    public partial class ÖgrenciKontrolSistemi : Form
    {
        public String numara;
        //Data Source=RUMEYSA-W10\SQLEXPRESS;Initial Catalog=DbNotKayıt;Integrated Security=True
        SqlConnection baglantı = new SqlConnection(@"Data Source=RUMEYSA-W10\SQLEXPRESS;Initial Catalog=DbNotKayıt;Integrated Security=True
");
        public ÖgrenciKontrolSistemi()
        {
            InitializeComponent();
        }

        private void ÖgrenciKontrolSistemi_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;
            baglantı.Open();
            SqlCommand komut = new SqlCommand("Select * from Tbl_ders where OGRNUMARA=@p1", baglantı);
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[2].ToString() + " " + dr[3].ToString();
                LblSınav1.Text = dr[4].ToString();
                LblSınav2.Text = dr[5].ToString();
                LblSınav3.Text = dr[6].ToString();
                LblOrtalama.Text = dr[7].ToString();
                LblDurum.Text = dr[8].ToString();
            }
            baglantı.Close();
        }
  
    }
}
