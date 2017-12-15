using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;


namespace Data.Interface
{
    public interface IAbastecimientoRepository
    {
        IList<AbastecimientoPipa> getAll();
        AbastecimientoPipa detail(int id);
        int create(AbastecimientoPipa abastecimiento);
        TransactionResult update(AbastecimientoPipa abastecimiento);
        TransactionResult delete(int id);

        TransactionResult createDetalle(DetalleAbastecimientoPipa detalle);
        TransactionResult deleteDetallesByIdAbastecimiento(int id);
        IList<DetalleAbastecimientoPipa> getAllDetallesByAbastecimientoId(int id);
    }
}