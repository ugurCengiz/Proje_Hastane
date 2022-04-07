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
    public partial class SekreterDetay : Form
    {
        public SekreterDetay()
        {
            InitializeComponent();
        }
        public string TCnumara;
        
        SqlBağlantısı bgl = new SqlBağlantısı();
        setup bgl1 = new setup();


        private void SekreterDetay_Load(object sender, EventArgs e)
        {
            Lbltc.Text = TCnumara;

            //AD SOYAD  hhhh
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut1 = new SqlCommand("Select SekreterAdSoyad from Tbl_Sekreter Where SekreterTC=@p1", conn);
            komut1.Parameters.AddWithValue("@p1", Lbltc.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read()) 

            {
                LblAdSoyad.Text = dr1[0].ToString();

            }
            conn.Close();
            

            //BRANŞLARİ DATAGRİDE AKTARMA

            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Tbl_Branşlar", conn);
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Doktorları DATAGRİDE Aktarma
            
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd + ' ' + DoktorSoyad) as ' Doktorlar ' , DoktorBrans from Tbl_Doktorlar ", conn);
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //BRANŞ GETİRME 
            
            SqlCommand komut2 = new SqlCommand("Select BranşAd from Tbl_Branşlar", conn);
            conn.Open();
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBranş.Items.Add(dr2[0]);

            }
            conn.Close();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komutKaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)",conn);
            komutKaydet.Parameters.AddWithValue("@r1", MskTarih.Text);
            komutKaydet.Parameters.AddWithValue("@r2", MskSaat.Text);
            komutKaydet.Parameters.AddWithValue("@r3", CmbBranş.Text);
            komutKaydet.Parameters.AddWithValue("@r4", CmbDoktor.Text);
            komutKaydet.ExecuteNonQuery();
            conn.Close();
            
            MessageBox.Show("Randevu Oluşturuldu");


        }

        private void CmbBranş_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut3 = new SqlCommand("Select DoktorAd, DoktorSoyad From Tbl_Doktorlar Where DoktorBrans=@p1", conn);
            komut3.Parameters.AddWithValue("@p1", CmbBranş.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())

            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            conn.Close();
            

        }

        private void BtnOluştur_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (duyuru) values (@d1)", conn);
            komut.Parameters.AddWithValue("@d1", RchDuyuru.Text);
            komut.ExecuteNonQuery();
            conn.Close();
            
            MessageBox.Show("Duyuru Oluşturuldu");

        }

        private void BtnDoktorPaneli_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();
            conn.Close();
        }

        private void BtnBranşPaneli_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            FrmBranş frb = new FrmBranş();
            frb.Show();
            conn.Close();

        }

        private void BtnRandevuListe_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            FrmRandevuListesi frl = new FrmRandevuListesi();
            frl.Show();
            conn.Close();

        }

        
        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
            conn.Close();
        }
    }
}
