using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface ISegmentoProductoRepository
    {
        IList<SegmentoProducto> getAll();
        SegmentoProducto detail(int id);
        TransactionResult create(SegmentoProducto segmentoproducto);
        TransactionResult update(SegmentoProducto segmentoproducto);
        TransactionResult delete(int id);
    }
}