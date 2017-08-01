using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class DevolucionVo
    {
        public int id { get; set; }

        public int compania_id { get; set; }

        public string motivo { get; set; }
        public int turno { get; set; }
        public int vale_id { get; set; } 

        public IList<RegistroDetalleDevVo> registros { get; set; }

        public int user_id { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }
    }
}