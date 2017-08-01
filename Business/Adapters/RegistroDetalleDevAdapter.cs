using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Business.Adapters
{
    public static class RegistroDetalleDevAdapter
    {
        public static RegistroDetalleDev voToObject(RegistroDetalleDevVo vo)
        {
            return new RegistroDetalleDev
            {
                id = vo.id,
                folio = vo.folio,
                tipodev = vo.tipodev,
                observaciones = vo.observaciones,
                devolucion = new Devolucion { id = vo.devolucion_id },
                user = new Models.Auth.User { id = vo.user_id }
            };
        }
    }
}