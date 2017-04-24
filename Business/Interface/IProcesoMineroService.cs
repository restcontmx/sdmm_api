using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface IProcesoMineroService
    {
        IList<ProcesoMinero> getAll();
        ProcesoMinero detail(int id);
        TransactionResult create(ProcesoMineroVo procesominero_vo, User user_log);
        TransactionResult update(ProcesoMineroVo procesominero_vo);
        TransactionResult delete(int id);
    }
}
