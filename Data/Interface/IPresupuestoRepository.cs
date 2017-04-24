using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface IPresupuestoRepository
    {
        IList<Presupuesto> getAll();
        Presupuesto detail(int id);
        TransactionResult create(Presupuesto presupuesto);
        TransactionResult update(Presupuesto presupuesto);
        TransactionResult delete(int id);
    }
}
