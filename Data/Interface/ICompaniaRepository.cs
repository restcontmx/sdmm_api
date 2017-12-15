using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface ICompaniaRepository
    {
        IList<Compania> getAll(int sistema);
        Compania detail(int id, int sistema);
        TransactionResult create(Compania compania, int sistema);
        TransactionResult update(Compania compania, int sistema);
        TransactionResult delete(int id, int sistema);
    }
}