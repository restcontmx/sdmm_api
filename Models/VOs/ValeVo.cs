using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class ValeVo
    {
        public int id { get; set; }
        public int compania_id { get; set; }

        public int turno { get; set; }

        public int user_id { get; set; }
        public int user_id_autorizo { get; set; }
        public int polvorero_id { get; set; }
        public int cargador1_id { get; set; }
        public int cargador2_id { get; set; }
        public IList<DetalleValeVo> detalles { get; set; }
        public int subnivel_id { get; set; }
        public int active { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }

        public int autorizo { get; set; }
    }
}