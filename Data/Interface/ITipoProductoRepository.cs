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
        IList<TipoProducto> getAll(int sistema);
        TipoProducto detail(int id, int sistema);
        TransactionResult create(TipoProducto tipoproducto, int sistema);
        TransactionResult update(TipoProducto tipoproducto, int sistema);
        TransactionResult delete(int id, int sistema);
    }
}