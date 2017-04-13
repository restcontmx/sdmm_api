using Business.Interface;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SDMM_API.Controllers
{
    /// <summary>
    /// Dummy controller; routes and all
    /// </summary>
    public class DummyController : BasicApiController
    {
        
        private IDummyService dummy_service;

        /// <summary>
        /// Dummy Constructor 
        /// </summary>
        /// <param name="dummy_service"></param>
        public DummyController(IDummyService dummy_service) {
            this.dummy_service = dummy_service;
        }

        /// <summary>
        /// dummy test for get list of objects
        /// </summary>
        /// <returns></returns>
        [Route("api/dummy")]
        [HttpGet]
        public HttpResponseMessage list() {
            return Request.CreateResponse(HttpStatusCode.OK, dummy_service.getAll());
        }

    }
}
