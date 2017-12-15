using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Tanque
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int capacidad { get; set; }
        public int litros { get; set; }
        public Pipa pipa { get; set; }
        public Combustible combustible { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}