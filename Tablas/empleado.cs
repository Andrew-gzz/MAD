using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.Tablas
{
    internal class Empleado
    {
        public int IdEmpleado { get; set; }
        public long Imss { get; set; }
        public string Curp { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public string Genero { get; set; }
        public long Telefono { get; set; }
        public string Rfc { get; set; }
        public string Direccion { get; set; }
        public decimal SalarioDiario { get; set; }
        public decimal SueldoMensual { get; set; }
        public decimal SalarioDiarioIntegrado { get; set; }
        public int Antiguedad { get; set; }
        public DateTime FechaDeIngreso { get; set; }
        public int IdPuesto { get; set; }
        public int IdDep { get; set; }
        public int IdTurno { get; set; }
        public bool Estatus {  get; set; }
        public int ID_ISR{ get; set; }

        // Constructor vacío
        public Empleado() { }

        // Constructor con parámetros
        public Empleado(int idEmpleado, long imss, string curp, string nombre, DateTime fechaNacimiento, string correo, string genero,
                        long telefono, string rfc, string direccion, decimal salarioDiario, decimal sueldoMensual,
                        decimal salarioDiarioIntegrado, int antiguedad, DateTime fechaDeIngreso, int idPuesto, int idDep, int idTurno, bool estatus, int iD_ISR)
        {
            IdEmpleado = idEmpleado;
            Imss = imss;
            Curp = curp;
            Nombre = nombre;
            FechaNacimiento = fechaNacimiento;
            Correo = correo;
            Genero = genero;
            Telefono = telefono;
            Rfc = rfc;
            Direccion = direccion;
            SalarioDiario = salarioDiario;
            SueldoMensual = sueldoMensual;
            SalarioDiarioIntegrado = salarioDiarioIntegrado;
            Antiguedad = antiguedad;
            FechaDeIngreso = fechaDeIngreso;
            IdPuesto = idPuesto;
            IdDep = idDep;
            IdTurno = idTurno;
            Estatus = estatus;
            ID_ISR = iD_ISR;
        }
    }
}

