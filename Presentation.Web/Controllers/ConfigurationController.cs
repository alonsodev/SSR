

using Business.Logic;
using Domain.Entities;
using Presentation.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web.Controllers
{
    public class ConfigurationController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_config })]
        // GET: User
        public ActionResult Index()
        {
            ConfigurationBL oConfigurationBL = new ConfigurationBL();
            return View(oConfigurationBL.Obtener());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_config })]
        public ActionResult Index([Bind(Include = "configuration_id,terms_conditions,exclude_speakers,remove_titles_speaker")] ConfigurationViewModel pConfigurationViewModel)
        {
            // TODO: Add insert logic here

            if (pConfigurationViewModel == null)
            {
                return HttpNotFound();
            }
            ConfigurationBL oConfigurationBL = new ConfigurationBL();
            pConfigurationViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oConfigurationBL.Modificar(pConfigurationViewModel);
            return RedirectToAction("Index");

        }



    }
}