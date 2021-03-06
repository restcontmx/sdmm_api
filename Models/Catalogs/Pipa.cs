﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Pipa
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string no_economico { get; set; }
        public string placas { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }

        public IList<Tanque> tanques { get; set; }
    }
}