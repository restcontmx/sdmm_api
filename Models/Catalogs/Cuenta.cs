﻿using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Catalogs
{
    public class Cuenta
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string num_categoria { get; set; }
        public string numero { get; set; }
        public User user { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime updated { get; set; }

        //Se usa solamente para el sistema de combustibles
        public TipoProducto tipo_producto;
    }
}