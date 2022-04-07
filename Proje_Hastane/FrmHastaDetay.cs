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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        SqlBağlantısı bgl = new SqlBağlantısı();
        setup bgl1 = new setup();
        public string tc;
        

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar Where DoktorBrans=@p1",conn);
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            conn.Close();
            
        }

       

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            Lbltc.Text = tc;
            //AD SOYAD ÇEKME
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut = new SqlCommand("Select HastaAd , HastaSoyad From Tbl_Hastalar Where HastaTC=@p1", conn);
            komut.Parameters.AddWithValue("@p1", Lbltc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read( ))
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];

            }
            conn.Close();
            

            //RANDEVU GEÇMİŞİ
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular Where HastaTC=" + tc, conn);
            conn.Open();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();

            //BRANŞLARI ÇEKME
            
            SqlCommand komut2 = new SqlCommand("Select BranşAd From Tbl_Branşlar", conn);
            conn.Open();
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            conn.Close();                         
        }

        

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular Where RandevuBrans='" + CmbBrans.Text + "'" + " and RandevuDoktor='" + CmbDoktor.Text + "' and RandevuDurum=0", conn);
            da.Fill(dt2);
            dataGridView2.DataSource = dt2;
            conn.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            FrmBilgiDüzenle fr = new FrmBilgiDüzenle();
            fr.TCno = Lbltc.Text;
            fr.Show();
            conn.Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
            conn.Close();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut3 = new SqlCommand("Update Tbl_Randevular Set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 Where Randevuıd=@p3", conn);
            komut3.Parameters.AddWithValue("@p1", Lbltc.Text);
            komut3.Parameters.AddWithValue("@p2", RchSikayet.Text);
            komut3.Parameters.AddWithValue("@p3", Txtid.Text);
            komut3.ExecuteNonQuery();
            conn.Close();
            bgl.bağlanti().Close();
            MessageBox.Show("Randevu Alındı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
