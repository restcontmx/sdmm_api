using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface INivelService
    {
        IList<Nivel> getAll();
        Nivel detail(int id);
        TransactionResult create(NivelVo nivel_vo, User user_log);
        TransactionResult update(NivelVo nivel_vo);
        TransactionResult delete(int id);
    }
}
