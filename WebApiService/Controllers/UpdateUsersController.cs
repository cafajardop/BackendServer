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
    public class UpdateUsersController : ApiController
    {
        /// <summary>
        /// Actualiza usuario en la tabla users
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdUsers")]
        public IHttpActionResult GetTypeDocument(Users objuser)
        {
            try
            {
                var resp = new Methods().UpdateUsers(objuser);
                return Ok(resp);

            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
