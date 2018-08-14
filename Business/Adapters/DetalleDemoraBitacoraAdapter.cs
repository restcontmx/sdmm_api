using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class DetalleDemoraBitacoraAdapter
    {
        public static DetalleDemoraBitacora voToObject(DetalleDemoraBitacoraVo vo)
        {
            return new DetalleDemoraBitacora
            {
                id = vo.id,
                bitacora_desarrollo = new BitacoraDesarrollo { id = vo.bitacora_desarrollo_id },
                bitacora_barrenacion = new BitacoraBarrenacion { id = vo.bitacora_barrenacion_id },
                tipo_bitacora = vo.tipo_bitacora,
                demora = new Demora { id = vo.demora_id },
                comentarios = vo.comentarios,
                horas_perdidas = Convert.ToDateTime(vo.horas_perdidas)
            };
        }
    }
}