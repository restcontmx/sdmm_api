using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;


namespace Data.Interface
{
    public interface IBitacoraBarrenacionRepository
    {
        IList<BitacoraBarrenacion> getAll();
        IList<BitacoraBarrenacion> getAllBitacoraByIdSupervisor(int user_id);
        BitacoraBarrenacion detail(int id);
        int create(BitacoraBarrenacion bitacora);
        TransactionResult update(BitacoraBarrenacion bitacora);
        TransactionResult delete(int id);

        TransactionResult autorizarEdicion(BitacoraBarrenacion bitacora);
        TransactionResult autorizarRango(BitacoraBarrenacion bitacora);

        TransactionResult createDetalleDemoraBitacora(DetalleDemoraBitacora demora);
        IList<DetalleDemoraBitacora> getDemorasByIdBitacora(int bitacora_id);
        TransactionResult deleteDetalleDemoraBitacora(int demora_id);

        int createLineaBitacora(Linea linea);
        IList<Linea> getLineasByIdBitacora(int bitacora_id);
        TransactionResult deleteLineaByIdBitacora(int bitacora_id);

        TransactionResult createBarrenoLineaBitacora(Barreno barreno);
        IList<Barreno> getBarrenosByIdLinea(int linea_id);
        TransactionResult deleteBarrenosByIdBitacora(int bitacora_id);
    }
}