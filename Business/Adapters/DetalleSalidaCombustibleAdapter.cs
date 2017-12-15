using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public class DetalleSalidaCombustibleAdapter
    {
        public static DetalleSalidaCombustible voToObject(DetalleSalidaCombustibleVo vo)
        {
            return new DetalleSalidaCombustible
            {
                litros_surtidos = vo.litros_surtidos,
                salida_combustible = new SalidaCombustible { id = vo.salida_combustible_id},
                tanque = new Tanque { id = vo.tanque_id },
                combustible = new Combustible { id = vo.combustible_id },
                pipa = new Pipa { id = vo.pipa_id }
            };
        }
    }
}