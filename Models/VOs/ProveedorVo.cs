using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class ProveedorVo
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
        public string timestamp { get; set; }
        public string updated { get; set; }
        public int user_id { get; set; }
    }
}