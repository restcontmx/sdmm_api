using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface IValeService
    {
        IList<Vale> getAll();
        Vale detail(int id);
        TransactionResult create(ValeVo vale_vo, User user_log);
        TransactionResult update(ValeVo vale_vo);
        TransactionResult delete(int id);
    }
}
