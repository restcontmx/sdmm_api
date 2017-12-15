using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface IPipaService
    {
        IList<Pipa> getAll();
        Pipa detail(int id);
        TransactionResult create(PipaVo pipa_vo);
        TransactionResult update(PipaVo pipa_vo);
        TransactionResult delete(int id);
    }
}