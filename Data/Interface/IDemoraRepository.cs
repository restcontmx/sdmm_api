using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface IDemoraRepository
    {
        IList<Demora> getAll();
        Demora detail(int id);
        TransactionResult create(Demora demora);
        TransactionResult update(Demora demora);
        TransactionResult delete(int id);
    }
}