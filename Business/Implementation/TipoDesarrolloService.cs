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
    public class TipoDesarrolloService : ITipoDesarrolloService
    {
        private ITipoDesarrolloRepository tipodes_repository;

        public TipoDesarrolloService(ITipoDesarrolloRepository tipodes_repository)
        {
            this.tipodes_repository = tipodes_repository;
        }

        public TransactionResult create(TipoDesarrolloVo tipodes_vo)
        {
            TipoDesarrollo tipo_desarrollo = TipoDesarrolloAdapter.voToObject(tipodes_vo);
            return tipodes_repository.create(tipo_desarrollo);
        }

        public TransactionResult delete(int id)
        {
            return tipodes_repository.delete(id);
        }

        public TipoDesarrollo detail(int id)
        {
            return tipodes_repository.detail(id);
        }

        public IList<TipoDesarrollo> getAll()
        {
            return tipodes_repository.getAll();
        }

        public TransactionResult update(TipoDesarrolloVo tipodes_vo)
        {
            return tipodes_repository.update(TipoDesarrolloAdapter.voToObject(tipodes_vo));
        }
    }
}