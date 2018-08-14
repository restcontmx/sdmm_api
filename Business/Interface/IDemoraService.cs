using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;
namespace Business.Interface
{
    public interface IDemoraService
    {
        IList<Demora> getAll();
        Demora detail(int id);
        TransactionResult create(DemoraVo demora_vo, User user_log);
        TransactionResult update(DemoraVo demora_vo);
        TransactionResult delete(int id);
    }
}