using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Business.Adapters
{
    public class BultoAdapter
    {
        public static BultoVo objectToVo(Bulto obj)
        {
            return new BultoVo
            {
            };
        }

        /// <summary>
        /// Void object to object
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static Bulto voToObject(BultoVo vo)
        {
            return new Bulto
            {
                id = vo.id,
                codigo = vo.codigo,
                active = vo.active,
                producto = new Producto { id = vo.producto_id },
                user = new Models.Auth.User { id = vo.user_id }
            };
        }
    }
}