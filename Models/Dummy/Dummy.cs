using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Dummy
{
    public class Dummy
    {
        public int dummy_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime date_start { get; set; }
        public DateTime date_end { get; set; }
    }
}