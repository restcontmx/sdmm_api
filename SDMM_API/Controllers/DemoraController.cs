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
    public class DemoraController : BasicApiController
    {
        private IDemoraService demora_service;
        public DemoraController(IDemoraService demora_service)
        {
            this.demora_service = demora_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/demora/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                IDictionary<string, IList<Demora>> data = new Dictionary<string, IList<Demora>>();
                data.Add("data", demora_service.getAll());
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
        [Route("api/demora/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            Demora demora = demora_service.detail(id);
            if (demora != null)
            {
                IDictionary<string, Demora> data = new Dictionary<string, Demora>();
                data.Add("data", demora);
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
        /// <param name="empleado_vo"></param>
        /// <returns></returns>
        [Route("api/demora/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] DemoraVo demora_vo)
        {
            TransactionResult tr = demora_service.create(demora_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
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
        /// <param name="empleado_vo"></param>
        /// <returns></returns>
        [Route("api/demora/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] DemoraVo demora_vo)
        {
            TransactionResult tr = demora_service.update(demora_vo);
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
        [Route("api/demora/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            TransactionResult tr = demora_service.delete(id);
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
