using Business.Logic;
using CrossCutting.Helper;
using Domain.Entities;
using Domain.Entities.Notifications;
using Presentation.Web.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Activar()
        {
            UserBL oUserBL = new UserBL();
            string user_code = Request.QueryString["code"];
            UserViewModel oUserViewModel = oUserBL.GetByUserCodeActivate(user_code);
            if (oUserViewModel == null || oUserViewModel.id <= 0)
            {

                ViewBag.message_error = "No hay una cuenta asociada al código de activación de contraseña o dicho código ha expirado.";

            }
            else
            {


                oUserBL.ActivarCuenta(oUserViewModel.id);

            }


            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session[System.Configuration.ConfigurationManager.AppSettings["session.usuario.actual"]] = null;

            return RedirectToAction("Login", "Account");
        }
        public ActionResult CambiarPassword()
        {
            CambiarPasswordViewModel pCambiarPasswordViewModel = new CambiarPasswordViewModel();
            pCambiarPasswordViewModel.user_code = Request.QueryString["code"];
            return View(pCambiarPasswordViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CambiarPassword([Bind(Include = "new_pass,user_code")] CambiarPasswordViewModel pCambiarPasswordViewModel)
        {

            UserBL oUserBL = new UserBL();
            UserViewModel oUserViewModel = oUserBL.GetByUserCodeRecover(pCambiarPasswordViewModel.user_code);

            if (oUserViewModel == null || oUserViewModel.id <= 0)
            {
                return Json(new
                {
                    message_error = "No hay una cuenta asociada al código de recuperación de contraseña o dicho código ha expirado.",
                    status = "0",

                });
            }
            if (oUserViewModel.user_status_id == 2)
            {
                return Json(new
                {
                    message_error = "El usuario esta en inactivo. Por favor comuniquese con el administrador del sistema para activar su cuenta",
                    status = "0",

                });
            }

            pCambiarPasswordViewModel.new_pass = Helper.Encripta(pCambiarPasswordViewModel.new_pass);
            pCambiarPasswordViewModel.userd_id = oUserViewModel.id;
            oUserBL.CambiarPassword(pCambiarPasswordViewModel);
            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",

            });


        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.change_own_pass })]
        public ActionResult CambiarMiPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.change_own_pass })]
        public ActionResult CambiarMiPassword([Bind(Include = "userd_id,old_pass,new_pass")] CambiarPasswordViewModel pCambiarPasswordViewModel, string returnUrl)
        {
            int tipo_error = 0;
            UserBL oUserBL = new UserBL();
            CurrentUserViewModel result = oUserBL.ValidarUsuario(AuthorizeUserAttribute.UsuarioLogeado().user_email, Helper.Encripta(pCambiarPasswordViewModel.old_pass), ref tipo_error);
            if (tipo_error == -3)
            {

                return Json(new
                {
                    message_error = "La contraseña anterior no es correcta.",
                    status = "0",

                });
            }
            else
            {
                pCambiarPasswordViewModel.new_pass = Helper.Encripta(pCambiarPasswordViewModel.new_pass);
                pCambiarPasswordViewModel.userd_id = AuthorizeUserAttribute.UsuarioLogeado().user_id;
                oUserBL.CambiarPassword(pCambiarPasswordViewModel);
                return Json(new
                {
                    // this is what datatables wants sending back
                    status = "1",

                });
            }

        }

        public ActionResult Recuperar()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Recuperar([Bind(Include = "user_email")] LoginViewModel pLoginModel)
        {
            UserBL oUserBL = new UserBL();
            UserViewModel oUserViewModel = oUserBL.ObtenerUser(pLoginModel.user_email);
            SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();

            if (oUserViewModel == null || oUserViewModel.id <= 0)
            {
                return Json(new
                {
                    message_error = "No hay una cuenta asociada al correo electrónico ingresado.",
                    status = "0",

                });
            }
            if (oUserViewModel.user_status_id == 2)
            {
                return Json(new
                {
                    message_error = "El usuario esta en inactivo. Por favor comuniquese con el administrador del sistema para activar su cuenta",
                    status = "0",

                });
            }
            string user_code = Guid.NewGuid().ToString();
            oUserBL.ActualizarCodigoRecuperar(oUserViewModel.id, user_code);
            NotificationGeneralAccountViewModel oNotification = new NotificationGeneralAccountViewModel();

            oNotification.url_recuperar_cuenta = ConfigurationManager.AppSettings["site.url"] + "/Account/CambiarPassword/?code=" + user_code;
            oNotification.url_home = ConfigurationManager.AppSettings["site.url"];
            oNotification.url_politicas = ConfigurationManager.AppSettings["site.url.politicas"];
            oNotification.url_contacto = ConfigurationManager.AppSettings["site.url.contacto"];
            oNotification.url_privacidad = ConfigurationManager.AppSettings["site.url.privacidad"];
            oNotification.name = oUserViewModel.contact_name;
            oNotification.to = oUserViewModel.user_email;
            oSendEmailNotificationBL.EnviarNotificacionRecuperarCuenta(oNotification);
            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",

            });

        }
        
        public ActionResult Login(string returnUrl)
        {
           
            ViewBag.ReturnUrl = returnUrl;
            //
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "user_email,user_pass")] LoginViewModel pLoginModel, string returnUrl)
        {



            int tipo_error = 0;
            UserBL oUserBL = new UserBL();
            CurrentUserViewModel result = oUserBL.ValidarUsuario(pLoginModel.user_email, Helper.Encripta(pLoginModel.user_pass), ref tipo_error);
 
            //List<UsuarioAccion> result = oLoginBL.ValidarUsuario(oLoginModel.usuario, oLoginModel.clave, ref tipo_error);

            if (result != null && tipo_error == 0)
            {
                Session[System.Configuration.ConfigurationManager.AppSettings["session.usuario.actual"]] = null;

                result.name_abbre = Helper.Substring(result.name, 20);
                Session[System.Configuration.ConfigurationManager.AppSettings["session.usuario.actual"]] = result;

                oUserBL.ActualizarFechaIngreso(result.user_id);



                if (result.permissions.Count() > 0)
                {
                    if (String.IsNullOrEmpty(returnUrl) || returnUrl=="/")
                    {
                        if (result.permissions.Contains((int)AuthorizeUserAttribute.Permission.list_draft_law))
                            return RedirectToAction("Index", "DraftLaw");

                        if (result.permissions.Contains((int)AuthorizeUserAttribute.Permission.my_draft_laws))
                            return RedirectToAction("MisProyectosLey", "Investigator");

                        if (result.permissions.Contains((int)AuthorizeUserAttribute.Permission.my_concepts))
                            return RedirectToAction("Index", "Concept");

                        if (result.permissions.Contains((int)AuthorizeUserAttribute.Permission.concepts_to_qualify))
                            return RedirectToAction("PorCalificar", "Concept");

                        if (result.permissions.Contains((int)AuthorizeUserAttribute.Permission.concepts_emited))
                            return RedirectToAction("Emitidos", "Concept");



                        return RedirectToAction("Index", "Home");
                    }

                    string strRedirect = returnUrl;
                    return RedirectToLocal(strRedirect);
                }

                else
                {
                    ModelState.AddModelError("", "Usted no tiene permisos a ninguna página del sistema.Comuníquese con el Administrador si desea acceder.");
                }




            }
            else
            {
                if (tipo_error == -1)
                {
                    ModelState.AddModelError("", "El usuario no existe.");
                }
                else if (tipo_error == -2)
                {
                    ModelState.AddModelError("", "El usuario esta en estado inactivo.");
                }
                else if (tipo_error == -3)
                {
                    ModelState.AddModelError("", "El usuario o la contraseña ingresados son incorrectos.");
                }
            }
            return View();
        }


        public ActionResult Crear()
        {
            SelectorBL oSelectorBL = new SelectorBL();

            List<SelectOptionItem> oEstatus = oSelectorBL.EstatusUserSelector();
            List<SelectOptionItem> oRoles = oSelectorBL.RolesSelector();

            List<SelectOptionItem> oNationalities = oSelectorBL.NationalitiesSelector();
            List<SelectOptionItem> oDocumentTypes = oSelectorBL.DocumentTypesSelector();

            List<SelectOptionItem> oGenders = oSelectorBL.GendersSelector();
            List<SelectOptionItem> oPrograms = oSelectorBL.ProgramsSelector();
            List<SelectOptionItem> oInterestAreas = oSelectorBL.InterestAreasSelector();
            List<SelectOptionItem> oDepartments = oSelectorBL.DepartmentsSelector();

            List<SelectOptionItem> oAcademicLevels = oSelectorBL.AcademicLevelsSelector();
            List<SelectOptionItem> oCommissions = oSelectorBL.CommissionsSelector();
            List<SelectOptionItem> oEducationalInstitutions = oSelectorBL.EducationalInstitutionsSelector();

            List<SelectListItem> estatus = Helper.ConstruirDropDownList<SelectOptionItem>(oEstatus, "Value", "Text", "", true, "", "");
            List<SelectListItem> roles = Helper.ConstruirDropDownList<SelectOptionItem>(oRoles, "Value", "Text", "", true, "", "");

            List<SelectListItem> nationalities = Helper.ConstruirDropDownList<SelectOptionItem>(oNationalities, "Value", "Text", "", true, "", "");
            List<SelectListItem> documentTypes = Helper.ConstruirDropDownList<SelectOptionItem>(oDocumentTypes, "Value", "Text", "", true, "", "");
            List<SelectListItem> genders = Helper.ConstruirDropDownList<SelectOptionItem>(oGenders, "Value", "Text", "", true, "", "");

            List<SelectListItem> programs = Helper.ConstruirDropDownList<SelectOptionItem>(oPrograms, "Value", "Text", "", true, "", "");
            List<SelectListItem> interest_areas = Helper.ConstruirDropDownList<SelectOptionItem>(oInterestAreas, "Value", "Text", "", false, "", "");
            List<SelectListItem> departments = Helper.ConstruirDropDownList<SelectOptionItem>(oDepartments, "Value", "Text", "", true, "", "");
            List<SelectListItem> academic_levels = Helper.ConstruirDropDownList<SelectOptionItem>(oAcademicLevels, "Value", "Text", "", true, "", "");
            List<SelectListItem> commissions = Helper.ConstruirDropDownList<SelectOptionItem>(oCommissions, "Value", "Text", "", true, "", "");
            List<SelectListItem> educational_institutions = Helper.ConstruirDropDownList<SelectOptionItem>(oEducationalInstitutions, "Value", "Text", "", true, "", "");

            List<SelectListItem> oListaVacia = Helper.ConstruirDropDownList<SelectOptionItem>(new List<SelectOptionItem>(), "Value", "Text", "", true, "", "");




            ViewBag.programs = oListaVacia;
            ViewBag.education_levels = oListaVacia;
            ViewBag.educational_institutions = educational_institutions;


            ViewBag.institutions = oListaVacia;
            ViewBag.investigation_groups = oListaVacia;
            ViewBag.interest_areas = interest_areas;
            //  ViewBag.programs = programs;
            ViewBag.departments = departments;
            ViewBag.municipalities = oListaVacia;

            ViewBag.estatus = estatus;
            ViewBag.roles = roles;
            ViewBag.nationalities = nationalities;
            ViewBag.documentTypes = documentTypes;
            ViewBag.genders = genders;
            ViewBag.academic_levels = academic_levels;
            ViewBag.commissions = commissions;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public JsonResult Crear([Bind(Include = "investigator_id,user_id,first_name,second_name,last_name,second_last_name,gender_id,mobile_phone," +
            "birthdate_text,user_email,user_pass,document_type_id,doc_nro,nationality_id,contract_name,phone,address,user_pass2,institution_id," +
            "investigation_group_id,program_id,interest_areas,address_country_id,department_id,address_municipality_id,commissions,educational_institution_id,education_level_id,CVLAC")] InvestigatorViewModel pViewModel)
        {
            // TODO: Add insert logic here

            if (pViewModel == null)
            {
                return Json(new
                {
                    message_error = "Datos inavalidos",
                    status = "0",

                });
            }
            pViewModel.investigator_id = 0;

            pViewModel.user_id_created = 0;
            pViewModel.birthdate = DateTime.ParseExact(pViewModel.birthdate_text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var user_pass = pViewModel.user_pass;
            pViewModel.user_pass = Helper.Encripta(pViewModel.user_pass);
            pViewModel.user_name = pViewModel.first_name + " " + pViewModel.second_name + " " + pViewModel.last_name + " " + pViewModel.second_last_name;
            pViewModel.contact_name = pViewModel.first_name + " " + pViewModel.last_name;
            string user_code = Guid.NewGuid().ToString();
            pViewModel.user_code_activate = user_code;
            UserBL oBL = new UserBL();

            var user_id = oBL.AgregarInvestigador(pViewModel);

            SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();

            NotificationGeneralAccountViewModel oNotification = new NotificationGeneralAccountViewModel();


            oNotification.url_activar_cuenta = ConfigurationManager.AppSettings["site.url"] + "/Account/Activar/?code=" + user_code;
            oNotification.url_home = ConfigurationManager.AppSettings["site.url"];

            oNotification.url_politicas = ConfigurationManager.AppSettings["site.url.politicas"];
            oNotification.url_contacto = ConfigurationManager.AppSettings["site.url.contacto"];
            oNotification.url_privacidad= ConfigurationManager.AppSettings["site.url.privacidad"];
            oNotification.name = pViewModel.contact_name;
            oNotification.to = pViewModel.user_email;
            oSendEmailNotificationBL.EnviarNotificacionActivarCuenta(oNotification);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",

            });

        }

        public ActionResult Editar(string id)
        {
            UserBL oBL = new UserBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            InvestigatorViewModel pViewModel = oBL.ObtenerInvestigator(pIntID);
            SelectorBL oSelectorBL = new SelectorBL();
            pViewModel.birthdate_text = pViewModel.birthdate.HasValue ? pViewModel.birthdate.Value.ToString("dd/MM/YYYY") : String.Empty;
            List<SelectOptionItem> oEstatus = oSelectorBL.EstatusUserSelector();
            List<SelectOptionItem> oRoles = oSelectorBL.RolesSelector();

            List<SelectOptionItem> oNationalities = oSelectorBL.NationalitiesSelector();
            List<SelectOptionItem> oDocumentTypes = oSelectorBL.DocumentTypesSelector();

            List<SelectOptionItem> oGenders = oSelectorBL.GendersSelector();
            List<SelectOptionItem> oPrograms = oSelectorBL.ProgramsSelector(pViewModel.educational_institution_id.Value);
            List<SelectOptionItem> oInterestAreas = oSelectorBL.InterestAreasSelector();
            List<SelectOptionItem> oDepartments = oSelectorBL.DepartmentsSelector();
            List<SelectOptionItem> oMunicipalities = oSelectorBL.MunicipalitiesSelector(pViewModel.department_id.Value);

            List<SelectOptionItem> oAcademicLevels = oSelectorBL.AcademicLevelsSelector();
            List<SelectOptionItem> oCommissions = oSelectorBL.CommissionsSelector();
            List<SelectOptionItem> oEducationalInstitutions = oSelectorBL.EducationalInstitutionsSelector();

            List<SelectOptionItem> oEducationLevels = oSelectorBL.EducationLevelsSelector(pViewModel.educational_institution_id.Value, pViewModel.program_id.Value);

            List<SelectOptionItem> oInvestigationGroups = oSelectorBL.InvestigationGroupsSelector(pViewModel.institution_id.Value);

            List<SelectListItem> estatus = Helper.ConstruirDropDownList<SelectOptionItem>(oEstatus, "Value", "Text", "", true, "", "");
            List<SelectListItem> roles = Helper.ConstruirDropDownList<SelectOptionItem>(oRoles, "Value", "Text", "", true, "", "");

            List<SelectListItem> nationalities = Helper.ConstruirDropDownList<SelectOptionItem>(oNationalities, "Value", "Text", "", true, "", "");
            List<SelectListItem> documentTypes = Helper.ConstruirDropDownList<SelectOptionItem>(oDocumentTypes, "Value", "Text", "", true, "", "");
            List<SelectListItem> genders = Helper.ConstruirDropDownList<SelectOptionItem>(oGenders, "Value", "Text", "", true, "", "");

            List<SelectListItem> programs = Helper.ConstruirDropDownList<SelectOptionItem>(oPrograms, "Value", "Text", "", true, "", "");
            List<SelectListItem> interest_areas = Helper.ConstruirDropDownList<SelectOptionItem>(oInterestAreas, "Value", "Text", "", false, "", "");
            List<SelectListItem> departments = Helper.ConstruirDropDownList<SelectOptionItem>(oDepartments, "Value", "Text", "", true, "", "");
            List<SelectListItem> municipalities = Helper.ConstruirDropDownList<SelectOptionItem>(oMunicipalities, "Value", "Text", "", true, "", "");

            List<SelectListItem> academic_levels = Helper.ConstruirDropDownList<SelectOptionItem>(oAcademicLevels, "Value", "Text", "", true, "", "");
            List<SelectListItem> commissions = Helper.ConstruirDropDownList<SelectOptionItem>(oCommissions, "Value", "Text", "", true, "", "");
            List<SelectListItem> educational_institutions = Helper.ConstruirDropDownList<SelectOptionItem>(oEducationalInstitutions, "Value", "Text", "", true, "", "");


            List<SelectListItem> investigation_groups = Helper.ConstruirDropDownList<SelectOptionItem>(oInvestigationGroups, "Value", "Text", "", true, "", "");
            List<SelectListItem> education_levels = Helper.ConstruirDropDownList<SelectOptionItem>(oEducationLevels, "Value", "Text", "", true, "", "");


            List<SelectListItem> oListaVacia = Helper.ConstruirDropDownList<SelectOptionItem>(new List<SelectOptionItem>(), "Value", "Text", "", true, "", "");


            interest_areas = interest_areas.Select(a => new SelectListItem
            {
                Text = a.Text,
                Value = a.Value,
                Selected = pViewModel.interest_areas.Contains(int.Parse(a.Value))
            }).ToList();


            List<SelectListItem> institutions = new List<SelectListItem>();

            institutions.Add(new SelectListItem
            {
                Text = pViewModel.institution,
                Value = pViewModel.institution_id.ToString(),
                Selected = true
            });

            ViewBag.programs = programs;
            ViewBag.education_levels = education_levels;
            ViewBag.educational_institutions = educational_institutions;


            ViewBag.institutions = institutions;
            ViewBag.investigation_groups = investigation_groups;
            ViewBag.interest_areas = interest_areas;
            //  ViewBag.programs = programs;
            ViewBag.departments = departments;
            ViewBag.municipalities = municipalities;

            ViewBag.estatus = estatus;
            ViewBag.roles = roles;
            ViewBag.nationalities = nationalities;
            ViewBag.documentTypes = documentTypes;
            ViewBag.genders = genders;
            ViewBag.academic_levels = academic_levels;
            pViewModel.commissionsMultiSelectList = new MultiSelectList(oSelectorBL.CommissionsSelector(), "Value", "Text"); ;

            return View(pViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Editar([Bind(Include = "investigator_id,user_id,first_name,second_name,last_name,second_last_name,gender_id,mobile_phone," +
            "birthdate_text,user_email,user_pass,document_type_id,doc_nro,nationality_id,contract_name,phone,address,user_pass2,institution_id," +
            "investigation_group_id,program_id,interest_areas,address_country_id,department_id,address_municipality_id,commissions,educational_institution_id,education_level_id,CVLAC")]  InvestigatorViewModel pViewModel)
        {
            // TODO: Add insert logic here

            if (pViewModel == null)
            {
                return HttpNotFound();
            }
            UserBL oUserBL = new UserBL();
            pViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            pViewModel.user_name = pViewModel.first_name + " " + pViewModel.second_name + " " + pViewModel.last_name + " " + pViewModel.second_last_name;
            pViewModel.contact_name = pViewModel.first_name + " " + pViewModel.last_name;
            pViewModel.birthdate = DateTime.ParseExact(pViewModel.birthdate_text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            pViewModel.avatar = null;
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid().ToString() + "." + extension;


                    pViewModel.avatar = "/Avatars/" + fileName;
                    var path = Path.Combine(Server.MapPath("~/Avatars/"), fileName);
                    file.SaveAs(path);
                }
            }

            oUserBL.ModificarInvestigator(pViewModel);

            Session[System.Configuration.ConfigurationManager.AppSettings["session.usuario.actual"]] = oUserBL.GetCurrentUser(pViewModel.user_id.Value);

            return Redirect("/Account/Editar/" + pViewModel.investigator_id);

        }


        [HttpPost]
        public JsonResult ObtenerMunicipalities(/*string q, int page*/)
        {

            SelectorBL oBL = new SelectorBL();
            string term_search = Request.Form["q"];
            string str_department_id = Request.Form["department_id"];
            int department_id = 0;
            int.TryParse(str_department_id, out department_id);
            // var resultado = oUserBL.VerificarDuplicado(user_id, email);

            var results = oBL.MunicipalitiesSelector(department_id);
            return Json(results);

        }
        [HttpPost]

        public JsonResult ActualizarNotificacion(int notification_id)
        {

            NotificationBL oNotificationBL = new NotificationBL();

            oNotificationBL.ActualizarLeido(notification_id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",

            });

        }
        [HttpPost]
        public JsonResult ObtenerInstituciones(/*string q, int page*/)
        {

            UserBL oUserBL = new UserBL();
            string term_search = Request.Form["q"];
            string qpage = Request.Form["page"];
            int page = 0;
            int.TryParse(qpage, out page);
            // var resultado = oUserBL.VerificarDuplicado(user_id, email);

            Select2Model results = oUserBL.ObtenerInstituciones(term_search, page);
            return Json(results);

        }

        [HttpPost]
        public JsonResult ObtenerGrupoInvestigacion(/*string q, int page*/)
        {

            SelectorBL oBL = new SelectorBL();
            string term_search = Request.Form["q"];
            string str_institution_id = Request.Form["institution_id"];
            int institution_id = 0;
            int.TryParse(str_institution_id, out institution_id);
            // var resultado = oUserBL.VerificarDuplicado(user_id, email);

            var results = oBL.InvestigationGroupsSelector(institution_id);
            return Json(results);

        }

        [HttpPost]
        public JsonResult ObtenerPrograma(/*string q, int page*/)
        {

            SelectorBL oBL = new SelectorBL();
            string term_search = Request.Form["q"];
            string str_educational_institution_id = Request.Form["educational_institution_id"];
            int educational_institution_id = 0;
            int.TryParse(str_educational_institution_id, out educational_institution_id);
            // var resultado = oUserBL.VerificarDuplicado(user_id, email);

            var results = oBL.ProgramsSelector(educational_institution_id);
            return Json(results);

        }

        [HttpPost]
        public JsonResult ObtenerNivelFormacion(/*string q, int page*/)
        {

            SelectorBL oBL = new SelectorBL();
            string term_search = Request.Form["q"];
            string str_educational_institution_id = Request.Form["educational_institution_id"];
            int educational_institution_id = 0;
            int.TryParse(str_educational_institution_id, out educational_institution_id);

            string str_program_id = Request.Form["program_id"];
            int program_id = 0;
            int.TryParse(str_program_id, out program_id);



            // var resultado = oUserBL.VerificarDuplicado(user_id, email);

            var results = oBL.EducationLevelsSelector(educational_institution_id, program_id);
            return Json(results);

        }



        [HttpPost]
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