using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class DetalleFichaEntregaRecepcion
    {
        public int litros { get; set; }
        public Tanque tanque { get; set; }
        public Pipa pipa { get; set; }
        public FichaEntregaRecepcion ficha { get; set; }
    }
}