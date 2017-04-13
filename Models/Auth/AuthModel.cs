using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Auth
{
    public class AuthModel
    {
        public int id { get; set; }
        public User user { get; set; }
        public Rol rol { get; set; }
    }
}