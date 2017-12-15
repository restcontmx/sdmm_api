using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class FichaEntregaRecepcion
    {
        public int id { get; set; }
        public User despachador_entrega { get; set; }
        public User despachador_recibe { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }

        public IList<DetalleFichaEntregaRecepcion> detalles { get; set; }
    }
}