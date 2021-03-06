﻿using Models.Auth;
using Models.VOs;
using System.Collections.Generic;
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
        IList<User> getAll(int sistema);

        /// <summary>
        /// Get all objects from repository
        /// </summary>
        /// <returns></returns>
        IList<AuthModel> getAllWithRol(int sistema);

        /// <summary>
        /// Get object by id
        /// </summary>
        /// <param name="id"> primary field on the db</param>
        /// <returns></returns>
        AuthModel detail(int id, int sistema);

        /// <summary>
        /// Creates object on repository
        /// </summary>
        /// <param name="user">user void object</param>
        /// <returns></returns>
        TransactionResult create(UserVo user, int sistema);

        /// <summary>
        /// Updates object on repostiory
        /// </summary>
        /// <param name="user">user void object</param>
        /// <returns></returns>
        TransactionResult update(UserVo user, int sistema);

        /// <summary>
        /// Delete object on repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TransactionResult delete(int id, int sistema);


        /// <summary>
        /// Get all objects from repository
        /// </summary>
        /// <returns></returns>
        IList<User> getAllDespachadores();
    }
}
