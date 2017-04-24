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
    public class PresupuestoService : IPresupuestoService
    {
        private IPresupuestoRepository presupuesto_repository;
        public PresupuestoService(IPresupuestoRepository presupuesto_repository) {
            this.presupuesto_repository = presupuesto_repository;
        }
        public TransactionResult create(PresupuestoVo presupuesto_vo, User user_log)
        {
            Presupuesto obj = PresupuestoAdapter.voToObject(presupuesto_vo);
            obj.user = user_log;
            return presupuesto_repository.create(obj);
        }

        public TransactionResult delete(int id)
        {
            return presupuesto_repository.delete(id);
        }

        public Presupuesto detail(int id)
        {
            return presupuesto_repository.detail(id);
        }

        public IList<Presupuesto> getAll()
        {
            return presupuesto_repository.getAll();
        }

        public TransactionResult update(PresupuestoVo presupuesto_vo)
        {
            return presupuesto_repository.update( PresupuestoAdapter.voToObject( presupuesto_vo ) );
        }
    }
}