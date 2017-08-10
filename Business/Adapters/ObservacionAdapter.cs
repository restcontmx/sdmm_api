using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public class ObservacionAdapter
    {
        public static ObservacionVo objectToVo(Observacion obj)
        {
            return new ObservacionVo
            {
                id = obj.id,
                comentarios = obj.comentarios,
                caja_id = obj.caja.id,
                folio_caja = obj.caja.codigo,
                user_id = obj.user.id,
                timestamp = obj.timestamp.ToString(),
                updated = obj.updated.ToString()
            };
        }

        /// <summary>
        /// Void object to object
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static Observacion voToObject(ObservacionVo vo)
        {
            return new Observacion
            {
                id = vo.id,
                comentarios = vo.comentarios,
                caja = new Caja { id = vo.caja_id, codigo = vo.folio_caja},
                user = new Models.Auth.User { id = vo.user_id}

            };
        }
    }
}