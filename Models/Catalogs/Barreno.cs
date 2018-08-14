using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Barreno
    {
        public int id { get; set; }
        public double cantidad { get; set; }
        public double longitud { get; set; }
        public double metros { get; set; }
        public Linea linea { get; set; }

        public BitacoraBarrenacion bitacora { get; set; }
    }
}