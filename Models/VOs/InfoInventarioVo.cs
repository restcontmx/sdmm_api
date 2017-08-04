using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class InfoInventarioVo
    {
        public string nom_producto { get; set; }
        public string unidad { get; set; }
        public float existenciaInicialT1 { get; set; }
        public float entradasT1 { get; set; }
        public float salidasT1 { get; set; }
        public float devolucionesT1 { get; set; }
        public float existenciaInicialT2 { get; set; }
        public float salidasT2 { get; set; }
        public float devolucionesT2 { get; set; }
        public float reservado { get; set; }
        public float existenciaFinal { get; set; }
    }
}