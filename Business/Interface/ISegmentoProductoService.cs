using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface ISegmentoProductoService
    {
        IList<SegmentoProducto> getAll();
        SegmentoProducto detail(int id);
        TransactionResult create(SegmentoProductoVo segmentoproducto_vo, User user_log);
        TransactionResult update(SegmentoProductoVo segmentoproducto_vo);
        TransactionResult delete(int id);
    }
}