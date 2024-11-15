using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DAO
{
    internal class TurnosDAO
    {
        // Método para obtener todos los turnos
        public static List<Turnos> ObtenerTurnos()
        {
            List<Turnos> lista = new List<Turnos>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_TURNO, Tipo, Horario FROM turnos";
                SqlCommand comando = new SqlCommand(query, conexion);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Turnos turno = new Turnos
                    {
                        IdTurno = reader.GetInt32(0),
                        Tipo = reader.GetString(1),
                        Horario = reader.GetTimeSpan(2)
                    };
                    lista.Add(turno);
                }
            }

            return lista;
        }

        // Método para insertar un nuevo turno
        public static int InsertarTurno(Turnos turno)
        {
            int resultado = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "INSERT INTO turnos (Tipo, Horario) VALUES (@Tipo, @Horario)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Tipo", turno.Tipo);
                comando.Parameters.AddWithValue("@Horario", turno.Horario);

                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }

        // Método para buscar ID y retornar el nombre del turno
        public static string ObtenerTipoTurnoPorId(int idTurno)
        {
            string tipoTurno = null;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT Tipo FROM turnos WHERE ID_TURNO = @ID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID", idTurno);

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    tipoTurno = reader.GetString(0);
                }
            }

            return tipoTurno;
        }

    }
}
