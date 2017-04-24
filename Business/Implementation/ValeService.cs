using Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using Warrior.Handlers.Enums;
using Data.Interface;
using Business.Adapters;

namespace Business.Implementation
{
    public class ValeService : IValeService
    {
        private IValeRepository vale_repository;

        public ValeService(IValeRepository vale_repository) {
            this.vale_repository = vale_repository;
        }

        public TransactionResult create(ValeVo vale_vo, User user_log)
        {
            Vale vale = ValeAdapter.voToObject(vale_vo);
            vale.user = user_log;
            int id = vale_repository.create(vale);
            if (id > 0) {
                var tr = TransactionResult.CREATED;
                foreach (DetalleValeVo dvo in vale_vo.detalles)
                {
                    dvo.vale_id = id;
                    tr = vale_repository.createDetalle( DetalleValeAdapter.voToObject(dvo) );
                    if (tr != TransactionResult.CREATED) {
                        return tr;
                    }
                }return tr;
            }return TransactionResult.ERROR;
        }

        public TransactionResult delete(int id)
        {
            throw new NotImplementedException();
        }

        public Vale detail(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Vale> getAll()
        {
            return vale_repository.getAll();
        }

        public TransactionResult update(ValeVo vale_vo)
        {
            throw new NotImplementedException();
        }
    }
}