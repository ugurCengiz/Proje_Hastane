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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        SqlBağlantısı bgl = new SqlBağlantısı();
        setup bgl1 = new setup();
        public string TC;

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            Lbltc.Text = TC;

            //DOKTOR AD SOYAD ÇEKME
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut1 = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar Where DoktorTC=@p1", conn);
            komut1.Parameters.AddWithValue("@p1", Lbltc.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())

            {
                LblAdSoyad.Text = dr1[0]+" "+dr1[1].ToString();

            }
            conn.Close();
            

            // RANDEVULAR
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular Where RandevuDoktor='" + LblAdSoyad.Text + "'", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            


        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            FrmDoktorBilgiDüzenle fr = new FrmDoktorBilgiDüzenle();
            fr.TCNO = Lbltc.Text;
            fr.Show();
            conn.Close();

        }

        private void RchSikayet_TextChanged(object sender, EventArgs e)
        {

            
        }

        private void BtnÇıkış_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            Application.Exit();
            conn.Close();

        }

        private void BtnDuyuru_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
            conn.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            RchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            conn.Close();


        }

        private void LblAdSoyad_Click(object sender, EventArgs e)
        {

        }
    }
}
