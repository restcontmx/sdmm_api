using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class BitacoraDesarrolloVo
    {
        public int id { get; set; }

        public int maquinaria_id { get; set; }
        public string fecha_bitacora { get; set; }
        public string grupo { get; set; }
        public int turno { get; set; }
        public int compania_id { get; set; }
        public string vale_acero { get; set; }
        public string vale_explosivos { get; set; }
        public int subnivel_id { get; set; }
        public string zona { get; set; }
        public int tipo_desarrollo_id { get; set; }
        public string hora_primer_barreno { get; set; }
        public string hora_ultimo_barreno { get; set; }
        public int numero_barrenos { get; set; }
        public int anclas { get; set; }
        public int mallas { get; set; }
        public int operador_id { get; set; }
        public int ayudante_id { get; set; }

        public IList<DetalleDemoraBitacoraVo> demoras { get; set; }

        public string comentarios { get; set; }
        public int status_edicion { get; set; }
        public int dias_apertura_calendario { get; set; }

        public int user_id { get; set; }

        public string timestamp { get; set; }
        public string updated { get; set; }
    }
}