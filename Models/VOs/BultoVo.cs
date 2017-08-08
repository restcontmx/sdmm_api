using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class BultoVo
    {
        public int id { get; set; }

        public string codigo { get; set; }
        public bool active { get; set; }
    
        public int producto_id { get; set; }
        public int user_id { get; set; }

    }
}