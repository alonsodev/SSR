

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
    public class ReasonRejectController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_reason_rejects })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_reason_rejects })]
        public ActionResult Crear()
        {


            return View();
        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_reason_rejects, AuthorizeUserAttribute.Permission.edit_reason_rejects })]
        public JsonResult Verificar(int id_reason_reject, string name)
        {

            ReasonRejectBL oBL = new ReasonRejectBL();
            var resultado = oBL.VerificarDuplicado(id_reason_reject, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_reason_rejects })]
        public ActionResult Crear([Bind(Include = "reason_reject_id,name")] ReasonRejectViewModel pReasonRejectViewModel)
        {
            // TODO: Add insert logic here

            if (pReasonRejectViewModel == null)
            {
                return HttpNotFound();
            }
            pReasonRejectViewModel.reason_reject_id = 0;
            pReasonRejectViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            
            ReasonRejectBL oBL = new ReasonRejectBL();
            oBL.Agregar(pReasonRejectViewModel);
            return RedirectToAction("Index");

        }



        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_reason_rejects })]
        public ActionResult Editar(string id)
        {
            ReasonRejectBL oBL = new ReasonRejectBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            ReasonRejectViewModel pReasonRejectViewModel = oBL.Obtener(pIntID);

            return View(pReasonRejectViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_reason_rejects })]
        public ActionResult Editar([Bind(Include = "reason_reject_id,name")] ReasonRejectViewModel pReasonRejectViewModel)
        {
            // TODO: Add insert logic here

            if (pReasonRejectViewModel == null)
            {
                return HttpNotFound();
            }
            ReasonRejectBL oReasonRejectBL = new ReasonRejectBL();
            pReasonRejectViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oReasonRejectBL.Modificar(pReasonRejectViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_reason_rejects })]
        public JsonResult Eliminar(int id)
        {

            ReasonRejectBL oReasonRejectBL = new ReasonRejectBL();

            oReasonRejectBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_reason_rejects })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ReasonRejectBL oReasonRejectBL = new ReasonRejectBL();
            //ReasonRejectFiltersViewModel ofilters = new ReasonRejectFiltersViewModel();
            GridModel<ReasonRejectViewModel> grid = oReasonRejectBL.ObtenerLista(ofilters);

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