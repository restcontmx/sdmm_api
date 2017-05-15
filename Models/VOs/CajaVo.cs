using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    /// <summary>
    /// Caja Void object
    /// </summary>
    public class CajaVo
    {
        public int id { get; set; }

        public string codigo { get; set; }
        public string folio_ini { get; set; }
        public string folio_fin { get; set; }
        public int cantidad { get; set; }
        public bool active { get; set; }

        public int producto_id { get; set; }
        public int user_id { get; set; }
    }
}