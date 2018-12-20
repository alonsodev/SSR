

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
    public class MeritRangeController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_merit_ranges })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_merit_ranges })]
        public ActionResult Crear()
        {


            return View();
        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_merit_ranges, AuthorizeUserAttribute.Permission.edit_merit_ranges })]
        public JsonResult Verificar(int id_merit_range, string name)
        {

            MeritRangeBL oBL = new MeritRangeBL();
            var resultado = oBL.VerificarDuplicado(id_merit_range, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_merit_ranges })]
        public ActionResult Crear([Bind(Include = "merit_range_id,name,lower_limit,upper_limit,url_image")] MeritRangeViewModel pMeritRangeViewModel)
        {
            // TODO: Add insert logic here

            if (pMeritRangeViewModel == null)
            {
                return HttpNotFound();
            }
            pMeritRangeViewModel.merit_range_id = 0;
            pMeritRangeViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            
            MeritRangeBL oBL = new MeritRangeBL();
            oBL.Agregar(pMeritRangeViewModel);
            return RedirectToAction("Index");

        }



        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_merit_ranges })]
        public ActionResult Editar(string id)
        {
            MeritRangeBL oBL = new MeritRangeBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            MeritRangeViewModel pMeritRangeViewModel = oBL.Obtener(pIntID);

            return View(pMeritRangeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_merit_ranges })]
        public ActionResult Editar([Bind(Include = "merit_range_id,name,lower_limit,upper_limit,url_image")] MeritRangeViewModel pMeritRangeViewModel)
        {
            // TODO: Add insert logic here

            if (pMeritRangeViewModel == null)
            {
                return HttpNotFound();
            }
            MeritRangeBL oMeritRangeBL = new MeritRangeBL();
            pMeritRangeViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oMeritRangeBL.Modificar(pMeritRangeViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_merit_ranges })]
        public JsonResult Eliminar(int id)
        {

            MeritRangeBL oMeritRangeBL = new MeritRangeBL();

            oMeritRangeBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_merit_ranges })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            MeritRangeBL oMeritRangeBL = new MeritRangeBL();
            //MeritRangeFiltersViewModel ofilters = new MeritRangeFiltersViewModel();
            GridModel<MeritRangeViewModel> grid = oMeritRangeBL.ObtenerLista(ofilters);

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