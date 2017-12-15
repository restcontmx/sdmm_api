using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class TipoMaquinariaAdapter
    {
        public static TipoMaquinariaVo objectToVo(TipoMaquinaria obj)
        {
            return new TipoMaquinariaVo
            {
            };
        }

        public static TipoMaquinaria voToObject(TipoMaquinariaVo vo)
        {
            return new TipoMaquinaria
            {
                id = vo.id,
                name = vo.name,
                description = vo.description
            };
        }
    }
}