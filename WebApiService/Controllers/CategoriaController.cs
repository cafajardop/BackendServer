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
    [RoutePrefix("Categoria")]
    public class CategoriaController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objuser"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCategoria")]
        public IHttpActionResult GetCategorias()
        {
            try
            {
                var resp = new MethodsGenerics().ConsultCategory();
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
        [Route("GetCategoriaId")]
        public IHttpActionResult GetCategoriaId(string id )
        {
            try
            {
                var resp = new MethodsGenerics().GetCategoryId(id);
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
        [Route("GetCategoriaNombre")]
        public IHttpActionResult GetCategoriaNombre(string id)
        {
            try
            {
                var resp = new MethodsGenerics().GetCategoryNombre(id);
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
        [Route("UpdateCategoria")]
        public IHttpActionResult UpdateCategorias(Categoria categoria )
        {
            try
            {
                var resp = new MethodsGenerics().UpdateCategory(categoria);
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
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertCategoria")]
        public IHttpActionResult InsertCategorias(Categoria categoria)
        {
            try
            {
                var resp = new MethodsGenerics().InsertarCategoria(categoria);
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
        [HttpDelete]
        [Route("DeleteCategoria")]
        public IHttpActionResult DeleteCategory(int id)
        {
            try
            {
                var resp = new MethodsGenerics().DeleteCategoria(id);
                return Ok(resp);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}