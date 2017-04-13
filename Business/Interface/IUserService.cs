using Models.Auth;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warrior.Handlers.Enums;

namespace Business.Interface
{
    /// <summary>
    /// User service definition
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get all objects from repository
        /// </summary>
        /// <returns></returns>
        IList<User> getAll();

        /// <summary>
        /// Get object by id
        /// </summary>
        /// <param name="id"> primary field on the db</param>
        /// <returns></returns>
        User detail(int id);

        /// <summary>
        /// Creates object on repository
        /// </summary>
        /// <param name="user">user void object</param>
        /// <returns></returns>
        TransactionResult create(UserVo user);

        /// <summary>
        /// Updates object on repostiory
        /// </summary>
        /// <param name="user">user void object</param>
        /// <returns></returns>
        TransactionResult update(UserVo user);

        /// <summary>
        /// Delete object on repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TransactionResult delete(int id);
    }
}
