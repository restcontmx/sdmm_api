using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface IBitacoraDesarrolloRepository
    {
        IList<BitacoraDesarrollo> getAll();
        IList<BitacoraDesarrollo> getAllBitacoraByIdSupervisor(int user_id);
        BitacoraDesarrollo detail(int id);
        int create(BitacoraDesarrollo bitacora);
        TransactionResult update(BitacoraDesarrollo bitacora);
        TransactionResult delete(int id);

        TransactionResult autorizarEdicion(BitacoraDesarrollo bitacora);
        TransactionResult autorizarRango(BitacoraDesarrollo bitacora);

        TransactionResult createDetalleDemoraBitacora(DetalleDemoraBitacora demora);
        IList<DetalleDemoraBitacora> getDemorasByIdBitacora(int bitacora_id);
        TransactionResult deleteDetalleDemoraBitacora(int demora_id);
    }
}
