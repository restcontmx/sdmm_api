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
    public class TipoMaquinariaService : ITipoMaquinariaService
    {
        private ITipoMaquinariaRepository tipomaquinaria_repository;

        public TipoMaquinariaService(ITipoMaquinariaRepository tipomaquinaria_repository)
        {
            this.tipomaquinaria_repository = tipomaquinaria_repository;
        }

        public TransactionResult create(TipoMaquinariaVo tipomaquinaria_vo)
        {
            TipoMaquinaria tipomaquinaria = TipoMaquinariaAdapter.voToObject(tipomaquinaria_vo);
            return tipomaquinaria_repository.create(tipomaquinaria);
        }

        public TransactionResult delete(int id)
        {
            return tipomaquinaria_repository.delete(id);
        }

        public TipoMaquinaria detail(int id)
        {
            return tipomaquinaria_repository.detail(id);
        }

        public IList<TipoMaquinaria> getAll()
        {
            return tipomaquinaria_repository.getAll();
        }

        public TransactionResult update(TipoMaquinariaVo tipomaquinaria_vo)
        {
            return tipomaquinaria_repository.update(TipoMaquinariaAdapter.voToObject(tipomaquinaria_vo));
        }
    }
}