using Business.Interface;
using System.Collections.Generic;
using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using Warrior.Handlers.Enums;
using Data.Interface;
using Business.Adapters;

namespace Business.Implementation
{
    public class SegmentoProductoService : ISegmentoProductoService
    {
        private ISegmentoProductoRepository segmentoproducto_repository;

        public SegmentoProductoService(ISegmentoProductoRepository segmentoproducto_repository)
        {
            this.segmentoproducto_repository = segmentoproducto_repository;
        }

        public TransactionResult create(SegmentoProductoVo segmentoproducto_vo, User user_log)
        {
            SegmentoProducto segmentoproducto = SegmentoProductoAdapter.voToObject(segmentoproducto_vo);
            return segmentoproducto_repository.create(segmentoproducto);
        }

        public TransactionResult delete(int id)
        {
            return segmentoproducto_repository.delete(id);
        }

        public SegmentoProducto detail(int id)
        {
            return segmentoproducto_repository.detail(id);
        }

        public IList<SegmentoProducto> getAll()
        {
            return segmentoproducto_repository.getAll();
        }

        public TransactionResult update(SegmentoProductoVo segmentoproducto_vo)
        {
            return segmentoproducto_repository.update(SegmentoProductoAdapter.voToObject(segmentoproducto_vo));
        }
    }
}