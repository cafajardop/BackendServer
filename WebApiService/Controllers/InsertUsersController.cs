using _01_Dal.Entities;
using _01_Dal.Methods;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("Users")]
    [Authorize]
    public class InsertUsersController : ApiController
    {
        /// <summary>
        /// Inserta usuario en la tabla users
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("InsUsers")]
        public IHttpActionResult GetTypeDocument(Users objuser)
        {
            try
            {
                var resp = new Methods().InsertUsers(objuser);
                return Ok(resp);

            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}