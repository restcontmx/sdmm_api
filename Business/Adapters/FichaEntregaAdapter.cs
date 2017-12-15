using Models.Catalogs;
using Models.VOs;
using System;


namespace Business.Adapters
{
    public static class FichaEntregaAdapter
    {
        public static FichaEntregaRecepcionVo objectToVo(FichaEntregaRecepcion obj)
        {
            return new FichaEntregaRecepcionVo
            {
            };
        }

        /// <summary>
        /// Void object to object
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static FichaEntregaRecepcion voToObject(FichaEntregaRecepcionVo vo)
        {
            return new FichaEntregaRecepcion
            {
                id = vo.id,
                despachador_entrega = new Models.Auth.User { id = vo.despachador_entrega_id },
                despachador_recibe = new Models.Auth.User { id = vo.despachador_recibe_id }
            };
        }
    }
}