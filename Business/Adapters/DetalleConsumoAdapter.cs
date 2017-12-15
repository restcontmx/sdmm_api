using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public class DetalleConsumoAdapter
    {
        public static DetalleConsumoMaquinaria voToObject(DetalleConsumoMaquinariaVo vo)
        {
            return new DetalleConsumoMaquinaria
            {
                id = vo.id,
                promedio = vo.promedio,
                tolerancia = vo.tolerancia,
                combustible = new Combustible { id = vo.combustible_id },
                maquinaria = new Maquinaria { id = vo.maquinaria_id }
            };
        }
    }
}