using System;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Net.Http.Headers;
using System.Text;
using Business.Interface;
using Ninject;
using Models.Auth;

namespace SDMM_API.Modules
{
    /// <summary>
    /// Implementation of authentication module class
    /// From the Http module declaration
    /// </summary>
    public class AuthenticationModule : IHttpModule
    {

        private IAuthenticationService authentication_service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authentication_service"></param>
        public AuthenticationModule(IAuthenticationService authentication_service) {
            this.authentication_service = authentication_service;
        }

        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        /// Does nothing
        public void Dispose() { }

        /// <summary>
        /// Inits the request envents for authorization
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context) {
            context.AuthenticateRequest += Context_AuthenticateRequest;
            context.EndRequest += Context_EndRequest;
        }

        /// <summary>
        /// End of request
        /// sends headers if not authorized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Context_EndRequest(object sender, EventArgs e)
        {
            var response = HttpContext.Current.Response;

            if ( response.StatusCode.Equals( HttpStatusCode.Unauthorized ) ) {
                response.Headers.Add("WWW-Authenticate", "Basic realm=\"insert for realm\""); ;
            }
        }
        
        /// <summary>
        /// This event function gets the authorization header
        /// sends it to Authentication
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Context_AuthenticateRequest(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            var header = request.Headers["Authorization"];
            if (header != null) {
                var parsedValued = AuthenticationHeaderValue.Parse(header);
                if (parsedValued.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && parsedValued.Parameter != null) {
                    Authenticate(parsedValued.Parameter);
                }
            }
        }

        /// <summary>
        /// Authenticates credentials
        /// </summary>
        /// <param name="credentialValues"></param>
        /// <returns></returns>
        private bool Authenticate(string credentialValues) {
            try
            {
                var credentials = Encoding.GetEncoding("utf-8").GetString(Convert.FromBase64String(credentialValues));
                var values = credentials.Split(':');
                AuthModel auth_model = authentication_service.validateUser(values[0], values[1]);
                if( auth_model != null ) { 
                    SetPrincipal(new GenericPrincipal(new GenericIdentity(auth_model.id.ToString()), null));
                    return true;
                }return false;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// Validates user on the service
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool ValidateUser(string username, string password)
        {
            return authentication_service.validateUser(username, password) != null;
        }

        /// <summary>
        /// Sets user on the thread
        /// </summary>
        /// <param name="principal"></param>
        private static void SetPrincipal(IPrincipal principal) {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null) {
                HttpContext.Current.User = principal;
            }
        }

        #endregion
    }// End of Authentication Module class
}
