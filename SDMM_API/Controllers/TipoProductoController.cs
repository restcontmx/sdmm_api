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
    public class TipoProductoController : BasicApiController
    {
        private ITipoProductoService tipoproducto_service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tipoproducto_service"></param>
        public TipoProductoController(ITipoProductoService tipoproducto_service)
        {
            this.tipoproducto_service = tipoproducto_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/tipoproducto/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                IDictionary<string, IList<TipoProducto>> data = new Dictionary<string, IList<TipoProducto>>();
                data.Add("data", tipoproducto_service.getAll(1));
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
        [Route("api/tipoproducto/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            TipoProducto tipoproducto = tipoproducto_service.detail(id, 1);
            if (tipoproducto != null)
            {
                IDictionary<string, TipoProducto> data = new Dictionary<string, TipoProducto>();
                data.Add("data", tipoproducto);
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
        [Route("api/tipoproducto/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] TipoProductoVo tipoproducto_vo)
        {
            TransactionResult tr = tipoproducto_service.create(tipoproducto_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) }, 1);
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
        [Route("api/tipoproducto/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] TipoProductoVo tipoproducto_vo)
        {
            TransactionResult tr = tipoproducto_service.update(tipoproducto_vo, 1);
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
        [Route("api/tipoproducto/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            TransactionResult tr = tipoproducto_service.delete(id, 1);
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


        //-------------------------------------------------------------------------//
        //----------- SECCIÓN PARA SOPORTE DEL SISTEMA DE COMBUSTIBLES ------------//
        //-------------------------------------------------------------------------//

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/combustible/tipoproducto/")]
        [HttpGet]
        public HttpResponseMessage listTiposCombustible()
        {
            try
            {
                IDictionary<string, IList<TipoProducto>> data = new Dictionary<string, IList<TipoProducto>>();
                data.Add("data", tipoproducto_service.getAll(2));
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
        [Route("api/combustible/tipoproducto/{id}")]
        [HttpGet]
        public HttpResponseMessage detailTiposCombustible(int id)
        {
            TipoProducto tipoproducto = tipoproducto_service.detail(id, 2);
            if (tipoproducto != null)
            {
                IDictionary<string, TipoProducto> data = new Dictionary<string, TipoProducto>();
                data.Add("data", tipoproducto);
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
        [Route("api/combustible/tipoproducto/")]
        [HttpPost]
        public HttpResponseMessage createTiposCombustible([FromBody] TipoProductoVo tipoproducto_vo)
        {
            TransactionResult tr = tipoproducto_service.create(tipoproducto_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) }, 2);
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
        [Route("api/combustible/tipoproducto/")]
        [HttpPut]
        public HttpResponseMessage updateTiposCombustible([FromBody] TipoProductoVo tipoproducto_vo)
        {
            TransactionResult tr = tipoproducto_service.update(tipoproducto_vo, 2);
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
        [Route("api/combustible/tipoproducto/{id}")]
        [HttpDelete]
        public HttpResponseMessage deleteTiposCombustible(int id)
        {
            TransactionResult tr = tipoproducto_service.delete(id, 2);
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
