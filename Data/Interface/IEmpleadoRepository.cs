using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface IEmpleadoRepository
    {
        IList<Empleado> getAll();
        Empleado detail( int id );
        TransactionResult create(Empleado empleado);
        TransactionResult update(Empleado empleado);
        TransactionResult delete(int id);
    }
}
