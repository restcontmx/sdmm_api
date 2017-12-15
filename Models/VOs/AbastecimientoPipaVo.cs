using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class AbastecimientoPipaVo
    {
        public int id { get; set; }
        public int pipa_id { get; set; }
        public int despachador_id { get; set; }

        public IList<DetalleAbastecimientoPipaVo> detalles { get; set; }
    }
}