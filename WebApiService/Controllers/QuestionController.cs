using _01_Dal.MethodsKnowledgeBaseEnglish;
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
    /// Comparatives
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("Question")]
    public class QuestionController : ApiController
    {
        /// <summary>
        /// Consulta por cualquier palabra
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFindComparativeID")]
        public IHttpActionResult GetFindComparativeID(string filter)
        {
            try
            {
                var resp = new KnowledgeBase().GetFindComparativeID(filter);
                return Ok(resp);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
