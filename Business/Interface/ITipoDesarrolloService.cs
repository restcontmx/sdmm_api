using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface ITipoDesarrolloService
    {
        IList<TipoDesarrollo> getAll();
        TipoDesarrollo detail(int id);
        TransactionResult create(TipoDesarrolloVo tipodes_vo);
        TransactionResult update(TipoDesarrolloVo tipodes_vo);
        TransactionResult delete(int id);
    }
}