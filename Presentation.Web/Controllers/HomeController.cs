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
    public class HomeController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] {  })]
        public ActionResult Index()
        {
            /*SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();
            oSendEmailNotificationBL.EnviarNotificacionActivarCuenta();*/
            return View();
        }

       /* public void ModifyDocument() {
            Rectangle rect = new Rectangle(36, 108);
            rect.EnableBorderSide(1);
            //rect.EnableBorderSide(2);
            //rect.enableBorderSide(4);
            //rect.enableBorderSide(8);
            
            rect.BorderColor=BaseColor.BLACK;
            rect.Border=Rectangle.BOX;
            rect.BorderWidth=2;
            document.add(rect);
        }*/

        
    }
}