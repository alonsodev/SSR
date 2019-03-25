﻿

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
    public class PeriodController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_period })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_period })]
        public ActionResult Crear()
        {


            return View();
        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_period, AuthorizeUserAttribute.Permission.edit_period })]
        public JsonResult Verificar(int id_period, string name)
        {

            PeriodBL oBL = new PeriodBL();
            var resultado = oBL.VerificarDuplicado(id_period, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_period })]
        public ActionResult Crear([Bind(Include = "period_id,name")] PeriodViewModel pPeriodViewModel)
        {
            // TODO: Add insert logic here

            if (pPeriodViewModel == null)
            {
                return HttpNotFound();
            }
            pPeriodViewModel.period_id = 0;
            pPeriodViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            
            PeriodBL oBL = new PeriodBL();
            oBL.Agregar(pPeriodViewModel);
            return RedirectToAction("Index");

        }



        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_period })]
        public ActionResult Editar(string id)
        {
            PeriodBL oBL = new PeriodBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            PeriodViewModel pPeriodViewModel = oBL.Obtener(pIntID);

            return View(pPeriodViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_period })]
        public ActionResult Editar([Bind(Include = "period_id,name")] PeriodViewModel pPeriodViewModel)
        {
            // TODO: Add insert logic here

            if (pPeriodViewModel == null)
            {
                return HttpNotFound();
            }
            PeriodBL oPeriodBL = new PeriodBL();
            pPeriodViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oPeriodBL.Modificar(pPeriodViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_period })]
        public JsonResult Eliminar(int id)
        {

            PeriodBL oPeriodBL = new PeriodBL();

            oPeriodBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_period })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            PeriodBL oPeriodBL = new PeriodBL();
            //PeriodFiltersViewModel ofilters = new PeriodFiltersViewModel();
            GridModel<PeriodViewModel> grid = oPeriodBL.ObtenerLista(ofilters);

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