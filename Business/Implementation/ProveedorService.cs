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
    /// <summary>
    /// Proveedor service implementation
    /// </summary>
    public class ProveedorService : IProveedorService
    {
        private IProveedorRepository proveedor_repository;

        /// <summary>
        /// Constructor injects repository
        /// </summary>
        /// <param name="proveedor_repository"></param>
        public ProveedorService(IProveedorRepository proveedor_repository) {
            this.proveedor_repository = proveedor_repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proveedor"></param>
        /// <param name="user_log"></param>
        /// <returns></returns>
        public TransactionResult create(ProveedorVo proveedor, User user_log)
        {
            Proveedor obj = ProveedorAdapter.voToObject(proveedor);
            obj.user = user_log;
            return proveedor_repository.create( obj );
        }

        /// <summary>
        /// Delete object on the repository by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransactionResult delete(int id)
        {
            return proveedor_repository.delete(id);
        }

        /// <summary>
        /// Retrieve object from the repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Proveedor detail(int id)
        {
            return proveedor_repository.detail(id);
        }

        /// <summary>
        /// Get all the objects
        /// </summary>
        /// <returns></returns>
        public IList<Proveedor> getAll()
        {
            return proveedor_repository.getAll();
        }

        /// <summary>
        /// Update object on the repository
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        public TransactionResult update(ProveedorVo proveedor)
        {
            return proveedor_repository.update(ProveedorAdapter.voToObject(proveedor));
        }
    }
}