using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.Tablas
{
    internal class Periodo
    {
        public int IdPeriodo { get; set; }
        public DateTime FInicial { get; set; }
        public DateTime FFin { get; set; }

        public Periodo() { }

        public Periodo(int idPeriodo, DateTime fInicial, DateTime fFin)
        {
            this.IdPeriodo = idPeriodo;
            this.FInicial = fInicial;
            this.FFin = fFin;
        }
    }
}

