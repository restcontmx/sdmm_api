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
    public class ProductoService : IProductoService
    {
        public IProductoRepository producto_repository;
        public ProductoService(IProductoRepository producto_repository) {
            this.producto_repository = producto_repository;
        }

        public TransactionResult create(ProductoVo producto_vo, User user_log)
        {
            Producto obj = ProductoAdapter.voToObject(producto_vo);
            obj.user = user_log;
            return producto_repository.create(obj);
        }

        public TransactionResult delete(int id)
        {
            return producto_repository.delete(id);
        }

        public Producto detail(int id)
        {
            return producto_repository.detail(id);
        }

        public IList<Producto> getAll()
        {
            return producto_repository.getAll();
        }

        public TransactionResult update(ProductoVo producto_vo)
        {
            return producto_repository.update(ProductoAdapter.voToObject(producto_vo));
        }
    }
}