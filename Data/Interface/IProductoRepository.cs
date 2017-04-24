using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface IProductoRepository
    {
        IList<Producto> getAll();
        Producto detail(int id);
        TransactionResult create(Producto producto);
        TransactionResult update(Producto producto);
        TransactionResult delete(int id);
    }
}
