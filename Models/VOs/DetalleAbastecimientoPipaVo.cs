using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class DetalleAbastecimientoPipaVo
    {
        public int id { get; set; }
        public string foto_recibo { get; set; }
        public int litros { get; set; }
        public int tanque_id { get; set; }
        public int pipa_id { get; set; }
        public int abastecimiento_id { get; set; }
    }
}