using Business.Interface;
using Models.Catalogs;
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
    /// Proveedor controller routes
    /// </summary>
    public class ProveedorController : BasicApiController
    {
        private IProveedorService proveedor_service;

        /// <summary>
        /// Constructro inject service
        /// </summary>
        /// <param name="proveedor_service"></param>
        public ProveedorController(IProveedorService proveedor_service) {
            this.proveedor_service = proveedor_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/proveedor/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                IDictionary<string, IList<Proveedor>> data = new Dictionary<string, IList<Proveedor>>();
                data.Add("data", proveedor_service.getAll());
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
        [Route("api/proveedor/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            Proveedor proveedor = proveedor_service.detail(id);
            if (proveedor != null)
            {
                IDictionary<string, Proveedor> data = new Dictionary<string, Proveedor>();
                data.Add("data", proveedor);
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
        /// <param name="proveedor_vo"></param>
        /// <returns></returns>
        [Route("api/proveedor/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] ProveedorVo proveedor_vo)
        {
            TransactionResult tr = proveedor_service.create(proveedor_vo, new Models.Auth.User { id = int.Parse( RequestContext.Principal.Identity.Name ) } );
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
        /// <param name="proveedor_vo"></param>
        /// <returns></returns>
        [Route("api/proveedor/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] ProveedorVo proveedor_vo)
        {
            TransactionResult tr = proveedor_service.update(proveedor_vo);
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
        [Route("api/proveedor/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            TransactionResult tr = proveedor_service.delete(id);
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
