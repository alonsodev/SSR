

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
    public class InterestAreaController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_interest_areas })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_interest_areas })]
        public ActionResult Crear()
        {

          
            return View();
        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_interest_areas , AuthorizeUserAttribute.Permission.edit_interest_areas })]
        public JsonResult Verificar(int id_interest_area, string name)
        {

            InterestAreaBL oBL = new InterestAreaBL();
            var resultado = oBL.VerificarDuplicado(id_interest_area, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_interest_areas })]
        public ActionResult Crear([Bind(Include = "interest_area_id,name")] InterestAreaViewModel pInterestAreaViewModel)
        {
            // TODO: Add insert logic here

            if (pInterestAreaViewModel == null)
            {
                return HttpNotFound();
            }
            pInterestAreaViewModel.interest_area_id = 0;
            pInterestAreaViewModel.user_id_created = 0;

            InterestAreaBL oBL = new InterestAreaBL();
            oBL.Agregar(pInterestAreaViewModel);
            return RedirectToAction("Index");

        }



        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_interest_areas })]
        public ActionResult Editar(string id)
        {
            InterestAreaBL oBL = new InterestAreaBL();
            
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            InterestAreaViewModel pInterestAreaViewModel = oBL.Obtener(pIntID);

           
            return View(pInterestAreaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_interest_areas })]
        public ActionResult Editar([Bind(Include = "interest_area_id,name")] InterestAreaViewModel pInterestAreaViewModel)
        {
            // TODO: Add insert logic here

            if (pInterestAreaViewModel == null)
            {
                return HttpNotFound();
            }
            InterestAreaBL oInterestAreaBL = new InterestAreaBL();
            oInterestAreaBL.Modificar(pInterestAreaViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_interest_areas })]
        public JsonResult Eliminar(int id)
        {

            InterestAreaBL oInterestAreaBL = new InterestAreaBL();

            oInterestAreaBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_interest_areas })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            InterestAreaBL oInterestAreaBL = new InterestAreaBL();
            //InterestAreaFiltersViewModel ofilters = new InterestAreaFiltersViewModel();
            GridModel<InterestAreaViewModel> grid = oInterestAreaBL.ObtenerLista(ofilters);

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