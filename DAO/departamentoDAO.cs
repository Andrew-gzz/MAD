using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DAO
{
    namespace MAD.DAO
    {
        internal class departamentoDAO
        {
            // Método para obtener todos los departamentos
            public static List<departamentos> ObtenerDepartamentos()
            {
                List<departamentos> lista = new List<departamentos>();

                using (SqlConnection conexion = BDConexion.ObtenerConexion())
                {
                    string query = "SELECT ID_DEP, Departamento FROM departamentos";
                    SqlCommand comando = new SqlCommand(query, conexion);

                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        departamentos departamento = new departamentos
                        {
                            IdDep = reader.GetInt32(0),
                            Departamento = reader.GetString(1)
                        };
                        lista.Add(departamento);
                    }
                }

                return lista;
            }

            // Método para insertar un nuevo departamento
            public static int InsertarDepartamento(departamentos departamento)
            {
                int resultado = 0;

                using (SqlConnection conexion = BDConexion.ObtenerConexion())
                {
                    string query = "INSERT INTO departamentos (Departamento) VALUES (@Departamento)";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@Departamento", departamento.Departamento);

                    resultado = comando.ExecuteNonQuery();
                }

                return resultado;
            }
            //Metodo para buscar por ID y retornar el nombre
            public static string ObtenerNombreDepartamentoPorId(int idDep)
            {
                string nombreDepartamento = null;

                using (SqlConnection conexion = BDConexion.ObtenerConexion())
                {
                    string query = "SELECT Departamento FROM departamentos WHERE ID_DEP = @ID";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@ID", idDep);

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        nombreDepartamento = reader.GetString(0);
                    }
                }

                return nombreDepartamento;
            }
            public static int ObtIdPorDep(string dep)
            {
                int idDep = 0;

                using (SqlConnection conexion = BDConexion.ObtenerConexion())
                {
                    string query = "SELECT ID_DEP FROM departamentos WHERE Departamento = @DEP";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@DEP", dep);

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        idDep = reader.GetInt32(0);
                    }
                }

                return idDep;
            }

        }
    }
   
}
