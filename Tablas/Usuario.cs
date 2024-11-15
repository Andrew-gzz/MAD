using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.Tablas
{
    internal class Usuario
    {
        public string IdUser { get; set; }
        public string Pass { get; set; }

        public Usuario() { }

        public Usuario(string idUser, string pass)
        {
            this.IdUser = idUser;
            this.Pass = pass;
        }
    }
}
