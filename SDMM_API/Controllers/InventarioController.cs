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
    public class InventarioController : BasicApiController
    {
        private IInventarioService inventario_service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="producto_service"></param>
        public InventarioController(IInventarioService inventario_service)
        {
            this.inventario_service = inventario_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/reporteInventario/")]
        [HttpPost]
        public HttpResponseMessage list([FromBody] ReporteInvVo rep)
        {
            try
            {
                IDictionary<string, IList<InfoInventario>> data = new Dictionary<string, IList<InfoInventario>>();
                data.Add("data", inventario_service.getAll(rep.rangeStart.ToString()));
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
        [Route("api/inventario/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            { 
                IDictionary<string, IList<InfoInventario>> data = new Dictionary<string, IList<InfoInventario>>();
                data.Add("data", inventario_service.getAll(DateTime.Now.ToString()));
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
        [Route("api/inventario/mobile/")]
        [HttpGet]
        public HttpResponseMessage listMobile()
        {
            try
            {
                IList<InfoInventario> aux = inventario_service.getAll(DateTime.Now.ToString());

                List<InfoInventario> aux2 = new List<InfoInventario>();
                
                foreach(InfoInventario inf in aux)
                {
                    if(inf.existenciaFinal > 0)
                    {
                        aux2.Add(inf);
                    }
                }
  

                IDictionary <string, IList<InfoInventario>> data = new Dictionary<string, IList<InfoInventario>>();
                data.Add("data", (IList<InfoInventario>)aux2);
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
        [Route("api/inventario/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            Inventario inventario = inventario_service.detail(id);
            if (inventario != null)
            {
                IDictionary<string, Inventario> data = new Dictionary<string, Inventario>();
                data.Add("data", inventario);
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
        /// <param name="inventario_vo"></param>
        /// <returns></returns>
        [Route("api/inventario/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] InventarioVo inventario_vo)
        {
            TransactionResult tr = inventario_service.create(inventario_vo);
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
        /// Delete object request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/inventario/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            TransactionResult tr = inventario_service.delete(id);
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
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/inventario/diario/")]
        [HttpGet]
        public HttpResponseMessage createInventarioDiario()
        {
            TransactionResult tr = inventario_service.createIventarioDiario();
            IDictionary<string, string> data = new Dictionary<string, string>();
            if (tr == TransactionResult.CREATED)
            {
                data.Add("message", "Inventario creado.");
                return Request.CreateResponse(HttpStatusCode.Created, data);
            }
            else
            {
                data.Add("message", "Error al crear el inventario diario.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }
    }
}