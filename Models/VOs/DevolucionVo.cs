﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class DevolucionVo
    {
        public int id { get; set; }

        public int compania_id { get; set; }

        public string folio { get; set; }

        public int tipo { get; set; }

        public int user_id { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }
    }
}