using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class EmpleadoVo
    {
        public int id { get; set; }

        public int tipoempleado_id { get; set; }
        public string nombre { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public int compania_id { get; set; }
        public int status { get; set; }


        public int user_id { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }
    }
}