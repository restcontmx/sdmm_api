using Business.Interface;
using Models.Auth;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Warrior.Handlers.Enums;

namespace SDMM_API.Controllers
{
    /// <summary>
    /// User Controller routes
    /// </summary>
    public class UserController : BasicApiController
    {
        private IUserService user_service;

        /// <summary>
        /// User Controller constructor
        /// </summary>
        /// <param name="user_service"></param>
        public UserController(IUserService user_service) {
            this.user_service = user_service;
        }

        /// <summary>
        /// User list
        /// </summary>
        /// <returns></returns>
        [Route("api/user")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                IDictionary<string, IList<User>> data = new Dictionary<string, IList<User>>();
                data.Add("data", user_service.getAll());
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch( Exception e ){
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format( "There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        /// <summary>
        /// Retrieve object request
        /// </summary>
        /// <param name="id">primary field on the db</param>
        /// <returns></returns>
        [Route("api/user/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            AuthModel user = user_service.detail(id);
            if (user != null)
            {
                IDictionary<string, AuthModel> data = new Dictionary<string, AuthModel>();
                data.Add("data", user);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            else
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", "Object not found.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        /// <summary>
        /// Create new object request
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("api/user/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] UserVo user) {
            TransactionResult tr = user_service.create(user);
            IDictionary<string, string> data = new Dictionary<string, string>();
            if (tr == TransactionResult.CREATED)
            {
                data.Add("message", "Object created.");
                return Request.CreateResponse(HttpStatusCode.Created, data);
            }
            else if (tr == TransactionResult.EXISTS)
            {
                data.Add("message", "Object already existed.");
                return Request.CreateResponse(HttpStatusCode.Conflict, data);
            }
            else
            {
                data.Add("message", "There was an error attending your request.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        /// <summary>
        /// Updates object request
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("api/user/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] UserVo user) {
            TransactionResult tr = user_service.update(user);
            IDictionary<string, string> data = new Dictionary<string, string>();
            if (tr == TransactionResult.OK)
            {
                data.Add("message", "Object updated.");
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            else
            {
                data.Add("message", "There was an error attending your request.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        /// <summary>
        /// Delete object request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/user/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id) {
            TransactionResult tr = user_service.delete(id);
            IDictionary<string, string> data = new Dictionary<string, string>();
            if (tr == TransactionResult.DELETED)
            {
                data.Add("message", "Object deleted.");
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            else
            {
                data.Add("message", "There was an error attending your request.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

    }
}
