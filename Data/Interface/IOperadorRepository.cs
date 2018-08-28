using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface IOperadorRepository
    {
        IList<Operador> getAll(int sistema);
        Operador detail(int id, int sistema);
        TransactionResult create(Operador operador, int sistema);
        TransactionResult update(Operador operador, int sistema);
        TransactionResult delete(int id, int sistema);
    }
}
