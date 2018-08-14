using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class TipoDesarrolloVo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string description { get; set; }
        public int status { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }
    }
}