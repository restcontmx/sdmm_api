using Models.Catalogs;
using Models.VOs;
using System;
namespace Business.Adapters
{
    public static class SalidaCombustibleAdapter
    {
        public static SalidaCombustibleVo objectToVo(SalidaCombustible obj)
        {
            return new SalidaCombustibleVo
            {
            };
        }

        /// <summary>
        /// Void object to object
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static SalidaCombustible voToObject(SalidaCombustibleVo vo)
        {
            return new SalidaCombustible
            {
                id = vo.id,
                odometro = vo.odometro,
                foto = vo.foto,
                maquinaria = new Maquinaria { id = vo.maquinaria_id },
                compania = new Compania { id = vo.compania_id },
                operador =  new Operador { id = vo.operador_id },
                subnivel =  new SubNivel {  id = vo.subnivel_id },
                despachador = new Models.Auth.User { id = vo.despachador_id }
            };
        }
    }
}