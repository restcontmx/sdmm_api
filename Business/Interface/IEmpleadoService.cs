using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface IEmpleadoService
    {
        IList<Empleado> getAll();
        Empleado detail(int id);
        TransactionResult create(EmpleadoVo empleado_vo, User user_log);
        TransactionResult update(EmpleadoVo empleado_vo);
        TransactionResult delete(int id);
    }
}
