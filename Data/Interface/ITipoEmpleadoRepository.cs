using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface ITipoEmpleadoRepository
    {
        IList<TipoEmpleado> getAll();
        TipoEmpleado detail(int id);
        TransactionResult create(TipoEmpleado tipoempleado);
        TransactionResult update(TipoEmpleado tipoempleado);
        TransactionResult delete(int id);
    }
}
