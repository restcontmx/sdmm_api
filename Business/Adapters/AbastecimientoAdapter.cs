using Models.Catalogs;
using Models.VOs;
using System;

namespace Business.Adapters
{
    public static class AbastecimientoAdapter
    {
        public static AbastecimientoPipaVo objectToVo(AbastecimientoPipa obj)
        {
            return new AbastecimientoPipaVo
            {
            };
        }

        /// <summary>
        /// Void object to object
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static AbastecimientoPipa voToObject(AbastecimientoPipaVo vo)
        {
            return new AbastecimientoPipa
            {
                id = vo.id,
                pipa = new Pipa { id = vo.pipa_id },
                despachador = new Models.Auth.User { id = vo.despachador_id }
            };
        }
    }
}