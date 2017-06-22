using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Observacion
    {
        public int id { get; set; }

        public string comentarios { get; set; }

        public Caja caja { get; set; }
        public User user { get; set; }

        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}