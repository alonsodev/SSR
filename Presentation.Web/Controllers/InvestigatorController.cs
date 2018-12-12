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
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.my_draft_laws })]

        public ActionResult MisProyectosLey()
        {
            return View();
        }

        public ActionResult Mejores()
        {
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oInterestAreas = oSelectorBL.InterestAreasSelector();
            List<SelectListItem> interest_areas = Helper.ConstruirDropDownList<SelectOptionItem>(oInterestAreas, "Value", "Text", "", false, "", "");
            ViewBag.interest_areas = interest_areas;
            return View();
        }



        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.my_draft_laws })]

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
        public JsonResult ObtenerRanking(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ConceptBL oConceptBL = new ConceptBL();
            string str_interest_area_id = Request.Form["interest_area_id"];
            int interest_area_id = 0;
            int.TryParse(str_interest_area_id, out interest_area_id);

            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();
            // int investigator_id = AuthorizeUserAttribute.UsuarioLogeado().investigator_id;

            
            GridModel<RankingViewModel> grid = oConceptBL.ObtenerRanking(ofilters, interest_area_id);

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
