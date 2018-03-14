using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;
using Models.Auth;

namespace Data.Interface
{
    public interface IValeRepository
    {

        IList<Vale> getAll();
        Vale detail(int id);
        int create(Vale vale);
        TransactionResult update(Vale vale);
        TransactionResult updateStatus(Vale vale);
        TransactionResult updateAutorizacion(Vale vale);
        TransactionResult delete(int id);
        TransactionResult deleteRegistroDetalle(int id);
        TransactionResult deleteDetalleVale(int id);
        IList<DetalleVale> getAllDetalles(int vale_id);
        int createDetalle(DetalleVale detalle);
        IList<RegistroDetalle> getAllRegistersByDetalle(int detalle_id);
        IList<RegistroDetalle> getAllRegistersOverByDetalle(int detalle_id);
        IList<RegistroDetalle> getAllRegistersByFolioCaja(string folioCaja);
        IList<RegistroDetalle> getAllRegistersHistorico();
        IList<RegistroDetalle> getAllRegistersHistoricoOver();
        IList<RegistroDetalle> getAllRegistersSacos();
        TransactionResult createRegistroDetalle(RegistroDetalle registro);
        TransactionResult createRegistroDetalleOver(RegistroDetalle registro);

        User validarLoginTablet(User user);

        TransactionResult cerrarVale(Vale vale);
    }
}
