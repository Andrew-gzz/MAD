using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DAO
{
    internal class PeriodoDAO
    {
        public static int InsertarPeriodo(Periodo periodo)
        {
            int retorno = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "INSERT INTO periodos (F_Inicial, F_Fin) VALUES (@FInicial, @FFin)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@FInicial", periodo.FInicial);
                comando.Parameters.AddWithValue("@FFin", periodo.FFin);

                retorno = comando.ExecuteNonQuery();
            }
            return retorno;
        }

        public static List<Periodo> ObtenerPeriodos()
        {
            List<Periodo> lista = new List<Periodo>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_Periodo, F_Inicial, F_Fin FROM periodos";
                SqlCommand comando = new SqlCommand(query, conexion);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Periodo periodo = new Periodo
                    {
                        IdPeriodo = reader.GetInt32(0),
                        FInicial = reader.GetDateTime(1),
                        FFin = reader.GetDateTime(2)
                    };

                    lista.Add(periodo);
                }
            }
            return lista;
        }
        public static List<Periodo> ObtenerPeriodosDesdeFechaIngreso(DateTime fechaIngreso)
        {
            List<Periodo> periodos = new List<Periodo>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_Periodo, F_Inicial, F_Fin FROM periodos WHERE F_Inicial >= @FechaIngreso";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@FechaIngreso", fechaIngreso);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Periodo periodo = new Periodo
                    {
                        IdPeriodo = reader.GetInt32(0),
                        FInicial = reader.GetDateTime(1),
                        FFin = reader.GetDateTime(2)
                    };

                    periodos.Add(periodo);
                }
            }

            return periodos;
        }
        //Optener periodo por ID       
        public static Periodo ObtenerPeriodoporID(int ID_Periodo)
        {
            Periodo periodo = null;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_Periodo, F_Inicial, F_Fin FROM periodos WHERE ID_Periodo = @ID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID", ID_Periodo);

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    periodo = new Periodo
                    {
                        IdPeriodo = reader.GetInt32(0),
                        FInicial = reader.GetDateTime(1),
                        FFin = reader.GetDateTime(2)
                    };
                }
            }

            return periodo;
        }
        public static int ObtIdPerPorF_Ingreso(DateTime fecha) {
            int idPeriodo = 0; // Inicializamos con 0 en caso de que no se encuentre un periodo.

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"
                SELECT ID_Periodo
                FROM Periodos
                WHERE @Fecha BETWEEN F_Inicial AND F_Fin";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Fecha", fecha);

                object resultado = comando.ExecuteScalar();

                if (resultado != null)
                {
                    idPeriodo = Convert.ToInt32(resultado);
                }
            }

            return idPeriodo;
        }
        public static List<Periodo> ObtPeridosEnUnRango(int PeriodoInicial, int PeriodoFinal)
        {
            List<Periodo> lista = new List<Periodo>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_Periodo, F_Inicial, F_Fin FROM periodos WHERE ID_Periodo BETWEEN @PeriodoInicial AND @PeriodoFinal";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@PeriodoInicial", PeriodoInicial);
                comando.Parameters.AddWithValue("@PeriodoFinal", PeriodoFinal);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Periodo periodo = new Periodo
                    {
                        IdPeriodo = reader.GetInt32(0),
                        FInicial = reader.GetDateTime(1),
                        FFin = reader.GetDateTime(2)
                    };

                    lista.Add(periodo);
                }
            }

            return lista;
        }
        public static List<Periodo> PeriodoEnAdelante(int idper)
        {
            List<Periodo> periodos = new List<Periodo>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_Periodo, F_Inicial, F_Fin FROM periodos WHERE ID_Periodo >= @ID_Periodo";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_Periodo", idper);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Periodo periodo = new Periodo
                    {
                        IdPeriodo = reader.GetInt32(0),
                        FInicial = reader.GetDateTime(1),
                        FFin = reader.GetDateTime(2)
                    };

                    periodos.Add(periodo);
                }
            }

            return periodos;
        }
        public static (int? IDPeriodoInicial, int? IDPeriodoFinal) ObtenerPeriodosPorFechas(DateTime fechaInicial, DateTime fechaFinal)
        {
            int? idPeriodoInicial = null;
            int? idPeriodoFinal = null;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                // Consulta para obtener el ID del período inicial
                string queryInicial = @"
            SELECT TOP 1 ID_Periodo
            FROM periodos
            WHERE @FechaInicial BETWEEN F_Inicial AND F_Fin";

                SqlCommand comandoInicial = new SqlCommand(queryInicial, conexion);
                comandoInicial.Parameters.AddWithValue("@FechaInicial", fechaInicial);

                object resultadoInicial = comandoInicial.ExecuteScalar();
                if (resultadoInicial != null)
                {
                    idPeriodoInicial = Convert.ToInt32(resultadoInicial);
                }

                // Consulta para obtener el ID del período final
                string queryFinal = @"
            SELECT TOP 1 ID_Periodo
            FROM periodos
            WHERE @FechaFinal BETWEEN F_Inicial AND F_Fin";

                SqlCommand comandoFinal = new SqlCommand(queryFinal, conexion);
                comandoFinal.Parameters.AddWithValue("@FechaFinal", fechaFinal);

                object resultadoFinal = comandoFinal.ExecuteScalar();
                if (resultadoFinal != null)
                {
                    idPeriodoFinal = Convert.ToInt32(resultadoFinal);
                }
            }

            return (idPeriodoInicial, idPeriodoFinal);
        }
        public static int ObtIdPerPorFInicial(DateTime fechaInicial)
        {
            int idPeriodo = 0; // Inicializamos con 0 en caso de que no se encuentre un periodo.

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"
                SELECT ID_Periodo
                FROM periodos
                WHERE MONTH(F_Inicial) = @Mes AND YEAR(F_Inicial) = @Año";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Mes", fechaInicial.Month);
                comando.Parameters.AddWithValue("@Año", fechaInicial.Year);

                object resultado = comando.ExecuteScalar();

                if (resultado != null)
                {
                    idPeriodo = Convert.ToInt32(resultado);
                }
            }

            return idPeriodo;
        }

        public static Periodo ObtenerPeriodoActual()
        {
            Periodo periodo = null;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT TOP 1 ID_Periodo, F_Inicial, F_Fin FROM periodos ORDER BY ID_Periodo DESC";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    periodo = new Periodo
                    {
                        IdPeriodo = reader.GetInt32(0),
                        FInicial = reader.GetDateTime(1),
                        FFin = reader.GetDateTime(2)
                    };
                }
            }

            return periodo;
        }

    }
}
