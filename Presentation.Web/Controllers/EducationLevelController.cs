

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
    public class EducationLevelController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_education_levels })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_education_levels })]
        public ActionResult Crear()
        {


            return View();
        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_education_levels, AuthorizeUserAttribute.Permission.edit_education_levels })]
        public JsonResult Verificar(int id_education_level, string name)
        {

            EducationLevelBL oBL = new EducationLevelBL();
            var resultado = oBL.VerificarDuplicado(id_education_level, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_education_levels })]
        public ActionResult Crear([Bind(Include = "education_level_id,name")] EducationLevelViewModel pEducationLevelViewModel)
        {
            // TODO: Add insert logic here

            if (pEducationLevelViewModel == null)
            {
                return HttpNotFound();
            }
            pEducationLevelViewModel.education_level_id = 0;
            pEducationLevelViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            
            EducationLevelBL oBL = new EducationLevelBL();
            oBL.Agregar(pEducationLevelViewModel);
            return RedirectToAction("Index");

        }



        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_education_levels })]
        public ActionResult Editar(string id)
        {
            EducationLevelBL oBL = new EducationLevelBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            EducationLevelViewModel pEducationLevelViewModel = oBL.Obtener(pIntID);

            return View(pEducationLevelViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_education_levels })]
        public ActionResult Editar([Bind(Include = "education_level_id,name")] EducationLevelViewModel pEducationLevelViewModel)
        {
            // TODO: Add insert logic here

            if (pEducationLevelViewModel == null)
            {
                return HttpNotFound();
            }
            EducationLevelBL oEducationLevelBL = new EducationLevelBL();
            pEducationLevelViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oEducationLevelBL.Modificar(pEducationLevelViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_education_levels })]
        public JsonResult Eliminar(int id)
        {

            EducationLevelBL oEducationLevelBL = new EducationLevelBL();

            oEducationLevelBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_education_levels })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            EducationLevelBL oEducationLevelBL = new EducationLevelBL();
            //EducationLevelFiltersViewModel ofilters = new EducationLevelFiltersViewModel();
            GridModel<EducationLevelViewModel> grid = oEducationLevelBL.ObtenerLista(ofilters);

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