using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DAO
{
    internal class puestosDAO
    {
        // Método para obtener todos los puestos
        public static List<puestos> ObtenerPuestos()
        {
            List<puestos> lista = new List<puestos>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_PUESTO, Puesto FROM puestos";
                SqlCommand comando = new SqlCommand(query, conexion);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    puestos puesto = new puestos
                    {
                        IdPuesto = reader.GetInt32(0),
                        Puesto = reader.GetString(1)
                    };
                    lista.Add(puesto);
                }
            }

            return lista;
        }

        // Método para insertar un nuevo puesto
        public static int InsertarPuesto(puestos puesto)
        {
            int resultado = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "INSERT INTO puestos (Puesto) VALUES (@Puesto)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Puesto", puesto.Puesto);

                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }

        //Metodo para buscar por ID y devolver el nombre del puesto
        public static string ObtenerNombrePuestoPorId(int idPuesto)
        {
            string nombrePuesto = null;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT Puesto FROM puestos WHERE ID_PUESTO = @ID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID", idPuesto);

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    nombrePuesto = reader.GetString(0);
                }
            }

            return nombrePuesto;
        }
        public static int ObtIdPorPuesto(string puesto)
        {
            int ID = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_PUESTO FROM puestos WHERE Puesto = @puesto";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@puesto", puesto);

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    ID = reader.GetInt32(0);
                }
            }

            return ID;
        }
    }
}
