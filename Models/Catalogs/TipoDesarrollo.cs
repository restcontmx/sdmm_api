﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class TipoDesarrollo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}