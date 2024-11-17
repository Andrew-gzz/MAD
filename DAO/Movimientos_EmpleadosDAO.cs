using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DAO
{
    internal class Movimientos_EmpleadosDAO
    {
        // Método para insertar un nuevo movimiento de alta y baja
        public static int InsertarMovimiento(Movimientos_Empleados movimientos)
        {
            int resultado = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"
                    INSERT INTO Movimientos_Empleados (ID_EMPLEADO, F_ALTA, ID_PERIODO_ALTA)
                    VALUES (@ID_EMP, @F_ALTA, @ID_PER_ALTA)";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_EMP", movimientos.ID_EMPLEADO);
                comando.Parameters.AddWithValue("@F_ALTA", movimientos.F_ALTA);
                comando.Parameters.AddWithValue("@ID_PER_ALTA", movimientos.ID_PERIODO_ALTA);
                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }
        // Método para obtener la lista de todos las altas y bajas
        public static List<Movimientos_Empleados> ObtenerMovimientos()
        {
            List<Movimientos_Empleados> listamovimientos = new List<Movimientos_Empleados>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT * FROM Movimientos_Empleados;";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Movimientos_Empleados movimiento = new Movimientos_Empleados
                    {
                        ID_MOVIMIENTO = reader.GetInt32(0),
                        ID_EMPLEADO = reader.GetInt32(1),
                        F_ALTA = reader.GetDateTime(2),
                        F_BAJA = reader.GetDateTime(3),
                        ID_PERIODO_ALTA = reader.GetInt32(1)                       
                    };

                    listamovimientos.Add(movimiento);
                }
            }

            return listamovimientos;
        }

       
    }
}
