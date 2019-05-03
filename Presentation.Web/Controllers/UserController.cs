using Business.Logic;
using CrossCutting.Helper;
using Domain.Entities;
using Domain.Entities.Notifications;
using Presentation.Web.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
            List<SelectOptionItem> oDepartments = oSelectorBL.DepartmentsSelector();


            List<SelectListItem> estatus = Helper.ConstruirDropDownList<SelectOptionItem>(oEstatus, "Value", "Text", "", true, "", "");
            List<SelectListItem> roles = Helper.ConstruirDropDownList<SelectOptionItem>(oRoles, "Value", "Text", "", true, "", "");

            List<SelectListItem> nationalities = Helper.ConstruirDropDownList<SelectOptionItem>(oNationalities, "Value", "Text", "", true, "", "");
            List<SelectListItem> documentTypes = Helper.ConstruirDropDownList<SelectOptionItem>(oDocumentTypes, "Value", "Text", "", true, "", "");
            List<SelectListItem> departments = Helper.ConstruirDropDownList<SelectOptionItem>(oDepartments, "Value", "Text", "", true, "", "");

            List<SelectListItem> oListaVacia = Helper.ConstruirDropDownList<SelectOptionItem>(new List<SelectOptionItem>(), "Value", "Text", "", true, "", "");

            ViewBag.estatus = estatus;
            ViewBag.roles = roles;
            ViewBag.nationalities = nationalities;
            ViewBag.documentTypes = documentTypes;

            ViewBag.departments = departments;
            ViewBag.municipalities = oListaVacia;

            List<SelectListItem> institutions = new List<SelectListItem>();
            ViewBag.institutions = institutions;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_user })]
        public ActionResult Crear([Bind(Include = "id,user_name,user_email,user_email2,user_pass,document_type_id,doc_nro,nationality_id,contact_name,phone,address,user_role_id,user_status_id,institution_ids,address_country_id,department_id,address_municipality_id")] UserViewModel pUserViewModel)
        {
            // TODO: Add insert logic here

            if (pUserViewModel == null)
            {
                return HttpNotFound();
            }
            pUserViewModel.id = 0;

            pUserViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            
            pUserViewModel.user_pass = Helper.Encripta(Guid.NewGuid().ToString());
            string user_code = Guid.NewGuid().ToString();
            pUserViewModel.user_code_recover= user_code;
            UserBL oUserBL = new UserBL();

            oUserBL.Agregar(pUserViewModel);

            SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();

          
            
            NotificationGeneralAccountViewModel oNotification = new NotificationGeneralAccountViewModel();

            oNotification.url_recuperar_cuenta = ConfigurationManager.AppSettings["site.url"] + "/Account/CambiarPassword/?code=" + user_code;
            oNotification.url_home = ConfigurationManager.AppSettings["site.url"];
            oNotification.url_politicas = ConfigurationManager.AppSettings["site.url.politicas"];
            oNotification.url_contacto = ConfigurationManager.AppSettings["site.url.contacto"];
            oNotification.url_privacidad = ConfigurationManager.AppSettings["site.url.privacidad"];
            oNotification.name = pUserViewModel.contact_name;
            oNotification.to = pUserViewModel.user_email;
            oSendEmailNotificationBL.EnviarNotificacionNuevaCuenta(oNotification);

            return RedirectToAction("Index");

        }
        
        public ActionResult MiCuenta()
        {
            UserBL oBL = new UserBL();
            var user_id = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            UserViewModel pUserViewModel = oBL.ObtenerUser(user_id);
            SelectorBL oSelectorBL = new SelectorBL();

            List<SelectOptionItem> oEstatus = oSelectorBL.EstatusUserSelector();
            List<SelectOptionItem> oRoles = oSelectorBL.RolesSelector();

            List<SelectOptionItem> oNationalities = oSelectorBL.NationalitiesSelector();
            List<SelectOptionItem> oDocumentTypes = oSelectorBL.DocumentTypesSelector();

            List<SelectOptionItem> oDepartments = oSelectorBL.DepartmentsSelector();
            List<SelectOptionItem> oMunicipalities = oSelectorBL.MunicipalitiesSelector(pUserViewModel.department_id.HasValue ? pUserViewModel.department_id.Value : 0);



            List<SelectListItem> estatus = Helper.ConstruirDropDownList<SelectOptionItem>(oEstatus, "Value", "Text", "", true, "", "");
            List<SelectListItem> roles = Helper.ConstruirDropDownList<SelectOptionItem>(oRoles, "Value", "Text", "", true, "", "");

            List<SelectListItem> nationalities = Helper.ConstruirDropDownList<SelectOptionItem>(oNationalities, "Value", "Text", "", true, "", "");
            List<SelectListItem> documentTypes = Helper.ConstruirDropDownList<SelectOptionItem>(oDocumentTypes, "Value", "Text", "", true, "", "");

            List<SelectListItem> departments = Helper.ConstruirDropDownList<SelectOptionItem>(oDepartments, "Value", "Text", "", true, "", "");
            List<SelectListItem> municipalities = Helper.ConstruirDropDownList<SelectOptionItem>(oMunicipalities, "Value", "Text", "", true, "", "");


            ViewBag.estatus = estatus;
            ViewBag.roles = roles;
            ViewBag.nationalities = nationalities;
            ViewBag.documentTypes = documentTypes;
            ViewBag.departments = departments;
            ViewBag.municipalities = municipalities;

            return View(pUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult MiCuenta([Bind(Include = "id,user_name,user_email,user_email2,user_pass,document_type_id,doc_nro,nationality_id,contact_name,phone,address,user_role_id,user_status_id,address_country_id,department_id,address_municipality_id")] UserViewModel pUserViewModel)
        {
            // TODO: Add insert logic here

            if (pUserViewModel == null)
            {
                return HttpNotFound();
            }
            pUserViewModel.id = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            UserBL oUserBL = new UserBL();
            pUserViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            pUserViewModel.avatar = null;
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid().ToString() + "." + extension;


                    pUserViewModel.avatar = "/Avatars/" + fileName;
                    var path = Path.Combine(Server.MapPath("~/Avatars/"), fileName);
                    file.SaveAs(path);
                }
            }
            oUserBL.ModificarMicuenta(pUserViewModel);
            CurrentUserViewModel result = oUserBL.GetCurrentUser(pUserViewModel.id);
            result.name_abbre = Helper.Substring(result.name, 20);
            Session[System.Configuration.ConfigurationManager.AppSettings["session.usuario.actual"]] = result;

            return Redirect("/User/MiCuenta/");
            

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

            List<SelectOptionItem> oDepartments = oSelectorBL.DepartmentsSelector();
            List<SelectOptionItem> oMunicipalities = oSelectorBL.MunicipalitiesSelector(pUserViewModel.department_id.HasValue ? pUserViewModel.department_id.Value : 0);



            List<SelectListItem> estatus = Helper.ConstruirDropDownList<SelectOptionItem>(oEstatus, "Value", "Text", "", true, "", "");
            List<SelectListItem> roles = Helper.ConstruirDropDownList<SelectOptionItem>(oRoles, "Value", "Text", "", true, "", "");

            List<SelectListItem> nationalities = Helper.ConstruirDropDownList<SelectOptionItem>(oNationalities, "Value", "Text", "", true, "", "");
            List<SelectListItem> documentTypes = Helper.ConstruirDropDownList<SelectOptionItem>(oDocumentTypes, "Value", "Text", "", true, "", "");

            List<SelectListItem> departments = Helper.ConstruirDropDownList<SelectOptionItem>(oDepartments, "Value", "Text", "", true, "", "");
            List<SelectListItem> municipalities = Helper.ConstruirDropDownList<SelectOptionItem>(oMunicipalities, "Value", "Text", "", true, "", "");

            ViewBag.estatus = estatus;
            ViewBag.roles = roles;
            ViewBag.nationalities = nationalities;
            ViewBag.documentTypes = documentTypes;
            ViewBag.departments = departments;
            ViewBag.municipalities = municipalities;

            pUserViewModel.institutionsMultiSelectList = new MultiSelectList(oSelectorBL.InstitutionsSelector(pUserViewModel.institution_ids), "Value", "Text");


            return View(pUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_user })]
        public ActionResult Editar([Bind(Include = "id,user_name,user_email,user_email2,user_pass,document_type_id,doc_nro,nationality_id,contact_name,phone,address,user_role_id,user_status_id,institution_ids,address_country_id,department_id,address_municipality_id")] UserViewModel pUserViewModel)
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