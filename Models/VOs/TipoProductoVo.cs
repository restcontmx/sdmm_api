﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class TipoProductoVo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int value { get; set; }
    }
}