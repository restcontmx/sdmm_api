using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Presupuesto
    {
        public int id { get; set; }
        public Producto producto { get; set; }
        public int year { get; set; }
        public int stock { get; set; }
        public decimal presupuesto { get; set; }
        public User user { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }

    }
}