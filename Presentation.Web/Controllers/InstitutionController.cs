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
    public class InstitutionController : Controller
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
        public JsonResult Verificar(int id_institution, string name)
        {

            InstitutionBL oBL = new InstitutionBL();
            var resultado = oBL.VerificarDuplicado(id_institution, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Crear([Bind(Include = "institution_id,name")] InstitutionViewModel pInstitutionViewModel)
        {
            // TODO: Add insert logic here

            if (pInstitutionViewModel == null)
            {
                return HttpNotFound();
            }
            pInstitutionViewModel.institution_id = 0;
            pInstitutionViewModel.user_id_created = 0;

            InstitutionBL oBL = new InstitutionBL();
            oBL.Agregar(pInstitutionViewModel);
            return RedirectToAction("Index");

        }

       


        public ActionResult Editar(string id)
        {
            InstitutionBL oBL = new InstitutionBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            InstitutionViewModel pInstitutionViewModel = oBL.Obtener(pIntID);

            return View(pInstitutionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Editar([Bind(Include = "institution_id,name")] InstitutionViewModel pInstitutionViewModel)
        {
            // TODO: Add insert logic here

            if (pInstitutionViewModel == null)
            {
                return HttpNotFound();
            }
            InstitutionBL oInstitutionBL = new InstitutionBL();
            oInstitutionBL.Modificar(pInstitutionViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public JsonResult Eliminar(int id)
        {

            InstitutionBL oInstitutionBL = new InstitutionBL();

            oInstitutionBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }


        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            InstitutionBL oInstitutionBL = new InstitutionBL();
            //InstitutionFiltersViewModel ofilters = new InstitutionFiltersViewModel();
            GridModel<InstitutionViewModel> grid = oInstitutionBL.ObtenerLista(ofilters);

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