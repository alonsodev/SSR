

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
    public class ConceptController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.my_concepts })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.concepts_emited})]
        public ActionResult Emitidos()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_concept })]
        public ActionResult Crear(string id)
        {
            int pIntID = 0;
            int.TryParse(id, out pIntID);

            DraftLawBL oDraftLawBL = new DraftLawBL();
            BadLanguageBL oBadLanguageBL = new BadLanguageBL();
            DraftLawViewModel oDraftLawViewModel=oDraftLawBL.Obtener(pIntID);

            ConceptViewModel oConceptViewModel = new ConceptViewModel();
            oConceptViewModel.commission_id = oDraftLawViewModel.commission_id;
            oConceptViewModel.draft_law_number = oDraftLawViewModel.draft_law_number;
            oConceptViewModel.title = oDraftLawViewModel.title;
            oConceptViewModel.commission_id = oDraftLawViewModel.commission_id;
            oConceptViewModel.draft_law_id= oDraftLawViewModel.draft_law_id;
            oConceptViewModel.investigator_id= AuthorizeUserAttribute.UsuarioLogeado().investigator_id;
            oConceptViewModel.bad_languages = String.Join(",", oBadLanguageBL.ObtenerPalabrasNoAdecuadas());


            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oCommissions = oSelectorBL.CommissionsSelector();
            List<SelectListItem> commissions = Helper.ConstruirDropDownList<SelectOptionItem>(oCommissions, "Value", "Text", "", true, "", "");
            ViewBag.commissions = commissions;

            List<SelectListItem> tags = Helper.ConstruirDropDownList<SelectOptionItem>(oSelectorBL.TagsSelector(), "Value", "Text", "", true, "", "");
            ViewBag.tags = tags;
            return View(oConceptViewModel);
        }

       
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_concept })]
        public ActionResult Crear([Bind(Include = "concept_id,summary,concept,investigator_id,draft_law_id,tags,bibliography")] ConceptViewModel pConceptViewModel)
        {
            // TODO: Add insert logic here

            if (pConceptViewModel == null)
            {
                return HttpNotFound();
            }
            pConceptViewModel.concept_id = 0;
            pConceptViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            pConceptViewModel.tag_ids = ObtenerTagIds(pConceptViewModel.tags);
            ConceptBL oBL = new ConceptBL();
            oBL.Agregar(pConceptViewModel);
            return RedirectToAction("Index");

        }

        private List<int> ObtenerTagIds(List<string> tags)
        {
            TagBL oTagBL = new TagBL();
            List<int> lista = new List<int>();
            foreach (var id in tags) {
               
                int pIntID = 0;
                if (int.TryParse(id, out pIntID))
                    lista.Add(pIntID);
                else {
                    TagViewModel oTagViewModel= oTagBL.ObtenerPorNombre(id.Trim());
                    if (oTagViewModel !=null && oTagViewModel.tag_id != 0)
                    {
                        lista.Add(oTagViewModel.tag_id);
                    }
                    else {
                        oTagViewModel = new TagViewModel();
                        oTagViewModel.tag_id = 0;
                        oTagViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
                        oTagViewModel.name = id.Trim();
                        pIntID= oTagBL.Agregar(oTagViewModel);
                        lista.Add(pIntID);
                    }
                }

            }

            return lista;
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_concept })]
        public ActionResult Editar(string id)
        {
            ConceptBL oBL = new ConceptBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            ConceptViewModel pConceptViewModel = oBL.Obtener(pIntID);
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oCommissions = oSelectorBL.CommissionsSelector();
            List<SelectListItem> commissions = Helper.ConstruirDropDownList<SelectOptionItem>(oCommissions, "Value", "Text", "", true, "", "");
            ViewBag.commissions = commissions;
            return View(pConceptViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_concept })]
        public ActionResult Editar([Bind(Include = "concept_id,summary,concept,investigator_id,draft_law_id")] ConceptViewModel pConceptViewModel)
        {
            // TODO: Add insert logic here

            if (pConceptViewModel == null)
            {
                return HttpNotFound();
            }
            ConceptBL oConceptBL = new ConceptBL();
            pConceptViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;

            oConceptBL.Modificar(pConceptViewModel);
            return RedirectToAction("Index");

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.my_concepts })]
        // GET: User
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();
            int investigator_id = AuthorizeUserAttribute.UsuarioLogeado().investigator_id;
            GridModel<ConceptViewModel> grid = oConceptBL.ObtenerLista(ofilters, investigator_id);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = ofilters.draw,
                recordsTotal = grid.total,
                recordsFiltered = grid.recordsFiltered,
                data = grid.rows
            });


        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.concepts_emited })]
        // GET: User

        public JsonResult ObtenerEmitidos(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();
           
            GridModel<ConceptViewModel> grid = oConceptBL.ObtenerEmitidos(ofilters);

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