using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.VOs
{
    public class SalidaCombustibleReportePDFVo
    {
        public string rangeStart { get; set; }
        public int turno { get; set; }
        public int pipa_id { get; set; }
    }
}