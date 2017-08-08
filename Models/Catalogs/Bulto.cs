using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Bulto
    {
        public int id { get; set; }

        public string codigo { get; set; }

        public bool active { get; set; }

        public Producto producto { get; set; }
        public User user { get; set; }

        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}