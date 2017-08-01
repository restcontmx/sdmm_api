using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Vale
    {
        public int id { get; set; }
        public Compania compania { get; set; }

        public int turno { get; set; }

        public User user { get; set; }
        public User userAutorizo { get; set; }
        public Empleado polvorero { get; set; }
        public Empleado cargador1 { get; set; }
        public Empleado cargador2 { get; set; }
        public SubNivel subnivel { get; set; }

        public int active { get; set; }

        public IList<DetalleVale> detalles { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }

    }
}