using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class DetalleConsumoMaquinaria
    {
        public int id { get; set; }
        public Combustible combustible { get; set; }
        public Maquinaria maquinaria { get; set; }
        public float promedio { get; set; }
        public float tolerancia { get; set; }
    }
}