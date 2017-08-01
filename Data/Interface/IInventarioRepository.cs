using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;


namespace Data.Interface
{
    public interface IInventarioRepository
    {
        IList<Inventario> getAll();
        Inventario detail(int id);
        TransactionResult create(Inventario inventario);
        TransactionResult update(Inventario inventario);
        TransactionResult delete(int id);
    }
}