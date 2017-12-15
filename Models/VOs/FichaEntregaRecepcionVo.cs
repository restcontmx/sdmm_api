using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class FichaEntregaRecepcionVo
    {
        public int id { get; set; }
        public int despachador_entrega_id { get; set; }
        public int despachador_recibe_id { get; set; }

        public IList<DetalleFichaEntregaRecepcionVo> detalles { get; set; }
    }
}