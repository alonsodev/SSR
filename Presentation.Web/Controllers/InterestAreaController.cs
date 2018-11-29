

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
    public class InterestAreaController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Crear()
        {

            List<SelectListItem> oListaVacia = Helper.ConstruirDropDownList<SelectOptionItem>(new List<SelectOptionItem>(), "Value", "Text", "", true, "", "");

            ViewBag.institutions = oListaVacia;

            ViewBag.investigation_groups = oListaVacia;
            return View();
        }

        [HttpPost]
        public JsonResult Verificar(int id_interest_area, string name)
        {

            InterestAreaBL oBL = new InterestAreaBL();
            var resultado = oBL.VerificarDuplicado(id_interest_area, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Crear([Bind(Include = "interest_area_id,investigation_group_id,name")] InterestAreaViewModel pInterestAreaViewModel)
        {
            // TODO: Add insert logic here

            if (pInterestAreaViewModel == null)
            {
                return HttpNotFound();
            }
            pInterestAreaViewModel.interest_area_id = 0;
            pInterestAreaViewModel.user_id_created = 0;

            InterestAreaBL oBL = new InterestAreaBL();
            oBL.Agregar(pInterestAreaViewModel);
            return RedirectToAction("Index");

        }

       


        public ActionResult Editar(string id)
        {
            InterestAreaBL oBL = new InterestAreaBL();
            SelectorBL oSelectorBL = new SelectorBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            InterestAreaViewModel pInterestAreaViewModel = oBL.Obtener(pIntID);

            SelectOptionItem oSelectOptionItem = new SelectOptionItem();
            oSelectOptionItem.Text = pInterestAreaViewModel.institution;
            oSelectOptionItem.Value = pInterestAreaViewModel.institution_id.ToString();
            var oLista = new List<SelectOptionItem>();
            oLista.Add(oSelectOptionItem);
            List<SelectListItem> institutions = Helper.ConstruirDropDownList<SelectOptionItem>(oLista, "Value", "Text", "", true, "", "");

            var oInvestigationGroups= oSelectorBL.InvestigationGroupsSelector(pInterestAreaViewModel.institution_id);
            List<SelectListItem> investigation_groups = Helper.ConstruirDropDownList<SelectOptionItem>(oInvestigationGroups, "Value", "Text", "", true, "", "");

            ViewBag.institutions = institutions;
            ViewBag.investigation_groups = investigation_groups;
            return View(pInterestAreaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Editar([Bind(Include = "interest_area_id,investigation_group_id,name")] InterestAreaViewModel pInterestAreaViewModel)
        {
            // TODO: Add insert logic here

            if (pInterestAreaViewModel == null)
            {
                return HttpNotFound();
            }
            InterestAreaBL oInterestAreaBL = new InterestAreaBL();
            oInterestAreaBL.Modificar(pInterestAreaViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public JsonResult Eliminar(int id)
        {

            InterestAreaBL oInterestAreaBL = new InterestAreaBL();

            oInterestAreaBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }


        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            InterestAreaBL oInterestAreaBL = new InterestAreaBL();
            //InterestAreaFiltersViewModel ofilters = new InterestAreaFiltersViewModel();
            GridModel<InterestAreaViewModel> grid = oInterestAreaBL.ObtenerLista(ofilters);

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