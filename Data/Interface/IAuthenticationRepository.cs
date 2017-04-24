using Models.Auth;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    /// <summary>
    /// Authentication repository definition
    /// This will define auth functions
    /// </summary>
    public interface IAuthenticationRepository
    {
        /// <summary>
        /// Validates user existence and credentials
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Authentication model</returns>
        AuthModel validateUser(string username);

        /// <summary>
        /// Create new authentication model
        /// </summary>
        /// <param name="auth_model">Authentication model</param>
        /// <returns></returns>
        TransactionResult create(AuthModel auth_model);

        /// <summary>
        /// Gets all the rols
        /// </summary>
        /// <returns>A list of rols</returns>
        IList<Rol> getAllRols();

        /// <summary>
        /// Delete authentication register
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TransactionResult deleteAuth(int id);
    }
}
