using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Proje_Hastane
{
    class SqlBağlantısı
    {
        public SqlConnection bağlanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-92FPH14;Initial Catalog=HastaneProje;Integrated Security=True");
            baglan.Open();
            return baglan;


        }
    }
}
