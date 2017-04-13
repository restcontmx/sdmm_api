﻿using Business.Interface;
using Models.Auth;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SDMM_API.Controllers
{
    /// <summary>
    /// Authenticaton Controller
    /// user authentication and stuff
    /// </summary>
    public class AuthenticationController : BasicApiController
    {

        private IAuthenticationService authentication_service;

        /// <summary>
        /// Constructor class
        /// </summary>
        /// <param name="authentication_service">Authentication services</param>
        public AuthenticationController(IAuthenticationService authentication_service) {
            this.authentication_service = authentication_service;
        }

        /// <summary>
        /// Validate user credentials
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("api/auth/login")]
        [HttpPost]
        public HttpResponseMessage login([FromBody]  UserVo user ) {
            AuthModel authentication_model = authentication_service.validateUser(user.username, user.password);
            if (authentication_model != null)
            {
                IDictionary<string, AuthModel> data = new Dictionary<string, AuthModel>();
                data.Add("data", authentication_model);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            else
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("message", "Not valid credentials.");
                return Request.CreateResponse(HttpStatusCode.Unauthorized, data);
            }
        }
    }// End of Authentication controller class
}
