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

        public Proveedor proveedor { get; set; }

        public string folio { get; set; }

        public int tipo { get; set; }


        public User user { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}