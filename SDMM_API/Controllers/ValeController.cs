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
    public class ValeController : BasicApiController
    {
        private IValeService vale_service;

        /// <summary>
        /// Vale controller constructor
        /// </summary>
        /// <param name="vale_service">Injects vale service</param>
        public ValeController(IValeService vale_service)
        {
            this.vale_service = vale_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/vale/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                IDictionary<string, IList<Vale>> data = new Dictionary<string, IList<Vale>>();
                data.Add("data", vale_service.getAll());
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
        [Route("api/vale/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] ValeVo vale_vo)
        {
            TransactionResult tr = vale_service.create(vale_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
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

        // <summary>
        /// Get vale detalle by vale id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            Vale vale = vale_service.detail(id);
            vale.detalles = vale_service.getDetailsByValeId(id);

            if (vale != null)
            {
                IDictionary<string, Vale> data = new Dictionary<string, Vale>();
                data.Add("data", vale);
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
        /// Get vale detalle by vale id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/{id}")]
        [HttpGet]
        public HttpResponseMessage listDetails(int id) {
            try
            {
                IDictionary<string, IList<DetalleVale>> data = new Dictionary<string, IList<DetalleVale>>();
                data.Add("data", vale_service.getDetailsByValeId(id));
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
        /// List registers by detalle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/registro/{id}")]
        [HttpGet]
        public HttpResponseMessage listRegisters(int id) {
            try
            {
                IDictionary<string, IList<RegistroDetalle>> data = new Dictionary<string, IList<RegistroDetalle>>();
                data.Add("data", vale_service.getAllRegistersByDetalle(id));
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
        /// List registers by detalle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/registro/over/{id}")]
        [HttpGet]
        public HttpResponseMessage listRegistersOver(int id)
        {
            try
            {
                IDictionary<string, IList<RegistroDetalle>> data = new Dictionary<string, IList<RegistroDetalle>>();
                data.Add("data", vale_service.getAllRegistersOverByDetalle(id));
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
        /// Create register
        /// </summary>
        /// <param name="registrodetalle_vo"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/registro/")]
        [HttpPost]
        public HttpResponseMessage createRegistro([FromBody] RegistroDetalleVo registrodetalle_vo)
        {
            TransactionResult tr = vale_service.createRegistroDetalle(registrodetalle_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
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
        /// Create register
        /// </summary>
        /// <param name="registrodetalle_vo"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/registro/especial/")]
        [HttpPost]
        public HttpResponseMessage createRegistroEspecial([FromBody] RegistroDetalleVo registrodetalle_vo)
        {
            TransactionResult tr = vale_service.createRegistroDetalleOver(registrodetalle_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
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

        [Route("api/vale/detalle/registro/bunch/")]
        [HttpPost]
        public HttpResponseMessage createRegistroBunch([FromBody]  RegistersVo registros )
        {
            Console.WriteLine( Request.Content );
            TransactionResult tr = vale_service.createRegistroDetalleByList(registros.registrodetalles_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
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
        /// Create object pettition
        /// </summary>
        /// <param name="empleado_vo"></param>
        /// <returns></returns>
        [Route("api/vale/updateactive/")]
        [HttpPut]
        public HttpResponseMessage updateActive([FromBody] ValeVo vale_vo)
        {
            vale_vo.active = 0;
            TransactionResult tr = vale_service.updateStatus(vale_vo);
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
        /// List registers by detalle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/vale/detalle/registro/caja/{id}")]
        [HttpGet]
        public HttpResponseMessage listRegistersByCaja(string id)
        {
            try
            {
                IDictionary<string, IList<RegistroDetalle>> data = new Dictionary<string, IList<RegistroDetalle>>();
                data.Add("data", vale_service.getAllRegistersByFolioCaja(id));
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
        [Route("api/vale/detalle/registro/sacos/")]
        [HttpGet]
        public HttpResponseMessage listRegistersSacos()
        {
            try
            {
                IDictionary<string, IList<RegistroDetalle>> data = new Dictionary<string, IList<RegistroDetalle>>();
                data.Add("data", vale_service.getAllRegistersSacos());
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", String.Format("There was an error attending the request; {0}.", e.ToString()));
                return Request.CreateResponse(HttpStatusCode.BadRequest, data);
            }
        }


        ////////////// update vale controller 
        /// <summary>
        /// Create object pettition
        /// </summary>
        /// <param name="empleado_vo"></param>
        /// <returns></returns>
        [Route("api/vale/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] ValeVo vale)
        {
            TransactionResult tr;

            if (vale.autorizo != 0)
            {
                tr = vale_service.updateAutorizacion(vale, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) });
            }
            else
            {
                tr = vale_service.update(vale);
            }
            //TransactionResult tr = vale_service.update(vale);
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
        [Route("api/vale/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            TransactionResult tr = vale_service.delete(id);
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
