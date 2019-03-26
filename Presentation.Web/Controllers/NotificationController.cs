

using Business.Logic;
using Domain.Entities;
using Presentation.Web.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web.Controllers
{
    public class NotificationController : Controller
    {
        //[AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_programs })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

       

        //[AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_programs })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            NotificationBL oNotificationBL = new NotificationBL();
            //ProgramFiltersViewModel ofilters = new ProgramFiltersViewModel();
            CurrentUserViewModel user = (CurrentUserViewModel)Session[ConfigurationManager.AppSettings["session.usuario.actual"]];
            GridModel<NotificationViewModel> grid = oNotificationBL.ObtenerLista(ofilters, user.user_id);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = ofilters.draw,
                recordsTotal = grid.total,
                recordsFiltered = grid.recordsFiltered,
                data = grid.rows
            });


        }


    }
}