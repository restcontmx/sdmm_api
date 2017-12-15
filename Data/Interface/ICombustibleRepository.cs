using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
     public interface ICombustibleRepository
    {
        IList<Combustible> getAll();
        Combustible detail(int id);
        TransactionResult create(Combustible combustible);
        TransactionResult update(Combustible combustible);
        TransactionResult delete(int id);
    }
}