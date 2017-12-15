using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class DetalleAbastecimientoAdapter
    {
        public static DetalleAbastecimientoPipa voToObject(DetalleAbastecimientoPipaVo vo)
        {
            return new DetalleAbastecimientoPipa
            {
                id = vo.id,
                foto_recibo = vo.foto_recibo,
                litros = vo.litros,
                tanque = new Tanque { id = vo.tanque_id },
                abastecimiento = new AbastecimientoPipa { id = vo.abastecimiento_id },
                pipa = new Pipa { id = vo.pipa_id }
            };
        }
    }
}