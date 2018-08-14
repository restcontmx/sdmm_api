using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class DetalleDemoraBitacora
    {
        public int id { get; set; }
        public BitacoraDesarrollo bitacora_desarrollo { get; set; }
        public BitacoraBarrenacion bitacora_barrenacion { get; set; }
        public int tipo_bitacora { get; set; }
        public Demora demora { get; set; }
        public string comentarios { get; set; }
        public DateTime horas_perdidas { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}