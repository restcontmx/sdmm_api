using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class PipaVo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string no_economico { get; set; }
        public string placas { get; set; }

        public IList<TanqueVo> tanques { get; set; }
    }
}