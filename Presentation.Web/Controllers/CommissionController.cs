

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
    public class CommissionController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Crear()
        {


            return View();
        }

        [HttpPost]
        public JsonResult Verificar(int id_commission, string name)
        {

            CommissionBL oBL = new CommissionBL();
            var resultado = oBL.VerificarDuplicado(id_commission, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Crear([Bind(Include = "commission_id,name")] CommissionViewModel pCommissionViewModel)
        {
            // TODO: Add insert logic here

            if (pCommissionViewModel == null)
            {
                return HttpNotFound();
            }
            pCommissionViewModel.commission_id = 0;
            pCommissionViewModel.user_id_created = 0;

            CommissionBL oBL = new CommissionBL();
            oBL.Agregar(pCommissionViewModel);
            return RedirectToAction("Index");

        }

       


        public ActionResult Editar(string id)
        {
            CommissionBL oBL = new CommissionBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            CommissionViewModel pCommissionViewModel = oBL.Obtener(pIntID);

            return View(pCommissionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Editar([Bind(Include = "commission_id,name")] CommissionViewModel pCommissionViewModel)
        {
            // TODO: Add insert logic here

            if (pCommissionViewModel == null)
            {
                return HttpNotFound();
            }
            CommissionBL oCommissionBL = new CommissionBL();
            oCommissionBL.Modificar(pCommissionViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public JsonResult Eliminar(int id)
        {

            CommissionBL oCommissionBL = new CommissionBL();

            oCommissionBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }


        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            CommissionBL oCommissionBL = new CommissionBL();
            //CommissionFiltersViewModel ofilters = new CommissionFiltersViewModel();
            GridModel<CommissionViewModel> grid = oCommissionBL.ObtenerLista(ofilters);

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