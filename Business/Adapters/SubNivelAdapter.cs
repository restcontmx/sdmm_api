using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class SubNivelAdapter
    {
        public static SubNivelVo objectToVo(SubNivel obj)
        {
            return new SubNivelVo
            {
            };
        }

        public static SubNivel voToObject(SubNivelVo vo)
        {
            return new SubNivel
            {
                id = vo.id,
                nivel = new Nivel { id = vo.nivel_id },
                nombre = vo.nombre,
                status = vo.status==0? false : true,
                user = new Models.Auth.User { id = vo.user_id }
            };
        }
    }
}