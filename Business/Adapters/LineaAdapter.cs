using Models.Catalogs;
using Models.VOs;
using System;

namespace Business.Adapters
{
    public class LineaAdapter
    {
        public static LineaVo objectToVo(Linea obj)
        {
            return new LineaVo
            {
            };
        }

        public static Linea voToObject(LineaVo vo)
        {

            return new Linea
            {
                id = vo.id,
                numero = vo.numero,
                vale = new Vale { id = vo.vale_id },
                subnivel = new SubNivel { id = vo.subnivel_id },
                tipo = vo.tipo,
                bitacora = new BitacoraBarrenacion { id = vo.bitacora_id }
            };

        }
    }
}