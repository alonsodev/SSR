

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
    public class InvestigationGroupController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_investigation_groups })]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_investigation_groups })]
        public ActionResult Crear()
        {

            List<SelectListItem> oListaVacia = Helper.ConstruirDropDownList<SelectOptionItem>(new List<SelectOptionItem>(), "Value", "Text", "", true, "", "");

            ViewBag.institutions = oListaVacia;
            return View();
        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_investigation_groups, AuthorizeUserAttribute.Permission.edit_investigation_groups })]
        public JsonResult Verificar(int id_investigation_group, string name)
        {

            InvestigationGroupBL oBL = new InvestigationGroupBL();
            var resultado = oBL.VerificarDuplicado(id_investigation_group, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_investigation_groups })]
        public ActionResult Crear([Bind(Include = "investigation_group_id,institution_id,name")] InvestigationGroupViewModel pInvestigationGroupViewModel)
        {
            // TODO: Add insert logic here

            if (pInvestigationGroupViewModel == null)
            {
                return HttpNotFound();
            }
            pInvestigationGroupViewModel.investigation_group_id = 0;
            pInvestigationGroupViewModel.user_id_created = 0;

            InvestigationGroupBL oBL = new InvestigationGroupBL();
            oBL.Agregar(pInvestigationGroupViewModel);
            return RedirectToAction("Index");

        }


        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_investigation_groups })]

        public ActionResult Editar(string id)
        {
            InvestigationGroupBL oBL = new InvestigationGroupBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            InvestigationGroupViewModel pInvestigationGroupViewModel = oBL.Obtener(pIntID);

            SelectOptionItem oSelectOptionItem = new SelectOptionItem();
            oSelectOptionItem.Text = pInvestigationGroupViewModel.institution;
            oSelectOptionItem.Value = pInvestigationGroupViewModel.institution_id.ToString();
            var oLista = new List<SelectOptionItem>();
            oLista.Add(oSelectOptionItem);
            List<SelectListItem> institutions = Helper.ConstruirDropDownList<SelectOptionItem>(oLista, "Value", "Text", "", true, "", "");

            ViewBag.institutions = institutions;
            return View(pInvestigationGroupViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_investigation_groups })]
        public ActionResult Editar([Bind(Include = "investigation_group_id,institution_id,name")] InvestigationGroupViewModel pInvestigationGroupViewModel)
        {
            // TODO: Add insert logic here

            if (pInvestigationGroupViewModel == null)
            {
                return HttpNotFound();
            }
            InvestigationGroupBL oInvestigationGroupBL = new InvestigationGroupBL();
            oInvestigationGroupBL.Modificar(pInvestigationGroupViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_investigation_groups })]
        public JsonResult Eliminar(int id)
        {

            InvestigationGroupBL oInvestigationGroupBL = new InvestigationGroupBL();

            oInvestigationGroupBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_investigation_groups })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            InvestigationGroupBL oInvestigationGroupBL = new InvestigationGroupBL();
            //InvestigationGroupFiltersViewModel ofilters = new InvestigationGroupFiltersViewModel();
            GridModel<InvestigationGroupViewModel> grid = oInvestigationGroupBL.ObtenerLista(ofilters);

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