using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface ICuentaRepository
    {
        IList<Cuenta> getAll(int sistema);
        Cuenta detail(int id, int sistema);
        TransactionResult create(Cuenta cuenta, int sistema);
        TransactionResult update(Cuenta cuenta, int sistema);
        TransactionResult delete(int id, int sistema);
    }
}
