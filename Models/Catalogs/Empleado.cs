using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Empleado
    {
        public int id { get; set; }

        public string afiliacion { get; set; }
        public TipoEmpleado tipo_empleado { get; set; }
        public string nombre { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public string nss { get; set; }
        public string codigo { get; set; }
        public DateTime ingreso { get; set; }
        public DateTime salida { get; set; }
        public bool status { get; set; }

        public User user { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }

    }
}