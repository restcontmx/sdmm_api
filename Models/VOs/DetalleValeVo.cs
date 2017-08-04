using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class DetalleValeVo
    {
        public int id { get; set; }
        public int producto_id { get; set; }
        public int vale_id { get; set; }
        public int cantidad { get; set; }

        public IList<RegistroDetalleVo> registros { get; set; }
    }
}