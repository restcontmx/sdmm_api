using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface ICompaniaRepository
    {
        IList<Compania> getAll();
        Compania detail(int id);
        TransactionResult create(Compania compania);
        TransactionResult update(Compania compania);
        TransactionResult delete(int id);
    }
}