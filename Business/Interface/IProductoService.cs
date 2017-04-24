using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface IProductoService
    {
        IList<Producto> getAll();
        Producto detail(int id);
        TransactionResult create(ProductoVo producto_vo, User user_log);
        TransactionResult update(ProductoVo producto_vo);
        TransactionResult delete(int id);
    }
}
