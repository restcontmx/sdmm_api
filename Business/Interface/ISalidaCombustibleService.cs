using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface ISalidaCombustibleService
    {
        IList<SalidaCombustible> getAll();
        SalidaCombustible detail(int id);
        TransactionResult create(SalidaCombustibleVo salida_vo);
        TransactionResult update(SalidaCombustibleVo salida_vo);
        TransactionResult delete(int id);
    }
}