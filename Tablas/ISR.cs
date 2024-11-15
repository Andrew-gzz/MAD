using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.Tablas
{
    internal class ISR
    {
        public int ID_ISR { get; set; }
        public decimal L_Inferior { get; set; }
        public decimal Cuota { get; set; }
        public decimal Porcentaje { get; set; }
        public DateTime Year { get; set; }

        public ISR() { }

        public ISR(int iD_ISR, decimal l_Inferior, decimal cuota, decimal porcentaje, DateTime year)
        {
            ID_ISR = iD_ISR;
            L_Inferior = l_Inferior;
            Cuota = cuota;
            Porcentaje = porcentaje;
            Year = year;
        }

    }
}
