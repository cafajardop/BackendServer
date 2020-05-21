using _01_Dal.Entities;
using _01_Dal.Methods;
using System.Net;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiService.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginUser loginUser)
        {
            if (loginUser== null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //TODO: Validate credentials Correctly, this code is only for demo !!
            bool isCredentialValid = new Authentication().loginValidate(loginUser.email, loginUser.Password);
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(loginUser.email);
                return Ok(token);
            }
            else
            {
                return Ok("0");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("registrarse")]
        public IHttpActionResult Register(LoginUser login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //TODO: Validate credentials Correctly, this code is only for demo !!
            string isCredentialValid = new Authentication().RegisterUser(login);
            if (isCredentialValid != "" && isCredentialValid != "0")
            {
                var token = TokenGenerator.GenerateTokenJwt(login.userName);
                return Ok(token);
            }
            else
            {
                return Ok("0");
            }
        }
    }
}