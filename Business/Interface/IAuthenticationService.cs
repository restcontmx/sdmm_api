using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    /// <summary>
    /// Authentication service definition
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Validates user credentials on the respository
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        AuthModel validateUser(string username, string password );

        /// <summary>
        /// Gets all the rols from the repository
        /// </summary>
        /// <returns></returns>
        IList<Rol> getAllRols();
    }
}
