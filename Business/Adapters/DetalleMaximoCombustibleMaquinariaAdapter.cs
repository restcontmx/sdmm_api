using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public class DetalleMaximoCombustibleMaquinariaAdapter
    {
        public static DetalleMaximoCombustibleMaquinaria voToObject(DetalleMaximoCombustibleMaquinariaVo vo)
        {
            return new DetalleMaximoCombustibleMaquinaria
            {
                id = vo.id,
                litros_maximo = vo.litros_maximo,
                combustible = new Combustible { id = vo.combustible_id },
                maquinaria = new Maquinaria { id = vo.maquinaria_id },
                user = new Models.Auth.User { id = vo.user_id },
                timestamp = Convert.ToDateTime(vo.timestamp)
            };
        }
    }
}