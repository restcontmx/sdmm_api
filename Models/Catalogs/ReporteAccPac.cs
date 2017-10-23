using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class ReporteAccPac
    {
        public Vale vale { get; set; }
        public IList<DetalleVale> registros { get; set; }
    }
}