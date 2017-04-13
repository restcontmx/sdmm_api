using Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Dummy;
using Warrior.Handlers.Enums;
using Data.Interface;

namespace Business.Implementation
{
    /// <summary>
    /// Dummy service implementation
    /// </summary>
    public class DummyService : IDummyService
    {
        private IDummyRepository dummy_repository;

        /// <summary>
        /// Dummy constructor
        /// </summary>
        /// <param name="dummy_repository">Dependency from dummy repo</param>
        public DummyService( IDummyRepository dummy_repository ) {
            this.dummy_repository = dummy_repository;
        }
        
        /// <summary>
        /// Creates object on the repository
        /// </summary>
        /// <param name="dummy">Dummy object</param>
        /// <returns>Transaction Result; success case should be CREATED</returns>
        public TransactionResult create(Dummy dummy)
        {
            return dummy_repository.create(dummy);
        }

        /// <summary>
        /// Deletes object on the respository
        /// </summary>
        /// <param name="id">Integer value for id field in the db</param>
        /// <returns>Transaction Result; success case should be DELETED</returns>
        public TransactionResult delete(int id)
        {
            return dummy_repository.delete(id);
        }

        /// <summary>
        /// Gets the object detail with id
        /// </summary>
        /// <param name="id">Integer value for id field</param>
        /// <returns>Dummy object</returns>
        public Dummy detail(int id)
        {
            return dummy_repository.detail(id);
        }

        /// <summary>
        /// Gets all objects from repository
        /// </summary>
        /// <returns>List of objects</returns>
        public IList<Dummy> getAll()
        {
            return dummy_repository.getAll();
        }

        /// <summary>
        /// Update object on the repository
        /// </summary>
        /// <param name="dummy">Dummy object</param>
        /// <returns>Transaction Result; success case should be UPDATED</returns>
        public TransactionResult update(Dummy dummy)
        {
            return dummy_repository.update(dummy);
        }

    } // End of Dummy service implementation
}