using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Inventario
    {
        public int id { get; set; }
        public Producto producto { get; set; }
        public int cantidad { get; set; }
        public int turno { get; set; }
        public DateTime updated { get; set; }
        public int cantidad_cajas { get; set; }
    }
}