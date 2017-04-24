using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class DetalleValeAdapter
    {
        public static DetalleVale voToObject(DetalleValeVo vo)
        {
            return new DetalleVale
            {
                id = vo.id,
                cantidad = vo.cantidad,
                producto = new Producto { id = vo.producto_id },
                vale = new Vale { id = vo.vale_id }
            };
        }
    }
}