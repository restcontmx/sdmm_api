using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface IDevolucionRepository
    {
        TransactionResult create(Devolucion devolucion);
       /* IList<Devolucion> getAll();
        Devolucion detail(int id);
        TransactionResult update(Devolucion devolucion);
        TransactionResult delete(int id);*/
    }
}