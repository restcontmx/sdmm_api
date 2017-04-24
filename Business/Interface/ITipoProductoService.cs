using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface ITipoProductoService
    {
        IList<TipoProducto> getAll();
        TipoProducto detail(int id);
        TransactionResult create(TipoProductoVo tipoproducto_vo, User user_log);
        TransactionResult update(TipoProductoVo tipoproducto_vo);
        TransactionResult delete(int id);
    }
}
