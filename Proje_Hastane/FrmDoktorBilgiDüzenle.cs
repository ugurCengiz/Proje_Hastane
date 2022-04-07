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
    public partial class FrmDoktorBilgiDüzenle : Form
    {
        public FrmDoktorBilgiDüzenle()
        {
            InitializeComponent();
        }

        SqlBağlantısı bgl = new SqlBağlantısı();
        setup bgl1 = new setup();
        public string TCNO;


        private void FrmDoktorBilgiDüzenle_Load(object sender, EventArgs e)
        {
            MskTc.Text = TCNO;
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut = new SqlCommand(" Select * from Tbl_Doktorlar Where DoktorTC=@p1", conn);
            komut.Parameters.AddWithValue("@p1", MskTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())

            {
                Txtad.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                cmbbranş.Text = dr[3].ToString();
                txtsifre.Text = dr[5].ToString();
            }
            conn.Close();
            



        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p4 Where DoktorTC=@p5", conn);
            komut.Parameters.AddWithValue("@p1", Txtad.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbbranş.Text);
            komut.Parameters.AddWithValue("@p4", txtsifre.Text);
            komut.Parameters.AddWithValue("@p5", MskTc.Text);
            komut.ExecuteNonQuery();
            conn.Close();
            
            MessageBox.Show("Kayıt Güncellendi");


        }
    }
}
