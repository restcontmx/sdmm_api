using Models.Auth;
using System;
using System.Collections.Generic;

namespace Models.Catalogs
{
    public class ReporteJumbo
    {
        public int id { get; set; }

        public Maquinaria maquinaria { get; set; }
        public DateTime fecha_bitacora { get; set; }
        public int dia { get; set; }
        public int turno { get; set; }
        public Compania compania { get; set; }
        public SubNivel subnivel { get; set; }
        public string zona { get; set; }
        public TipoDesarrollo tipo_desarrollo { get; set; }
        public int numero_barrenos { get; set; }
        public int anclas { get; set; }
        public int mallas { get; set; }
        public Operador operador { get; set; }
        public Operador ayudante { get; set; }

        public IList<DetalleDemoraBitacora> demoras { get; set; }
        public string comentarios { get; set; }
        public User user { get; set; }

    }
}