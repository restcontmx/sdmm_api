using Models.Catalogs;
using Models.VOs;

namespace Business.Adapters
{
    public static class CuentaAdapter
    {
        public static CuentaVo objectToVo(Cuenta obj)
        {
            return new CuentaVo
            {
            };
        }

        public static Cuenta voToObject(CuentaVo vo)
        {
            return new Cuenta
            {
                id = vo.id,
                nombre = vo.nombre,
                numero = vo.numero,
                num_categoria = vo.num_categoria,
                user = new Models.Auth.User { id = vo.user_id }

            };
        }
    }
}