using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class DetalleSalidaCombustible
    {
        public int litros_surtidos { get; set; }
        public SalidaCombustible salida_combustible { get; set; }
        public Tanque tanque { get; set; }
        public Pipa pipa { get; set; }
        public Combustible combustible { get; set; }

    }
}