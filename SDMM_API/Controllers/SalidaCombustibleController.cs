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
    public class SalidaCombustibleController : BasicApiController
    {
        private ISalidaCombustibleService salida_service;
        public SalidaCombustibleController(ISalidaCombustibleService salida_service)
        {
            this.salida_service = salida_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/salidas/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                IDictionary<string, IList<SalidaCombustible>> data = new Dictionary<string, IList<SalidaCombustible>>();
                data.Add("data", salida_service.getAll());
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
        [Route("api/salidas/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            SalidaCombustible salida = salida_service.detail(id);
            if (salida != null)
            {
                IDictionary<string, SalidaCombustible> data = new Dictionary<string, SalidaCombustible>();
                data.Add("data", salida);
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
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/salidas/reporte/")]
        [HttpPost]
        public HttpResponseMessage listParaReporte(ReportesVo reportes_vo)
        {
            try
            {
                IDictionary<string, IList<SalidaCombustible>> data = new Dictionary<string, IList<SalidaCombustible>>();
                IList<SalidaCombustible> salidas = salida_service.getAll();
                IList<SalidaCombustible> salidasAux = new List<SalidaCombustible>();

                foreach (SalidaCombustible s in salidas)
                {
                    if (s.timestamp >= DateTime.Parse(reportes_vo.rangeStart) && s.timestamp <= DateTime.Parse(reportes_vo.rangeEnd))
                    {
                        salidasAux.Add(s);
                    }
                }

                data.Add("data", salidasAux);
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
        /// Create object pettition
        /// </summary>
        /// <param name="empleado_vo"></param>
        /// <returns></returns>
        [Route("api/salidas/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] SalidaCombustibleVo salida_vo)
        {
            TransactionResult tr = salida_service.create(salida_vo);
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
        [Route("api/salidas/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] SalidaCombustibleVo salida_vo)
        {
            TransactionResult tr = salida_service.update(salida_vo);
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
        [Route("api/salidas/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            TransactionResult tr = salida_service.delete(id);
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