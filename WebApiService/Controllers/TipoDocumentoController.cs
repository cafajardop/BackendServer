using System.Web.Http;
using System.Web.Http.Cors;
using _01_Dal.Methods;

namespace WebApiService.Controllers
{
    /// <summary>
    /// Trae el tipo de documento
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("TipoDocumento")]
    public class TipoDocumentoController : ApiController
    {
        /// <summary>
        /// Consultar Tipos de documento
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetType")]
        public IHttpActionResult GetTypeDocument()
        {
            try
            {
                var resp = new Methods().ConsultType();
                return Ok(resp);

            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
