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
    public class AccountController : Controller
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

            List<SelectOptionItem> oNationalities = oSelectorBL.NationalitiesSelector();
            List<SelectOptionItem> oDocumentTypes = oSelectorBL.DocumentTypesSelector();

            List<SelectOptionItem> oGenders = oSelectorBL.GendersSelector();
            

            List<SelectListItem> estatus = Helper.ConstruirDropDownList<SelectOptionItem>(oEstatus, "Value", "Text", "", true, "", "");
            List<SelectListItem> roles = Helper.ConstruirDropDownList<SelectOptionItem>(oRoles, "Value", "Text", "", true, "", "");

            List<SelectListItem> nationalities = Helper.ConstruirDropDownList<SelectOptionItem>(oNationalities, "Value", "Text", "", true, "", "");
            List<SelectListItem> documentTypes = Helper.ConstruirDropDownList<SelectOptionItem>(oDocumentTypes, "Value", "Text", "", true, "", "");
            List<SelectListItem> genders = Helper.ConstruirDropDownList<SelectOptionItem>(oGenders, "Value", "Text", "", true, "", "");

            ViewBag.estatus = estatus;
            ViewBag.roles = roles;
            ViewBag.nationalities = nationalities;
            ViewBag.documentTypes = documentTypes;
            ViewBag.genders = genders;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Crear([Bind(Include = "investigator_id,user_id,first_name,second_name,last_name,second_last_name,gender_id,mobile_phone,birthdate,user_email,user_pass,document_type_id,doc_nro,nationality_id,contract_name,phone,address")] InvestigatorViewModel pViewModel)
        {
            // TODO: Add insert logic here

            if (pViewModel == null)
            {
                return HttpNotFound();
            }
            pViewModel.investigator_id= 0;

            pViewModel.user_id_created = 0;

            UserBL oBL = new UserBL();

            oBL.AgregarInvestigador(pViewModel);

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