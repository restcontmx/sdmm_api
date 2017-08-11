using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class RegistroDetalleOverVo
    {
        public int id { get; set; }
        public string folio { get; set; }
        public string folio_caja { get; set; }
        public int detallevale_id { get; set; }
        public int turno { get; set; }
        public int producto_id { get; set; }
        public int vale_id { get; set; }
    }
}