using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface IMaquinariaService
    {
        IList<Maquinaria> getAll();
        Maquinaria detail(int id);
        TransactionResult create(MaquinariaVo maquina_vo);
        TransactionResult update(MaquinariaVo maquina_vo);
        TransactionResult delete(int id);
    }
}