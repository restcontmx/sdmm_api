using Models.Catalogs;
using Models.VOs;
using System;

namespace Business.Adapters
{
    public static class OperadorAdapter
    {
        public static OperadorVo objectToVo(Operador obj)
        {
            return new OperadorVo
            {
            };
        }

        /// <summary>
        /// Void object to object
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static Operador voToObject(OperadorVo vo)
        {
            return new Operador
            {
                id = vo.id,
                nombre = vo.nombre,
                ap_paterno = vo.ap_paterno,
                ap_materno = vo.ap_materno,
                timestamp = Convert.ToDateTime(vo.timestamp),
                updated = Convert.ToDateTime(vo.updated),
                compania = new Compania { id = vo.compania_id }
            };
        }
    }
}