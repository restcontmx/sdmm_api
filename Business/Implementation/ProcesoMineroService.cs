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
    public class ProcesoMineroService : IProcesoMineroService
    {
        private IProcesoMineroRepository procesominero_repository;
        public ProcesoMineroService(IProcesoMineroRepository procesominero_repository) {
            this.procesominero_repository = procesominero_repository;
        }
        public TransactionResult create(ProcesoMineroVo procesominero_vo, User user_log)
        {
            ProcesoMinero obj = ProcesoMineroAdapter.voToObject(procesominero_vo);
            obj.user = user_log;
            return procesominero_repository.create(obj);
        }

        public TransactionResult delete(int id)
        {
            return procesominero_repository.delete(id);
        }

        public ProcesoMinero detail(int id)
        {
            return procesominero_repository.detail(id);
        }

        public IList<ProcesoMinero> getAll()
        {
            return procesominero_repository.getAll();
        }

        public TransactionResult update(ProcesoMineroVo procesominero_vo)
        {
            return procesominero_repository.update(ProcesoMineroAdapter.voToObject(procesominero_vo));
        }
    }
}