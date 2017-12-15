using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface IAbastecimientoService
    {
        IList<AbastecimientoPipa> getAll();
        AbastecimientoPipa detail(int id);
        TransactionResult create(AbastecimientoPipaVo abastecimiento_vo);
        TransactionResult update(AbastecimientoPipaVo abastecimiento_vo);
        TransactionResult delete(int id);
    }
}