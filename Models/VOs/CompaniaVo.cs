using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class CompaniaVo
    {
        public int id { get; set; }

        public string razon_social { get; set; }
        public string nombre_sistema { get; set; }
        public int cuenta_propia { get; set; }
        public string cuenta_id { get; set; }
        public int user_id { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }
    }
}