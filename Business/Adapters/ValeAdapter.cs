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
                compania = new Compania { id = vo.compania_id},
                turno = vo.turno,
                user = new User { id = vo.user_id},
                polvorero = new Empleado { id = vo.polvorero_id},
                cargador1 = new Empleado { id = vo.cargador1_id },
                cargador2 = new Empleado { id = vo.cargador2_id },
                subnivel = new SubNivel { id = vo.subnivel_id },
                userAutorizo = new User { id = vo.user_id_autorizo},
                active = vo.active,
                autorizo = vo.autorizo,
                fuente = vo.fuente,
                folio_fisico = vo.folio_fisico
            };
        }
    }
}