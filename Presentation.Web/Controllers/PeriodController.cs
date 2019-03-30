

using Business.Logic;
using Domain.Entities;
using Presentation.Web.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public JsonResult Verificar([Bind(Include = "period_id,name,start_date_text, end_date_text")] PeriodViewModel pPeriodViewModel)
        {
            pPeriodViewModel.start_date = DateTime.ParseExact(pPeriodViewModel.start_date_text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            pPeriodViewModel.end_date = DateTime.ParseExact(pPeriodViewModel.end_date_text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            PeriodBL oBL = new PeriodBL();
            var valido_duplicado = oBL.VerificarDuplicado(pPeriodViewModel.period_id, pPeriodViewModel.name);
            var valido_fechas= oBL.VerificarDuplicado(pPeriodViewModel);

            return Json(new
            {
                // this is what datatables wants sending back
                valido_duplicado = valido_duplicado,
                valido_fechas = valido_fechas,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_period })]
        public ActionResult Crear([Bind(Include = "period_id,name,start_date_text, end_date_text")] PeriodViewModel pPeriodViewModel)
        {
            // TODO: Add insert logic here

            if (pPeriodViewModel == null)
            {
                return HttpNotFound();
            }
            pPeriodViewModel.period_id = 0;
            pPeriodViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            pPeriodViewModel.start_date = DateTime.ParseExact(pPeriodViewModel.start_date_text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            pPeriodViewModel.end_date = DateTime.ParseExact(pPeriodViewModel.end_date_text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


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
            if (pPeriodViewModel.start_date.HasValue)
                pPeriodViewModel.start_date_text = pPeriodViewModel.start_date.Value.ToString("dd/MM/yyyy");
            if (pPeriodViewModel.end_date.HasValue)
                pPeriodViewModel.end_date_text = pPeriodViewModel.end_date.Value.ToString("dd/MM/yyyy");
            return View(pPeriodViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_period })]
        public ActionResult Editar([Bind(Include = "period_id,name,start_date_text, end_date_text")] PeriodViewModel pPeriodViewModel)
        {
            // TODO: Add insert logic here

            if (pPeriodViewModel == null)
            {
                return HttpNotFound();
            }
            PeriodBL oPeriodBL = new PeriodBL();
            pPeriodViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;

            pPeriodViewModel.start_date = DateTime.ParseExact(pPeriodViewModel.start_date_text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            pPeriodViewModel.end_date = DateTime.ParseExact(pPeriodViewModel.end_date_text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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