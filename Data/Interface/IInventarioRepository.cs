using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;


namespace Data.Interface
{
    public interface IInventarioRepository
    {
        IList<InfoInventario> getAll(string DateCheck);
        Inventario detail(int id);
        TransactionResult create(Inventario inventario);
        TransactionResult createIventarioDiario(int id);
        TransactionResult createAllIventarioDiario();
        TransactionResult delete(int id);
    }
}