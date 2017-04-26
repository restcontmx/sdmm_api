using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface ICuentaService
    {
        IList<Cuenta> getAll();
        Cuenta detail(int id);
        TransactionResult create(CuentaVo Cuenta_vo, User user_log);
        TransactionResult update(CuentaVo Cuenta_vo);
        TransactionResult delete(int id);
    }
}
