using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class ProductoAdapter
    {
        public static ProductoVo objectToVo(Producto obj)
        {
            return new ProductoVo
            {
            };
        }

        public static Producto voToObject(ProductoVo vo)
        {
            return new Producto
            {
                id = vo.id,
                tipo_producto = new TipoProducto { id = vo.tipoproducto_id },
                proveedor = new Proveedor { id = vo.proveedor_id },
                segmento = new SegmentoProducto { id = vo.segmentoproducto_id },
                codigo = vo.codigo,
                nombre = vo.nombre,
                costo = vo.costo,
                peso = vo.peso,
                user = new Models.Auth.User { id = vo.user_id },
                revision =  vo.revision
            };
        }
    }
}