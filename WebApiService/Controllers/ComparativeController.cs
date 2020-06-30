using _01_Dal.Entities;
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
    [RoutePrefix("Comparatives")]
    public class ComparativeController : ApiController
    {

        /// <summary>
        /// Trae lista de comparativos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllComparatives")]
        public IHttpActionResult GetAllComparatives()
        {
            try
            {
                var resp = new KnowledgeBase().GetAllComparatives();
                return Ok(resp);

            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetComparativeById")]
        public IHttpActionResult GetComparativeById(string id)
        {
            try
            {
                var resp = new KnowledgeBase().GetComparativeById(id);
                return Ok(resp);

            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Consulta por cualquier palabra
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFindComparativeName")]
        public IHttpActionResult GetFindComparativeName(string filter)
        {
            try
            {
                var resp = new KnowledgeBase().GetFindComparativeName(filter);
                return Ok(resp);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateComparative")]
        public IHttpActionResult UpdateCategorias(Question categoria)
        {
            try
            {
                var resp = new KnowledgeBase().UpdateComparative(categoria);
                return Ok(resp);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Insertar Comparativos
        /// </summary>
        /// /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertComparative")]
        public IHttpActionResult InsertComparative(Question user)
        {
            try
            {
                var resp = new KnowledgeBase().InsertComparative(user);
                return Ok(resp);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}