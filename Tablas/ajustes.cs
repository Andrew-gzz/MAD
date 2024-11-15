using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.Tablas
{
    internal class Ajuste
    {
        public int IdAjuste { get; set; }
        public string Motivo { get; set; }
        public string Tipo { get; set; }
        public decimal Porcentaje { get; set; }
        public Ajuste() { }

        public Ajuste(int idAjuste, string motivo, string tipo, decimal porcentaje)
        {
            IdAjuste = idAjuste;
            Motivo = motivo;
            Tipo = tipo;
            Porcentaje = porcentaje;
        }
    }
}


