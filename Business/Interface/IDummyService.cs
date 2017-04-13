using Models.Dummy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    /// <summary>
    /// Dummy service definition
    /// </summary>
    public interface IDummyService
    {
        /// <summary>
        /// Gets all objects from repository
        /// </summary>
        /// <returns>List of objects</returns>
        IList<Dummy> getAll();

        /// <summary>
        /// Gets the object detail with id
        /// </summary>
        /// <param name="id">Integer value for id field</param>
        /// <returns>Dummy object</returns>
        Dummy detail(int id);

        /// <summary>
        /// Creates object on the repository
        /// </summary>
        /// <param name="dummy">Dummy object</param>
        /// <returns>Transaction Result; success case should be CREATED</returns>
        TransactionResult create(Dummy dummy);

        /// <summary>
        /// Update object on the repository
        /// </summary>
        /// <param name="dummy">Dummy object</param>
        /// <returns>Transaction Result; success case should be UPDATED</returns>
        TransactionResult update(Dummy dummy);

        /// <summary>
        /// Deletes object on the respository
        /// </summary>
        /// <param name="id">Integer value for id field in the db</param>
        /// <returns>Transaction Result; success case should be DELETED</returns>
        TransactionResult delete(int id);

    } // End of Dummy service definition
}
