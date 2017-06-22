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
                presentacion = vo.presentacion,
                codigo = vo.codigo,
                nombre = vo.nombre,
                cantidad = vo.cantidad,
                costo = vo.costo,
                peso = vo.peso,
                modo = vo.modo,
                user = new Models.Auth.User { id = vo.user_id },
                revision =  vo.revision
            };
        }
    }
}