using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class CombustibleVo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string unidad { get; set; }
        public string codigo { get; set; }
        public int tipoproducto_id { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }
    }

}