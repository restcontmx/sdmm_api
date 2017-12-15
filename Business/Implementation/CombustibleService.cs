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
    public class CombustibleService : ICombustibleService
    {
        private ICombustibleRepository combustible_repository;

        public CombustibleService(ICombustibleRepository combustible_repository)
        {
            this.combustible_repository = combustible_repository;
        }

        public TransactionResult create(CombustibleVo combustible_vo)
        {
            Combustible combustible = CombustibleAdapter.voToObject(combustible_vo);
            return combustible_repository.create(combustible);
        }

        public TransactionResult delete(int id)
        {
            return combustible_repository.delete(id);
        }

        public Combustible detail(int id)
        {
            return combustible_repository.detail(id);
        }

        public IList<Combustible> getAll()
        {
            return combustible_repository.getAll();
        }

        public TransactionResult update(CombustibleVo combustible_vo)
        {
            return combustible_repository.update(CombustibleAdapter.voToObject(combustible_vo));
        }
    }
}