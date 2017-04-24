using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class TipoProductoAdapter
    {
        public static TipoProductoVo objectToVo(TipoProducto obj)
        {
            return new TipoProductoVo
            {
            };
        }

        public static TipoProducto voToObject(TipoProductoVo vo)
        {
            return new TipoProducto
            {
                id = vo.id,
                name = vo.name,
                description = vo.description,
                value = vo.value
            };
        }
    }
}