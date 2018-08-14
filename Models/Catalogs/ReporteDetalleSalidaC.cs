using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class ReporteDetalleSalidaC
    {
        public SalidaCombustible salidacombustible { get; set; }
        public Cuenta cuenta { get; set; }
        public DetalleSalidaCombustible detallesalida { get; set; }
        public Pipa pipa { get; set; }
    }
}