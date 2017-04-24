using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface INivelRepository
    {
        IList<Nivel> getAll();
        Nivel detail(int id);
        TransactionResult create(Nivel nivel);
        TransactionResult update(Nivel nivel);
        TransactionResult delete(int id);
    }
}
