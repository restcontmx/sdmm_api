using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface IProcesoMineroRepository
    {
        IList<ProcesoMinero> getAll();
        ProcesoMinero detail(int id);
        TransactionResult create(ProcesoMinero proceso_minero);
        TransactionResult update(ProcesoMinero proceso_minero);
        TransactionResult delete(int id);
    }
}
