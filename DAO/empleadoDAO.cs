using MAD.Tablas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DAO
{
    internal class EmpleadoDAO
    {
        // Método para insertar un nuevo empleado
        public static int InsertarEmpleado(Empleado empleado)
        {
            int resultado = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"
                    INSERT INTO empleados (IMSS, CURP, Nombre, F_Nacimiento, Correo, Genero, Telefono, RFC, Direccion, Salario_Diario, Sueldo_Mensual, Salario_Diario_Integrado, Antiguedad, Fecha_de_Ingreso, ID_PUESTO, ID_DEP, ID_TURNO, Estatus, ID_ISR)
                    VALUES (@IMSS, @CURP, @Nombre, @F_Nacimiento, @Correo, @Genero, @Telefono, @RFC, @Direccion, @Salario_Diario, @Sueldo_Mensual, @Salario_Diario_Integrado, @Antiguedad, @Fecha_de_Ingreso, @ID_PUESTO, @ID_DEP, @ID_TURNO, @Estatus, @ISR)";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@IMSS", empleado.Imss);
                comando.Parameters.AddWithValue("@CURP", empleado.Curp);
                comando.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                comando.Parameters.AddWithValue("@F_Nacimiento", empleado.FechaNacimiento);
                comando.Parameters.AddWithValue("@Correo", empleado.Correo);
                comando.Parameters.AddWithValue("@Genero", empleado.Genero);
                comando.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                comando.Parameters.AddWithValue("@RFC", empleado.Rfc);
                comando.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                comando.Parameters.AddWithValue("@Salario_Diario", empleado.SalarioDiario);
                comando.Parameters.AddWithValue("@Sueldo_Mensual", empleado.SueldoMensual);
                comando.Parameters.AddWithValue("@Salario_Diario_Integrado", empleado.SalarioDiarioIntegrado);
                comando.Parameters.AddWithValue("@Antiguedad", empleado.Antiguedad);
                comando.Parameters.AddWithValue("@Fecha_de_Ingreso", empleado.FechaDeIngreso);
                comando.Parameters.AddWithValue("@ID_PUESTO", empleado.IdPuesto);
                comando.Parameters.AddWithValue("@ID_DEP", empleado.IdDep);
                comando.Parameters.AddWithValue("@ID_TURNO", empleado.IdTurno);
                comando.Parameters.AddWithValue("@Estatus", empleado.Estatus);
                comando.Parameters.AddWithValue("@ISR", empleado.ID_ISR);

                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }

        // Método para obtener la lista de todos los empleados
        public static List<Empleado> ObtenerEmpleados()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT * FROM empleados;";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Empleado empleado = new Empleado
                    {
                        IdEmpleado = reader.GetInt32(0),
                        Imss = reader.GetInt64(1),
                        Curp = reader.GetString(2),
                        Nombre = reader.GetString(3),
                        FechaNacimiento = reader.GetDateTime(4),
                        Correo = reader.GetString(5),
                        Genero = reader.GetString(6),
                        Telefono = reader.GetInt64(7),
                        Rfc = reader.GetString(8),
                        Direccion = reader.GetString(9),
                        SalarioDiario = reader.GetDecimal(10),
                        SueldoMensual = reader.GetDecimal(11),
                        SalarioDiarioIntegrado = reader.GetDecimal(12),
                        Antiguedad = reader.GetInt32(13),
                        FechaDeIngreso = reader.GetDateTime(14),
                        IdPuesto = reader.GetInt32(15),
                        IdDep = reader.GetInt32(16),
                        IdTurno = reader.GetInt32(17),
                        Estatus = reader.GetBoolean(18),
                        ID_ISR = reader.GetInt32(19),
                    };

                    listaEmpleados.Add(empleado);
                }
            }

            return listaEmpleados;
        }

        //Obtener empleados por departamento       
        public static List<Empleado> ObtEmpPorIdDep(int ID)
        {
            List<Empleado> listaEmpleados = new List<Empleado>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT * FROM empleados WHERE ID_DEP = @ID_DEP;";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_DEP", ID);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Empleado empleado = new Empleado
                    {
                        IdEmpleado = reader.GetInt32(0),
                        Imss = reader.GetInt64(1),
                        Curp = reader.GetString(2),
                        Nombre = reader.GetString(3),
                        FechaNacimiento = reader.GetDateTime(4),
                        Correo = reader.GetString(5),
                        Genero = reader.GetString(6),
                        Telefono = reader.GetInt64(7),
                        Rfc = reader.GetString(8),
                        Direccion = reader.GetString(9),
                        SalarioDiario = reader.GetDecimal(10),
                        SueldoMensual = reader.GetDecimal(11),
                        SalarioDiarioIntegrado = reader.GetDecimal(12),
                        Antiguedad = reader.GetInt32(13),
                        FechaDeIngreso = reader.GetDateTime(14),
                        IdPuesto = reader.GetInt32(15),
                        IdDep = reader.GetInt32(16),
                        IdTurno = reader.GetInt32(17),
                        Estatus = reader.GetBoolean(18),
                        ID_ISR = reader.GetInt32(19),
                    };

                    listaEmpleados.Add(empleado);
                }
            }

            return listaEmpleados;
        }
        // Método para actualizar un empleado existente
        public static int ActualizarEmpleado(Empleado empleado)
        {
            int resultado = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"
                    UPDATE empleados 
                    SET IMSS = @IMSS, CURP = @CURP, Nombre = @Nombre, F_Nacimiento = @F_Nacimiento, Correo = @Correo, Genero = @Genero,
                        Telefono = @Telefono, RFC = @RFC, Direccion = @Direccion, Salario_Diario = @Salario_Diario, Sueldo_Mensual = @Sueldo_Mensual,
                        Salario_Diario_Integrado = @Salario_Diario_Integrado, Antiguedad = @Antiguedad, Fecha_de_Ingreso = @Fecha_de_Ingreso,
                        ID_PUESTO = @ID_PUESTO, ID_DEP = @ID_DEP, ID_TURNO = @ID_TURNO, Estatus = @Estatus, ID_ISR = @ISR
                    WHERE ID_EMPLEADO = @ID_EMPLEADO";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@IMSS", empleado.Imss);
                comando.Parameters.AddWithValue("@CURP", empleado.Curp);
                comando.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                comando.Parameters.AddWithValue("@F_Nacimiento", empleado.FechaNacimiento);
                comando.Parameters.AddWithValue("@Correo", empleado.Correo);
                comando.Parameters.AddWithValue("@Genero", empleado.Genero);
                comando.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                comando.Parameters.AddWithValue("@RFC", empleado.Rfc);
                comando.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                comando.Parameters.AddWithValue("@Salario_Diario", empleado.SalarioDiario);
                comando.Parameters.AddWithValue("@Sueldo_Mensual", empleado.SueldoMensual);
                comando.Parameters.AddWithValue("@Salario_Diario_Integrado", empleado.SalarioDiarioIntegrado);
                comando.Parameters.AddWithValue("@Antiguedad", empleado.Antiguedad);
                comando.Parameters.AddWithValue("@Fecha_de_Ingreso", empleado.FechaDeIngreso);
                comando.Parameters.AddWithValue("@ID_PUESTO", empleado.IdPuesto);
                comando.Parameters.AddWithValue("@ID_DEP", empleado.IdDep);
                comando.Parameters.AddWithValue("@ID_TURNO", empleado.IdTurno);
                comando.Parameters.AddWithValue("@ID_EMPLEADO", empleado.IdEmpleado);
                comando.Parameters.AddWithValue("@Estatus", empleado.Estatus);
                comando.Parameters.AddWithValue("@ISR", empleado.ID_ISR);

                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }
        // Método para borrar un empleado
        public static int BajaEmpleado(int idEmpleado)
        {
            int resultado = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"UPDATE empleados
                                SET Estatus = @Estatus
                                WHERE ID_EMPLEADO = @ID_EMPLEADO";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Estatus", false);
                comando.Parameters.AddWithValue("@ID_EMPLEADO", idEmpleado);

                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }
        //Metodo para buscar Empleado por ID
        public static Empleado ObtenerEmpleadoPorId(int idEmpleado)
        {
            Empleado empleado = null;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT * FROM empleados WHERE ID_EMPLEADO = @ID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID", idEmpleado);

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    empleado = new Empleado
                    {
                        IdEmpleado = reader.GetInt32(0),
                        Imss = reader.GetInt64(1),
                        Curp = reader.GetString(2),
                        Nombre = reader.GetString(3),
                        FechaNacimiento = reader.GetDateTime(4),
                        Correo = reader.GetString(5),
                        Genero = reader.GetString(6),
                        Telefono = reader.GetInt64(7),
                        Rfc = reader.GetString(8),
                        Direccion = reader.GetString(9),
                        SalarioDiario = reader.GetDecimal(10),
                        SueldoMensual = reader.GetDecimal(11),
                        SalarioDiarioIntegrado = reader.GetDecimal(12),
                        Antiguedad = reader.GetInt32(13),
                        FechaDeIngreso = reader.GetDateTime(14),
                        IdPuesto = reader.GetInt32(15),
                        IdDep = reader.GetInt32(16),
                        IdTurno = reader.GetInt32(17),
                        Estatus = reader.GetBoolean(18),
                        ID_ISR = reader.GetInt32(19)
                    };
                }
            }

            return empleado;
        }
        public static bool EsDatoUnico(long imss, string curp, long telefono, string rfc)
        {
            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"
                SELECT COUNT(*)
                FROM empleados
                WHERE IMSS = @IMSS OR CURP = @CURP OR Telefono = @Telefono OR RFC = @RFC";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@IMSS", imss);
                comando.Parameters.AddWithValue("@CURP", curp);
                comando.Parameters.AddWithValue("@Telefono", telefono);
                comando.Parameters.AddWithValue("@RFC", rfc);

                int count = (int)comando.ExecuteScalar();
                return count == 0; // Devuelve true si no hay coincidencias, false si hay al menos una.
            }
        }
        public static bool EsDatoUnico(long imss, string curp, long telefono, string rfc, int idEmpleado)
        {
            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"
            SELECT COUNT(*)
            FROM empleados
            WHERE (IMSS = @IMSS OR CURP = @CURP OR Telefono = @Telefono OR RFC = @RFC)
            AND ID_EMPLEADO != @ID_EMPLEADO";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@IMSS", imss);
                comando.Parameters.AddWithValue("@CURP", curp);
                comando.Parameters.AddWithValue("@Telefono", telefono);
                comando.Parameters.AddWithValue("@RFC", rfc);
                comando.Parameters.AddWithValue("@ID_EMPLEADO", idEmpleado);

                int count = (int)comando.ExecuteScalar();
                return count == 0; // Devuelve true si no hay coincidencias (único), false si hay duplicados.
            }
        }
        public static int ObtIdPorCURP(string curp)
        {
            int idEmpleado = -1; // Valor por defecto en caso de no encontrar el empleado.

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"
                    SELECT ID_EMPLEADO
                    FROM empleados
                    WHERE CURP = @CURP";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@CURP", curp);

                object resultado = comando.ExecuteScalar();

                if (resultado != null)
                {
                    idEmpleado = Convert.ToInt32(resultado); // Convierte el resultado al tipo int.
                }
            }

            return idEmpleado; // Devuelve -1 si no encuentra el empleado.
        }
        public static int ReingresoEmpleado(Empleado empleado)
        {
            int resultado = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"
                    UPDATE empleados 
                    SET  Fecha_de_Ingreso = @Fecha_de_Ingreso,
                         Estatus = @Estatus
                    WHERE ID_EMPLEADO = @ID_EMPLEADO";

                SqlCommand comando = new SqlCommand(query, conexion);                
                comando.Parameters.AddWithValue("@Fecha_de_Ingreso", empleado.FechaDeIngreso);
                comando.Parameters.AddWithValue("@Estatus", true);
                comando.Parameters.AddWithValue("@ID_EMPLEADO", empleado.IdEmpleado);
                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }
        public static List<Empleado> ObtEmpPorIdPuesto(int ID)
        {
            List<Empleado> listaEmpleados = new List<Empleado>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT * FROM empleados WHERE ID_PUESTO = @ID_PUESTO;";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_PUESTO", ID);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Empleado empleado = new Empleado
                    {
                        IdEmpleado = reader.GetInt32(0),
                        Imss = reader.GetInt64(1),
                        Curp = reader.GetString(2),
                        Nombre = reader.GetString(3),
                        FechaNacimiento = reader.GetDateTime(4),
                        Correo = reader.GetString(5),
                        Genero = reader.GetString(6),
                        Telefono = reader.GetInt64(7),
                        Rfc = reader.GetString(8),
                        Direccion = reader.GetString(9),
                        SalarioDiario = reader.GetDecimal(10),
                        SueldoMensual = reader.GetDecimal(11),
                        SalarioDiarioIntegrado = reader.GetDecimal(12),
                        Antiguedad = reader.GetInt32(13),
                        FechaDeIngreso = reader.GetDateTime(14),
                        IdPuesto = reader.GetInt32(15),
                        IdDep = reader.GetInt32(16),
                        IdTurno = reader.GetInt32(17),
                        Estatus = reader.GetBoolean(18),
                        ID_ISR = reader.GetInt32(19),
                    };

                    listaEmpleados.Add(empleado);
                }
            }

            return listaEmpleados;
        }

    }
}
