using Models.Auth;
using System;

namespace Models.Catalogs
{
    public class Producto
    {
        public int id { get; set; }
        public TipoProducto tipo_producto { get; set; }
        public string presentacion { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public int cantidad { get; set; }
        public decimal costo { get; set; }
        public decimal peso { get; set; }
        public string modo { get; set; }
        public User user { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}