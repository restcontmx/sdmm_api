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
    /// <summary>
    /// Vale controller implementation
    /// </summary>
    public class DevolucionController : BasicApiController
    {
        private IDevolucionService devolucion_service;

        /// <summary>
        /// Vale controller constructor
        /// </summary>
        /// <param name="vale_service">Injects vale service</param>
        public DevolucionController(IDevolucionService devolucion_service)
        {
            this.devolucion_service = devolucion_service;
        }

        //Crear Devolucion de Producto

        [Route("api/devolucion/producto/")]
        [HttpPost]
        public HttpResponseMessage createDevolucionP([FromBody] DevolucionVo devolucion_vo)
        {
            TransactionResult tr = devolucion_service.createP(devolucion_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
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
        /// Get DetalleDevByCaja by flolio caja
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        [Route("api/devolucion/detalleCaja/{folio}")]
        [HttpGet]
        public HttpResponseMessage listDetails(string folio)
        {
            DetalleDevByCajaVo detalle = devolucion_service.getDetalleByCaja(folio);
            if (detalle != null)
            {
                IDictionary<string, DetalleDevByCajaVo> data = new Dictionary<string, DetalleDevByCajaVo>();
                data.Add("data", detalle);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            else
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", "Object not found.");
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }


    }
}