using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface IBitacoraBarrenacionService
    {
        IList<BitacoraBarrenacion> getAll();
        IList<BitacoraBarrenacion> getAllBitacoraByIdSupervisor(int user_id);
        BitacoraBarrenacion detail(int id);
        TransactionResult create(BitacoraBarrenacionVo bitacora_vo, User user_log);
        TransactionResult update(BitacoraBarrenacionVo bitacora_vo, User user_log);
        TransactionResult delete(int id);

        TransactionResult autorizarEdicion(BitacoraBarrenacionVo bitacora_vo);
        TransactionResult autorizarRango(BitacoraBarrenacionVo bitacora_vo);
    }
}