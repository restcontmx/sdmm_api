using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class BitacoraBarrenacionVo
    {
        public int id { get; set; }

        public int maquinaria_id { get; set; }
        public int operador_id { get; set; }
        public int ayudante_id { get; set; }
        public int turno { get; set; }
        public string fecha_bitacora { get; set; }
        public string mesa { get; set; }
        public string beta { get; set; }
        public string comentarios { get; set; }

        public double metros_finales { get; set; }
        public string vale_acero { get; set; }
        public string hora_primer_barreno { get; set; }
        public string hora_ultimo_barreno { get; set; }

        public IList<LineaVo> lineas { get; set; }
        public IList<DetalleDemoraBitacoraVo> demoras { get; set; }

        public int status_edicion { get; set; }
        public int dias_apertura_calendario { get; set; }

        public int user_id { get; set; }

        public string timestamp { get; set; }
        public string updated { get; set; }
    }
}