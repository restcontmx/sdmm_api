using Data.Interface;
using Models.Dummy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Warrior.Handlers.Enums;

namespace Data.Implementation
{

    /// <summary>
    /// Implementation of the dummy repository
    /// </summary>
    public class DummyRepository : IDummyRepository
    {

        private IList<Dummy> dummies;

        /// <summary>
        /// Constructor
        /// </summary>
        public DummyRepository() {

            dummies = new List<Dummy>();

            Dummy d1 = new Dummy();
            Dummy d2 = new Dummy();
            Dummy d3 = new Dummy();

            dummies.Add(d1);
            dummies.Add(d2);
            dummies.Add(d3);

        }

        /// <summary>
        /// Creates an object on the db
        /// </summary>
        /// <param name="dummy"></param>
        /// <returns>Transaction Result; success case should be CREATED</returns>
        public TransactionResult create(Dummy dummy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an object on the db with id
        /// </summary>
        /// <param name="id">Integer for id field on the db</param>
        /// <returns>Transaction result; success case should be DELETED</returns>
        public TransactionResult delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an object on the db
        /// </summary>
        /// <param name="dummy"></param>
        /// <returns>Transaction result; success case should be UPDATED</returns>
        public TransactionResult update(Dummy dummy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves an object detail from the db
        /// </summary>
        /// <param name="id">Integer for id filed on the db</param>
        /// <returns>Dummy object</returns>
        public Dummy detail(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all objects from data base
        /// </summary>
        /// <returns>List of objects</returns>
        public IList<Dummy> getAll()
        {
            return dummies;
        }

    } // End of Dummy repository implementation
}