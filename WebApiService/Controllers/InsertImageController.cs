using _01_Dal.Entities;
using _01_Dal.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("Imagenes")]
    public class InsertImageController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objImage"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsImage")]
        public IHttpActionResult InsertImage(Imagenes objImage)
        {
            try
            {
                var resp = new MethodsGenerics().InsertImage(objImage);
                return Ok(resp);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
