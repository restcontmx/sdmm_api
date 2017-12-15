using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface IFichaEntregaRepository
    {
        IList<FichaEntregaRecepcion> getAll();
        FichaEntregaRecepcion detail(int id);
        int create(FichaEntregaRecepcion ficha);
        TransactionResult update(FichaEntregaRecepcion ficha);
        TransactionResult delete(int id);

        TransactionResult createDetalle(DetalleFichaEntregaRecepcion detalle);
        TransactionResult deleteDetallesByIdFicha(int id);
        IList<DetalleFichaEntregaRecepcion> getAllDetallesByFichaId(int id);
    }
}