using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class MaquinariaVo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int cuenta_id { get; set; }
        public string timestamp { get; set; }
        public string updated { get; set; }

        public IList<DetalleConsumoMaquinariaVo> detalles { get; set; }
    }
}