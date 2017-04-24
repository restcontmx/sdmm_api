using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class TipoEmpleadoAdapter
    {
        public static TipoEmpleadoVo objectToVo(TipoEmpleado obj)
        {
            return new TipoEmpleadoVo
            {
            };
        }

        public static TipoEmpleado voToObject(TipoEmpleadoVo vo)
        {
            return new TipoEmpleado
            {
                id = vo.id,
                name = vo.name,
                description = vo.description,
                value = vo.value
            };
        }
    }
}