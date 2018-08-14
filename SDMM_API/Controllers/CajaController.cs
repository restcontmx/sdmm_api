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
    public class CajaController : BasicApiController
    {
        public ICajaService caja_service;

        /// <summary>
        /// Caja Controller constructor
        /// </summary>
        /// <param name="caja_service"></param>
        public CajaController(ICajaService caja_service) {
            this.caja_service = caja_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/caja/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                IDictionary<string, IList<Caja>> data = new Dictionary<string, IList<Caja>>();
                data.Add("data", caja_service.getAll());
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
        /// Get all cajas activas
        /// </summary>
        /// <returns></returns>
        [Route("api/caja/activas")]
        [HttpGet]
        public HttpResponseMessage listActivas()
        {
            try
            {
                IDictionary<string, IList<Caja>> data = new Dictionary<string, IList<Caja>>();
                IList<Caja> cajas = new List<Caja>();

                cajas = caja_service.getAll().Where(p => p.cantidad > 0 && p.active == true).ToList().OrderByDescending(a => a.id).ToList();
                data.Add("data", cajas);
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
        [Route("api/caja/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            Caja empleado = caja_service.detail(id);
            if (empleado != null)
            {
                IDictionary<string, Caja> data = new Dictionary<string, Caja>();
                data.Add("data", empleado);
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
        /// <param name="caja_vo"></param>
        /// <returns></returns>
        [Route("api/caja/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] CajaVo caja_vo)
        {
            TransactionResult tr = caja_service.create(caja_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
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
        /// <param name="caja_vo"></param>
        /// <returns></returns>
        [Route("api/caja/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] CajaVo caja_vo)
        {
            TransactionResult tr = caja_service.update(caja_vo);
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
        /// Update object request
        /// </summary>
        /// <param name="caja_vo"></param>
        /// <returns></returns>
        [Route("api/caja/cantidad/")]
        [HttpPut]
        public HttpResponseMessage updateCantidad([FromBody] CajaVo caja_vo)
        {
            TransactionResult tr = caja_service.updateCantidad(caja_vo);

            IDictionary<string, string> data = new Dictionary<string, string>();
            if (tr == TransactionResult.OK)
            {
                data.Add("message", "Object updated.");
                data.Add("status", "1");
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
        [Route("api/caja/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            TransactionResult tr = caja_service.delete(id);
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


        /// <summary>
        /// Create object pettition
        /// </summary>
        /// <param name="obs_vo"></param>
        /// <returns></returns>
        [Route("api/caja/observacion/")]
        [HttpPost]
        public HttpResponseMessage createObservacion([FromBody] ObservacionVo obs_vo)
        {
            TransactionResult tr = caja_service.createObservacion(obs_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
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
    }
}
