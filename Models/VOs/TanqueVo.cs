﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class TanqueVo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public float capacidad { get; set; }
        public float litros { get; set; }
        public int pipa_id { get; set; }
        public int combustible_id { get; set; }
    }
}