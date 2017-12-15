using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class DetalleFichaEntregaAdapter
    {
        public static DetalleFichaEntregaRecepcion voToObject(DetalleFichaEntregaRecepcionVo vo)
        {
            return new DetalleFichaEntregaRecepcion
            {
                litros = vo.litros,
                tanque = new Tanque { id = vo.tanque_id },
                ficha = new FichaEntregaRecepcion { id = vo.ficha_id},
                pipa = new Pipa { id = vo.pipa_id }
            };
        }
    }
}