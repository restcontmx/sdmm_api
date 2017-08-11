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
    public interface IValeService
    {
        IList<Vale> getAll();
        Vale detail(int id);
        TransactionResult create(ValeVo vale_vo, User user_log);
        TransactionResult update(ValeVo vale_vo);
        TransactionResult updateStatus(ValeVo vale_vo);
        TransactionResult updateAutorizacion(ValeVo vale, User user_log);
        TransactionResult delete(int id);
        IList<DetalleVale> getDetailsByValeId(int vale_id);
        IList<RegistroDetalle> getAllRegistersByDetalle(int detalle_id);
        IList<RegistroDetalle> getAllRegistersOverByDetalle(int detalle_id);
        IList<RegistroDetalle> getAllRegistersByFolioCaja(string folioCaja);
        IList<RegistroDetalle> getAllRegistersSacos();
        IList<RegistroDetalle> getAllRegistersHistorico();
        IList<RegistroDetalle> getAllRegistersHistoricoOver();
        TransactionResult createRegistroDetalle(RegistroDetalleVo registro_vo, User user_log);
        TransactionResult createRegistroDetalleOver(RegistroDetalleVo registro_vo, User user_log);
        TransactionResult createRegistroDetalleByList(IList<RegistroDetalleVo> registrodetalles_vo, User user);
    }
}
