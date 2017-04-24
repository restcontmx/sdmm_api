using Models.Auth;
using Models.Catalogs;
using Models.VOs;

namespace Business.Adapters
{
    public static class ValeAdapter
    {
        public static Vale voToObject(ValeVo vo)
        {
            return new Vale
            {
                id = vo.id,
                compania = new Proveedor { id = vo.compania_id},
                turno = vo.turno,
                lugar = vo.lugar,
                user = new User { id = vo.user_id},
                polvorero = new Empleado { id = vo.polvorero_id},
                cargador1 = new Empleado { id = vo.cargador1_id },
                cargador2 = new Empleado { id = vo.cargador2_id }
            };
        }
    }
}