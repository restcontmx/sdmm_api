using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class PresupuestoAdapter
    {
        public static PresupuestoVo objectToVo(Presupuesto obj)
        {
            return new PresupuestoVo
            {
            };
        }

        public static Presupuesto voToObject(PresupuestoVo vo)
        {
            return new Presupuesto
            {
                id = vo.id,
                producto = new Producto { id = vo.producto_id },
                presupuesto = vo.presupuesto,
                stock = vo.stock,
                year = vo.year,
                user = new Models.Auth.User { id = vo.user_id }
            };
        }
    }
}