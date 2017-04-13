using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IAuthenticationService
    {
        AuthModel validateUser(string username, string password );
    }
}
