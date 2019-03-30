using System;
using System.Net;
using System.Threading;
using System.Web.Http;
using Arca.WebApi.Models;
using Arca.WebApi.Security;

namespace Arca.WebApi.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : System.Web.Http.ApiController
    {
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

          

            //TODO: This code is only for demo - extract method in new class & validate correctly in your application !!
            var isAdminValid = (login.Username == "senador1@gmail.com" && login.Password == "123456");
            if (isAdminValid)
            {
                var rolename = "Administrator";
                var token = TokenGenerator.GenerateTokenJwt(login.Username, rolename);
                return Ok(token);
            }

            // Unauthorized access 
            return Unauthorized();
        }
    }
}
