

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
    public class KnowledgeAreaController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_knowledge_areas })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_knowledge_areas })]
        public ActionResult Crear()
        {


            return View();
        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_knowledge_areas, AuthorizeUserAttribute.Permission.edit_knowledge_areas })]
        public JsonResult Verificar(int id_knowledge_area, string name)
        {

            KnowledgeAreaBL oBL = new KnowledgeAreaBL();
            var resultado = oBL.VerificarDuplicado(id_knowledge_area, name);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_knowledge_areas })]
        public ActionResult Crear([Bind(Include = "knowledge_area_id,name")] KnowledgeAreaViewModel pKnowledgeAreaViewModel)
        {
            // TODO: Add insert logic here

            if (pKnowledgeAreaViewModel == null)
            {
                return HttpNotFound();
            }
            pKnowledgeAreaViewModel.knowledge_area_id = 0;
            pKnowledgeAreaViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            
            KnowledgeAreaBL oBL = new KnowledgeAreaBL();
            oBL.Agregar(pKnowledgeAreaViewModel);
            return RedirectToAction("Index");

        }



        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_knowledge_areas })]
        public ActionResult Editar(string id)
        {
            KnowledgeAreaBL oBL = new KnowledgeAreaBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            KnowledgeAreaViewModel pKnowledgeAreaViewModel = oBL.Obtener(pIntID);

            return View(pKnowledgeAreaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_knowledge_areas })]
        public ActionResult Editar([Bind(Include = "knowledge_area_id,name")] KnowledgeAreaViewModel pKnowledgeAreaViewModel)
        {
            // TODO: Add insert logic here

            if (pKnowledgeAreaViewModel == null)
            {
                return HttpNotFound();
            }
            KnowledgeAreaBL oKnowledgeAreaBL = new KnowledgeAreaBL();
            pKnowledgeAreaViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oKnowledgeAreaBL.Modificar(pKnowledgeAreaViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_knowledge_areas })]
        public JsonResult Eliminar(int id)
        {

            KnowledgeAreaBL oKnowledgeAreaBL = new KnowledgeAreaBL();

            oKnowledgeAreaBL.Eliminar(id);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",
               
            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_knowledge_areas })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            KnowledgeAreaBL oKnowledgeAreaBL = new KnowledgeAreaBL();
            //KnowledgeAreaFiltersViewModel ofilters = new KnowledgeAreaFiltersViewModel();
            GridModel<KnowledgeAreaViewModel> grid = oKnowledgeAreaBL.ObtenerLista(ofilters);

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