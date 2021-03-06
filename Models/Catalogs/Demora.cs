﻿using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Demora
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public bool status { get; set; }
        public User user { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}