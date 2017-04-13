using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class UserVo
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public string email { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }

    }
}