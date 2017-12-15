using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface IPipaRepository
    {
        IList<Pipa> getAll();
        Pipa detail(int id);
        int create(Pipa pipa);
        TransactionResult update(Pipa pipa);
        TransactionResult delete(int id);

        TransactionResult createTanque(Tanque tanque);
        TransactionResult deleteTanquesByIdPipa(int id);
        IList<Tanque> getAllTanquesByIdPipa(int id);

    }
}