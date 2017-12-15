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
    public class TipoMaquinariaController : BasicApiController
    {
        private ITipoMaquinariaService tipomaquinaria_service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tipomaquinaria_service"></param>
        public TipoMaquinariaController(ITipoMaquinariaService tipomaquinaria_service)
        {
            this.tipomaquinaria_service = tipomaquinaria_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/tipomaquinaria/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                IDictionary<string, IList<TipoMaquinaria>> data = new Dictionary<string, IList<TipoMaquinaria>>();
                data.Add("data", tipomaquinaria_service.getAll());
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
        [Route("api/tipomaquinaria/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            TipoMaquinaria tipomaquinaria = tipomaquinaria_service.detail(id);
            if (tipomaquinaria != null)
            {
                IDictionary<string, TipoMaquinaria> data = new Dictionary<string, TipoMaquinaria>();
                data.Add("data", tipomaquinaria);
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
        /// <param name="tipoproducto_vo"></param>
        /// <returns></returns>
        [Route("api/tipomaquinaria/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] TipoMaquinariaVo tipomaquinaria_vo)
        {
            TransactionResult tr = tipomaquinaria_service.create(tipomaquinaria_vo);
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
        /// <param name="tipoproducto_vo"></param>
        /// <returns></returns>
        [Route("api/tipomaquinaria/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] TipoMaquinariaVo tipomaquinaria_vo)
        {
            TransactionResult tr = tipomaquinaria_service.update(tipomaquinaria_vo);
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
        [Route("api/tipomaquinaria/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            TransactionResult tr = tipomaquinaria_service.delete(id);
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
