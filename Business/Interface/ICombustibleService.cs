using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface ICombustibleService
    {
        IList<Combustible> getAll();
        Combustible detail(int id);
        TransactionResult create(CombustibleVo combustible_vo);
        TransactionResult update(CombustibleVo combustible_vo);
        TransactionResult delete(int id);
    }
}