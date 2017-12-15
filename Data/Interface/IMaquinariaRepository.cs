using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface IMaquinaRepository
    {
        IList<Maquinaria> getAll();
        Maquinaria detail(int id);
        int create(Maquinaria maquina);
        TransactionResult createDetalle(DetalleConsumoMaquinaria maquina);
        TransactionResult update(Maquinaria maquina);
        TransactionResult delete(int id);
        TransactionResult deleteDetallesByIdMaquinaria(int id);

        IList<DetalleConsumoMaquinaria> getAllDetallesByMaquinariaId(int id);
    }
}