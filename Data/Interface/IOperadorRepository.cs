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
        IList<Operador> getAll();
        Operador detail(int id);
        TransactionResult create(Operador operador);
        TransactionResult update(Operador operador);
        TransactionResult delete(int id);
    }
}
