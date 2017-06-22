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
    public class SubNivelService : ISubNivelService
    {
        private ISubNivelRepository subnivel_repository;

        public SubNivelService(ISubNivelRepository subnivel_repository) {
            this.subnivel_repository = subnivel_repository;
        }

        public TransactionResult create(SubNivelVo subnivel_vo, User user_log)
        {
            SubNivel obj = SubNivelAdapter.voToObject(subnivel_vo);
            obj.user = user_log;
            return subnivel_repository.create( obj );
        }

        public TransactionResult delete(int id)
        {
            return subnivel_repository.delete(id);
        }

        public SubNivel detail(int id)
        {
            return subnivel_repository.detail(id);
        }

        public IList<SubNivel> getAll()
        {
            return subnivel_repository.getAll();
        }

        public TransactionResult update(SubNivelVo subnivel_vo, User user_log)
        {
            SubNivel obj = SubNivelAdapter.voToObject(subnivel_vo);
            obj.user = user_log;

            return subnivel_repository.update(obj);
        }
    }
}