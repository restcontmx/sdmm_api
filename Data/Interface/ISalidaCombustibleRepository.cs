using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface ISalidaCombustibleRepository
    {
        IList<SalidaCombustible> getAll();
        SalidaCombustible detail(int id);
        int create(SalidaCombustible salida);
        TransactionResult update(SalidaCombustible salida);
        TransactionResult delete(int id);

        TransactionResult createDetalle(DetalleSalidaCombustible detalle);
        TransactionResult deleteDetallesByIdSalida(int id);
        IList<DetalleSalidaCombustible> getAllDetallesBySalidaId(int id);

        bool checkExists(SalidaCombustible salida);
    }
}