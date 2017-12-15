using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public class TanqueAdapter
    {
        public static Tanque voToObject(TanqueVo vo)
        {
            return new Tanque
            {
                id = vo.id,
                nombre = vo.nombre,
                capacidad = vo.capacidad,
                litros = vo.litros,
                pipa = new Pipa { id = vo.pipa_id },
                combustible =  new Combustible { id = vo.combustible_id }
            };
        }
    }
}