using Business.Logic;
using CrossCutting.Helper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public ActionResult CerrarSesion()
        {
            Session[System.Configuration.ConfigurationManager.AppSettings["session.usuario.actual"]] = null;

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Login()
        {
            return View();
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


                Session[System.Configuration.ConfigurationManager.AppSettings["session.usuario.actual"]] = result;

                return Redirect("/User");

                /* if (result.perfiles.Count() > 0)
                 {
                     if (String.IsNullOrEmpty(returnUrl))
                         returnUrl = ObtenerUrlRedirectInicio();
                     string strRedirect = returnUrl;

                     return RedirectToLocal(strRedirect);
                 }

                 else
                 {
                     ModelState.AddModelError("", "Usted no tiene permisos a ninguna página del sistema.Comuníquese con el Administrador si desea acceder.");
                 }
                 */



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

            List<SelectListItem> estatus = Helper.ConstruirDropDownList<SelectOptionItem>(oEstatus, "Value", "Text", "", true, "", "");
            List<SelectListItem> roles = Helper.ConstruirDropDownList<SelectOptionItem>(oRoles, "Value", "Text", "", true, "", "");

            List<SelectListItem> nationalities = Helper.ConstruirDropDownList<SelectOptionItem>(oNationalities, "Value", "Text", "", true, "", "");
            List<SelectListItem> documentTypes = Helper.ConstruirDropDownList<SelectOptionItem>(oDocumentTypes, "Value", "Text", "", true, "", "");
            List<SelectListItem> genders = Helper.ConstruirDropDownList<SelectOptionItem>(oGenders, "Value", "Text", "", true, "", "");

            List<SelectListItem> programs = Helper.ConstruirDropDownList<SelectOptionItem>(oPrograms, "Value", "Text", "", true, "", "");

            List<SelectListItem> oListaVacia = Helper.ConstruirDropDownList<SelectOptionItem>(new List<SelectOptionItem>(), "Value", "Text", "", true, "", "");

            ViewBag.institutions = oListaVacia;
            ViewBag.investigation_groups = oListaVacia;
            ViewBag.interest_areas = oListaVacia;
            ViewBag.programs = programs;

            ViewBag.estatus = estatus;
            ViewBag.roles = roles;
            ViewBag.nationalities = nationalities;
            ViewBag.documentTypes = documentTypes;
            ViewBag.genders = genders;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Crear([Bind(Include = "investigator_id,user_id,first_name,second_name,last_name,second_last_name,gender_id,mobile_phone,birthdate_text,user_email,user_pass,document_type_id,doc_nro,nationality_id,contract_name,phone,address,user_pass2,institution_id,investigation_group_id,program_id,interest_areas")] InvestigatorViewModel pViewModel)
        {
            // TODO: Add insert logic here

            if (pViewModel == null)
            {
                return HttpNotFound();
            }
            pViewModel.investigator_id= 0;

            pViewModel.user_id_created = 0;
            pViewModel.birthdate = DateTime.ParseExact(pViewModel.birthdate_text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var user_pass = pViewModel.user_pass;
            pViewModel.user_pass = Helper.Encripta(pViewModel.user_pass);
            pViewModel.user_name = pViewModel.first_name + " " + pViewModel.second_name + " " + pViewModel.last_name + " " + pViewModel.second_last_name;
            pViewModel.contact_name = pViewModel.user_name;
           UserBL oBL = new UserBL();

            oBL.AgregarInvestigador(pViewModel);

            return RedirectToAction("Index","User");

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

        public ActionResult Editar([Bind(Include = "id,user_name,user_email,user_pass,document_type_id,doc_nro,nationality_id,contract_name,phone,address,user_role_id,user_status_id")] UserViewModel pUserViewModel)
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
        public JsonResult ObtenerAreasInteres(/*string q, int page*/)
        {

            SelectorBL oBL = new SelectorBL();
            string term_search = Request.Form["q"];
            string str_investigation_group_id = Request.Form["investigation_group_id"];
            int investigation_group_id = 0;
            int.TryParse(str_investigation_group_id, out investigation_group_id);
            // var resultado = oUserBL.VerificarDuplicado(user_id, email);

            var results = oBL.InterestAreasSelector(investigation_group_id);
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