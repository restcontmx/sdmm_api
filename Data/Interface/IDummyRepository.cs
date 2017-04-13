using Models.Dummy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    /// <summary>
    /// Definition of the dummy repository
    /// </summary>
    public interface IDummyRepository
    {
        /// <summary>
        /// Get all objects from data base
        /// </summary>
        /// <returns>List of objects</returns>
        IList<Dummy> getAll();

        /// <summary>
        /// Creates an object on the db
        /// </summary>
        /// <param name="dummy"></param>
        /// <returns>Transaction Result; success case should be CREATED</returns>
        TransactionResult create( Dummy dummy );

        /// <summary>
        /// Updates an object on the db
        /// </summary>
        /// <param name="dummy"></param>
        /// <returns>Transaction result; success case should be UPDATED</returns>
        TransactionResult update( Dummy dummy );

        /// <summary>
        /// Retrieves an object detail from the db
        /// </summary>
        /// <param name="id">Integer for id filed on the db</param>
        /// <returns>Dummy object</returns>
        Dummy detail( int id );

        /// <summary>
        /// Deletes an object on the db with id
        /// </summary>
        /// <param name="id">Integer for id field on the db</param>
        /// <returns>Transaction result; success case should be DELETED</returns>
        TransactionResult delete( int id );

    } // End of Dummy Repository definition
}
