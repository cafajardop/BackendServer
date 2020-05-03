using _01_Dal.Methods;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiService.Controllers
{
    /// <summary>
    /// Trae el tipo de documento
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("Users")]
    public class ConsultaUsuariosController : ApiController
    {
        /// <summary>
        /// Consultar Usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUsers")]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                var resp = new Methods().ConsultPerson();
                return Ok(resp);

            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
