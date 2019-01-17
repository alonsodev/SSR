

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
    public class EducationalInstitutionController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_educational_institutions })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_educational_institutions })]
        public ActionResult Crear()
        {


            return View();
        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_educational_institutions, AuthorizeUserAttribute.Permission.edit_educational_institutions })]
        public JsonResult Verificar(int id_educational_institution, string name)
        {

            EducationalInstitutionBL oBL = new EducationalInstitutionBL();
            var resultado = oBL.VerificarDuplicado(id_educational_institution, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_educational_institutions })]
        public ActionResult Crear([Bind(Include = "educational_institution_id,name")] EducationalInstitutionViewModel pEducationalInstitutionViewModel)
        {
            // TODO: Add insert logic here

            if (pEducationalInstitutionViewModel == null)
            {
                return HttpNotFound();
            }
            pEducationalInstitutionViewModel.educational_institution_id = 0;
            pEducationalInstitutionViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            
            EducationalInstitutionBL oBL = new EducationalInstitutionBL();
            oBL.Agregar(pEducationalInstitutionViewModel);
            return RedirectToAction("Index");

        }



        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_educational_institutions })]
        public ActionResult Editar(string id)
        {
            EducationalInstitutionBL oBL = new EducationalInstitutionBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            EducationalInstitutionViewModel pEducationalInstitutionViewModel = oBL.Obtener(pIntID);

            return View(pEducationalInstitutionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_educational_institutions })]
        public ActionResult Editar([Bind(Include = "educational_institution_id,name")] EducationalInstitutionViewModel pEducationalInstitutionViewModel)
        {
            // TODO: Add insert logic here

            if (pEducationalInstitutionViewModel == null)
            {
                return HttpNotFound();
            }
            EducationalInstitutionBL oEducationalInstitutionBL = new EducationalInstitutionBL();
            pEducationalInstitutionViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oEducationalInstitutionBL.Modificar(pEducationalInstitutionViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_educational_institutions })]
        public JsonResult Eliminar(int id)
        {

            EducationalInstitutionBL oEducationalInstitutionBL = new EducationalInstitutionBL();

            oEducationalInstitutionBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_educational_institutions })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            EducationalInstitutionBL oEducationalInstitutionBL = new EducationalInstitutionBL();
            //EducationalInstitutionFiltersViewModel ofilters = new EducationalInstitutionFiltersViewModel();
            GridModel<EducationalInstitutionViewModel> grid = oEducationalInstitutionBL.ObtenerLista(ofilters);

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