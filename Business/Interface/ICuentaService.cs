using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface ICuentaService
    {
        IList<Cuenta> getAll(int sistema);
        Cuenta detail(int id, int sistema);
        TransactionResult create(CuentaVo Cuenta_vo, User user_log, int sistema);
        TransactionResult update(CuentaVo Cuenta_vo, int sistema);
        TransactionResult delete(int id, int sistema);
    }
}
