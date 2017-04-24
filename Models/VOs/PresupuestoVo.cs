using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class PresupuestoVo
    {
        public int id { get; set; }
        public int producto_id { get; set; }
        public int year { get; set; }
        public int stock { get; set; }
        public decimal presupuesto { get; set; }
        public int user_id { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }
    }
}