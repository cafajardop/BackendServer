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
            if (loginUser == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //TODO: Validate credentials Correctly, this code is only for demo !!
            var isCredentialValid = new Authentication().loginValidate(loginUser.email, loginUser.Password);
            if (isCredentialValid.Item1)
            {
                var token = TokenGenerator.GenerateTokenJwt(loginUser.email);
                dynamic res = new { ID = isCredentialValid.Item3, Token = token, user = isCredentialValid.Item2 };
                return Ok(res);
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
        /// <summary>
        /// Consulta por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUsersLogin")]
        public IHttpActionResult GetUsersLogin(string id)
        {
            try
            {
                var resp = new Authentication().GetLoginUser(id);
                return Ok(resp);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Consulta por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatePassword")]
        public IHttpActionResult UpdPass(Pass id)
        {
            try
            {
                bool resp = new Authentication().UpdatePassword(id);
                return Ok(resp);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Consulta por id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateUser")]
        public IHttpActionResult UpdUsert(LoginUser user)
        {
            try
            {
                bool resp = new Authentication().UpdateUser(user);
                return Ok(resp);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}