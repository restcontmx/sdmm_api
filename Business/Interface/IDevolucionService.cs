using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface IDevolucionService
    {
        TransactionResult createP(DevolucionVo devolucion_vo, User user_log);
        DetalleDevByCajaVo getDetalleByCaja(string folio);
        IList<Devolucion> getAll();
        IList<RegistroDetalleDev> detail(int id);
        /*TransactionResult update(DevolucionVo devolucion_vo);
        TransactionResult delete(int id);*/




    }
}
