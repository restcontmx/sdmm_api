using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class InventarioVo
    {
        public int id { get; set; }
        public int producto_id { get; set; }
        public int cantidad { get; set; }
        public int reservado { get; set; }
        public string updated { get; set; }
        public int cantidad_cajas { get; set; }
    }
}