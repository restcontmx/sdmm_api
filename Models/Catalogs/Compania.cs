using Models.Auth;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Compania
    {
        public int id { get; set; }

        public string razon_social { get; set; }
        public string nombre_sistema { get; set; }
        public Cuenta cuenta { get; set; }
        public Categoria categoria { get; set; }
        public User user { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}