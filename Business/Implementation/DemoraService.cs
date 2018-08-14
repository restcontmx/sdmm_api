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
    public class DemoraService : IDemoraService
    {
        private IDemoraRepository demora_repository;

        public DemoraService(IDemoraRepository demora_repository)
        {
            this.demora_repository = demora_repository;
        }

        public TransactionResult create(DemoraVo demora_vo, User user_log)
        {
            Demora demora = DemoraAdapter.voToObject(demora_vo);
            demora.user = user_log;
            return demora_repository.create(demora);
        }

        public TransactionResult delete(int id)
        {
            return demora_repository.delete(id);
        }

        public Demora detail(int id)
        {
            return demora_repository.detail(id);
        }

        public IList<Demora> getAll()
        {
            return demora_repository.getAll();
        }

        public TransactionResult update(DemoraVo demora_vo)
        {
            return demora_repository.update(DemoraAdapter.voToObject(demora_vo));
        }
    }
}