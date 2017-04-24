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
    public class CategoriaService : ICategoriaService
    {
        private ICategoriaRepository categoria_repository;
        public CategoriaService(ICategoriaRepository categoria_repository) {
            this.categoria_repository = categoria_repository;
        }
        public TransactionResult create(CategoriaVo categoria_vo, User user_log)
        {
            Categoria obj = CategoriaAdapter.voToObject(categoria_vo);
            obj.user = user_log;
            return categoria_repository.create(obj);
        }

        public TransactionResult delete(int id)
        {
            return categoria_repository.delete(id);
        }

        public Categoria detail(int id)
        {
            return categoria_repository.detail(id);
        }

        public IList<Categoria> getAll()
        {
            return categoria_repository.getAll();
        }

        public TransactionResult update(CategoriaVo categoria_vo)
        {
            return categoria_repository.update(CategoriaAdapter.voToObject(categoria_vo));
        }
    }
}