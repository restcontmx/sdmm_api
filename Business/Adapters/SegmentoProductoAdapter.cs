using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Business.Adapters
{
    public static class SegmentoProductoAdapter
    {
        public static SegmentoProductoVo objectToVo(SegmentoProducto obj)
        {
            return new SegmentoProductoVo
            {
            };
        }

        public static SegmentoProducto voToObject(SegmentoProductoVo vo)
        {
            return new SegmentoProducto
            {
                id = vo.id,
                name = vo.name,
                description = vo.description
            };
        }
    }
}