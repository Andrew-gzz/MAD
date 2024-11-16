using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.Tablas
{
    internal class Vacaciones
    {
        public int Antiguedad { get; set; }
        public int DiasVacaciones { get; set; }

        public Vacaciones() { }

        public Vacaciones(int antiguedad, int diasVacaciones)
        {
            Antiguedad = antiguedad;
            DiasVacaciones = diasVacaciones;
        }
    }
}
