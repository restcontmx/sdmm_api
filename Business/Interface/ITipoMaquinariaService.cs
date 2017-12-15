using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;


namespace Business.Interface
{
    public interface ITipoMaquinariaService
    {
        IList<TipoMaquinaria> getAll();
        TipoMaquinaria detail(int id);
        TransactionResult create(TipoMaquinariaVo tipomaquinaria_vo);
        TransactionResult update(TipoMaquinariaVo tipomaquinaria_vo);
        TransactionResult delete(int id);
    }
}