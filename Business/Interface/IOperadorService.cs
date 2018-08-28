using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;
namespace Business.Interface
{
    public interface IOperadorService
    {
        IList<Operador> getAll(int sistema);
        Operador detail(int id, int sistema);
        TransactionResult create(OperadorVo operador_vo, int sistema);
        TransactionResult update(OperadorVo operador_vo, int sistema);
        TransactionResult delete(int id, int sistema);
    }
}