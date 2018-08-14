using Models.Catalogs;
using Models.VOs;
using System;
namespace Business.Adapters
{
    public static class DemoraAdapter
    {
        public static DemoraVo objectToVo(Demora obj)
        {
            return new DemoraVo
            {
            };
        }

        /// <summary>
        /// Void object to object
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static Demora voToObject(DemoraVo vo)
        {
            return new Demora
            {
                id = vo.id,
                nombre = vo.nombre,
                status = vo.status == 0 ? false : true,
                user = new Models.Auth.User { id = vo.user_id }
            };
        }
    }
}