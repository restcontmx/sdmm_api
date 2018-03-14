using Models.Auth;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class UserAdapter
    {
        public static UserVo objectToVo(User obj) {
            return new UserVo {
                id = obj.id,
                username = obj.username,
                first_name = obj.first_name,
                second_name = obj.second_name,
                password = obj.password,
                email = obj.email,
                timestamp = obj.timestamp.ToString(),
                updated = obj.timestamp.ToString(),
                rol = obj.rol.id
                
            };
        }

        public static User voToObject( UserVo vo )
        {
            return new User {
                id = vo.id,
                username = vo.username,
                first_name = vo.first_name,
                second_name = vo.second_name,
                password = vo.password,
                email = vo.email,
                rol = new Rol { id = vo.rol }
            };
        }
    }
}