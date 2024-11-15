using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.Tablas
{
    internal class Empresa
    {
        public int IdEmpresa { get; set; }
        public string RepLegal { get; set; }
        public string Ra_S { get; set; }
        public string Direccion { get; set; }
        public string Re_Fiscal { get; set; }
        public string RFC { get; set; }
        public int Id_RP { get; set; }

        public Empresa() { }

        public Empresa(int idEmpresa, string repLegal, string ra_S, string direccion, string re_Fiscal,
                       string rFC, int id_RP)
        {
            IdEmpresa = idEmpresa;
            RepLegal = repLegal;
            Ra_S = ra_S;
            Direccion = direccion;
            Re_Fiscal = re_Fiscal;
            RFC = rFC;
            Id_RP = id_RP;
        }
    }
}
