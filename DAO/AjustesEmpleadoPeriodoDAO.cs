using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAD.DAO
{
    internal class AjustesEmpleadoPeriodoDAO
    {
        public static int InsertarAjusteEmpleadoPeriodo(AjustesEmpleadoPeriodo ajusteEmpleadoPeriodo)
        {
            int retorno = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "INSERT INTO Ajustes_Empleado_Periodo (ID_AJUSTE, ID_EMP, ID_PERIODO, DiasHorasIMSS) VALUES (@IdAjuste, @IdEmp, @IdPeriodo, @DiasHorasIMSS)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@IdAjuste", ajusteEmpleadoPeriodo.IdAjuste);
                comando.Parameters.AddWithValue("@IdEmp", ajusteEmpleadoPeriodo.IdEmp);
                comando.Parameters.AddWithValue("@IdPeriodo", ajusteEmpleadoPeriodo.IdPeriodo);
                comando.Parameters.AddWithValue("@DiasHorasIMSS", ajusteEmpleadoPeriodo.DiasHorasIMSS);
                retorno = comando.ExecuteNonQuery();
            }
            return retorno;
        }

        public static List<AjustesEmpleadoPeriodo> ObtenerAjustesEmpleadoPeriodo()
        {
            List<AjustesEmpleadoPeriodo> lista = new List<AjustesEmpleadoPeriodo>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_AJUSTE, ID_EMP, ID_PERIODO, DiasHorasIMSS FROM Ajustes_Empleado_Periodo";
                SqlCommand comando = new SqlCommand(query, conexion);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    AjustesEmpleadoPeriodo ajusteEmpleadoPeriodo = new AjustesEmpleadoPeriodo
                    {
                        IdAjuste = reader.GetInt32(0),
                        IdEmp = reader.GetInt32(1),
                        IdPeriodo = reader.GetInt32(2),
                        DiasHorasIMSS = reader.GetInt64(3)
                    };

                    lista.Add(ajusteEmpleadoPeriodo);
                }
            }
            return lista;
        }
        //Metodo para verificar repetidos
        public static bool VerificarRepetidos(int idEmp, int idAjuste, int idPeriodo)
        {
            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM Ajustes_Empleado_Periodo WHERE ID_EMP = @IdEmp AND ID_AJUSTE = @IdAjuste AND ID_PERIODO = @IdPeriodo";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@IdEmp", idEmp);
                comando.Parameters.AddWithValue("@IdAjuste", idAjuste);
                comando.Parameters.AddWithValue("@IdPeriodo", idPeriodo);

                int count = (int)comando.ExecuteScalar();

                return count > 0;
            }
        }
        public static List<AjustesEmpleadoPeriodo> ObtenerAjustesPorEmpleado(int idEmp)
        {
            List<AjustesEmpleadoPeriodo> lista = new List<AjustesEmpleadoPeriodo>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_AJUSTE, ID_EMP, ID_PERIODO, DiasHorasIMSS FROM Ajustes_Empleado_Periodo WHERE ID_EMP = @IdEmp";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@IdEmp", idEmp);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    AjustesEmpleadoPeriodo ajuste = new AjustesEmpleadoPeriodo
                    {
                        IdAjuste = reader.GetInt32(0),
                        IdEmp = reader.GetInt32(1),
                        IdPeriodo = reader.GetInt32(2),
                        DiasHorasIMSS = reader.GetInt64(3)
                    };
                    lista.Add(ajuste);
                }
            }

            return lista;
        }
        public static bool EliminarAjusteEmpleado(int idAjuste, int idEmp, int idPeriodo)
        {

           
            int retorno = 0;

            using (SqlConnection conexion =  BDConexion.ObtenerConexion())
            {
                string query = "DELETE FROM Ajustes_Empleado_Periodo WHERE ID_AJUSTE = @ID_AJUSTE AND ID_EMP = @ID_EMP AND ID_PERIODO = @ID_PERIODO";
                SqlCommand command = new SqlCommand(query, conexion);
                command.Parameters.AddWithValue("@ID_AJUSTE", idAjuste);
                command.Parameters.AddWithValue("@ID_EMP", idEmp);
                command.Parameters.AddWithValue("@ID_PERIODO", idPeriodo);
                retorno = command.ExecuteNonQuery();                                   
            }
            return retorno > 0;
           
        }


    }
}
