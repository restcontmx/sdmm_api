using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class DetalleFichaEntregaRecepcionVo
    {
        public float litros { get; set; }
        public int tanque_id { get; set; }
        public int pipa_id { get; set; }
        public int ficha_id { get; set; }
    }
}