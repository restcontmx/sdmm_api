using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;
namespace Data.Interface
{
    public interface ITipoDesarrolloRepository
    {
        IList<TipoDesarrollo> getAll();
        TipoDesarrollo detail(int id);
        TransactionResult create(TipoDesarrollo tipo_de);
        TransactionResult update(TipoDesarrollo tipo_des);
        TransactionResult delete(int id);
    }
}