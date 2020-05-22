using _01_Dal.Entities;
using System.Net;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("Users")]
    public class EmailController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendEmail")]
        public IHttpActionResult sendEmail(Email email)
        {
            try
            {
                string subject = email.subject;
                string body = $"Telefono:  {email.telefono}  Mensaje: {email.body}";
                string to = "carlosafp84@gmail.com";

                MailMessage mm = new MailMessage();
                mm.From = new MailAddress("carlosafp84@gmail.com");
                mm.To.Add(to);
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                NetworkCredential nc = new NetworkCredential();
                smtp.UseDefaultCredentials = true;
                nc.UserName = "carlosafp84@gmail.com";
                nc.Password = "cafa80199444";
                smtp.Credentials = nc;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(mm);

                return Ok();

            }
            catch (System.Exception)
            {
                throw;
            }



        }
    }
}
