using Models.Auth;
using System;
using System.Collections.Generic;

namespace Models.Catalogs
{
    public class BitacoraDesarrollo
    {
        public int id { get; set; }

        public Maquinaria maquinaria { get; set; }
        public DateTime fecha_bitacora { get; set; }
        public string grupo { get; set; }
        public int turno { get; set; }
        public Compania compania { get; set; }
        public string vale_acero { get; set; }
        public string vale_explosivos { get; set; }
        public SubNivel subnivel { get; set; }
        public string zona { get; set; }
        public TipoDesarrollo tipo_desarrollo{ get; set; }
        public DateTime hora_primer_barreno { get; set; }
        public DateTime hora_ultimo_barreno { get; set; }
        public int numero_barrenos { get; set; }
        //public int profundidad_metros { get; set; }
        public int anclas { get; set; }
        public int mallas { get; set; }
        public Operador operador { get; set; }
        public Operador ayudante { get; set; }

        public IList<DetalleDemoraBitacora> demoras { get; set; }

        public string comentarios { get; set; }
        public int status_edicion { get; set; }
        public int dias_apertura_calendario { get; set; }

        public User user { get; set; }

        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}