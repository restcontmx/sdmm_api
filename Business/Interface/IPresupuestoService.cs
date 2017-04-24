using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface IPresupuestoService
    {
        IList<Presupuesto> getAll();
        Presupuesto detail(int id);
        TransactionResult create(PresupuestoVo presupuesto_vo, User user_log);
        TransactionResult update(PresupuestoVo presupuesto_vo);
        TransactionResult delete(int id);
    }
}
