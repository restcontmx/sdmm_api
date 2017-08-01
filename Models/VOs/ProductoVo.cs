using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class ProductoVo
    {
        public int id { get; set; }

        public string nombre { get; set; }
        public string codigo { get; set; }
        public decimal costo { get; set; }
        public decimal peso { get; set; }
        public int revision { get; set; }
        public int proveedor_id { get; set; }
        public int segmentoproducto_id { get; set; }
        public int tipoproducto_id { get; set; }
        public int user_id { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }
        

    }
}