using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class RegistroDetalleVo
    {
        public int id { get; set; }

        public int detallevale_id { get; set; }
        public string folioCaja { get; set; }
        public string folio { get; set; }
        public int turno { get; set; }
        public int status { get; set; }

        public int producto_id { get; set; }
        public int vale_id { get; set; }
        public int user_id { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }
    }
}