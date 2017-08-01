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
    public class SegmentoProductoController : BasicApiController
    {
        private ISegmentoProductoService segmentoproducto_service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="segmentoproducto_service"></param>
        public SegmentoProductoController(ISegmentoProductoService segmentoproducto_service)
        {
            this.segmentoproducto_service = segmentoproducto_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/segmentoproducto/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                IDictionary<string, IList<SegmentoProducto>> data = new Dictionary<string, IList<SegmentoProducto>>();
                data.Add("data", segmentoproducto_service.getAll());
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
        [Route("api/segmentoproducto/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            SegmentoProducto segmentoproducto = segmentoproducto_service.detail(id);
            if (segmentoproducto != null)
            {
                IDictionary<string, SegmentoProducto> data = new Dictionary<string, SegmentoProducto>();
                data.Add("data", segmentoproducto);
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
        /// <param name="segmentoproducto_vo"></param>
        /// <returns></returns>
        [Route("api/segmentoproducto/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] SegmentoProductoVo segmentoproducto_vo)
        {
            TransactionResult tr = segmentoproducto_service.create(segmentoproducto_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
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
        /// <param name="segmentoproducto_vo"></param>
        /// <returns></returns>
        [Route("api/segmentoproducto/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] SegmentoProductoVo segmentoproducto_vo)
        {
            TransactionResult tr = segmentoproducto_service.update(segmentoproducto_vo);
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
        [Route("api/segmentoproducto/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            TransactionResult tr = segmentoproducto_service.delete(id);
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