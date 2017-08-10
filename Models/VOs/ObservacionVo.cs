using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class ObservacionVo
    {
        public int id { get; set; }

        public string comentarios { get; set; }

        public int caja_id { get; set; }
        public string folio_caja { get; set; }

        public int user_id { get; set; }

        public string timestamp { get; set; }
        public string updated { get; set; }
    }
}