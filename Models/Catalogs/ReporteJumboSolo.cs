using Models.Auth;
using System;
using System.Collections.Generic;

namespace Models.Catalogs
{
    public class ReporteJumboSolo
    {
        public int id { get; set; }

        public Maquinaria maquinaria { get; set; }
        public Operador operador { get; set; }
        public Operador ayudante { get; set; }
        public int turno { get; set; }
        public DateTime fecha_bitacora { get; set; }
        public int dia { get; set; }
        public string comentarios { get; set; }

        public IList<Linea> lineas { get; set; }
        public IList<DetalleDemoraBitacora> demoras { get; set; }

    }
}