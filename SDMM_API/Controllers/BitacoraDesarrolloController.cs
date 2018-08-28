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
    public class BitacoraDesarrolloController : BasicApiController
    {
        private IBitacoraDesarrolloService bitacora_service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="categoria_service"></param>
        public BitacoraDesarrolloController(IBitacoraDesarrolloService bitacora_service)
        {
            this.bitacora_service = bitacora_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/bitacora/desarrollo/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                IDictionary<string, IList<BitacoraDesarrollo>> data = new Dictionary<string, IList<BitacoraDesarrollo>>();
                data.Add("data", bitacora_service.getAll());
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
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/bitacora/desarrollo/supervisor/{id}")]
        [HttpGet]
        public HttpResponseMessage listBySupervisor(int id)
        {
            try
            {
                IDictionary<string, IList<BitacoraDesarrollo>> data = new Dictionary<string, IList<BitacoraDesarrollo>>();
                data.Add("data", bitacora_service.getAllBitacoraByIdSupervisor(id));
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
        [Route("api/bitacora/desarrollo/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            BitacoraDesarrollo bitacora = bitacora_service.detail(id);
            if (bitacora != null)
            {
                IDictionary<string, BitacoraDesarrollo> data = new Dictionary<string, BitacoraDesarrollo>();
                data.Add("data", bitacora);
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
        /// <param name="bitacora_vo"></param>
        /// <returns></returns>
        [Route("api/bitacora/desarrollo/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] BitacoraDesarrolloVo bitacora_vo)
        {
            TransactionResult tr = bitacora_service.create(bitacora_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
            //TransactionResult tr = TransactionResult.CREATED;
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
        /// <param name="bitacora_vo"></param>
        /// <returns></returns>
        [Route("api/bitacora/desarrollo/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] BitacoraDesarrolloVo bitacora_vo)
        {
            TransactionResult tr = bitacora_service.update(bitacora_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
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
        [Route("api/bitacora/desarrollo/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            TransactionResult tr = bitacora_service.delete(id);
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


        // -------------------------- AUTORIZACIONES -----------------------------------
        /// <summary>
        /// Create object pettition
        /// </summary>
        /// <param name="bitacora_vo"></param>
        /// <returns></returns>
        [Route("api/bitacora/desarrollo/atorizar/edicion")]
        [HttpPost]
        public HttpResponseMessage autorizarEdicion([FromBody] BitacoraDesarrolloVo bitacora_vo)
        {
            TransactionResult tr = bitacora_service.autorizarEdicion(bitacora_vo);
            IDictionary<string, string> data = new Dictionary<string, string>();
            if (tr == TransactionResult.OK)
            {
                data.Add("message", "Bitácora actualizada.");
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
        /// Create object pettition
        /// </summary>
        /// <param name="bitacora_vo"></param>
        /// <returns></returns>
        [Route("api/bitacora/desarrollo/autorizar/rango")]
        [HttpPost]
        public HttpResponseMessage autorizarRango([FromBody] BitacoraDesarrolloVo bitacora_vo)
        {
            TransactionResult tr = bitacora_service.autorizarRango(bitacora_vo);
            IDictionary<string, string> data = new Dictionary<string, string>();
            if (tr == TransactionResult.OK)
            {
                data.Add("message", "Rango actualizado.");
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
