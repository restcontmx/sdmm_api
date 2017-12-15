using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class AbastecimientoPipa
    {
        public int id { get; set; }
        public Pipa pipa { get; set; }
        public User despachador { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }

        public IList<DetalleAbastecimientoPipa> detalles { get; set; }
    }
}