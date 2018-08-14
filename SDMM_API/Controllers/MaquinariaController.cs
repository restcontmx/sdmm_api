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
    public class MaquinariaController : BasicApiController
    {
        private IMaquinariaService maquinaria_service;
        public MaquinariaController(IMaquinariaService maquinaria_service)
        {
            this.maquinaria_service = maquinaria_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/maquinaria/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                IDictionary<string, IList<Maquinaria>> data = new Dictionary<string, IList<Maquinaria>>();
                data.Add("data", maquinaria_service.getAll());
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
        [Route("api/maquinaria/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            Maquinaria maquina = maquinaria_service.detail(id);
            if (maquina != null)
            {
                IDictionary<string, Maquinaria> data = new Dictionary<string, Maquinaria>();
                data.Add("data", maquina);
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
        [Route("api/maquinaria/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] MaquinariaVo maquina_vo)
        {
            if (maquina_vo.detalles == null)
            {
                maquina_vo.detalles = new List<DetalleConsumoMaquinariaVo>();
            }

            if (maquina_vo.cuentas != null)
            {
                foreach (CuentaVo c in maquina_vo.cuentas)
                {
                    c.user_id = int.Parse(RequestContext.Principal.Identity.Name);
                }
            }
            else
            {
                maquina_vo.cuentas = new List<CuentaVo>();
            }

            TransactionResult tr = maquinaria_service.create(maquina_vo);
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
        /// <param name="empleado_vo"></param>
        /// <returns></returns>
        [Route("api/maquinaria/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] MaquinariaVo maquina_vo)
        {
            if(maquina_vo.detalles == null)
            {
                maquina_vo.detalles = new List<DetalleConsumoMaquinariaVo>();
            }

            if (maquina_vo.cuentas != null)
            {
                foreach (CuentaVo c in maquina_vo.cuentas)
                {
                    c.user_id = int.Parse(RequestContext.Principal.Identity.Name);
                }
            }
            else
            {
                maquina_vo.cuentas = new List<CuentaVo>();
            }

            TransactionResult tr = maquinaria_service.update(maquina_vo);
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
        [Route("api/maquinaria/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            TransactionResult tr = maquinaria_service.delete(id);
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