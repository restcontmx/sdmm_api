using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class DetalleDemoraBitacoraVo
    {
        public int id { get; set; }
        public int bitacora_desarrollo_id { get; set; }
        public int bitacora_barrenacion_id { get; set; }
        public int tipo_bitacora { get; set; }
        public int demora_id { get; set; }
        public string comentarios { get; set; }
        public string horas_perdidas { get; set; }

    }
}