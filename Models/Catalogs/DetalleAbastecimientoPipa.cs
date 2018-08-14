using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class DetalleAbastecimientoPipa
    {
        public int id { get; set; }
        public string foto_recibo { get; set; }
        public float litros { get; set; }
        public Tanque tanque { get; set; }
        public Pipa pipa { get; set; }
        public AbastecimientoPipa abastecimiento { get; set; }
    }
}