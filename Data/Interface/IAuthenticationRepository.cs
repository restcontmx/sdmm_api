using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
