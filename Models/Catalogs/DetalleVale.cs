using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class DetalleVale
    {
        public int id { get; set; }
        public Producto producto { get; set; }
        public int cantidad { get; set; }
        public Vale vale { get; set; }

        public IList<RegistroDetalle> registros { get; set; }
    }
}