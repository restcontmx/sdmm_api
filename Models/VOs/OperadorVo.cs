using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class OperadorVo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public int compania_id { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }
    }
}