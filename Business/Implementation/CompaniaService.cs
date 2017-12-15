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
    public class CompaniaService : ICompaniaService
    {
        private ICompaniaRepository compania_repository;
        public CompaniaService(ICompaniaRepository compania_repository)
        {
            this.compania_repository = compania_repository;
        }

        public TransactionResult create(CompaniaVo compania_vo, User user_log, int sistema)
        {
            Compania obj = CompaniaAdapter.voToObject(compania_vo);
            obj.user = user_log;
            return compania_repository.create(obj, sistema);
        }

        public TransactionResult delete(int id, int sistema)
        {
            return compania_repository.delete(id, sistema);
        }

        public Compania detail(int id, int sistema)
        {
            return compania_repository.detail(id, sistema);
        }

        public IList<Compania> getAll(int sistema)
        {
            return compania_repository.getAll(sistema);
        }

        public TransactionResult update(CompaniaVo compania_vo, int sistema)
        {
            return compania_repository.update(CompaniaAdapter.voToObject(compania_vo), sistema);
        }
    }
}