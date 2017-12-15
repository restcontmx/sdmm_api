using Models.Auth;
using System;

namespace Models.Catalogs
{
    public class Producto
    {
        public int id { get; set; }
        
        public string nombre { get; set; }
        public string codigo { get; set; }
        public decimal costo { get; set; }
        public decimal peso { get; set; }
        public int revision { get; set; }
        public int cantidad_caja_promedio { get; set; }
        public int rango_caja_cierre { get; set; }
        public Proveedor proveedor { get; set; }
        public SegmentoProducto segmento { get; set; }
        public TipoProducto tipo_producto { get; set; }
        public User user { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
        
    }
}