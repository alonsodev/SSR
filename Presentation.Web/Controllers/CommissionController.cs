

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
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_commissions })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_commissions })]
        public ActionResult Crear()
        {


            return View();
        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_commissions, AuthorizeUserAttribute.Permission.new_commissions })]
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
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_commissions })]
        public ActionResult Crear([Bind(Include = "commission_id,name")] CommissionViewModel pCommissionViewModel)
        {
            // TODO: Add insert logic here

            if (pCommissionViewModel == null)
            {
                return HttpNotFound();
            }
            pCommissionViewModel.commission_id = 0;
            pCommissionViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            
            CommissionBL oBL = new CommissionBL();
            oBL.Agregar(pCommissionViewModel);
            return RedirectToAction("Index");

        }



        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_commissions })]
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
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_commissions })]
        public ActionResult Editar([Bind(Include = "commission_id,name")] CommissionViewModel pCommissionViewModel)
        {
            // TODO: Add insert logic here

            if (pCommissionViewModel == null)
            {
                return HttpNotFound();
            }
            CommissionBL oCommissionBL = new CommissionBL();
            pCommissionViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oCommissionBL.Modificar(pCommissionViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_commissions })]
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

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_commissions })]
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