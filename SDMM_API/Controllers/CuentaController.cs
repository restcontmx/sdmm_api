using Business.Interface;
using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Warrior.Handlers.Enums;

namespace SDMM_API.Controllers
{
    public class CuentaController : BasicApiController
    {
        private ICuentaService cuenta_service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cuenta_service"></param>
        public CuentaController(ICuentaService cuenta_service)
        {
            this.cuenta_service = cuenta_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/cuenta/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                IDictionary<string, IList<Cuenta>> data = new Dictionary<string, IList<Cuenta>>();
                data.Add("data", cuenta_service.getAll());
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }

        /// <summary>
        /// Retrieve object request
        /// </summary>
        /// <param name="id">primary field on the db</param>
        /// <returns></returns>
        [Route("api/cuenta/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            Cuenta cuenta = cuenta_service.detail(id);
            if (cuenta != null)
            {
                IDictionary<string, Cuenta> data = new Dictionary<string, Cuenta>();
                data.Add("data", cuenta);
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
        /// Create object pettition
        /// </summary>
        /// <param name="cuenta_vo"></param>
        /// <returns></returns>
        [Route("api/cuenta/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] CuentaVo cuenta_vo)
        {
            TransactionResult tr = cuenta_service.create(cuenta_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
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
        /// Update object request
        /// </summary>
        /// <param name="cuenta_vo"></param>
        /// <returns></returns>
        [Route("api/cuenta/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] CuentaVo cuenta_vo)
        {
            TransactionResult tr = cuenta_service.update(cuenta_vo);
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
        [Route("api/cuenta/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            TransactionResult tr = cuenta_service.delete(id);
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
