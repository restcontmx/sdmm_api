using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface IFichaEntregaService
    {
        IList<FichaEntregaRecepcion> getAll();
        FichaEntregaRecepcion detail(int id);
        TransactionResult create(FichaEntregaRecepcionVo ficha_vo);
        TransactionResult update(FichaEntregaRecepcionVo ficha_vo);
        TransactionResult delete(int id);
    }
}