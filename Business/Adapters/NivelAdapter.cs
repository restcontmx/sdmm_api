using Models.Catalogs;
using Models.VOs;

namespace Business.Adapters
{
    public static class NivelAdapter
    {
        public static NivelVo objectToVo(Nivel obj)
        {
            return new NivelVo
            {
            };
        }

        public static Nivel voToObject(NivelVo vo)
        {
            return new Nivel
            {
                id = vo.id,
                nombre = vo.nombre,
                codigo = vo.codigo,
                user = new Models.Auth.User { id = vo.user_id }
            };
        }
    }
}