using Models.Catalogs;
using Models.VOs;
using System;

namespace Business.Adapters
{
    public class BarrenoAdapter
    {
        public static BarrenoVo objectToVo(Barreno obj)
        {
            return new BarrenoVo
            {
            };
        }

        public static Barreno voToObject(BarrenoVo vo)
        {

            return new Barreno
            {
                id = vo.id,
                cantidad =  vo.cantidad,
                longitud = vo.longitud,
                metros = vo.metros,
                linea =  new Linea { id = vo.linea_id },
                bitacora =  new BitacoraBarrenacion { id = vo.bitacora_id }
            };

        }
    }
}