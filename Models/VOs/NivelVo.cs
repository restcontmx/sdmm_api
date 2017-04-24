using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class NivelVo
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public int user_id { get; set; }

    }
}