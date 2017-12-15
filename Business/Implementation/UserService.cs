using Business.Interface;
using System.Collections.Generic;
using Models.Auth;
using Data.Interface;
using Models.VOs;
using Warrior.Handlers.Enums;
using Business.Adapters;

namespace Business.Implementation
{
    /// <summary>
    /// User service implementation
    /// </summary>
    public class UserService : IUserService, IAuthenticationService
    {
        private IUserRepository user_repository;
        private IAuthenticationRepository authentication_repository;

        /// <summary>
        /// Constructor function
        /// </summary>
        /// <param name="user_repository">The user data source</param>
        public UserService(IUserRepository user_repository, IAuthenticationRepository authentication_repository) {
            this.user_repository = user_repository;
            this.authentication_repository = authentication_repository;
        }

        /// <summary>
        /// Get all the objects from the repostitory
        /// </summary>
        /// <returns></returns>
        public IList<User> getAll(int sistema)
        {
            return user_repository.getAll(sistema);
        }

        /// <summary>
        /// Get object by id
        /// </summary>
        /// <param name="id"> pirmary field of the model </param>
        /// <returns></returns>
        public AuthModel detail(int id, int sistema)
        {
            User temp_user = user_repository.detail(id, sistema);
            if (temp_user != null)
            {
                return authentication_repository.validateUser(user_repository.detail(id, sistema).username, sistema);
            } return null;
        }

        /// <summary>
        /// Create object on repository
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public TransactionResult create(UserVo user, int sistema)
        {
            TransactionResult user_tr = user_repository.create(UserAdapter.voToObject(user), sistema);
            if (user_tr == TransactionResult.CREATED) {
                return authentication_repository.create(new AuthModel
                {
                    rol = new Rol
                    {
                        id = user.rol
                    },
                    user = user_repository.getUserByUserName(user.username, sistema),

                }, sistema);
            }return user_tr;
        }

        /// <summary>
        /// Updates object on repository
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public TransactionResult update(UserVo user, int sistema)
        {
            return user_repository.update(UserAdapter.voToObject(user), sistema);
        }

        /// <summary>
        /// Delete object on the repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransactionResult delete(int id, int sistema)
        {
            TransactionResult tr_auth = authentication_repository.deleteAuth(id, sistema);
            if (tr_auth == TransactionResult.DELETED) {
                return user_repository.delete(id, sistema);
            }return tr_auth;
        }

        /// <summary>
        /// Validates user getting it from the repository by username
        /// then validates the password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AuthModel validateUser(string username, string password, int sistema)
        {
            // Get the user auth model by username
            AuthModel authentication_model = authentication_repository.validateUser(username, sistema);
            // then validate the password here with the encrryption method (TODO)
            if (authentication_model != null)
            {
                return password.Equals(authentication_model.user.password) ? authentication_model : null;
            }return null;
        }

        /// <summary>
        /// Get all the roles from the repository
        /// </summary>
        /// <returns>A list of rols</returns>
        public IList<Rol> getAllRols(int sistema)
        {
            return authentication_repository.getAllRols(sistema);
        }

        /// <summary>
        /// Get all the objects from the repostitory
        /// </summary>
        /// <returns></returns>
        public IList<User> getAllDespachadores()
        {
            return user_repository.getAllDespachadores();
        }
    }
}