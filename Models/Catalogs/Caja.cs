using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Caja
    {
        public int id { get; set; }

        public string codigo { get; set; }
        public string folio_ini { get; set; }
        public string folio_fin { get; set; }
        public int cantidad { get; set; }
        public bool active { get; set; }

        public Producto producto { get; set; }
        public User user { get; set; }

        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}