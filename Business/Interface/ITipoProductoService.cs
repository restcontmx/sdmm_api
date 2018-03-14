using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface ITipoProductoService
    {
        IList<TipoProducto> getAll(int sistema);
        TipoProducto detail(int id, int sistema);
        TransactionResult create(TipoProductoVo tipoproducto_vo, User user_log, int sistema);
        TransactionResult update(TipoProductoVo tipoproducto_vo, int sistema);
        TransactionResult delete(int id, int sistema);
    }
}
