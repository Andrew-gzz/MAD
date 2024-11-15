using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DAO
{
    internal class EmpresaDAO
    {
        //Optener lista de empresas
        public static List<Empresa> ObtenerEmpresa()
        {
            List<Empresa> listaEmpresas = new List<Empresa>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT * FROM Empresa;";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Empresa empresa = new Empresa
                    {
                        IdEmpresa = reader.GetInt32(0),
                        RepLegal = reader.GetString(1),
                        Ra_S = reader.GetString(2),
                        Direccion = reader.GetString(3),
                        Re_Fiscal = reader.GetString(4),
                        RFC = reader.GetString(5),
                        Id_RP = reader.GetInt32(6)                        
                    };

                    listaEmpresas.Add(empresa);
                }
            }

            return listaEmpresas;
        }
        //Buscar por ID
        public static Empresa ObtenerEmpresaPorId(int idEmpresa)
        {
            Empresa empresa = null;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT * FROM Empresa WHERE ID_EMPRESA = @ID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID", idEmpresa);

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    empresa = new Empresa
                    {
                        IdEmpresa = reader.GetInt32(0),
                        RepLegal = reader.GetString(1),
                        Ra_S = reader.GetString(2),
                        Direccion = reader.GetString(3),
                        Re_Fiscal = reader.GetString(4),
                        RFC = reader.GetString(5),
                        Id_RP = reader.GetInt32(6)
                    };
                }
            }

            return empresa;
        }
    }
}
