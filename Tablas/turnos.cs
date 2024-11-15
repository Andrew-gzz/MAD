using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.Tablas
{
    internal class Turnos
    {
        public int IdTurno { get; set; }
        public string Tipo { get; set; }
        public TimeSpan Horario { get; set; }

        public Turnos() { }

        public Turnos(int idTurno, string tipo, TimeSpan horario)
        {
            IdTurno = idTurno;
            Tipo = tipo;
            Horario = horario;
        }
    }
}
