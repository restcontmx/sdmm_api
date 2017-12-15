using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Operador
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public Compania compania { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}