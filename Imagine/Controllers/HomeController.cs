using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Imagine.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string exito = null)
        {

            return View();
        }
        [HttpPost]
        public JsonResult EnviarEmailContacto(string Nombre, string Email, string Mensaje)
        {
            try
            {
                var EmailDestino = new MailAddress("contacto@dev-imagine.com", "Imagine");
                var EmailEmisor = new MailAddress("desarrollos.imagine@gmail.com", "Imagine");
                const string fromPassword = "DesarrollosImagine2018";
                string subject = Nombre + " - " + Email;
                string body = Mensaje;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(EmailEmisor.Address, fromPassword),
                    Timeout = 20000,
                };
                using (var message = new MailMessage(EmailEmisor, EmailDestino)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}