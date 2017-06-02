using Models.Auth;
using Models.Catalogs;
using Models.VOs;

namespace Business.Adapters
{
    public static class DevolucionAdapter
    {
        public static Devolucion voToObject(DevolucionVo vo)
        {
            return new Devolucion
            {
                id = vo.id,
                proveedor = new Proveedor { id = vo.compania_id },
                folio = vo.folio,
                user = new User { id = vo.user_id}
                
            };
        }

    }
}