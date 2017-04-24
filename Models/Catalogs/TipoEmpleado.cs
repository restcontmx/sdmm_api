using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class TipoEmpleado
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int value { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }

    }
}