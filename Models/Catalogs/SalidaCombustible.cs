using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class SalidaCombustible
    {
        public int id { get; set; }
        public int odometro { get; set; }
        public string foto { get; set; }
        public int turno { get; set; }
        public Maquinaria maquinaria { get; set; }
        public Compania compania { get; set; }
        public Operador operador { get; set; }
        public SubNivel subnivel { get; set; }
        public User despachador { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }

        public IList<DetalleSalidaCombustible> detalles { get; set; }
    }
}