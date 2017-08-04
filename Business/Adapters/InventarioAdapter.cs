using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class InventarioAdapter
    {

        public static InventarioVo objectToVo(Inventario obj)
        {
            return new InventarioVo
            {
            };
        }

        /// <summary>
        /// Void object to object
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static Inventario voToObject(InventarioVo vo)
        {
            return new Inventario
            {
                id = vo.id,
                producto = new Producto { id = vo.producto_id },
                cantidad = vo.cantidad,
                turno = vo.turno

            };
        }
    }
}