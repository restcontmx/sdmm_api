using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class CajaAdapter
    {

        public static CajaVo objectToVo(Caja obj)
        {
            return new CajaVo
            {
            };
        }

        /// <summary>
        /// Void object to object
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static Caja voToObject(CajaVo vo)
        {
            return new Caja
            {
                id = vo.id,
                codigo = vo.codigo,
                folio_ini = vo.folio_ini,
                folio_fin = vo.folio_fin,
                cantidad = vo.cantidad,
                active = vo.active,
                producto = new Producto { id = vo.producto_id },
                user = new Models.Auth.User { id = vo.user_id },
                revision =  vo.revision
            };
        }

    }
}