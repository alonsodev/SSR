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
    public class ProgramController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_programs })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_programs })]
        public ActionResult Crear()
        {


            return View();
        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_programs, AuthorizeUserAttribute.Permission.edit_programs })]
        public JsonResult Verificar(int id_program, string name)
        {

            ProgramBL oBL = new ProgramBL();
            var resultado = oBL.VerificarDuplicado(id_program, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_programs })]
        public ActionResult Crear([Bind(Include = "program_id,name")] ProgramViewModel pProgramViewModel)
        {
            // TODO: Add insert logic here

            if (pProgramViewModel == null)
            {
                return HttpNotFound();
            }
            pProgramViewModel.program_id = 0;
            pProgramViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            
            ProgramBL oBL = new ProgramBL();
            oBL.Agregar(pProgramViewModel);
            return RedirectToAction("Index");

        }



        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_programs })]
        public ActionResult Editar(string id)
        {
            ProgramBL oBL = new ProgramBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            ProgramViewModel pProgramViewModel = oBL.Obtener(pIntID);

            return View(pProgramViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_programs })]
        public ActionResult Editar([Bind(Include = "program_id,name")] ProgramViewModel pProgramViewModel)
        {
            // TODO: Add insert logic here

            if (pProgramViewModel == null)
            {
                return HttpNotFound();
            }
            ProgramBL oProgramBL = new ProgramBL();
            pProgramViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oProgramBL.Modificar(pProgramViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_programs })]
        public JsonResult Eliminar(int id)
        {

            ProgramBL oProgramBL = new ProgramBL();

            oProgramBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_programs })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ProgramBL oProgramBL = new ProgramBL();
            //ProgramFiltersViewModel ofilters = new ProgramFiltersViewModel();
            GridModel<ProgramViewModel> grid = oProgramBL.ObtenerLista(ofilters);

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