

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
    public class ConsultationTypeController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_consultation_types })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_consultation_types })]
        public ActionResult Crear()
        {


            return View();
        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_consultation_types, AuthorizeUserAttribute.Permission.edit_consultation_types })]
        public JsonResult Verificar(int id_consultation_type, string name)
        {

            ConsultationTypeBL oBL = new ConsultationTypeBL();
            var resultado = oBL.VerificarDuplicado(id_consultation_type, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_consultation_types })]
        public ActionResult Crear([Bind(Include = "consultation_type_id,name")] ConsultationTypeViewModel pConsultationTypeViewModel)
        {
            // TODO: Add insert logic here

            if (pConsultationTypeViewModel == null)
            {
                return HttpNotFound();
            }
            pConsultationTypeViewModel.consultation_type_id = 0;
            pConsultationTypeViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            
            ConsultationTypeBL oBL = new ConsultationTypeBL();
            oBL.Agregar(pConsultationTypeViewModel);
            return RedirectToAction("Index");

        }



        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_consultation_types })]
        public ActionResult Editar(string id)
        {
            ConsultationTypeBL oBL = new ConsultationTypeBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            ConsultationTypeViewModel pConsultationTypeViewModel = oBL.Obtener(pIntID);

            return View(pConsultationTypeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_consultation_types })]
        public ActionResult Editar([Bind(Include = "consultation_type_id,name")] ConsultationTypeViewModel pConsultationTypeViewModel)
        {
            // TODO: Add insert logic here

            if (pConsultationTypeViewModel == null)
            {
                return HttpNotFound();
            }
            ConsultationTypeBL oConsultationTypeBL = new ConsultationTypeBL();
            pConsultationTypeViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oConsultationTypeBL.Modificar(pConsultationTypeViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_consultation_types })]
        public JsonResult Eliminar(int id)
        {

            ConsultationTypeBL oConsultationTypeBL = new ConsultationTypeBL();

            oConsultationTypeBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_consultation_types })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ConsultationTypeBL oConsultationTypeBL = new ConsultationTypeBL();
            //ConsultationTypeFiltersViewModel ofilters = new ConsultationTypeFiltersViewModel();
            GridModel<ConsultationTypeViewModel> grid = oConsultationTypeBL.ObtenerLista(ofilters);

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