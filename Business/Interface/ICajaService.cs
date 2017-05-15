using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    /// <summary>
    /// Caja service definition
    /// </summary>
    public interface ICajaService
    {
        IList<Caja> getAll();
        Caja detail(int id);
        TransactionResult create(CajaVo caja_vo, User user_log);
        TransactionResult update(CajaVo caja_vo);
        TransactionResult delete(int id);
    }
}
