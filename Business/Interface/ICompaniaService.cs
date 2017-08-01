using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface ICompaniaService
    {
        IList<Compania> getAll();
        Compania detail(int id);
        TransactionResult create(CompaniaVo compania, User user_log);
        TransactionResult update(CompaniaVo compania);
        TransactionResult delete(int id);
    }
}