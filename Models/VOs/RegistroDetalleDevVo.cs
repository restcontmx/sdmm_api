using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class RegistroDetalleDevVo
    {
        public int id { get; set; }
        public string folio { get; set; }
        public int tipodev { get; set; }
        public string observaciones { get; set; }

        public int producto_id { get; set; }
        public int devolucion_id { get; set; }
        public int user_id { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }
    }
}