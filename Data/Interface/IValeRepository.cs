using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface IValeRepository
    {

        IList<Vale> getAll();
        Vale detail(int id);
        int create(Vale vale);
        TransactionResult update(Vale vale);
        TransactionResult delete(int id);
        IList<DetalleVale> getAllDetalles(int vale_id);
        TransactionResult createDetalle(DetalleVale detalle);
        IList<RegistroDetalle> getAllRegistersByDetalle(int detalle_id);
        TransactionResult createRegistroDetalle(RegistroDetalle registro);
    }
}
