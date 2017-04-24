using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface ICategoriaRepository
    {
        IList<Categoria> getAll();
        Categoria detail(int id);
        TransactionResult create(Categoria categoria);
        TransactionResult update(Categoria categoria);
        TransactionResult delete(int id);
    }
}
