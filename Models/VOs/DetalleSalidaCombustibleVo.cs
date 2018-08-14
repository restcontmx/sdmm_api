using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class DetalleSalidaCombustibleVo
    {
        public float litros_surtidos { get; set; }
        public int salida_combustible_id { get; set; }
        public int tanque_id { get; set; }
        public int pipa_id { get; set; }
        public int combustible_id { get; set; }
    }
}