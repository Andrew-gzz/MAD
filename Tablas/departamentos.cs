using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.Tablas
{
    internal class departamentos
    {
        public int IdDep { get; set; }
        public string Departamento { get; set; }

        public departamentos() { }

        public departamentos(int idDep, string departamento)
        {
            IdDep = idDep;
            Departamento = departamento;
        }
    }
}

