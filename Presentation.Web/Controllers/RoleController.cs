

using Business.Logic;
using Domain.Entities;
using Presentation.Web.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web.Controllers
{
    public class RoleController : Controller
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
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_role, AuthorizeUserAttribute.Permission.edit_role })]
        [HttpPost]
        public JsonResult Verificar(int id_role, string name)
        {

            RoleBL oBL = new RoleBL();




            var resultado = oBL.VerificarDuplicado(id_role, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }
       //[AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_role })]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Crear([Bind(Include = "role_id,role")] RoleViewModel pRoleViewModel)
        {
            // TODO: Add insert logic here

            if (pRoleViewModel == null)
            {
                return HttpNotFound();
            }
           if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                
                if (file != null && file.ContentLength > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid().ToString() + "." + extension;


                    pRoleViewModel.manual_file = "/Manual/" + fileName;
                    var path = Path.Combine(Server.MapPath("~/Manual/"), fileName);
                    file.SaveAs(path);
                }
            }
            pRoleViewModel.role_id = 0;

            pRoleViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            
            RoleBL oBL = new RoleBL();

            oBL.Agregar(pRoleViewModel);

            return RedirectToAction("Index");

        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.role_permissions })]
        public ActionResult Permisos(string id)
        {
            RoleBL oBL = new RoleBL();
            PermissionBL oPermissionBL = new PermissionBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            RoleViewModel pRoleViewModel = oBL.ObtenerRole(pIntID);
            ViewBag.NameRole = pRoleViewModel.role;
            ViewBag.role_id = pRoleViewModel.role_id;

            var all = oPermissionBL.ObtenerListaPermisos();

            List<CheckboxViewModel> permisos = new List<CheckboxViewModel>();
            var permission_enabled = oPermissionBL.ObtenerListaPermisos(pIntID);
            foreach (var permiso in all)
            {
                CheckboxViewModel oCheckboxViewModel = new CheckboxViewModel();
                oCheckboxViewModel.Name = permiso.title;
                oCheckboxViewModel.Value = permiso.id_permission.ToString();
                if (permission_enabled.Contains(permiso.id_permission))
                    oCheckboxViewModel.Checked = "checked";
                else
                    oCheckboxViewModel.Checked = String.Empty;

                permisos.Add(oCheckboxViewModel);
            }

            ViewBag.permisos = permisos;

            return View(pRoleViewModel);
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.role_permissions })]
        [HttpPost]
        public JsonResult Permisos(int role_id, string ids)
        {

            RoleBL oRoleBL = new RoleBL();

            /* oRoleBL.Eliminar(id);*/
            oRoleBL.GuardarPermisos(role_id, ids);
            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",

            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_role })]
        public ActionResult Editar(string id)
        {
            RoleBL oBL = new RoleBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            RoleViewModel pRoleViewModel = oBL.ObtenerRole(pIntID);

            return View(pRoleViewModel);
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_role })]
        public ActionResult EditarManual(string id)
        {
            RoleBL oBL = new RoleBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            RoleViewModel pRoleViewModel = oBL.ObtenerRole(pIntID);

            return View(pRoleViewModel);
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_role })]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Editar([Bind(Include = "role_id,role")] RoleViewModel pRoleViewModel)
        {
            // TODO: Add insert logic here

            if (pRoleViewModel == null)
            {
                return HttpNotFound();
            }

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid().ToString() + extension;


                    pRoleViewModel.manual_file = "/Manual/" + fileName;
                    var path = Path.Combine(Server.MapPath("~/Manual/"), fileName);
                    file.SaveAs(path);
                }
            }

            RoleBL oRoleBL = new RoleBL();
            pRoleViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oRoleBL.Modificar(pRoleViewModel);
            return RedirectToAction("Index");

        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_role })]
        [HttpPost]
        public JsonResult Eliminar(int id)
        {

            RoleBL oRoleBL = new RoleBL();

            oRoleBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",

            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_roles })]
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