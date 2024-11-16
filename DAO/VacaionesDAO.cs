using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DAO
{
    internal class VacacionesDAO
    {
        //Devuelve los dias de vacaciones segun la antiguedad del empleado
        public static int ObtenerDiasVacaciones(int antiguedad)
        {
            int diasVacaciones = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT DiasVacaciones FROM Vacaciones WHERE Antiguedad = @Antiguedad";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Antiguedad", antiguedad);

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    diasVacaciones = reader.GetInt32(0);
                }
            }

            return diasVacaciones;
        }
        //Devuelve una lista con todos los dias y años de antiguedad
        public static List<Vacaciones> ListarVacaciones()
        {
            List<Vacaciones> lista = new List<Vacaciones>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT Antiguedad, DiasVacaciones FROM Vacaciones";
                SqlCommand comando = new SqlCommand(query, conexion);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Vacaciones vacacion = new Vacaciones
                    {
                        Antiguedad = reader.GetInt32(0),
                        DiasVacaciones = reader.GetInt32(1)
                    };
                    lista.Add(vacacion);
                }
            }

            return lista;
        }
    }
}
