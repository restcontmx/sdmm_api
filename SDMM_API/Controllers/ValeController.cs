﻿using Business.Interface;
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


       
    }
}
