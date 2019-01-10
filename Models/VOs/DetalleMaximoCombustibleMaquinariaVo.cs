using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class DetalleMaximoCombustibleMaquinariaVo
    {
        public int id { get; set; }
        public int combustible_id { get; set; }
        public int maquinaria_id { get; set; }
        public float litros_maximo { get; set; }
        public string timestamp { get; set; }
        public int user_id { get; set; }
    }
}