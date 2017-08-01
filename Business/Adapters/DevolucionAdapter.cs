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
                compania = new Compania { id = vo.compania_id },
                motivo = vo.motivo,
                turno = vo.turno,
                vale = new Vale { id = vo.vale_id },
                user = new User { id = vo.user_id}
                
            };
        }

    }
}