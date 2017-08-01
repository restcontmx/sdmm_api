using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class RegistroDetalleDev
    {
        public int id { get; set; }
        public string folio { get; set; }
        public int tipodev { get; set; }
        public string observaciones { get; set; }

        public Devolucion devolucion { get; set; }
        public User user { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}