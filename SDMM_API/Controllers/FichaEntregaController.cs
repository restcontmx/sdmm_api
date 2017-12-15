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
    public class FichaEntregaController : BasicApiController
    {
        private IFichaEntregaService ficha_service;
        public FichaEntregaController(IFichaEntregaService ficha_service)
        {
            this.ficha_service = ficha_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/ficha/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                IDictionary<string, IList<FichaEntregaRecepcion>> data = new Dictionary<string, IList<FichaEntregaRecepcion>>();
                data.Add("data", ficha_service.getAll());
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
        [Route("api/ficha/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            FichaEntregaRecepcion ficha = ficha_service.detail(id);
            if (ficha != null)
            {
                IDictionary<string, FichaEntregaRecepcion> data = new Dictionary<string, FichaEntregaRecepcion>();
                data.Add("data", ficha);
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
        [Route("api/ficha/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] FichaEntregaRecepcionVo ficha_vo)
        {
            TransactionResult tr = ficha_service.create(ficha_vo);
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
        [Route("api/ficha/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] FichaEntregaRecepcionVo ficha_vo)
        {
            TransactionResult tr = ficha_service.update(ficha_vo);
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
        [Route("api/ficha/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            TransactionResult tr = ficha_service.delete(id);
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