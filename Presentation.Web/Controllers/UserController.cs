using Business.Logic;
using CrossCutting.Helper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Crear()
        {
            SelectorBL oSelectorBL = new SelectorBL();

            List<SelectOptionItem> oEstatus = oSelectorBL.EstatusUserSelector();
            List<SelectOptionItem> oRoles = oSelectorBL.RolesSelector();
            List<SelectListItem> estatus = Helper.ConstruirDropDownList<SelectOptionItem>(oEstatus, "Value", "Text", "", true, "", "");
            List<SelectListItem> roles = Helper.ConstruirDropDownList<SelectOptionItem>(oRoles, "Value", "Text", "", true, "", "");

            ViewBag.estatus = estatus;
            ViewBag.roles = roles;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Crear([Bind(Include = "id,user_name,user_email,user_pass,user_role_id,user_status_id")] UserViewModel pUserViewModel)
        {
            // TODO: Add insert logic here

            if (pUserViewModel == null)
            {
                return HttpNotFound();
            }
            pUserViewModel.id = 0;

            pUserViewModel.user_id_created = 0;

            UserBL oBL = new UserBL();

            oBL.Agregar(pUserViewModel);

            return RedirectToAction("Index");

        }

        public ActionResult Editar(string id)
        {
            UserBL oBL = new UserBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            UserViewModel pUserViewModel = oBL.ObtenerUser(pIntID);
            SelectorBL oSelectorBL = new SelectorBL();

            List<SelectOptionItem> oEstatus = oSelectorBL.EstatusUserSelector();
            List<SelectOptionItem> oRoles = oSelectorBL.RolesSelector();
            List<SelectListItem> estatus = Helper.ConstruirDropDownList<SelectOptionItem>(oEstatus, "Value", "Text", "", true, "", "");
            List<SelectListItem> roles = Helper.ConstruirDropDownList<SelectOptionItem>(oRoles, "Value", "Text", "", true, "", "");

            ViewBag.estatus = estatus;
            ViewBag.roles = roles;

            return View(pUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Editar([Bind(Include = "id,user_name,user_email,user_pass,user_role_id,user_status_id")] UserViewModel pUserViewModel)
        {
            // TODO: Add insert logic here

            if (pUserViewModel == null)
            {
                return HttpNotFound();
            }
            UserBL oUserBL = new UserBL();
            oUserBL.Modificar(pUserViewModel);
            return RedirectToAction("Index");

        }

        public ActionResult Eliminar(int id)
        {

            UserBL oUserBL = new UserBL();

            oUserBL.Eliminar(id);

            return RedirectToAction("Index");

        }


        public JsonResult ObtenerLista(UserFiltersViewModel ofilters)//DataTableAjaxPostModel model
        {
            UserBL oUserBL = new UserBL();
            //UserFiltersViewModel ofilters = new UserFiltersViewModel();
            GridModel<UserViewModel> grid = oUserBL.ObtenerLista(ofilters);

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