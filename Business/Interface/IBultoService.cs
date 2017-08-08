using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;


namespace Business.Interface
{
    public interface IBultoService
    {
        IList<Bulto> getAll();
        Bulto detail(int id);
        TransactionResult create(BultoVo bulto_vo, User user_log);
        //TransactionResult update(BultoVo bulto_vo);
        TransactionResult delete(int id);

        TransactionResult createBultosByList(IList<BultoVo> bultos_vo, User user);
    }
}