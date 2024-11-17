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

        // Método para actualizar un movimiento
        public static int ActualizarMov(DateTime alta, int idalta, int idmov)
        {
            int resultado = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"
                UPDATE  Movimientos_Empleados
                SET F_ALTA = @F_ALTA, ID_PERIODO_ALTA = @ID_PERIODO_ALTA
                WHERE ID_MOVIMIENTO = @ID_MOVIMIENTO";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@F_ALTA", alta);
                comando.Parameters.AddWithValue("@ID_PERIODO_ALTA", idalta);
                comando.Parameters.AddWithValue("@ID_MOVIMIENTO", idmov);

                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }

        // Método para obtener el ID_MOVIMIENTO activo
        public static int ObtenerIdMovimientoActivo(int idEmpleado)
        {
            int idMovimiento = -1;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_MOVIMIENTO, ID_PERIODO_ALTA FROM Movimientos_Empleados WHERE ID_EMPLEADO = @ID_EMPLEADO AND F_BAJA IS NULL";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_EMPLEADO", idEmpleado);

                object resultado = comando.ExecuteScalar();

                if (resultado != null)
                {
                    idMovimiento = Convert.ToInt32(resultado);
                }
            }

            return idMovimiento;
        }

        public static int BajaEmpleado(DateTime baja, int idbaja, int idmov)
        {
            int resultado = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"
                UPDATE  Movimientos_Empleados
                SET F_BAJA = @F_BAJA, ID_PERIODO_BAJA = @ID_PERIODO_BAJA
                WHERE ID_MOVIMIENTO = @ID_MOVIMIENTO";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@F_BAJA", baja);
                comando.Parameters.AddWithValue("@ID_PERIODO_BAJA", idbaja);
                comando.Parameters.AddWithValue("@ID_MOVIMIENTO", idmov);

                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }

        public static List<Movimientos_Empleados> ObtPeriodosPorEmp(int empleado)
        {
            List<Movimientos_Empleados> lista = new List<Movimientos_Empleados>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_PERIODO_ALTA, ID_PERIODO_BAJA FROM Movimientos_Empleados WHERE ID_EMPLEADO = @ID_EMPLEADO AND ID_PERIODO_BAJA IS NOT NULL";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_EMPLEADO", empleado);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Movimientos_Empleados datos = new Movimientos_Empleados
                    {
                        ID_PERIODO_ALTA = reader.GetInt32(0),
                        ID_PERIODO_BAJA = reader.GetInt32(1)
                    };

                    lista.Add(datos);
                }
            }

            return lista;
        }

        public static int ObtPeriodoAlta(int idEmpleado)
        {
            int periodo = -1;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_PERIODO_ALTA FROM Movimientos_Empleados WHERE ID_EMPLEADO = @ID_EMPLEADO AND F_BAJA IS NULL";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_EMPLEADO", idEmpleado);

                object resultado = comando.ExecuteScalar();

                if (resultado != null)
                {
                    periodo = Convert.ToInt32(resultado);
                }
            }

            return periodo;
        }
    }

}
