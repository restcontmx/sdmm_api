﻿using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class RegistroDetalle
    {
        public int id { get; set; }

        public DetalleVale detallevale { get; set; }

        public string folio { get; set; }
        public string folioCaja { get; set; }
        public int turno { get; set; }
        public int status { get; set; }

        public Producto producto { get; set;}
        public Vale vale { get; set; }
        public User user { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }
    }
}