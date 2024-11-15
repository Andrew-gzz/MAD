using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DAO
{
    internal class AjusteDAO
    {
        public static List<Ajuste> ObtenerAjustes()
        {
            List<Ajuste> listaAjustes = new List<Ajuste>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_AJUSTE, Motivo, Tipo, Porcentaje FROM ajustes";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Ajuste ajuste = new Ajuste
                    {
                        IdAjuste = reader.GetInt32(0),
                        Motivo = reader.GetString(1),
                        Tipo = reader.GetString(2),
                        Porcentaje = reader.GetDecimal(3)
                    };
                    listaAjustes.Add(ajuste);
                }
            }

            return listaAjustes;
        }

        public static int InsertarAjuste(Ajuste ajuste)
        {
            int resultado;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "INSERT INTO ajustes (Motivo, Tipo, Porcentaje) VALUES (@Motivo, @Tipo, @Porcentaje)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Motivo", ajuste.Motivo);
                comando.Parameters.AddWithValue("@Tipo", ajuste.Tipo);
                comando.Parameters.AddWithValue("@Porcentaje", ajuste.Porcentaje);
                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }

        //  método para obtener ajustes obligatorios
        public static List<Ajuste> ObtenerAjustesObligatorios()
        {
            List<Ajuste> listaAjustesObligatorios = new List<Ajuste>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_AJUSTE, Motivo, Tipo FROM ajustes WHERE ID_AJUSTE IN (1, 2, 4, 10, 11, 12, 14)";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Ajuste ajuste = new Ajuste
                    {
                        IdAjuste = reader.GetInt32(0),
                        Motivo = reader.GetString(1),
                        Tipo = reader.GetString(2),
                        Porcentaje = reader.GetDecimal(3)
                    };
                    listaAjustesObligatorios.Add(ajuste);
                }
            }

            return listaAjustesObligatorios;
        }

        public static List<Ajuste> ObtenerAjustesPorTipo(string tipo)
        {
            List<Ajuste> listaAjustes = new List<Ajuste>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_AJUSTE, Motivo, Tipo, Porcentaje FROM ajustes WHERE Tipo = @Tipo";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Tipo", tipo);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Ajuste ajuste = new Ajuste
                    {
                        IdAjuste = !reader.IsDBNull(0) ? reader.GetInt32(0) : default(int),
                        Motivo = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty,
                        Tipo = !reader.IsDBNull(2) ? reader.GetString(2) : string.Empty,
                        Porcentaje = !reader.IsDBNull(3) ? reader.GetDecimal(3) : default
                    };
                    listaAjustes.Add(ajuste);
                }
            }

            return listaAjustes;
        }
        public static int BuscarIdAjustePorMotivo(string motivo)
        {
            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_AJUSTE FROM ajustes WHERE Motivo = @Motivo";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Motivo", motivo);
                object resultado = comando.ExecuteScalar();
                if (resultado != null)
                {
                    return (int)resultado;
                }
                else
                {
                    return -1; // Indicar que no se encontró el ID_AJUSTE } }
                }
            }
        }
        public static Ajuste ObtenerAjustePorId(int idAjuste)
        {
            Ajuste ajuste = null;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_AJUSTE, Motivo FROM ajustes WHERE ID_AJUSTE = @IdAjuste";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@IdAjuste", idAjuste);

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    ajuste = new Ajuste
                    {
                        IdAjuste = reader.GetInt32(0),
                        Motivo = reader.GetString(1)
                    };
                }
            }

            return ajuste;
        }

    }
}
