using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Personel_Takip_Programı
{
    public class SqlBaglantim
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=ceyda\\SQLEXPRESS;Initial Catalog=\"Yurt Otomasyonu\";Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
