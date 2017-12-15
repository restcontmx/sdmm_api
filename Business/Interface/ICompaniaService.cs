using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface ICompaniaService
    {
        IList<Compania> getAll(int sistema);
        Compania detail(int id, int sistema);
        TransactionResult create(CompaniaVo compania, User user_log, int sistema);
        TransactionResult update(CompaniaVo compania, int sistema);
        TransactionResult delete(int id, int sistema);
    }
}