

using Business.Logic;
using CrossCutting.Helper;
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
    public class ConsultationController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_consultation_realized })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_consultation_send })]
        // GET: User
        public ActionResult Enviadas()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_consultation })]
        public ActionResult Crear()
        {
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oInterestAreas = oSelectorBL.InterestAreasSelector();
            List<SelectListItem> interest_areas = Helper.ConstruirDropDownList<SelectOptionItem>(oInterestAreas, "Value", "Text", "", false, "", "");
            ViewBag.interest_areas = interest_areas;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_consultation })]
        public ActionResult Crear([Bind(Include = "consultation_id,title,message,interest_areas")] ConsultationViewModel pConsultationViewModel)
        {
            // TODO: Add insert logic here

            if (pConsultationViewModel == null)
            {
                return HttpNotFound();
            }
            pConsultationViewModel.consultation_id = 0;
            pConsultationViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;

            ConsultationBL oBL = new ConsultationBL();
            oBL.Agregar(pConsultationViewModel);
            return RedirectToAction("Index");

        }



        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.view_consultation })]
        public ActionResult Ver(string id)
        {
            ConsultationBL oBL = new ConsultationBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            ConsultationViewModel pConsultationViewModel = oBL.Obtener(pIntID);

            SelectorBL oSelectorBL = new SelectorBL();
            pConsultationViewModel.interest_areasMultiSelectList = new MultiSelectList(oSelectorBL.InterestAreasSelector(), "Value", "Text");
            return View(pConsultationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_consultations })]
        public ActionResult Editar([Bind(Include = "consultation_id,name")] ConsultationViewModel pConsultationViewModel)
        {
            // TODO: Add insert logic here

            if (pConsultationViewModel == null)
            {
                return HttpNotFound();
            }
            ConsultationBL oConsultationBL = new ConsultationBL();
            pConsultationViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            // oConsultationBL.Modificar(pConsultationViewModel);
            return RedirectToAction("Index");

        }


        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_consultation_realized })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ConsultationBL oConsultationBL = new ConsultationBL();
            int user_id = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            GridModel<ConsultationViewModel> grid = oConsultationBL.ObtenerLista(ofilters, user_id);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = ofilters.draw,
                recordsTotal = grid.total,
                recordsFiltered = grid.recordsFiltered,
                data = grid.rows.Select(a => new ConsultationViewModel
                {
                    consultation_id = a.consultation_id,
                    title = a.title,
                    message = a.message,
                    attended = a.attended,
                    date_created = a.date_created,
                    interest_areas_str = string.Join(", ", a.interest_areas_list),
                }).ToList()
            });


        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_consultation_send })]
        public JsonResult ObtenerListaEnviados(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ConsultationBL oConsultationBL = new ConsultationBL();
            
            GridModel<ConsultationViewModel> grid = oConsultationBL.ObtenerListaEnviados(ofilters);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = ofilters.draw,
                recordsTotal = grid.total,
                recordsFiltered = grid.recordsFiltered,
                data = grid.rows.Select(a => new ConsultationViewModel
                {
                    consultation_id = a.consultation_id,
                    title = a.title,
                    message = a.message,
                    attended = a.attended,
                    date_created = a.date_created,
                    interest_areas_str = string.Join(", ", a.interest_areas_list),
                }).ToList()
            });


        }


        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.view_consultation })]
        public JsonResult ObtenerInvestigadores(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ConsultationBL oConsultationBL = new ConsultationBL();
            ConsultationViewModel pConsultationViewModel = oConsultationBL.Obtener(ofilters.consultation_id);


            GridModel<InvestigatorViewModel> grid = oConsultationBL.ObtenerInvestigadores(ofilters, pConsultationViewModel.interest_areas);

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