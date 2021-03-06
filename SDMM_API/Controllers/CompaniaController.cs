﻿using Business.Interface;
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
    public class CompaniaController : BasicApiController
    {
        private ICompaniaService compania_service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="categoria_service"></param>
        public CompaniaController(ICompaniaService compania_service)
        {
            this.compania_service = compania_service;
        }

        /// <summary>
        /// Get all objects route
        /// </summary>
        /// <returns></returns>
        [Route("api/compania/")]
        [HttpGet]
        public HttpResponseMessage list()
        {
            try
            {
                //Envia el parámetro 1 por default para indicar que es el sistema de combustibles
                IDictionary<string, IList<Compania>> data = new Dictionary<string, IList<Compania>>();
                data.Add("data", compania_service.getAll(1));
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
        [Route("api/compania/{id}")]
        [HttpGet]
        public HttpResponseMessage detail(int id)
        {
            //Envia el parámetro 1 por default para indicar que es el sistema de combustibles
            Compania compania = compania_service.detail(id,1);
            if (compania != null)
            {
                IDictionary<string, Compania> data = new Dictionary<string, Compania>();
                data.Add("data", compania);
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
        /// <param name="categoria_vo"></param>
        /// <returns></returns>
        [Route("api/compania/")]
        [HttpPost]
        public HttpResponseMessage create([FromBody] CompaniaVo compania_vo)
        {
            //Envia el parámetro 1 por default para indicar que es el sistema de combustibles
            TransactionResult tr = compania_service.create(compania_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) },1);
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
        /// <param name="categoria_vo"></param>
        /// <returns></returns>
        [Route("api/compania/")]
        [HttpPut]
        public HttpResponseMessage update([FromBody] CompaniaVo compania_vo)
        {
            //Envia el parámetro 1 por default para indicar que es el sistema de combustibles
            TransactionResult tr = compania_service.update(compania_vo, 1);
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
        [Route("api/compania/{id}")]
        [HttpDelete]
        public HttpResponseMessage delete(int id)
        {
            //Envia el parámetro 1 por default para indicar que es el sistema de combustibles
            TransactionResult tr = compania_service.delete(id, 1);
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
        [Route("api/combustible/compania/")]
        [HttpGet]
        public HttpResponseMessage listCompaniasCombustibles()
        {
            try
            {
                //Envia el parámetro 2 por default para indicar que es el sistema de combustibles
                IDictionary<string, IList<Compania>> data = new Dictionary<string, IList<Compania>>();
                data.Add("data", compania_service.getAll(2));
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
        [Route("api/combustible/compania/{id}")]
        [HttpGet]
        public HttpResponseMessage detailCompaniasCombustibles(int id)
        {
            //Envia el parámetro 2 por default para indicar que es el sistema de combustibles
            Compania compania = compania_service.detail(id, 2);
            if (compania != null)
            {
                IDictionary<string, Compania> data = new Dictionary<string, Compania>();
                data.Add("data", compania);
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
        /// <param name="categoria_vo"></param>
        /// <returns></returns>
        [Route("api/combustible/compania/")]
        [HttpPost]
        public HttpResponseMessage createCompaniasCombustibles([FromBody] CompaniaVo compania_vo)
        {
            //Envia el parámetro 2 por default para indicar que es el sistema de combustibles
            TransactionResult tr = compania_service.create(compania_vo, new Models.Auth.User { id = int.Parse(RequestContext.Principal.Identity.Name) }, 2);
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
        /// <param name="categoria_vo"></param>
        /// <returns></returns>
        [Route("api/combustible/compania/")]
        [HttpPut]
        public HttpResponseMessage updateCompaniasCombustibles([FromBody] CompaniaVo compania_vo)
        {
            //Envia el parámetro 2 por default para indicar que es el sistema de combustibles
            TransactionResult tr = compania_service.update(compania_vo, 2);
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
        [Route("api/combustible/compania/{id}")]
        [HttpDelete]
        public HttpResponseMessage deleteCompaniasCombustibles(int id)
        {
            //Envia el parámetro 2 por default para indicar que es el sistema de combustibles
            TransactionResult tr = compania_service.delete(id, 2);
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