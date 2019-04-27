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
using Newtonsoft.Json;


namespace Presentation.Web.Controllers
{
    public class InvestigatorController : Controller
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: User
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.my_draft_laws })]

        public ActionResult MisProyectosLey()
        {
            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();


            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oPeriods = oSelectorBL.PeriodsSelector();
            List<SelectListItem> periods = Helper.ConstruirDropDownList<SelectOptionItem>(oPeriods, "Value", "Text", "", false, "", "");

            ViewBag.periods = periods;

            GeneralFilterViewModel oGeneralFilterViewModel = new GeneralFilterViewModel();
            oGeneralFilterViewModel.period_id = oPeriod.period_id;
            return View(oGeneralFilterViewModel);
            
        }
        // GET: User
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.ranking_investigator })]
        public ActionResult Mejores()
        {
            SelectorBL oSelectorBL = new SelectorBL();
            MeritRangeBL oMeritRangeBL = new MeritRangeBL();

            List<MeritRangeViewModel> oMeritRange= oMeritRangeBL.ObtenerTodos();
            List<SelectOptionItem> oInterestAreas = oSelectorBL.InterestAreasSelector();
            List<SelectListItem> interest_areas = Helper.ConstruirDropDownList<SelectOptionItem>(oInterestAreas, "Value", "Text", "0", true, "0", "TODOS");
            ViewBag.interest_areas = interest_areas;
            ViewBag.merit_ranges_json = JsonConvert.SerializeObject(oMeritRange);
            ViewBag.merit_ranges = oMeritRange;
            return View();
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.my_backgrounds })]
        public ActionResult MiHistorial()
        {
            SelectorBL oSelectorBL = new SelectorBL();
            MeritRangeBL oMeritRangeBL = new MeritRangeBL();

            List<MeritRangeViewModel> oMeritRange = oMeritRangeBL.ObtenerTodos();
            ViewBag.merit_ranges_json = JsonConvert.SerializeObject(oMeritRange);
            ViewBag.merit_ranges = oMeritRange;
            ConceptBL oConceptBL = new ConceptBL();
            MyHistoryViewModel oMyHistoryViewModel = oConceptBL.ObtenerMiHistorial(AuthorizeUserAttribute.UsuarioLogeado().investigator_id);

            if (oMyHistoryViewModel == null ) {
                oMyHistoryViewModel = new MyHistoryViewModel();
                
                oMyHistoryViewModel.nro_concepts = 0;
                oMyHistoryViewModel.my_points = 0;
                oMyHistoryViewModel.qualified_concepts = 0;
                oMyHistoryViewModel.approved_concepts = 0;

            }
            ViewBag.my_points = oMyHistoryViewModel.my_points;
            return View(oMyHistoryViewModel);
        }
            

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.my_draft_laws })]

        public JsonResult ObtenerLista(DraftLawFiltersViewModel ofilters, [Bind(Include = "period_id")]  GeneralFilterViewModel generalfiltros)//DataTableAjaxPostModel model
        {
            DraftLawBL oDraftLawBL = new DraftLawBL();
            //DraftLawFiltersViewModel ofilters = new DraftLawFiltersViewModel();

            UserBL oUserBL = new UserBL();
            InvestigatorViewModel oInvestigatorViewModel= oUserBL.ObtenerInvestigator(AuthorizeUserAttribute.UsuarioLogeado().investigator_id);
            GridModel<DraftLawViewModel> grid = oDraftLawBL.ObtenerMisProyectosLey(ofilters, oInvestigatorViewModel.commissions, oInvestigatorViewModel.interest_areas, generalfiltros);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = ofilters.draw,
                recordsTotal = grid.total,
                recordsFiltered = grid.recordsFiltered,
                data = grid.rows
            });


        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.ranking_investigator })]
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
