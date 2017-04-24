using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface ITipoProductoRepository
    {
        IList<TipoProducto> getAll();
        TipoProducto detail(int id);
        TransactionResult create(TipoProducto tipoproducto);
        TransactionResult update(TipoProducto tipoproducto);
        TransactionResult delete(int id);
    }
}