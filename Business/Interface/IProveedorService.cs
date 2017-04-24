using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    public interface IProveedorService
    {
        /// <summary>
        /// Get all objects from the repository
        /// </summary>
        /// <returns></returns>
        IList<Proveedor> getAll();

        /// <summary>
        /// Create object on the repository
        /// </summary>
        /// <param name="proveedor"></param>
        /// <param name="user_log">User that creates the object</param>
        /// <returns></returns>
        TransactionResult create(ProveedorVo proveedor, User user_log );

        /// <summary>
        /// Detail object from the repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Proveedor detail(int id);

        /// <summary>
        /// Update object on the repository
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        TransactionResult update(ProveedorVo proveedor);

        /// <summary>
        /// Delete object on the repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TransactionResult delete(int id);
    }
}
