using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface ICuentaRepository
    {
        IList<Cuenta> getAll();
        Cuenta detail(int id);
        TransactionResult create(Cuenta cuenta);
        TransactionResult update(Cuenta cuenta);
        TransactionResult delete(int id);
    }
}
