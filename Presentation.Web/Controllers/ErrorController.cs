using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NoAutorizadoAD()
        {
            return View();
        }

        public ActionResult NoAutorizado()
        {
            return View();
        }

        public ActionResult NoSesion()
        {
            return View();
        }

        public ActionResult IntentosExcedidos()
        {
            return View();
        }
    }
}