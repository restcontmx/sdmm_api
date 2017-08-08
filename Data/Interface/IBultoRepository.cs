using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface IBultoRepository
    {
        IList<Bulto> getAll();
        Bulto detail(int id);
        TransactionResult create(Bulto bulto);
        //TransactionResult update(Bulto bulto);
        TransactionResult delete(int id);

        TransactionResult createInventario(Inventario inventario);
    }
}