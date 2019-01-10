using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Auth;

namespace Models.Catalogs
{
    public class DetalleMaximoCombustibleMaquinaria
    {
        public int id { get; set; }
        public Combustible combustible { get; set; }
        public Maquinaria maquinaria { get; set; }
        public float litros_maximo { get; set; }
        public DateTime timestamp { get; set; }
        public User user { get; set; }
    }
}