using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class ProductoVo
    {
        public int id { get; set; }
        public int tipoproducto_id { get; set; }
        public string presentacion { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public int cantidad { get; set; }
        public decimal costo { get; set; }
        public decimal peso { get; set; }
        public string modo { get; set; }
        public int user_id { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }

    }
}