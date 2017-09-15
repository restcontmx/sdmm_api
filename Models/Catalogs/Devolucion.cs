using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Devolucion
    {
        public int id { get; set; }

        public Compania compania { get; set; }

        public string motivo { get; set; }
        public int turno { get; set; }
        public Vale vale { get; set; }

        public IList<RegistroDetalleDev> registros { get; set; }
        public IList<DetalleVale> detalles { get; set; }

        public User user { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}