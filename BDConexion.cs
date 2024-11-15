using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD
{
    public class BDConexion
    {
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conection = new SqlConnection("Data Source=PC_ANDY\\SQLEXPRESS;Initial Catalog=PierreDB;Integrated Security=True;");
            conection.Open();
            return conection;
        }
    }
}
