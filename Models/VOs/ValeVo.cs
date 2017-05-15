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

        public string turno { get; set; }
        public string lugar { get; set; }

        public int user_id { get; set; }
        public int polvorero_id { get; set; }
        public int cargador1_id { get; set; }
        public int cargador2_id { get; set; }
        public IList<DetalleValeVo> detalles { get; set; }
        public int cuenta_id { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }
    }
}