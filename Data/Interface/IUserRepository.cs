using Models.Auth;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    /// <summary>
    /// User repository definition
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get all objects from data base
        /// </summary>
        /// <returns>List of objects</returns>
        IList<User> getAll(int sistema);

        /// <summary>
        /// Creates an object on the db
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Transaction Result; success case should be CREATED</returns>
        TransactionResult create(User user, int sistema);

        /// <summary>
        /// Updates an object on the db
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Transaction result; success case should be UPDATED</returns>
        TransactionResult update(User user, int sistema);

        /// <summary>
        /// Retrieves an object detail from the db
        /// </summary>
        /// <param name="id">Integer for id filed on the db</param>
        /// <returns>Dummy object</returns>
        User detail(int id, int sistema);

        /// <summary>
        /// Deletes an object on the db with id
        /// </summary>
        /// <param name="id">Integer for id field on the db</param>
        /// <returns>Transaction result; success case should be DELETED</returns>
        TransactionResult delete(int id, int sistema);

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        User getUserByUserName(string username, int sistema);


        /// <summary>
        /// Get all objects from data base
        /// </summary>
        /// <returns>List of objects</returns>
        IList<User> getAllDespachadores();
    }
}
