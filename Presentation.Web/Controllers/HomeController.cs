using Business.Logic;
using Presentation.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] {  })]
        public ActionResult Index()
        {
            /*SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();
            oSendEmailNotificationBL.EnviarNotificacionActivarCuenta();*/
            return View();
        }

        
    }
}