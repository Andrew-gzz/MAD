using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DAO
{
    internal class ISRDAO
    {
        // Método para insertar un nuevo registro en la tabla ISR
        public static int InsertarISR(ISR isr)
        {
            int retorno = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "INSERT INTO Tabla_ISR (L_Inferior, Cuota, Porcentaje, Año) VALUES (@LInferior, @Cuota, @Porcentaje, @Año)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@LInferior", isr.L_Inferior);
                comando.Parameters.AddWithValue("@Cuota", isr.Cuota);
                comando.Parameters.AddWithValue("@Porcentaje", isr.Porcentaje);
                comando.Parameters.AddWithValue("@Año", isr.Year.Year); // Extrae solo el año

                retorno = comando.ExecuteNonQuery();
            }
            return retorno;
        }

        // Método para buscar registros de ISR por año
        public static List<ISR> ObtenerISRPorid(int id)
        {
            List<ISR> listaISR = new List<ISR>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_ISR, L_Inferior, Cuota, Porcentaje, Año FROM Tabla_ISR WHERE ID_ISR = @ISR";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ISR", id);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    ISR isr = new ISR
                    {
                        ID_ISR = reader.GetInt32(0),
                        L_Inferior = reader.GetDecimal(1),
                        Cuota = reader.GetDecimal(2),
                        Porcentaje = reader.GetDecimal(3),
                        Year = new DateTime(reader.GetInt32(4), 1, 1) // Crea una fecha con solo el año
                    };

                    listaISR.Add(isr);
                }
            }
            return listaISR;
        }
        public static List<ISR> ObtenerISR()
        {
            List<ISR> listaISR = new List<ISR>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_ISR, L_Inferior, Cuota, Porcentaje, Año FROM Tabla_ISR ";
                SqlCommand comando = new SqlCommand(query, conexion);                
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    ISR isr = new ISR
                    {
                        ID_ISR = reader.GetInt32(0),
                        L_Inferior = reader.GetDecimal(1),
                        Cuota = reader.GetDecimal(2),
                        Porcentaje = reader.GetDecimal(3),
                        Year = new DateTime(reader.GetInt32(4), 1, 1)
                    };

                    listaISR.Add(isr);
                }
            }
            return listaISR;
        }
        //Buscar por año y salario
        public static ISR ObtenerISRPorSalario(decimal salarioMensual, int anio)
        {
            ISR isr = null;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"SELECT TOP 1 ID_ISR, L_Inferior, Cuota, Porcentaje, Año
                         FROM Tabla_ISR
                         WHERE L_Inferior <= @SalarioMensual AND Año = @Año
                         ORDER BY L_Inferior DESC";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@SalarioMensual", salarioMensual);
                comando.Parameters.AddWithValue("@Año", anio);

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    isr = new ISR
                    {
                        ID_ISR = reader.GetInt32(0),
                        L_Inferior = reader.GetDecimal(1),
                        Cuota = reader.GetDecimal(2),
                        Porcentaje = reader.GetDecimal(3),
                        Year = new DateTime(reader.GetInt32(4), 1, 1)
                    };
                }
            }
            return isr;
        }

    }
}
