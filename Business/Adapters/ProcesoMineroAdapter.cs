using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class ProcesoMineroAdapter
    {
        public static ProcesoMineroVo objectToVo(ProcesoMinero obj)
        {
            return new ProcesoMineroVo
            {
            };
        }

        public static ProcesoMinero voToObject(ProcesoMineroVo vo)
        {
            return new ProcesoMinero
            {
                id = vo.id,
                nombre = vo.nombre,
                codigo = vo.codigo,
                user = new Models.Auth.User { id = vo.user_id }
            };
        }
    }
}