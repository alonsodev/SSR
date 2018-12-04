

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
    public class BadLanguageController : Controller
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
        public JsonResult Verificar(int id_bad_language, string name)
        {

            BadLanguageBL oBL = new BadLanguageBL();
            var resultado = oBL.VerificarDuplicado(id_bad_language, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Crear([Bind(Include = "bad_language_id,name")] BadLanguageViewModel pBadLanguageViewModel)
        {
            // TODO: Add insert logic here

            if (pBadLanguageViewModel == null)
            {
                return HttpNotFound();
            }
            pBadLanguageViewModel.bad_language_id = 0;
            pBadLanguageViewModel.user_id_created = 0;

            BadLanguageBL oBL = new BadLanguageBL();
            oBL.Agregar(pBadLanguageViewModel);
            return RedirectToAction("Index");

        }

       


        public ActionResult Editar(string id)
        {
            BadLanguageBL oBL = new BadLanguageBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            BadLanguageViewModel pBadLanguageViewModel = oBL.Obtener(pIntID);

            return View(pBadLanguageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Editar([Bind(Include = "bad_language_id,name")] BadLanguageViewModel pBadLanguageViewModel)
        {
            // TODO: Add insert logic here

            if (pBadLanguageViewModel == null)
            {
                return HttpNotFound();
            }
            BadLanguageBL oBadLanguageBL = new BadLanguageBL();
            oBadLanguageBL.Modificar(pBadLanguageViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public JsonResult Eliminar(int id)
        {

            BadLanguageBL oBadLanguageBL = new BadLanguageBL();

            oBadLanguageBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }


        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            BadLanguageBL oBadLanguageBL = new BadLanguageBL();
            //BadLanguageFiltersViewModel ofilters = new BadLanguageFiltersViewModel();
            GridModel<BadLanguageViewModel> grid = oBadLanguageBL.ObtenerLista(ofilters);

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