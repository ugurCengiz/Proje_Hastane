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
    public partial class FrmDoktorGiriş : Form
    {
        public FrmDoktorGiriş()
        {
            InitializeComponent();
        }
        SqlBağlantısı bgl = new SqlBağlantısı();
        setup bgl1 = new setup();


        private void Btngiris_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            SqlCommand komut = new SqlCommand("Select * From Tbl_Doktorlar Where DoktorTC=@p1 and DoktorSifre=@p2", conn);
            komut.Parameters.AddWithValue("@p1", MskTc.Text);
            komut.Parameters.AddWithValue("@p2", txtsifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())  
            {
                FrmDoktorDetay fr = new FrmDoktorDetay();
                fr.TC = MskTc.Text;
                fr.Show();
                this.Hide();


            }
            else
            {
                MessageBox.Show("Hatalı TC & Şifre", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            conn.Close();
            


        }

       
    }
}
