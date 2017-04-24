using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface ICategoriaService
    {
        IList<Categoria> getAll();
        Categoria detail(int id);
        TransactionResult create(CategoriaVo categoria_vo, User user_log);
        TransactionResult update(CategoriaVo categoria_vo);
        TransactionResult delete(int id);
    }
}
