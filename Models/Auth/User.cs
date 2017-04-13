using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Auth
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public string email { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }

    }
}