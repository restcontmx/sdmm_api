using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class SalidaCombustibleVo
    {
        public int id { get; set; }
        public int odometro { get; set; }
        public string foto { get; set; }
        public int maquinaria_id { get; set; }
        public int compania_id { get; set; }
        public int operador_id { get; set; }
        public int subnivel_id { get; set; }
        public int despachador_id { get; set; }

        public IList<DetalleSalidaCombustibleVo> detalles { get; set; }
    }
}