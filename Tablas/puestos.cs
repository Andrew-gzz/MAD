using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.Tablas
{
    internal class puestos
    {
        public int IdPuesto { get; set; }
        public string Puesto { get; set; }

        public puestos() { }

        public puestos(int idPuesto, string puesto)
        {
            IdPuesto = idPuesto;
            Puesto = puesto;
        }
    }
}

