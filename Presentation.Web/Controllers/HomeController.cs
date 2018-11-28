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
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.module_access_users })]
        public ActionResult Index()
        {
            RedirectToAction("Index", "User");
            return View();
        }

        
    }
}