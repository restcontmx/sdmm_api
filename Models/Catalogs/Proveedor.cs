using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Proveedor
    {
        public int id { get; set; }
        public string razon_social { get; set; }
        public string nombre_comercial { get; set; }
        public string rfc { get; set; }
        public string codigo_proveedor { get; set; }
        public string permiso_sedena { get; set; }
        public string calle { get; set; }
        public int no_ext { get; set; }
        public int no_int { get; set; }
        public string colonia { get; set; }
        public int cp { get; set; }
        public string localidad { get; set; }
        public string ciudad { get; set; }
        public string estado { get; set; }
        public string folio { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
        public User user { get; set; }
    }
}