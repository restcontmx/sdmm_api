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
    public class CuentaService : ICuentaService
    {
        private ICuentaRepository Cuenta_repository;
        public CuentaService(ICuentaRepository Cuenta_repository)
        {
            this.Cuenta_repository = Cuenta_repository;
        }
        public TransactionResult create(CuentaVo Cuenta_vo, User user_log, int sistema)
        {
            Cuenta obj = CuentaAdapter.voToObject(Cuenta_vo);
            obj.user = user_log;
            return Cuenta_repository.create(obj, sistema);
        }

        public TransactionResult delete(int id, int sistema)
        {
            return Cuenta_repository.delete(id, sistema);
        }

        public Cuenta detail(int id, int sistema)
        {
            return Cuenta_repository.detail(id, sistema);
        }

        public IList<Cuenta> getAll(int sistema)
        {
            return Cuenta_repository.getAll(sistema);
        }

        public TransactionResult update(CuentaVo Cuenta_vo, int sistema)
        {
            return Cuenta_repository.update(CuentaAdapter.voToObject(Cuenta_vo), sistema);
        }
    }
}