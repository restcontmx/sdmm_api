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
    public class NivelService : INivelService
    {
        private INivelRepository nivel_repository;
        public NivelService(INivelRepository nivel_repository) {
            this.nivel_repository = nivel_repository;
        }
        public TransactionResult create(NivelVo nivel_vo, User user_log)
        {
            Nivel obj = NivelAdapter.voToObject(nivel_vo);
            obj.user = user_log;
            return nivel_repository.create(obj);
        }

        public TransactionResult delete(int id)
        {
            return nivel_repository.delete(id);
        }

        public Nivel detail(int id)
        {
            return nivel_repository.detail(id);
        }

        public IList<Nivel> getAll()
        {
            return nivel_repository.getAll();
        }

        public TransactionResult update(NivelVo nivel_vo)
        {
            return nivel_repository.update(NivelAdapter.voToObject(nivel_vo));
        }
    }
}