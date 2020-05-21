using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiService.Controllers
{
    /// <summary>
    /// customer controller class for testing security token
    /// </summary>
    [Authorize]
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            var customerFake = "customer-fake";
            return Ok(customerFake);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var customersFake = new string[] { "customer-1", "customer-2", "customer-3" };
            return Ok(customersFake);
        }

    }
}
