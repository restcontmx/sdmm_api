using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface ITipoEmpleadoService
    {
        IList<TipoEmpleado> getAll();
        TipoEmpleado detail(int id);
        TransactionResult create(TipoEmpleadoVo tipoempleado_vo, User user_log);
        TransactionResult update(TipoEmpleadoVo tipoempleado_vo);
        TransactionResult delete(int id);
    }
}
