using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;


namespace Data.Interface
{
    public interface ITipoMaquinariaRepository
    {
        IList<TipoMaquinaria> getAll();
        TipoMaquinaria detail(int id);
        TransactionResult create(TipoMaquinaria tipomaquinaria);
        TransactionResult update(TipoMaquinaria tipomaquinaria);
        TransactionResult delete(int id);
    }
}