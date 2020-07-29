using _01_Dal.Entities;
using _01_Dal.Methods;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
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
        //[HttpPut]
        //[Route("InsImage")]
        //public IHttpActionResult InsertImage(HttpPostedFileBase postedFile, Imagenes objImage)
        //{
        //    try
        //    {
        //        var resp = new MethodsGenerics().InsertImage(objImage);
        //        return Ok(resp);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}



        [HttpPost]
        [Route("InsImage")]
        public HttpResponseMessage PostUserImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png", ".jpeg" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            //objImage.Imagen = 
                            //  where you want to attach your imageurl
                            //if needed write the code to update the table

                            var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + extension);
                            //Userimage myfolder name where i want to save my image
                            postedFile.SaveAs(filePath);

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }

    }
}
