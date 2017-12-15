using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;
namespace Business.Interface
{
    public interface IOperadorService
    {
        IList<Operador> getAll();
        Operador detail(int id);
        TransactionResult create(OperadorVo operador_vo);
        TransactionResult update(OperadorVo operador_vo);
        TransactionResult delete(int id);
    }
}