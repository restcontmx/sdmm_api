using Models.Auth;
using System;
using System.Collections.Generic;

namespace Models.Catalogs
{
    public class BitacoraBarrenacion
    {
        public int id { get; set; }

        public Maquinaria maquinaria { get; set; }
        public Operador operador { get; set; }
        public Operador ayudante { get; set; }
        public int turno { get; set; }
        public DateTime fecha_bitacora { get; set; }
        public string mesa { get; set; }
        public string beta { get; set; }
        public string comentarios { get; set; }

        public double metros_finales { get; set; }
        public string vale_acero { get; set; }
        public DateTime hora_primer_barreno { get; set; }
        public DateTime hora_ultimo_barreno { get; set; }

        public IList<Linea> lineas { get; set; }
        public IList<DetalleDemoraBitacora> demoras { get; set; }

        public int status_edicion { get; set; }
        public int dias_apertura_calendario { get; set; }

        public User user { get; set; }

        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}