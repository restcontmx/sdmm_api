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

        public TransactionResult create(CompaniaVo compania_vo, User user_log)
        {
            Compania obj = CompaniaAdapter.voToObject(compania_vo);
            obj.user = user_log;
            return compania_repository.create(obj);
        }

        public TransactionResult delete(int id)
        {
            return compania_repository.delete(id);
        }

        public Compania detail(int id)
        {
            return compania_repository.detail(id);
        }

        public IList<Compania> getAll()
        {
            return compania_repository.getAll();
        }

        public TransactionResult update(CompaniaVo compania_vo)
        {
            return compania_repository.update(CompaniaAdapter.voToObject(compania_vo));
        }
    }
}