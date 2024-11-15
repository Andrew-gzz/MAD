using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.Tablas
{
    internal class RegistroPatronal
    {

        public int ID_RP { get; set; }
        public string Direccion { get; set; }
        public string Fraccion_I { get; set; }
        public string Fraccion_II { get; set; }
        public string Fraccion_III { get; set; }
        public string Fraccion_IV { get; set; }
        public string Fraccion_V { get; set; }
        public string Riesgo { get; set; }
        public string Sellos { get; set; }

        public RegistroPatronal() { }

        public RegistroPatronal(int iD_RP, string direccion, string fraccion_I
            , string fraccion_II, string fraccion_III, string fraccion_IV, string fraccion_V
            , string riesgo, string sellos)
        {
            ID_RP = iD_RP;
            Direccion = direccion;
            Fraccion_I = fraccion_I;
            Fraccion_II = fraccion_II;
            Fraccion_III = fraccion_III;
            Fraccion_IV = fraccion_IV;
            Fraccion_V = fraccion_V;
            Riesgo = riesgo;
            Sellos = sellos;
        }
    }
}
