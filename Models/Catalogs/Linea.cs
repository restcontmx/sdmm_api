using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Linea
    {
        public int id { get; set; }
        public string numero { get; set; }
        public Vale vale { get; set; }
        public SubNivel subnivel { get; set; }
        public int tipo { get; set; }

        public BitacoraBarrenacion bitacora { get; set; }

        public IList<Barreno> barrenos { get; set; }
    }
}