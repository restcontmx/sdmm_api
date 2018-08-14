using Models.Catalogs;
using Models.VOs;
using System;

namespace Business.Adapters
{
    public class TipoDesarrolloAdapter
    {
        public static TipoDesarrolloVo objectToVo(TipoDesarrollo obj)
        {
            return new TipoDesarrolloVo
            {
            };
        }

        /// <summary>
        /// Void object to object
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static TipoDesarrollo voToObject(TipoDesarrolloVo vo)
        {
            return new TipoDesarrollo
            {
                id = vo.id,
                nombre = vo.nombre,
                description = vo.description,
                status = vo.status == 0 ? false : true
            };
        }
    }
}