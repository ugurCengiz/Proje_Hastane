﻿using System;
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
    public partial class FrmDuyurular : Form
    {
        public FrmDuyurular()
        {
            InitializeComponent();
        }
        SqlBağlantısı bgl = new SqlBağlantısı();
        setup bgl1 = new setup();


        private void FrmDuyurular_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(bgl1.Adres);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_duyurular", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();

            
        }
    }
}
