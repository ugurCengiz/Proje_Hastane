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
    public partial class FrmBranş : Form
    {
        public FrmBranş()
        {
            InitializeComponent();
        }
        SqlBağlantısı bgl = new SqlBağlantısı();
        setup bgl1 = new setup();



        private void FrmBranş_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Branşlar", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();

        }
        

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Branşlar (BranşAd) values (@b1)", conn);
            komut.Parameters.AddWithValue("@b1", TxtBranş.Text);
            komut.ExecuteNonQuery();
            conn.Close();
            
            MessageBox.Show("Branş Eklendi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtBranş.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            conn.Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut = new SqlCommand("Delete  From Tbl_Branşlar where Branşıd=@b1", conn);
            komut.Parameters.AddWithValue("@b1", Txtid.Text);
            komut.ExecuteNonQuery();
            conn.Close();
            
            MessageBox.Show("Branş Silindi");



        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut = new SqlCommand("update Tbl_Branşlar set BranşAd=@p1 Where Branşıd=@p2", conn);
            komut.Parameters.AddWithValue("@p1", TxtBranş.Text);
            komut.Parameters.AddWithValue("@p2", Txtid.Text);
            komut.ExecuteNonQuery();
            conn.Close();
            
            MessageBox.Show("Branş Güncellendi");

        }
    }
}
