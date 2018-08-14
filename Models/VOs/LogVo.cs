using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class LogVo
    {
        public int id { get; set; }
        public string message { get; set; }
        public string source { get; set; }
        public string timestamp { get; set; }
        
    }
}