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

        public TipoEmpleado tipo_empleado { get; set; }
        public string nombre { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public Compania compania { get; set; }

        public bool status { get; set; }

        public User user { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }

    }
}