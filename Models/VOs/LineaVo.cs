using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class LineaVo
    {
        public int id { get; set; }
        public string numero { get; set; }
        public int vale_id { get; set; }
        public int subnivel_id { get; set; }
        public int tipo { get; set; }

        public int bitacora_id { get; set; }

        public IList<BarrenoVo> barrenos { get; set; }
    }
}