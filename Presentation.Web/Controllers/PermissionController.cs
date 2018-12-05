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
    public class PermissionController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_roles })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_role })]
        public ActionResult Crear()
        {


            return View();
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_role })]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Crear([Bind(Include = "id_permission,title,name")] PermissionViewModel pPermissionViewModel)
        {
            // TODO: Add insert logic here

            if (pPermissionViewModel == null)
            {
                return HttpNotFound();
            }
            pPermissionViewModel.id_permission = 0;

            pPermissionViewModel.user_id_created = 0;

            PermissionBL oBL = new PermissionBL();

            oBL.Agregar(pPermissionViewModel);

            return RedirectToAction("Index");

        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_role })]
        public ActionResult Editar(string id)
        {
            PermissionBL oBL = new PermissionBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            PermissionViewModel pPermissionViewModel = oBL.ObtenerPermission(pIntID);

            return View(pPermissionViewModel);
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_role })]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Editar([Bind(Include = "id_permission,title,name")] PermissionViewModel pPermissionViewModel)
        {
            // TODO: Add insert logic here

            if (pPermissionViewModel == null)
            {
                return HttpNotFound();
            }
            PermissionBL oPermissionBL = new PermissionBL();
            oPermissionBL.Modificar(pPermissionViewModel);
            return RedirectToAction("Index");

        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_role })]
        [HttpPost]
        public JsonResult Eliminar(int id)
        {

            PermissionBL oPermissionBL = new PermissionBL();

            oPermissionBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",

            });

        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_role,AuthorizeUserAttribute.Permission.edit_role })]
        [HttpPost]
        public JsonResult Verificar(int id_permission, string name)
        {

            PermissionBL oPermissionBL = new PermissionBL();




            var resultado = oPermissionBL.VerificarDuplicado(id_permission, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_roles })]

        public JsonResult ObtenerListaPermisos(PermissionFiltersViewModel ofilters)//DataTableAjaxPostModel model
        {
            PermissionBL oPermissionBL = new PermissionBL();
            //PermissionFiltersViewModel ofilters = new PermissionFiltersViewModel();
            GridModel<PermissionViewModel> grid = oPermissionBL.ObtenerLista(ofilters);

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
