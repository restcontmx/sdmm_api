using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface ISubNivelService
    {
        IList<SubNivel> getAll();
        SubNivel detail(int id);
        TransactionResult create(SubNivelVo subnivel_vo, User user_log);
        TransactionResult update(SubNivelVo subnivel_vo, User user_log);
        TransactionResult delete(int id);
        IList<LugarVo> getNombresLugares();
    }
}
