using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Log
    {
        public int id { get; set; }
        public string message { get; set; }
        public string source { get; set; }
        public DateTime timestamp { get; set; }
    }
}