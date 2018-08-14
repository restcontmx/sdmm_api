using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface IBitacoraDesarrolloService
    {
        IList<BitacoraDesarrollo> getAll();
        IList<BitacoraDesarrollo> getAllBitacoraByIdSupervisor(int user_id);
        BitacoraDesarrollo detail(int id);
        TransactionResult create(BitacoraDesarrolloVo bitacora_vo, User user_log);
        TransactionResult update(BitacoraDesarrolloVo bitacora_vo, User user_log);
        TransactionResult delete(int id);

        TransactionResult autorizarEdicion(BitacoraDesarrolloVo bitacora_vo);
        TransactionResult autorizarRango(BitacoraDesarrolloVo bitacora_vo);
    }
}