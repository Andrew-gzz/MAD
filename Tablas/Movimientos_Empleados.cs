using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.Tablas
{
    internal class Movimientos_Empleados
    {
        public int ID_MOVIMIENTO { get; set; }
        public int ID_EMPLEADO { get; set; }
        public DateTime F_ALTA { get; set; }
        public DateTime F_BAJA { get; set; }
        public int ID_PERIODO_ALTA { get; set; }
        public int ID_PERIODO_BAJA{ get; set; }

        public Movimientos_Empleados() { }

        public Movimientos_Empleados(int iD_MOVIMIENTO, int iD_EMPLEADO, DateTime f_ALTA, DateTime f_BAJA, int iD_PERIODO_ALTA, int iD_PERIODO_BAJA)
        {
            ID_MOVIMIENTO = iD_MOVIMIENTO;
            ID_EMPLEADO = iD_EMPLEADO;
            F_ALTA = f_ALTA;
            F_BAJA = f_BAJA;
            ID_PERIODO_ALTA = iD_PERIODO_ALTA; 
            ID_PERIODO_BAJA = iD_PERIODO_BAJA;
        }
    }
}
