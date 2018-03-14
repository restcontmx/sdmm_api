using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Combustible
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string unidad { get; set; }
        public string codigo { get; set; }
        public TipoProducto tipo_producto { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}