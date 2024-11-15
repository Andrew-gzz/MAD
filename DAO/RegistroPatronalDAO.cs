using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DAO
{
    internal class RegistroPatronalDAO
    {
        //Buscar por ID
        public static RegistroPatronal ObtenerRegistroPorId(int id_reg)
        {
            RegistroPatronal registro = null;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT * FROM RegistroPatronal WHERE ID_RP = @ID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID", id_reg);

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    registro = new RegistroPatronal
                    {
                        ID_RP = reader.GetInt32(0),                       
                        Direccion = reader.GetString(1),
                        Fraccion_I = reader.GetString(2),
                        Fraccion_II = reader.GetString(3),
                        Fraccion_III = reader.GetString(4),
                        Fraccion_IV = reader.GetString(5),
                        Fraccion_V = reader.GetString(6),
                        Riesgo = reader.GetString(7),
                        Sellos = reader.GetString(8)
                    };
                }
            }

            return registro;
        }
    }
}
