using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;


namespace Business.Interface
{
    public interface IInventarioService
    {
        IList<InfoInventario> getAll(string DateCheck);
        Inventario detail(int id);
        TransactionResult createIventarioDiario();
        TransactionResult create(InventarioVo inventario_vo);
        TransactionResult delete(int id);
    }
}