

using Business.Logic;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web.Controllers
{
    public class RoleController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Crear()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Crear([Bind(Include = "role_id,role")] RoleViewModel pRoleViewModel)
        {
            // TODO: Add insert logic here

            if (pRoleViewModel == null)
            {
                return HttpNotFound();
            }
            pRoleViewModel.role_id = 0;

            pRoleViewModel.user_id_created = 0;

            RoleBL oBL = new RoleBL();

            oBL.Agregar(pRoleViewModel);

            return RedirectToAction("Index");

        }

        public ActionResult Editar(string id)
        {
            RoleBL oBL = new RoleBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            RoleViewModel pRoleViewModel = oBL.ObtenerRole(pIntID);

            return View(pRoleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Editar([Bind(Include = "role_id,role")] RoleViewModel pRoleViewModel)
        {
            // TODO: Add insert logic here

            if (pRoleViewModel == null)
            {
                return HttpNotFound();
            }
            RoleBL oRoleBL = new RoleBL();
            oRoleBL.Modificar(pRoleViewModel);
            return RedirectToAction("Index");

        }

        public ActionResult Eliminar(int id)
        {

            RoleBL oRoleBL = new RoleBL();

            oRoleBL.Eliminar(id);

            return RedirectToAction("Index");

        }


        public JsonResult ObtenerLista(RoleFiltersViewModel ofilters)//DataTableAjaxPostModel model
        {
            RoleBL oRoleBL = new RoleBL();
            //RoleFiltersViewModel ofilters = new RoleFiltersViewModel();
            GridModel<RoleViewModel> grid = oRoleBL.ObtenerLista(ofilters);

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