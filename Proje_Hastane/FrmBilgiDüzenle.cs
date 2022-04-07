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


namespace Proje_Hastane
{
    public partial class FrmBilgiDüzenle : Form
    {
        public FrmBilgiDüzenle()
        {
            InitializeComponent();
        }
        public string TCno;

        SqlBağlantısı bgl = new SqlBağlantısı();
        setup bgl1 = new setup();

        



        private void FrmBilgiDüzenle_Load(object sender, EventArgs e)
        {
            MskTc.Text = TCno;
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut = new SqlCommand("Select * From Tbl_Hastalar where HastaTC=@p1", conn);
            komut.Parameters.AddWithValue("@p1", MskTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Txtad.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                MskTelefon.Text = dr[4].ToString();
                txtsifre.Text = dr[5].ToString();
                CmbCinsiyet.Text = dr[6].ToString();

            }
            conn.Close();
            


        }

        private void Btnkayıtyap_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut2 = new SqlCommand("update Tbl_Hastalar set HastaAd=@p1,HastaSoyad=@p2,HastaTelefon=@p3,HastaSifre=@p4,HastaCinsiyet=@p5 Where HastaTc=@p6", conn);
            komut2.Parameters.AddWithValue("@p1", Txtad.Text);
            komut2.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", MskTelefon.Text);
            komut2.Parameters.AddWithValue("@p4", txtsifre.Text);
            komut2.Parameters.AddWithValue("@p5", CmbCinsiyet.Text);
            komut2.Parameters.AddWithValue("@p6", MskTc.Text);
            komut2.ExecuteNonQuery();
            conn.Close();
            
            MessageBox.Show("Bilgileriniz Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
