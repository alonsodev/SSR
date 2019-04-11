using Business.Logic;
using iTextSharp.text;
using Presentation.Web.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rectangle = iTextSharp.text.Rectangle;

namespace Presentation.Web.Controllers
{
    public class PoliticasUsoController : Controller
    {
       
        public ActionResult Index()
        {
            /*SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();
            oSendEmailNotificationBL.EnviarNotificacionActivarCuenta();*/

            ConfigurationBL oConfigurationBL = new ConfigurationBL();
            ViewBag.terms_conditions = oConfigurationBL.Obtener().terms_conditions;

            return View();
        }

     
        
    }
}