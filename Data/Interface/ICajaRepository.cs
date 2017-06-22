using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    /// <summary>
    /// Caja repository definition
    /// </summary>
    public interface ICajaRepository
    {
        IList<Caja> getAll();
        Caja detail(int id);
        TransactionResult create(Caja caja);
        TransactionResult update(Caja caja);
        TransactionResult delete(int id);

        TransactionResult createObservacion(Observacion obs);
    }
}
