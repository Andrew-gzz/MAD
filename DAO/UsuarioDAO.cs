using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DAO
{
    internal class UsuarioDAO
    {

        public static bool AutenticarUsuario(string usuario, string contrasena)
        {
            bool autenticado = false;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM Users WHERE ID_USER = @usuario AND PASS = @contrasena;";

                SqlCommand comando = new SqlCommand(query, conexion);

                // Parametrización para evitar inyección SQL
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@contrasena", contrasena); // Si guardas el hash de la contraseña, deberías usar el mismo proceso de hashing aquí.

                int resultado = (int)comando.ExecuteScalar();

                // Si hay al menos un registro que coincide, el inicio de sesión es exitoso
                if (resultado > 0)
                {
                    autenticado = true;
                }
            }

            return autenticado;
        }
    }
}
