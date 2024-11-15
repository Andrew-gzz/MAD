﻿using MAD.Tablas;
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
    }
}
