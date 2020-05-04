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
    public class DeleteUsersController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DelUsers")]
        public IHttpActionResult DeleteUsers(string id)
        {
            try
            {
                var resp = new Methods().DeleteUsers(id);
                return Ok(resp);

            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
