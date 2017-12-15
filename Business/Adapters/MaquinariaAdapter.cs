using Models.Catalogs;
using Models.VOs;
using System;

namespace Business.Adapters
{
    public static class MaquinariaAdapter
    {
        public static MaquinariaVo objectToVo(Maquinaria obj)
        {
            return new MaquinariaVo
            {
            };
        }

        /// <summary>
        /// Void object to object
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static Maquinaria voToObject(MaquinariaVo vo)
        {
            return new Maquinaria
            {
                id = vo.id,
                nombre = vo.nombre,
                cuenta = new Cuenta { id = vo.cuenta_id },
                timestamp = Convert.ToDateTime(vo.timestamp),
                updated = Convert.ToDateTime(vo.updated)
            };
        }
    }
}