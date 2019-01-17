

using Business.Logic;
using CrossCutting.Helper;
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
    public class SnieController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_snies })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_snies })]
        public ActionResult Crear()
        {

            SelectorBL oSelectorBL = new SelectorBL();

            List<SelectOptionItem> oEducationalInstitutions = oSelectorBL.EducationalInstitutionsSelector();
            List<SelectListItem> educational_institutions = Helper.ConstruirDropDownList<SelectOptionItem>(oEducationalInstitutions, "Value", "Text", "", true, "", "");
            ViewBag.educational_institutions = educational_institutions;


            List<SelectOptionItem> oknowledge_areas = oSelectorBL.KnowledgeAreasSelector();
            List<SelectListItem> knowledge_areas = Helper.ConstruirDropDownList<SelectOptionItem>(oknowledge_areas, "Value", "Text", "", true, "", "");
            ViewBag.knowledge_areas = knowledge_areas;

            List<SelectOptionItem> oprograms = oSelectorBL.ProgramsSelector();
            List<SelectListItem> programs = Helper.ConstruirDropDownList<SelectOptionItem>(oprograms, "Value", "Text", "", true, "", "");
            ViewBag.programs = programs;

            List<SelectOptionItem> oacademic_levels = oSelectorBL.AcademicLevelsSelector();
            List<SelectListItem> academic_levels = Helper.ConstruirDropDownList<SelectOptionItem>(oacademic_levels, "Value", "Text", "", true, "", "");
            ViewBag.academic_levels = academic_levels;

            List<SelectOptionItem> oeducation_levels = oSelectorBL.EducationLevelsSelector();
            List<SelectListItem> education_levels = Helper.ConstruirDropDownList<SelectOptionItem>(oeducation_levels, "Value", "Text", "", true, "", "");
            ViewBag.education_levels = education_levels;


            return View();
        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_snies, AuthorizeUserAttribute.Permission.edit_snies })]
        public JsonResult Verificar(int id_snie, string name)
        {

            SnieBL oBL = new SnieBL();
            var resultado = oBL.VerificarDuplicado(id_snie, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_snies })]
        public ActionResult Crear([Bind(Include = "educational_institution_id,knowledge_area_id,program_id,academic_level_id,education_level_id, snie_id,name")] SnieViewModel pSnieViewModel)
        {
            // TODO: Add insert logic here

            if (pSnieViewModel == null)
            {
                return HttpNotFound();
            }
            pSnieViewModel.snie_id = 0;
            pSnieViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;

            SnieBL oBL = new SnieBL();
            oBL.Agregar(pSnieViewModel);
            return RedirectToAction("Index");

        }



        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_snies })]
        public ActionResult Editar(string id)
        {
            SnieBL oBL = new SnieBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            SnieViewModel pSnieViewModel = oBL.Obtener(pIntID);
            SelectorBL oSelectorBL = new SelectorBL();

            List<SelectOptionItem> oEducationalInstitutions = oSelectorBL.EducationalInstitutionsSelector();
            List<SelectListItem> educational_institutions = Helper.ConstruirDropDownList<SelectOptionItem>(oEducationalInstitutions, "Value", "Text", "", true, "", "");
            ViewBag.educational_institutions = educational_institutions;


            List<SelectOptionItem> oknowledge_areas = oSelectorBL.KnowledgeAreasSelector();
            List<SelectListItem> knowledge_areas = Helper.ConstruirDropDownList<SelectOptionItem>(oknowledge_areas, "Value", "Text", "", true, "", "");
            ViewBag.knowledge_areas = knowledge_areas;

            List<SelectOptionItem> oprograms = oSelectorBL.ProgramsSelector();
            List<SelectListItem> programs = Helper.ConstruirDropDownList<SelectOptionItem>(oprograms, "Value", "Text", "", true, "", "");
            ViewBag.programs = programs;

            List<SelectOptionItem> oacademic_levels = oSelectorBL.AcademicLevelsSelector();
            List<SelectListItem> academic_levels = Helper.ConstruirDropDownList<SelectOptionItem>(oacademic_levels, "Value", "Text", "", true, "", "");
            ViewBag.academic_levels = academic_levels;

            List<SelectOptionItem> oeducation_levels = oSelectorBL.EducationLevelsSelector();
            List<SelectListItem> education_levels = Helper.ConstruirDropDownList<SelectOptionItem>(oeducation_levels, "Value", "Text", "", true, "", "");
            ViewBag.education_levels = education_levels;

            return View(pSnieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_snies })]
        public ActionResult Editar([Bind(Include = "educational_institution_id,knowledge_area_id,program_id,academic_level_id,education_level_id, snie_id,name")] SnieViewModel pSnieViewModel)
        {
            // TODO: Add insert logic here

            if (pSnieViewModel == null)
            {
                return HttpNotFound();
            }
            SnieBL oSnieBL = new SnieBL();
            pSnieViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oSnieBL.Modificar(pSnieViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_snies })]
        public JsonResult Eliminar(int id)
        {

            SnieBL oSnieBL = new SnieBL();

            oSnieBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_snies })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            SnieBL oSnieBL = new SnieBL();
            //SnieFiltersViewModel ofilters = new SnieFiltersViewModel();
            GridModel<SnieViewModel> grid = oSnieBL.ObtenerLista(ofilters);

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