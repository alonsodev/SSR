using Business.Logic;
using CrossCutting.Helper;
using Domain.Entities;
using Presentation.Web.Filters;
using Presentation.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web.Controllers
{
    public class InvestigatorController : Controller
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: User
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] {  })]

        public ActionResult MisProyectosLey()
        {
            return View();
        }


       



        public JsonResult ObtenerLista(DraftLawFiltersViewModel ofilters)//DataTableAjaxPostModel model
        {
            DraftLawBL oDraftLawBL = new DraftLawBL();
            //DraftLawFiltersViewModel ofilters = new DraftLawFiltersViewModel();

            UserBL oUserBL = new UserBL();
            InvestigatorViewModel oInvestigatorViewModel= oUserBL.ObtenerInvestigator(AuthorizeUserAttribute.UsuarioLogeado().investigator_id);
            GridModel<DraftLawViewModel> grid = oDraftLawBL.ObtenerMisProyectosLey(ofilters, oInvestigatorViewModel.commissions, oInvestigatorViewModel.interest_areas);

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
