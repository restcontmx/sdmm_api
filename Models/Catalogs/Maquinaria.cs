using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Maquinaria
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public Cuenta cuenta { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }

        public IList<DetalleConsumoMaquinaria> detalles { get; set; }
    }
}