using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class BarrenoVo
    {
        public int id { get; set; }
        public double cantidad { get; set; }
        public double longitud { get; set; }
        public double metros { get; set; }
        public int linea_id { get; set; }

        public int bitacora_id { get; set; }
    }
}