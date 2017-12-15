using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class DetalleConsumoMaquinariaVo
    {
        public int id { get; set; }
        public int combustible_id { get; set; }
        public int maquinaria_id { get; set; }
        public float promedio { get; set; }
        public float tolerancia { get; set; }
    }
}