using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface IDevolucionRepository
    {
        int createP(Devolucion devolucion);
        DetalleDevByCaja getDetalleByCaja(string folio);
        TransactionResult createRegistroDetalleDev(RegistroDetalleDev registro);
        IList<Devolucion> getAll();
        IList<RegistroDetalleDev> detail(int id);
        Devolucion detailComprobante(int id);
        /*TransactionResult update(Devolucion devolucion);
        TransactionResult delete(int id);*/
    }
}