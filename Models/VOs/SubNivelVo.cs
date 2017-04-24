using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class SubNivelVo
    {
        public int id { get; set; }
        public int nivel_id { get; set; }
        public string nombre { get; set; }
        public int status { get; set; }
        public int user_id { get; set; }
    }
}