using Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    /// <summary>
    /// Proveedor repository definition
    /// </summary>
    public interface IProveedorRepository
    {
        /// <summary>
        /// Get all objects from db
        /// </summary>
        /// <returns></returns>
        IList<Proveedor> getAll();

        /// <summary>
        /// Create new object on the db
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        TransactionResult create(Proveedor proveedor);

        /// <summary>
        /// Update object on the db
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        TransactionResult update(Proveedor proveedor);

        /// <summary>
        /// Retrieve object by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Proveedor detail(int id);

        /// <summary>
        /// Delete object by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TransactionResult delete(int id);
    }
}
