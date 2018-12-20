using Business.Logic;
using CrossCutting.Helper;
using Domain.Entities;
using Presentation.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web.Controllers
{
    public class UserController : Controller
    {
        [AuthorizeUser(Permissions  = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_users })]
        
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_user })]
        public ActionResult Crear()
        {
            SelectorBL oSelectorBL = new SelectorBL();

            List<SelectOptionItem> oEstatus = oSelectorBL.EstatusUserSelector();
            List<SelectOptionItem> oRoles = oSelectorBL.RolesSelector();

            List<SelectOptionItem> oNationalities = oSelectorBL.NationalitiesSelector();
            List<SelectOptionItem> oDocumentTypes = oSelectorBL.DocumentTypesSelector();


            List<SelectListItem> estatus = Helper.ConstruirDropDownList<SelectOptionItem>(oEstatus, "Value", "Text", "", true, "", "");
            List<SelectListItem> roles = Helper.ConstruirDropDownList<SelectOptionItem>(oRoles, "Value", "Text", "", true, "", "");

            List<SelectListItem> nationalities = Helper.ConstruirDropDownList<SelectOptionItem>(oNationalities, "Value", "Text", "", true, "", "");
            List<SelectListItem> documentTypes = Helper.ConstruirDropDownList<SelectOptionItem>(oDocumentTypes, "Value", "Text", "", true, "", "");

            ViewBag.estatus = estatus;
            ViewBag.roles = roles;
            ViewBag.nationalities = nationalities;
            ViewBag.documentTypes = documentTypes;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_user })]
        public ActionResult Crear([Bind(Include = "id,user_name,user_email,user_pass,document_type_id,doc_nro,nationality_id,contact_name,phone,address,user_role_id,user_status_id")] UserViewModel pUserViewModel)
        {
            // TODO: Add insert logic here

            if (pUserViewModel == null)
            {
                return HttpNotFound();
            }
            pUserViewModel.id = 0;

            pUserViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            
            pUserViewModel.user_pass = Helper.Encripta("1234Abcd");
            UserBL oBL = new UserBL();

            oBL.Agregar(pUserViewModel);

            return RedirectToAction("Index");

        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_user })]
        public ActionResult Editar(string id)
        {
            UserBL oBL = new UserBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            UserViewModel pUserViewModel = oBL.ObtenerUser(pIntID);
            SelectorBL oSelectorBL = new SelectorBL();

            List<SelectOptionItem> oEstatus = oSelectorBL.EstatusUserSelector();
            List<SelectOptionItem> oRoles = oSelectorBL.RolesSelector();

            List<SelectOptionItem> oNationalities = oSelectorBL.NationalitiesSelector();
            List<SelectOptionItem> oDocumentTypes = oSelectorBL.DocumentTypesSelector();


            List<SelectListItem> estatus = Helper.ConstruirDropDownList<SelectOptionItem>(oEstatus, "Value", "Text", "", true, "", "");
            List<SelectListItem> roles = Helper.ConstruirDropDownList<SelectOptionItem>(oRoles, "Value", "Text", "", true, "", "");

            List<SelectListItem> nationalities = Helper.ConstruirDropDownList<SelectOptionItem>(oNationalities, "Value", "Text", "", true, "", "");
            List<SelectListItem> documentTypes = Helper.ConstruirDropDownList<SelectOptionItem>(oDocumentTypes, "Value", "Text", "", true, "", "");

            ViewBag.estatus = estatus;
            ViewBag.roles = roles;
            ViewBag.nationalities = nationalities;
            ViewBag.documentTypes = documentTypes;

            return View(pUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_user })]
        public ActionResult Editar([Bind(Include = "id,user_name,user_email,user_pass,document_type_id,doc_nro,nationality_id,contact_name,phone,address,user_role_id,user_status_id")] UserViewModel pUserViewModel)
        {
            // TODO: Add insert logic here

            if (pUserViewModel == null)
            {
                return HttpNotFound();
            }
            UserBL oUserBL = new UserBL();
            pUserViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oUserBL.Modificar(pUserViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
      //  [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_user, AuthorizeUserAttribute.Permission.new_user })]
        public JsonResult Verificar(int user_id, string email)
        {

            UserBL oUserBL = new UserBL();
            var resultado = oUserBL.VerificarDuplicado(user_id, email);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }


        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_user })]
        public JsonResult Eliminar(int id)
        {

            UserBL oUserBL = new UserBL();

            oUserBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",

            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_users })]
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