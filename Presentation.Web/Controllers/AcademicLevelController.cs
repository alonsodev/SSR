

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
    public class AcademicLevelController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_academic_levels })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_academic_levels })]
        public ActionResult Crear()
        {


            return View();
        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_academic_levels, AuthorizeUserAttribute.Permission.edit_academic_levels })]
        public JsonResult Verificar(int id_academic_level, string name)
        {

            AcademicLevelBL oBL = new AcademicLevelBL();
            var resultado = oBL.VerificarDuplicado(id_academic_level, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_academic_levels })]
        public ActionResult Crear([Bind(Include = "academic_level_id,name")] AcademicLevelViewModel pAcademicLevelViewModel)
        {
            // TODO: Add insert logic here

            if (pAcademicLevelViewModel == null)
            {
                return HttpNotFound();
            }
            pAcademicLevelViewModel.academic_level_id = 0;
            pAcademicLevelViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;

            AcademicLevelBL oBL = new AcademicLevelBL();
            oBL.Agregar(pAcademicLevelViewModel);
            return RedirectToAction("Index");

        }



        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_academic_levels })]
        public ActionResult Editar(string id)
        {
            AcademicLevelBL oBL = new AcademicLevelBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            AcademicLevelViewModel pAcademicLevelViewModel = oBL.Obtener(pIntID);

            return View(pAcademicLevelViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_academic_levels })]
        public ActionResult Editar([Bind(Include = "academic_level_id,name")] AcademicLevelViewModel pAcademicLevelViewModel)
        {
            // TODO: Add insert logic here

            if (pAcademicLevelViewModel == null)
            {
                return HttpNotFound();
            }
            AcademicLevelBL oAcademicLevelBL = new AcademicLevelBL();
            pAcademicLevelViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oAcademicLevelBL.Modificar(pAcademicLevelViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_academic_levels })]
        public JsonResult Eliminar(int id)
        {

            AcademicLevelBL oAcademicLevelBL = new AcademicLevelBL();

            oAcademicLevelBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_academic_levels })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            AcademicLevelBL oAcademicLevelBL = new AcademicLevelBL();
            //AcademicLevelFiltersViewModel ofilters = new AcademicLevelFiltersViewModel();
            GridModel<AcademicLevelViewModel> grid = oAcademicLevelBL.ObtenerLista(ofilters);

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