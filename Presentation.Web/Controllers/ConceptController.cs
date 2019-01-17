

using Business.Logic;
using CrossCutting.Helper;
using Domain.Entities;
using Presentation.Web.Filters;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Presentation.Web.Controllers
{
    public class ConceptController : Controller
    {

        public FileResult OpenPDF(string id)
        {



            ConceptBL oBL = new ConceptBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            string file_path = oBL.ObtenerPdfpath(pIntID);
            if (!System.IO.File.Exists(file_path))
            {
                for (int i = 0; i < 30; i++)
                {
                    Thread.Sleep(1000);
                    if (System.IO.File.Exists(file_path))
                    {
                        break;
                    }
                }
            }
            if (!System.IO.File.Exists(file_path))
            {
                ConceptViewModel pConceptViewModel = oBL.Obtener(pIntID);
                GenerarPdf(pConceptViewModel);
            }
            return File(file_path, "application/pdf");
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.my_concepts })]
        // GET: User
        public ActionResult Index()
        {
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oReasonRejects = oSelectorBL.ReasonRejectsSelector();
            List<SelectListItem> reason_rejects = Helper.ConstruirDropDownList<SelectOptionItem>(oReasonRejects, "Value", "Text", "", true, "", "");
            ViewBag.reason_rejects = reason_rejects;
            return View();
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.concepts_emited })]
        public ActionResult Emitidos()
        {
            return View();
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.concepts_received })]
        public ActionResult Recibidos()
        {
            return View();
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.concepts_to_qualify })]
        public ActionResult PorCalificar()
        {
            return View();
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.my_certificates })]
        public ActionResult Certificados()
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
            DraftLawViewModel oDraftLawViewModel = oDraftLawBL.Obtener(pIntID);

            ConceptViewModel oConceptViewModel = new ConceptViewModel();
            oConceptViewModel.commission_id = oDraftLawViewModel.commission_id;
            oConceptViewModel.draft_law_number = oDraftLawViewModel.draft_law_number;
            oConceptViewModel.title = oDraftLawViewModel.title;
            oConceptViewModel.commission_id = oDraftLawViewModel.commission_id;
            oConceptViewModel.draft_law_id = oDraftLawViewModel.draft_law_id;
            oConceptViewModel.investigator_id = AuthorizeUserAttribute.UsuarioLogeado().investigator_id;
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

            String fileName = Guid.NewGuid().ToString() + ".pdf";
            pConceptViewModel.pdf_path = Path.Combine(Server.MapPath("~/PDF/"), fileName);

            ConceptBL oBL = new ConceptBL();
            oBL.Agregar(pConceptViewModel);

            HostingEnvironment.QueueBackgroundWorkItem(
                 token => GenerarPdfBackground(pConceptViewModel, token)
             );
            return RedirectToAction("Index");

        }

        private void GenerarPdfBackground(ConceptViewModel pConceptViewModel, CancellationToken token)
        {
            try
            {
                GenerarPdf(pConceptViewModel);
            }
            catch (Exception ex)
            {
                if (token.IsCancellationRequested)
                {
                    //
                }
            }

        }

        private void GenerarPdf(ConceptViewModel pConceptViewModel)
        {
            HtmlToPdf converter = new HtmlToPdf();
            PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
               "A4", true);
            PdfPageOrientation pdfOrientation =
                (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                "Portrait", true);
            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;
            ConceptHtmlViewModel oConcept = new ConceptHtmlViewModel();
            string html = ConceptBL.ObtenerHtmlConcept(oConcept, ConfigurationManager.AppSettings["concept.html.xslPath"]);
            html = html.Replace("@imagen", pConceptViewModel.concept);
            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString(html, String.Empty);

            doc.Save(pConceptViewModel.pdf_path);
            doc.Close();
        }



        /*
         private async Task<int> GenerarPdf(ConceptViewModel pConceptViewModel)
         {
             HtmlToPdf converter = new HtmlToPdf();
             PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
                "A4", true);
             PdfPageOrientation pdfOrientation =
                 (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                 "Portrait", true);
             // set converter options
             converter.Options.PdfPageSize = pageSize;
             converter.Options.PdfPageOrientation = pdfOrientation;
             converter.Options.WebPageWidth = 1024;
             converter.Options.WebPageHeight = 0;
             ConceptHtmlViewModel oConcept = new ConceptHtmlViewModel();
             string html = ConceptBL.ObtenerHtmlConcept(oConcept, ConfigurationManager.AppSettings["concept.html.xslPath"]);
             html = html.Replace("@imagen", pConceptViewModel.concept);
             // create a new pdf document converting an url
             PdfDocument doc = converter.ConvertHtmlString(html, String.Empty);

             doc.Save(pConceptViewModel.pdf_path);
             doc.Close();
             return 1;
         }*/
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { })]
        public JsonResult ObtenerRechazo(int concept_id)
        {

            ConceptBL oConceptBL = new ConceptBL();


            RejectConceptViewModel oRejectConceptViewModel = oConceptBL.ObtenerRechazo(concept_id);

            return Json(new
            {
                // this is what datatables wants sending back
                reject = oRejectConceptViewModel,
                status = "1",

            });

        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { })]
        public JsonResult Aprobar(int concept_id)
        {

            ConceptBL oConceptBL = new ConceptBL();

            ConceptStatusLogViewModel oConceptStatusLogViewModel = new ConceptStatusLogViewModel();
            oConceptStatusLogViewModel.concept_id = concept_id;
            oConceptStatusLogViewModel.concept_status_id = 2;
            oConceptStatusLogViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oConceptBL.ActualizarStatus(oConceptStatusLogViewModel);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",

            });

        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { })]
        public JsonResult Rechazar(int concept_id, int reason_reject_id, string reason_reject_description)
        {

            ConceptBL oConceptBL = new ConceptBL();
            ConceptStatusLogViewModel oConceptStatusLogViewModel = new ConceptStatusLogViewModel();
            oConceptStatusLogViewModel.concept_id = concept_id;
            oConceptStatusLogViewModel.concept_status_id = 3;
            oConceptStatusLogViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oConceptStatusLogViewModel.description = reason_reject_description;
            oConceptStatusLogViewModel.reason_reject_id = reason_reject_id;
            oConceptBL.ActualizarStatus(oConceptStatusLogViewModel);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",

            });

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.concepts_to_qualify })]
        public JsonResult Calificar(int concept_id, int qualification)
        {

            ConceptBL oConceptBL = new ConceptBL();
            ConceptStatusLogViewModel oConceptStatusLogViewModel = new ConceptStatusLogViewModel();
            oConceptStatusLogViewModel.concept_id = concept_id;

            oConceptStatusLogViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oConceptStatusLogViewModel.qualification = qualification;

            oConceptBL.Calificar(oConceptStatusLogViewModel);

            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",

            });

        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_concept })]
        public ActionResult Editar(string id)
        {
            BadLanguageBL oBadLanguageBL = new BadLanguageBL();
            ConceptBL oBL = new ConceptBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            ConceptViewModel pConceptViewModel = oBL.Obtener(pIntID);
            pConceptViewModel.bad_languages = String.Join(",", oBadLanguageBL.ObtenerPalabrasNoAdecuadas());
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oCommissions = oSelectorBL.CommissionsSelector();
            List<SelectListItem> commissions = Helper.ConstruirDropDownList<SelectOptionItem>(oCommissions, "Value", "Text", "", true, "", "");
            ViewBag.commissions = commissions;

            pConceptViewModel.tagsMultiSelectList = new MultiSelectList(oSelectorBL.TagsSelector(), "Value", "Text");

            return View(pConceptViewModel);
        }

        private List<int> ObtenerTagIds(List<string> tags)
        {
            TagBL oTagBL = new TagBL();
            List<int> lista = new List<int>();
            foreach (var id in tags)
            {

                int pIntID = 0;
                if (int.TryParse(id, out pIntID))
                    lista.Add(pIntID);
                else
                {
                    TagViewModel oTagViewModel = oTagBL.ObtenerPorNombre(id.Trim());
                    if (oTagViewModel != null && oTagViewModel.tag_id != 0)
                    {
                        lista.Add(oTagViewModel.tag_id);
                    }
                    else
                    {
                        oTagViewModel = new TagViewModel();
                        oTagViewModel.tag_id = 0;
                        oTagViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
                        oTagViewModel.name = id.Trim();
                        pIntID = oTagBL.Agregar(oTagViewModel);
                        lista.Add(pIntID);
                    }
                }

            }

            return lista;
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.view_concept })]
        public ActionResult Ver(string id)
        {
            BadLanguageBL oBadLanguageBL = new BadLanguageBL();
            ConceptBL oBL = new ConceptBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            ConceptViewModel pConceptViewModel = oBL.Obtener(pIntID);
            pConceptViewModel.bad_languages = String.Join(",", oBadLanguageBL.ObtenerPalabrasNoAdecuadas());
            pConceptViewModel.reject = 0;
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oCommissions = oSelectorBL.CommissionsSelector();
            List<SelectListItem> commissions = Helper.ConstruirDropDownList<SelectOptionItem>(oCommissions, "Value", "Text", "", true, "", "");
            ViewBag.commissions = commissions;

            pConceptViewModel.tagsMultiSelectList = new MultiSelectList(oSelectorBL.TagsSelector(), "Value", "Text");



            List<SelectOptionItem> oReasonRejects = oSelectorBL.ReasonRejectsSelector();
            List<SelectListItem> reason_rejects = Helper.ConstruirDropDownList<SelectOptionItem>(oReasonRejects, "Value", "Text", "", true, "", "");
            ViewBag.reason_rejects = reason_rejects;
            ViewBag.concept_id = pConceptViewModel.concept_id;
            return View(pConceptViewModel);
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.view_concept })] // falta corregir
        public ActionResult Evaluar(string id)
        {
            BadLanguageBL oBadLanguageBL = new BadLanguageBL();
            ConceptBL oBL = new ConceptBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            ConceptViewModel pConceptViewModel = oBL.Obtener(pIntID);
            pConceptViewModel.bad_languages = String.Join(",", oBadLanguageBL.ObtenerPalabrasNoAdecuadas());
            pConceptViewModel.reject = 0;
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oCommissions = oSelectorBL.CommissionsSelector();
            List<SelectListItem> commissions = Helper.ConstruirDropDownList<SelectOptionItem>(oCommissions, "Value", "Text", "", true, "", "");
            ViewBag.commissions = commissions;

            pConceptViewModel.tagsMultiSelectList = new MultiSelectList(oSelectorBL.TagsSelector(), "Value", "Text");



            List<SelectOptionItem> oReasonRejects = oSelectorBL.ReasonRejectsSelector();
            List<SelectListItem> reason_rejects = Helper.ConstruirDropDownList<SelectOptionItem>(oReasonRejects, "Value", "Text", "", true, "", "");
            ViewBag.reason_rejects = reason_rejects;
            ViewBag.concept_id = pConceptViewModel.concept_id;
            return View(pConceptViewModel);
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.qualify_concepts })]
        public ActionResult Calificar(string id)
        {
            BadLanguageBL oBadLanguageBL = new BadLanguageBL();
            ConceptBL oBL = new ConceptBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            ConceptViewModel pConceptViewModel = oBL.Obtener(pIntID);
            pConceptViewModel.bad_languages = String.Join(",", oBadLanguageBL.ObtenerPalabrasNoAdecuadas());
            pConceptViewModel.reject = 0;
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oCommissions = oSelectorBL.CommissionsSelector();
            List<SelectListItem> commissions = Helper.ConstruirDropDownList<SelectOptionItem>(oCommissions, "Value", "Text", "", true, "", "");
            ViewBag.commissions = commissions;

            pConceptViewModel.tagsMultiSelectList = new MultiSelectList(oSelectorBL.TagsSelector(), "Value", "Text");



            List<SelectOptionItem> oReasonRejects = oSelectorBL.ReasonRejectsSelector();
            List<SelectListItem> reason_rejects = Helper.ConstruirDropDownList<SelectOptionItem>(oReasonRejects, "Value", "Text", "", true, "", "");
            ViewBag.reason_rejects = reason_rejects;

            List<SelectOptionItem> oQualifications = new List<SelectOptionItem>();
            oQualifications.Add(new SelectOptionItem("1", "1"));
            oQualifications.Add(new SelectOptionItem("2", "2"));
            oQualifications.Add(new SelectOptionItem("3", "3"));
            oQualifications.Add(new SelectOptionItem("4", "4"));
            oQualifications.Add(new SelectOptionItem("5", "5"));

            List<SelectListItem> qualifications = Helper.ConstruirDropDownList<SelectOptionItem>(oQualifications, "Value", "Text", "", true, "", "");
            ViewBag.qualifications = qualifications;


            ConceptBL oConceptBL = new ConceptBL();
            ConceptStatusLogViewModel oConceptStatusLogViewModel = new ConceptStatusLogViewModel();
            oConceptStatusLogViewModel.concept_id = pConceptViewModel.concept_id;
            oConceptStatusLogViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;

            ViewBag.concept_id = pConceptViewModel.concept_id;
            oConceptBL.Leido(oConceptStatusLogViewModel);
            return View(pConceptViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_concept })]
        public ActionResult Editar([Bind(Include = "concept_id,summary,concept,investigator_id,draft_law_id,tags,bibliography")] ConceptViewModel pConceptViewModel)
        {
            // TODO: Add insert logic here

            if (pConceptViewModel == null)
            {
                return HttpNotFound();
            }
            ConceptBL oConceptBL = new ConceptBL();
            pConceptViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;

            pConceptViewModel.tag_ids = ObtenerTagIds(pConceptViewModel.tags);
            pConceptViewModel.concept_status_id = 1;
            oConceptBL.Modificar(pConceptViewModel);
            return RedirectToAction("Index");

        }


        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.my_concepts })]
        // GET: User
        public JsonResult ObtenerCertificados(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();
            int investigator_id = AuthorizeUserAttribute.UsuarioLogeado().investigator_id;
            GridModel<ConceptViewModel> grid = oConceptBL.ObtenerCertificados(ofilters, investigator_id);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = ofilters.draw,
                recordsTotal = grid.total,
                recordsFiltered = grid.recordsFiltered,
                data = grid.rows
            });


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
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.concepts_to_qualify })]
        // GET: User

        public JsonResult ObtenerPorCalificar(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();

            GridModel<ConceptViewModel> grid = oConceptBL.ObtenerPorCalificar(ofilters, AuthorizeUserAttribute.UsuarioLogeado().user_id);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = ofilters.draw,
                recordsTotal = grid.total,
                recordsFiltered = grid.recordsFiltered,
                data = grid.rows
            });


        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.concepts_received })]
        // GET: User

        public JsonResult ObtenerRecibidos(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();

            GridModel<ConceptViewModel> grid = oConceptBL.ObtenerRecibidos(ofilters, AuthorizeUserAttribute.UsuarioLogeado().user_id);

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
