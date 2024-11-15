using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.Tablas
{
    internal class AjustesEmpleadoPeriodo
    {
        public int IdAjuste { get; set; }
        public int IdEmp { get; set; }
        public int IdPeriodo { get; set; }
        public long? DiasHorasIMSS { get; set; }
        public AjustesEmpleadoPeriodo() { }

        public AjustesEmpleadoPeriodo(int idAjuste, int idEmp, int idPeriodo, long diasHorasIMSS)
        {
            this.IdAjuste = idAjuste;
            this.IdEmp = idEmp;
            this.IdPeriodo = idPeriodo;
            DiasHorasIMSS = diasHorasIMSS;  
        }
    }
}

