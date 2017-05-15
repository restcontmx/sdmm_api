using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class RegistroDetalleAdapter
    {
        /// <summary>
        /// Object to void object function
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static RegistroDetalleVo objectToVo(RegistroDetalle obj)
        {
            return new RegistroDetalleVo
            {
            };
        }

        /// <summary>
        /// Void object to object
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static RegistroDetalle voToObject(RegistroDetalleVo vo)
        {
            return new RegistroDetalle
            {
                id = vo.id,
                detallevale = new DetalleVale { id = vo.detallevale_id },
                folio = vo.folio,
                user = new Models.Auth.User { id = vo.user_id }
            };
        }
    }
}