

using Business.Logic;
using CrossCutting.Helper;
using Domain.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.IO;

using Presentation.Web.Filters;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using iTextSharp.text.html.simpleparser;
using System.Web.UI;
using System.Net;
using System.Drawing;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing.Imaging;
using Domain.Entities.Notifications;
using Humanizer;
using Domain.Entities.Movil;

namespace Presentation.Web.Controllers
{
    public class ConceptController : Controller
    {
        public FileResult OpenPDF2(string id)
        {

            id = id.Replace(".pdf", "");

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
                ConceptHtmlViewModel oConcept = new ConceptHtmlViewModel();


                GenerarPdf(oConcept);
            }
            return File(file_path, "application/pdf");
        }
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
                ConceptHtmlViewModel oConcept = new ConceptHtmlViewModel();


                GenerarPdf(oConcept);
            }
            return File(file_path, "application/pdf");
        }
        public JsonResult GenerarCertificationPDF(int concept_id)
        {



            ConceptBL oBL = new ConceptBL();


            ConceptBL oConceptBL = new ConceptBL();

            CertificationHtmlViewModel oCertification = oConceptBL.ObtenerHtmlCertification(concept_id);
            String fileName = Guid.NewGuid().ToString() + ".pdf";
            oCertification.pdf_path = Path.Combine(Server.MapPath("~/PDF/"), fileName);

            oCertification.fecha_aceptacion_txt = oCertification.fecha_aceptacion.Value.ToString("dd/MM/yyyy");
            oCertification.fecha_certificacion_txt = oCertification.fecha_certificacion.Value.ToString("dd/MM/yyyy");

            DateTime today = DateTime.Today;

            oCertification.year = today.Year;
            oCertification.day = today.Day;

            oCertification.day_name = oCertification.day.ToWords();
            oCertification.year_name = (oCertification.year - 2000).ToWords();


            string[] meses = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            oCertification.month_name = meses[today.Month - 1].ToLower();


            oCertification.base_url = ConfigurationManager.AppSettings["site.url"];

            GenerarCertificactionPdf(oCertification);

            return Json(new { path = "/PDF/" + fileName });
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.my_concepts })]
        // GET: User
        public ActionResult Index()
        {


            NotificationBL oNotificationBL = new NotificationBL();
            oNotificationBL.ActualizarNotificacionLeido("/Concept", AuthorizeUserAttribute.UsuarioLogeado().user_id);

            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oReasonRejects = oSelectorBL.ReasonRejectsSelector();
            List<SelectListItem> reason_rejects = Helper.ConstruirDropDownList<SelectOptionItem>(oReasonRejects, "Value", "Text", "", true, "", "");
            ViewBag.reason_rejects = reason_rejects;

            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();


            
            List<SelectOptionItem> oPeriods = oSelectorBL.PeriodsSelector();
            List<SelectListItem> periods = Helper.ConstruirDropDownList<SelectOptionItem>(oPeriods, "Value", "Text", "", false, "", "");

            ViewBag.periods = periods;

            RejectConceptViewModel oRejectConceptViewModel = new RejectConceptViewModel();
            oRejectConceptViewModel.period_id = oPeriod.period_id;
            return View(oRejectConceptViewModel);

            
        }

        
        // GET: User
        public ActionResult Repositorio()
        {


            
            SelectorBL oSelectorBL = new SelectorBL();
         

            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();



            List<SelectOptionItem> oPeriods = oSelectorBL.PeriodsSelector();
            List<SelectListItem> periods = Helper.ConstruirDropDownList<SelectOptionItem>(oPeriods, "Value", "Text", "", false, "", "");

            ViewBag.periods = periods;



          
            List<SelectOptionItem> oCommissions = oSelectorBL.CommissionsSelector();
            List<SelectListItem> commissions = Helper.ConstruirDropDownList<SelectOptionItem>(oCommissions, "Value", "Text", "", true, "0", "TODOS");
            ViewBag.commissions = commissions;

            List<SelectListItem> tags = Helper.ConstruirDropDownList<SelectOptionItem>(oSelectorBL.TagsSelector(), "Value", "Text", "", true, "0", "TODOS");
            ViewBag.tags = tags;


            List<SelectOptionItem> oInterestAreas = oSelectorBL.InterestAreasSelector();
            List<SelectListItem> interest_areas = Helper.ConstruirDropDownList<SelectOptionItem>(oInterestAreas, "Value", "Text", "", true, "0", "TODOS");
            ViewBag.interest_areas = interest_areas;


            List<SelectOptionItem> oOrigins = oSelectorBL.OriginSelector();
            List<SelectListItem> origins = Helper.ConstruirDropDownList<SelectOptionItem>(oOrigins, "Value", "Text", "", true, "0", "TODOS");
            ViewBag.origins = origins;


            GeneralFilterViewModel oGeneralFilterViewModel = new GeneralFilterViewModel();
            oGeneralFilterViewModel.period_id = oPeriod.period_id;

            return View(oGeneralFilterViewModel);


        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.concepts_emited })]
        public ActionResult Emitidos()
        {
            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();


            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oPeriods = oSelectorBL.PeriodsSelector();
            List<SelectListItem> periods = Helper.ConstruirDropDownList<SelectOptionItem>(oPeriods, "Value", "Text", "", false, "", "");

            ViewBag.periods = periods;

            GeneralFilterViewModel oGeneralFilterViewModel = new GeneralFilterViewModel();
            oGeneralFilterViewModel.period_id = oPeriod.period_id;
            return View(oGeneralFilterViewModel);
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.concepts_received })]
        public ActionResult Recibidos()
        {
            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();


            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oPeriods = oSelectorBL.PeriodsSelector();
            List<SelectListItem> periods = Helper.ConstruirDropDownList<SelectOptionItem>(oPeriods, "Value", "Text", "", false, "", "");

            ViewBag.periods = periods;

            GeneralFilterViewModel oGeneralFilterViewModel = new GeneralFilterViewModel();
            oGeneralFilterViewModel.period_id = oPeriod.period_id;
            return View(oGeneralFilterViewModel);
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { /*AuthorizeUserAttribute.Permission.concepts_received*/ })]
        public ActionResult Evaluados()
        {
            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();


            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oPeriods = oSelectorBL.PeriodsSelector();
            List<SelectListItem> periods = Helper.ConstruirDropDownList<SelectOptionItem>(oPeriods, "Value", "Text", "", false, "", "");

            ViewBag.periods = periods;

            GeneralFilterViewModel oGeneralFilterViewModel = new GeneralFilterViewModel();
            oGeneralFilterViewModel.period_id = oPeriod.period_id;
            return View(oGeneralFilterViewModel);
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.concepts_to_qualify })]
        public ActionResult PorCalificar()
        {
            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();


            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oPeriods = oSelectorBL.PeriodsSelector();
            List<SelectListItem> periods = Helper.ConstruirDropDownList<SelectOptionItem>(oPeriods, "Value", "Text", "", false, "", "");

            ViewBag.periods = periods;

            GeneralFilterViewModel oGeneralFilterViewModel = new GeneralFilterViewModel();
            oGeneralFilterViewModel.period_id = oPeriod.period_id;
            return View(oGeneralFilterViewModel);
        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.my_certificates })]
        public ActionResult Certificados()
        {
            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();


            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oPeriods = oSelectorBL.PeriodsSelector();
            List<SelectListItem> periods = Helper.ConstruirDropDownList<SelectOptionItem>(oPeriods, "Value", "Text", "", false, "", "");

            ViewBag.periods = periods;

            GeneralFilterViewModel oGeneralFilterViewModel = new GeneralFilterViewModel();
            oGeneralFilterViewModel.period_id = oPeriod.period_id;
            return View(oGeneralFilterViewModel);
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_concept })]
        public ActionResult Crear(string id)
        {
            int pIntID = 0;
            int.TryParse(id, out pIntID);

            DraftLawBL oDraftLawBL = new DraftLawBL();
            ConceptBL oConceptBL = new ConceptBL();
            BadLanguageBL oBadLanguageBL = new BadLanguageBL();
            DraftLawViewModel oDraftLawViewModel = oDraftLawBL.Obtener(pIntID);

            ConceptViewModel oConceptViewModel = new ConceptViewModel();
            oConceptViewModel.commission_id = oDraftLawViewModel.commission_id;
            oConceptViewModel.draft_law_number = oDraftLawViewModel.draft_law_number;
            oConceptViewModel.title = oDraftLawViewModel.title;
            oConceptViewModel.commission_id = oDraftLawViewModel.commission_id;
            oConceptViewModel.draft_law_id = oDraftLawViewModel.draft_law_id;
            oConceptViewModel.status = oDraftLawViewModel.status;
            oConceptViewModel.investigator_id = AuthorizeUserAttribute.UsuarioLogeado().investigator_id;
            oConceptViewModel.bad_languages = String.Join(",", oBadLanguageBL.ObtenerPalabrasNoAdecuadas());
            oConceptViewModel.period_closed = 1;
            if (oDraftLawViewModel.start_date <= DateTime.Today && oDraftLawViewModel.end_date >= DateTime.Today) {
                oConceptViewModel.period_closed = 0;
            }
            oConceptViewModel.notificable = 0;
            if (oDraftLawViewModel.notifiable)
                oConceptViewModel.notificable = 1;
            

            oConceptViewModel.existe_concepto = oConceptBL.ExisteConcepto(oDraftLawViewModel.draft_law_id, AuthorizeUserAttribute.UsuarioLogeado().investigator_id) ? 1 : 0;

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
        public JsonResult Crear([Bind(Include = "concept_id,summary,concept,investigator_id,draft_law_id,tags,bibliography")] ConceptViewModel pConceptViewModel)
        {
            // TODO: Add insert logic here

            if (pConceptViewModel == null)
            {
                // return HttpNotFound();

                return Json(new
                {
                    message_error = "Datos inavalidos",
                    status = "0",

                });
            }
            pConceptViewModel.concept_id = 0;
            pConceptViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            pConceptViewModel.tag_ids = ObtenerTagIds(pConceptViewModel.tags);

            String fileName = Guid.NewGuid().ToString() + ".pdf";
            pConceptViewModel.pdf_path = Path.Combine(Server.MapPath("~/PDF/"), fileName);

            ConceptBL oBL = new ConceptBL();
            oBL.Agregar(pConceptViewModel);

            ConceptHtmlViewModel oConcept = oBL.ObtenerHtmlConcept(pConceptViewModel.concept_id);

            oConcept.fecha_presentacion_txt = oConcept.fecha_presentacion.Value.ToString("dd/MM/yyyy");
            oConcept.fecha_elaboracion_txt = oConcept.fecha_elaboracion.Value.ToString("dd/MM/yyyy");
            oConcept.pdf_path = pConceptViewModel.pdf_path;
            oConcept.draf_law_fecha_presentacion_txt = oConcept.draf_law_fecha_presentacion.Value.ToString("dd/MM/yyyy");
            oConcept.tag_names = String.Join(", ", oConcept.tags);


            oConcept.base_url = ConfigurationManager.AppSettings["site.url"];


            /* HostingEnvironment.QueueBackgroundWorkItem(
                  token => GenerarPdfBackground(oConcept, token)
              );*/
            GenerarPdf(oConcept);
            NotificacionNuevoConcepto(pConceptViewModel);


            return Json(new
            {
                // this is what datatables wants sending back
                concept_id = pConceptViewModel.concept_id,
                status = "1",

            });
            //return RedirectToAction("Ver", "Concept", new { id = pConceptViewModel.concept_id });

        }
        private void NotificacionRechazadoConcepto(ConceptViewModel pConceptViewModel)
        {


            var base_url = ConfigurationManager.AppSettings["site.url"];

            UserBL userBL = new UserBL();



            UserViewModel investigador = userBL.ObtenerUser(pConceptViewModel.user_id_created.Value);



            SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();

            NotificationConceptViewModel oNotificationConceptViewModel = new NotificationConceptViewModel();

            oNotificationConceptViewModel.name = investigador.contact_name;
            oNotificationConceptViewModel.url_edit_concept = base_url + @"/Concept/Editar/" + pConceptViewModel.concept_id;
            oNotificationConceptViewModel.to = investigador.user_email;

            oNotificationConceptViewModel.url_home = ConfigurationManager.AppSettings["site.url"];

            oNotificationConceptViewModel.url_politicas = ConfigurationManager.AppSettings["site.url.politicas"];
            oNotificationConceptViewModel.url_contacto = ConfigurationManager.AppSettings["site.url.contacto"];
            oNotificationConceptViewModel.url_privacidad = ConfigurationManager.AppSettings["site.url.privacidad"];

            oNotificationConceptViewModel.draft_law_title = pConceptViewModel.title;
            oNotificationConceptViewModel.reject_reason = pConceptViewModel.reason_reject_description;
            oSendEmailNotificationBL.EnviarNotificacionConcepto(oNotificationConceptViewModel, "notificacion.concept.rechazado");


            NotificationBL oNotificationBL = new NotificationBL();
            NotificationViewModel pNotificationViewModel = new NotificationViewModel();
            pNotificationViewModel.user_id = investigador.id;
            pNotificationViewModel.message = "El concepto con número '" + pConceptViewModel.concept_id + "' ha sido rechazado.";
            pNotificationViewModel.url = @"/Concept/Editar/" + pConceptViewModel.concept_id;
            oNotificationBL.Agregar(pNotificationViewModel);


        }

        private void NotificacionAprobadoConcepto(ConceptViewModel pConceptViewModel)
        {

            var base_url = ConfigurationManager.AppSettings["site.url"];
            UserBL userBL = new UserBL();

            UserViewModel investigador = userBL.ObtenerUser(pConceptViewModel.user_id_created.Value);
            SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();

            NotificationConceptViewModel oNotificationConceptViewModel = new NotificationConceptViewModel();
            oNotificationConceptViewModel.name = investigador.contact_name;
            oNotificationConceptViewModel.url_view_concept = base_url + @"/Concept/Certificado/"+pConceptViewModel.concept_id;
            oNotificationConceptViewModel.to = investigador.user_email;
            
            oNotificationConceptViewModel.url_home = ConfigurationManager.AppSettings["site.url"];

            oNotificationConceptViewModel.url_politicas = ConfigurationManager.AppSettings["site.url.politicas"];
            oNotificationConceptViewModel.url_contacto = ConfigurationManager.AppSettings["site.url.contacto"];
            oNotificationConceptViewModel.url_privacidad = ConfigurationManager.AppSettings["site.url.privacidad"];

            oNotificationConceptViewModel.draft_law_title = pConceptViewModel.title;


            oSendEmailNotificationBL.EnviarNotificacionConcepto(oNotificationConceptViewModel, "notificacion.concept.aprobado");


            NotificationBL oNotificationBL = new NotificationBL();
            NotificationViewModel pNotificationViewModel = new NotificationViewModel();
            pNotificationViewModel.user_id = investigador.id;
            pNotificationViewModel.message = "El concepto con número '" + pConceptViewModel.concept_id + "' ha sido aprobado.";
            pNotificationViewModel.url = @"/Concept";
            oNotificationBL.Agregar(pNotificationViewModel);

        }

        private void NotificacionPorCalificarConcepto(ConceptViewModel pConceptViewModel)
        {

            var base_url = ConfigurationManager.AppSettings["site.url"];

            UserBL userBL = new UserBL();

            List<UserViewModel> ponentes = userBL.ObtenerPorConcepto(pConceptViewModel.concept_id);// 12 = Ponentes

            foreach (UserViewModel ponente in ponentes)
            {
                SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();

                NotificationConceptViewModel oNotificationConceptViewModel = new NotificationConceptViewModel();
                oNotificationConceptViewModel.name = ponente.contact_name;
                oNotificationConceptViewModel.url_calificar_concept = base_url + @"/Concept/Calificar/" + pConceptViewModel.concept_id;
                oNotificationConceptViewModel.to = ponente.user_email;

                oNotificationConceptViewModel.url_home = ConfigurationManager.AppSettings["site.url"];

                oNotificationConceptViewModel.url_politicas = ConfigurationManager.AppSettings["site.url.politicas"];
                oNotificationConceptViewModel.url_contacto = ConfigurationManager.AppSettings["site.url.contacto"];
                oNotificationConceptViewModel.url_privacidad = ConfigurationManager.AppSettings["site.url.privacidad"];

                oNotificationConceptViewModel.draft_law_title = pConceptViewModel.title;
                //oSendEmailNotificationBL.EnviarNotificacionConcepto(oNotificationConceptViewModel, "notificacion.concept.calificar");/*Removido a pedido de DIANA */

                NotificationBL oNotificationBL = new NotificationBL();
                NotificationViewModel pNotificationViewModel = new NotificationViewModel();
                pNotificationViewModel.user_id = ponente.id;
                pNotificationViewModel.message = "El concepto con número '" + pConceptViewModel.concept_id + "' esta listo para su calificación.";
                pNotificationViewModel.url = @"/Concept/Calificar/" + pConceptViewModel.concept_id;
                oNotificationBL.Agregar(pNotificationViewModel);
            }

        }

        private void NotificacionCalificadoConcepto(ConceptViewModel pConceptViewModel)
        {

            var base_url = ConfigurationManager.AppSettings["site.url"];
            UserBL userBL = new UserBL();
            UserViewModel investigador = userBL.ObtenerUser(pConceptViewModel.user_id_created.Value);
            SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();

            NotificationConceptViewModel oNotificationConceptViewModel = new NotificationConceptViewModel();
            oNotificationConceptViewModel.name = investigador.contact_name;
            oNotificationConceptViewModel.url_view_concept = base_url + @"/Concept";
            oNotificationConceptViewModel.to = investigador.user_email;


            oNotificationConceptViewModel.url_politicas = ConfigurationManager.AppSettings["site.url.politicas"];
            oNotificationConceptViewModel.url_contacto = ConfigurationManager.AppSettings["site.url.contacto"];
            oNotificationConceptViewModel.url_privacidad = ConfigurationManager.AppSettings["site.url.privacidad"];

            oNotificationConceptViewModel.draft_law_title = pConceptViewModel.title;
            oSendEmailNotificationBL.EnviarNotificacionConcepto(oNotificationConceptViewModel, "notificacion.concept.calificado");

            NotificationBL oNotificationBL = new NotificationBL();
            NotificationViewModel pNotificationViewModel = new NotificationViewModel();
            pNotificationViewModel.user_id = investigador.id;
            pNotificationViewModel.message = "El concepto con número '" + pConceptViewModel.concept_id + "' ha sido calificado.";
            pNotificationViewModel.url = @"/Concept";
            oNotificationBL.Agregar(pNotificationViewModel);

        }

        private void NotificacionNuevoConcepto(ConceptViewModel pConceptViewModel)
        {



            var base_url = ConfigurationManager.AppSettings["site.url"];

            UserBL userBL = new UserBL();

            List<UserViewModel> evaluadores = userBL.ObtenerPorPermiso((int)AuthorizeUserAttribute.Permission.evaluate_concept);// 12 = perfil evaluador

            foreach (UserViewModel evaluador in evaluadores)
            {
                SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();

                NotificationConceptViewModel oNotificationConceptViewModel = new NotificationConceptViewModel();
                oNotificationConceptViewModel.name = evaluador.contact_name;
                oNotificationConceptViewModel.url_view_concept = base_url + @"/Concept/Evaluar/" + pConceptViewModel.concept_id;
                oNotificationConceptViewModel.to = evaluador.user_email;

                oNotificationConceptViewModel.url_home = ConfigurationManager.AppSettings["site.url"];
                oNotificationConceptViewModel.url_politicas = ConfigurationManager.AppSettings["site.url.politicas"];
                oNotificationConceptViewModel.url_contacto = ConfigurationManager.AppSettings["site.url.contacto"];
                oNotificationConceptViewModel.url_privacidad = ConfigurationManager.AppSettings["site.url.privacidad"];
                oSendEmailNotificationBL.EnviarNotificacionConcepto(oNotificationConceptViewModel, "notificacion.concept.eval");


                NotificationBL oNotificationBL = new NotificationBL();
                NotificationViewModel pNotificationViewModel = new NotificationViewModel();
                pNotificationViewModel.user_id = evaluador.id;
                pNotificationViewModel.message = "El concepto con número '" + pConceptViewModel.concept_id + "' esta listo para su evaluación";
                pNotificationViewModel.url = @"/Concept/Evaluar/" + pConceptViewModel.concept_id;

                oNotificationBL.Agregar(pNotificationViewModel);
            }




        }
        private void GenerarCertificactionPdfBackground(CertificationHtmlViewModel oCertification, CancellationToken token)
        {
            try
            {
                GenerarCertificactionPdf(oCertification);
            }
            catch (Exception ex)
            {
                if (token.IsCancellationRequested)
                {
                    //
                }
            }

        }



        private void GenerarCertificactionPdf(CertificationHtmlViewModel oCertification)
        {
            HtmlToPdf converter = new HtmlToPdf();
            PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
               "A4", true);
            PdfPageOrientation pdfOrientation =
                (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                "Portrait", true);
            // set converter options


            converter.Options.PdfPageSize = pageSize;
            /*converter.Options.PdfPageOrientation = pdfOrientation;*/
            converter.Options.WebPageWidth = 806;
            //converter.Options.AutoFitWidth = 1;
            //converter.Options.WebPageHeight = 950;
            converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;



            var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);

            var url_Verificar_certificacion = oCertification.base_url + "/Concept/VerificarCertificacion/" + oCertification.hash.ToString();
            var qrCode = qrEncoder.Encode(url_Verificar_certificacion);

            var renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            var qr_path = Server.MapPath("~/Template/Concept/QRCode/" + oCertification.concept_id.ToString() + ".png");
            var qr_url = @"QRCode/" + oCertification.concept_id.ToString() + ".png";
            using (var stream = new FileStream(qr_path, FileMode.Create))
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
            oCertification.qr_url = qr_url;

            oCertification.base_url = ConfigurationManager.AppSettings["site.url"];
            string html = ConceptBL.ObtenerHtmlCertification(oCertification, ConfigurationManager.AppSettings["certification.html.xslPath"]);
            html = html.Replace("@url_site", oCertification.base_url);



            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, oCertification.base_url + @"/Template/Concept/");
            doc.Save(oCertification.pdf_path);
            doc.Close();
        }



        private void GenerarPdfBackground(ConceptHtmlViewModel oConcept, CancellationToken token)
        {
            try
            {
                GenerarPdf(oConcept);
            }
            catch (Exception ex)
            {
                if (token.IsCancellationRequested)
                {
                    //
                }
            }

        }

        private void GenerarPdf(ConceptHtmlViewModel oConcept)
        {
            HtmlToPdf converter = new HtmlToPdf();
            PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
               "A4", true);
            PdfPageOrientation pdfOrientation =
                (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                "Portrait", true);
            // set converter options


            converter.Options.PdfPageSize = pageSize;
            /*converter.Options.PdfPageOrientation = pdfOrientation;*/
            converter.Options.WebPageWidth = 850;
            //converter.Options.AutoFitWidth = 1;
            /* converter.Options.WebPageHeight = 1020;*/



            /*************header**************/
            converter.Options.DisplayHeader = true;

            converter.Header.Height = 120;

            string headerUrl = Server.MapPath("~/Template/Concept/header.html");

            PdfHtmlSection headerHtml = new PdfHtmlSection(headerUrl);
            //headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            headerHtml.WebPageWidth = 850;

            converter.Header.Add(headerHtml);
            /*************header**************/

            /*************footer**************/
            converter.Options.DisplayFooter = true;

            converter.Footer.Height = 70;

            var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            var qrCode = qrEncoder.Encode(oConcept.concept_id.ToString());

            var renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            var qr_path = Server.MapPath("~/Template/Concept/QRCode/" + oConcept.concept_id.ToString() + ".png");
            var qr_url = @"QRCode/" + oConcept.concept_id.ToString() + ".png";
            using (var stream = new FileStream(qr_path, FileMode.Create))
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);


            string footerUrl = Server.MapPath("~/Template/Concept/footer.html");
            string footer_content = System.IO.File.ReadAllText(footerUrl);

            footer_content = footer_content.Replace("@qr_url", qr_url);
            footer_content = footer_content.Replace("@concep_id", oConcept.concept_id.ToString());

            PdfHtmlSection footerHtml = new PdfHtmlSection(footerUrl);
            //footerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            footerHtml.WebPageWidth = 850;
            converter.Footer.Add(footerHtml);
            /*************footer**************/


            string html = ConceptBL.ObtenerHtmlConcept(oConcept, ConfigurationManager.AppSettings["concept.html.xslPath"]);

            html = html.Replace("@concept", oConcept.concept);

            html = html.Replace("@url_site", oConcept.base_url);


            // create a new pdf document converting an url
            //SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, String.Empty);
            /* string conceptUrl = Server.MapPath("~/Template/Concept/body.html");

             string contents = System.IO.File.ReadAllText(conceptUrl);*/
            // SelectPdf.PdfDocument doc = converter.ConvertUrl(conceptUrl);
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html);



            doc.Save(oConcept.pdf_path);
            doc.Close();
        }

        
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
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.evaluate_concept })]
        public JsonResult Aprobar(int concept_id, string speakers)
        {

            ConceptBL oConceptBL = new ConceptBL();

            ConceptStatusLogViewModel oConceptStatusLogViewModel = new ConceptStatusLogViewModel();
            oConceptStatusLogViewModel.concept_id = concept_id;
            oConceptStatusLogViewModel.concept_status_id = 2;
            oConceptStatusLogViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oConceptStatusLogViewModel.speakers = speakers.Split(',').Select(a => Convert.ToInt32(a)).ToList();
            oConceptBL.ActualizarStatus(oConceptStatusLogViewModel);


            ConceptViewModel pConceptViewModel = oConceptBL.Obtener(oConceptStatusLogViewModel.concept_id);
            NotificacionAprobadoConcepto(pConceptViewModel);
            NotificacionPorCalificarConcepto(pConceptViewModel);
            return Json(new
            {
                // this is what datatables wants sending back
                status = "1",

            });

        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.evaluate_concept })]
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

            ConceptViewModel pConceptViewModel = oConceptBL.Obtener(oConceptStatusLogViewModel.concept_id);
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oReasonRejects = oSelectorBL.ReasonRejectsSelector();

            pConceptViewModel.reason_reject_description = oReasonRejects.Where(a => a.Value == reason_reject_id.ToString()).Take(1).FirstOrDefault().Text;
            NotificacionRechazadoConcepto(pConceptViewModel);
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
            String fileName = Guid.NewGuid().ToString() + ".pdf";
            var pdf_path = Path.Combine(Server.MapPath("~/PDF/"), fileName);



            ConceptBL oConceptBL = new ConceptBL();
            var calificado = oConceptBL.VerificarCalificado(concept_id);
            ConceptStatusLogViewModel oConceptStatusLogViewModel = new ConceptStatusLogViewModel();
            oConceptStatusLogViewModel.concept_id = concept_id;

            oConceptStatusLogViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oConceptStatusLogViewModel.qualification = qualification;
            oConceptStatusLogViewModel.certification_path = pdf_path;

            oConceptBL.Calificar(oConceptStatusLogViewModel);




            if (!calificado)
            {
                ConceptViewModel pConceptViewModel = oConceptBL.Obtener(oConceptStatusLogViewModel.concept_id);
                NotificacionCalificadoConcepto(pConceptViewModel);
            }
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

            if (pConceptViewModel.user_id_created != AuthorizeUserAttribute.UsuarioLogeado().user_id)

                return Redirect("/Error/NoAutorizadoEditarConcepto");
            //

            NotificationBL oNotificationBL = new NotificationBL();
            oNotificationBL.ActualizarNotificacionLeido("/Concept/Editar/" + id, AuthorizeUserAttribute.UsuarioLogeado().user_id);


            pConceptViewModel.bad_languages = String.Join(",", oBadLanguageBL.ObtenerPalabrasNoAdecuadas());
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oCommissions = oSelectorBL.CommissionsSelector();
            List<SelectListItem> commissions = Helper.ConstruirDropDownList<SelectOptionItem>(oCommissions, "Value", "Text", "", true, "", "");
            ViewBag.commissions = commissions;

            pConceptViewModel.tagsMultiSelectList = new MultiSelectList(oSelectorBL.TagsSelector(), "Value", "Text");

            return View(pConceptViewModel);
        }

        public ActionResult VerificarCertificacion(string id)
        {
            VerifyCertificationViewModel pVerifyCertificationViewModel = new VerifyCertificationViewModel();

            ConceptBL oBL = new ConceptBL();

            Guid pIntID;
            bool valido = Guid.TryParse(id, out pIntID);
            if (valido)
            {
                pVerifyCertificationViewModel = oBL.ObtenerVerificacionCertificado(pIntID);
                pVerifyCertificationViewModel.fecha_presentacion_concepto = pVerifyCertificationViewModel.fecha_presentacion.ToString("dd/MM/yyyy");
                if (pVerifyCertificationViewModel == null || pVerifyCertificationViewModel.concept_id == 0)
                    valido = false;
                else
                {
                    if (!(pVerifyCertificationViewModel.concept_status_id == 2 || pVerifyCertificationViewModel.concept_status_id == 4 || pVerifyCertificationViewModel.concept_status_id == 5 || pVerifyCertificationViewModel.concept_status_id == 6))
                        valido = false;
                }

            }

            ViewBag.valido = valido;


            return View(pVerifyCertificationViewModel);
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
        public ActionResult Certificado(string id)
        {

            ConceptBL oBL = new ConceptBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            ConceptViewModel pConceptViewModel = oBL.Obtener(pIntID);

            ViewBag.concept_id = pConceptViewModel.concept_id;
            return View(pConceptViewModel);
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
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.view_concept })]
        public ActionResult VerCalificado(string id)
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

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.view_concept })]
        public ActionResult VerEvaluado(string id)
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

        
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.evaluate_concept })] // falta corregir
        public ActionResult Evaluar(string id)
        {

            ConceptBL oBL = new ConceptBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);

            NotificationBL oNotificationBL = new NotificationBL();
            oNotificationBL.ActualizarNotificacionLeido("/Concept/Evaluar/"+id, AuthorizeUserAttribute.UsuarioLogeado().user_id);

            ConceptViewModel pConceptViewModel = oBL.Obtener(pIntID);

            pConceptViewModel.reject = 0;
            SelectorBL oSelectorBL = new SelectorBL();


            pConceptViewModel.speakersMultiSelectList = new MultiSelectList(oSelectorBL.DebateSpeakersSelector(), "Value", "Text");



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

            NotificationBL oNotificationBL = new NotificationBL();
            oNotificationBL.ActualizarNotificacionLeido("/Concept/Calificar/" + id, AuthorizeUserAttribute.UsuarioLogeado().user_id);

            ConceptViewModel pConceptViewModel = oBL.Obtener(pIntID);
            if (!pConceptViewModel.speakers_concept.Contains(AuthorizeUserAttribute.UsuarioLogeado().user_id))
                return Redirect("/Error/NoAutorizadoCalificarConcepto");

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

            if (pConceptViewModel.concept_status_id == 2 || pConceptViewModel.concept_status_id == 4 || pConceptViewModel.concept_status_id == 5)
            {
                ConceptStatusLogViewModel oConceptStatusLogViewModel = new ConceptStatusLogViewModel();
                oConceptStatusLogViewModel.concept_id = pConceptViewModel.concept_id;
                oConceptStatusLogViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;


                oConceptBL.Leido(oConceptStatusLogViewModel);
            }
            var calificado = oConceptBL.VerificarCalificado(pConceptViewModel.concept_id, AuthorizeUserAttribute.UsuarioLogeado().user_id);


            if (calificado)
                pConceptViewModel.concept_status_id = 6;

            ViewBag.concept_id = pConceptViewModel.concept_id;

            return View(pConceptViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_concept })]
        public JsonResult Editar([Bind(Include = "concept_id,summary,concept,investigator_id,draft_law_id,tags,bibliography")] ConceptViewModel pConceptViewModel)
        {
            // TODO: Add insert logic here

            if (pConceptViewModel == null)
            {
                // return HttpNotFound();

                return Json(new
                {
                    message_error = "Datos inavalidos",
                    status = "0",

                });
            }
            ConceptBL oConceptBL = new ConceptBL();
            pConceptViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;


            pConceptViewModel.tag_ids = ObtenerTagIds(pConceptViewModel.tags);
            pConceptViewModel.concept_status_id = 1;


            String fileName = Guid.NewGuid().ToString() + ".pdf";
            pConceptViewModel.pdf_path = Path.Combine(Server.MapPath("~/PDF/"), fileName);


            oConceptBL.Modificar(pConceptViewModel);

            ConceptHtmlViewModel oConcept = oConceptBL.ObtenerHtmlConcept(pConceptViewModel.concept_id);

            oConcept.fecha_presentacion_txt = oConcept.fecha_presentacion.Value.ToString("dd/MM/yyyy");
            oConcept.fecha_elaboracion_txt = oConcept.fecha_elaboracion.Value.ToString("dd/MM/yyyy");
            oConcept.pdf_path = pConceptViewModel.pdf_path;
            oConcept.draf_law_fecha_presentacion_txt = oConcept.draf_law_fecha_presentacion.Value.ToString("dd/MM/yyyy");
            oConcept.tag_names = String.Join(", ", oConcept.tags);


            oConcept.base_url = ConfigurationManager.AppSettings["site.url"];


          
            GenerarPdf(oConcept);
            NotificacionNuevoConcepto(pConceptViewModel);


            return Json(new
            {
                // this is what datatables wants sending back
                concept_id = pConceptViewModel.concept_id,
                status = "1",

            });

        }


        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.my_concepts })]
        // GET: User
        public JsonResult ObtenerCertificados(DataTableAjaxPostModel ofilters, [Bind(Include = "period_id")]  GeneralFilterViewModel generalfiltros)//DataTableAjaxPostModel model
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();
            int investigator_id = AuthorizeUserAttribute.UsuarioLogeado().investigator_id;
            GridModel<ConceptViewModel> grid = oConceptBL.ObtenerCertificados(ofilters, investigator_id,generalfiltros);

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
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters, [Bind(Include = "period_id")]  GeneralFilterViewModel generalfiltros)//DataTableAjaxPostModel model
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();
            int investigator_id = AuthorizeUserAttribute.UsuarioLogeado().investigator_id;
            GridModel<ConceptViewModel> grid = oConceptBL.ObtenerLista(ofilters, investigator_id,generalfiltros);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = ofilters.draw,
                recordsTotal = grid.total,
                recordsFiltered = grid.recordsFiltered,
                data = grid.rows
            });


        }

        
        // GET: User
        public JsonResult ObtenerRepositorio(DataTableAjaxPostModel ofilters, [Bind(Include = "period_id,commission_id,origin_id,interest_area_id,draft_law_number,tag_id,draft_law_title")]  GeneralFilterViewModel generalfiltros)//DataTableAjaxPostModel model
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();
           
            GridModel<ConceptViewModel> grid = oConceptBL.ObtenerRepositorio(ofilters, generalfiltros);

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

        public JsonResult ObtenerEmitidos(DataTableAjaxPostModel ofilters, [Bind(Include = "period_id")]  GeneralFilterViewModel generalfiltros)//DataTableAjaxPostModel model
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();

            GridModel<ConceptViewModel> grid = oConceptBL.ObtenerEmitidos(ofilters,generalfiltros);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = ofilters.draw,
                recordsTotal = grid.total,
                recordsFiltered = grid.recordsFiltered,
                data = grid.rows
            });


        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { /*AuthorizeUserAttribute.Permission.concepts_emited */})]
        // GET: User

        public JsonResult ObtenerEvaluados(DataTableAjaxPostModel ofilters, [Bind(Include = "period_id")]  GeneralFilterViewModel generalfiltros)//DataTableAjaxPostModel model
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();

            GridModel<ConceptViewModel> grid = oConceptBL.ObtenerEvaluados(ofilters, generalfiltros);

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

        public JsonResult ObtenerPorCalificar(DataTableAjaxPostModel ofilters, [Bind(Include = "period_id")]  GeneralFilterViewModel generalfiltros)//DataTableAjaxPostModel model
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();

            GridModel<ConceptViewModel> grid = oConceptBL.ObtenerPorCalificar(ofilters, AuthorizeUserAttribute.UsuarioLogeado().user_id, generalfiltros);

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

        public JsonResult ObtenerRecibidos(DataTableAjaxPostModel ofilters, [Bind(Include = "period_id")]  GeneralFilterViewModel generalfiltros)//DataTableAjaxPostModel model
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();

            GridModel<ConceptViewModel> grid = oConceptBL.ObtenerRecibidos(ofilters, AuthorizeUserAttribute.UsuarioLogeado().user_id, generalfiltros);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = ofilters.draw,
                recordsTotal = grid.total,
                recordsFiltered = grid.recordsFiltered,
                data = grid.rows
            });


        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.qualify_concepts, AuthorizeUserAttribute.Permission.view_concept })]
        public JsonResult EnviarNotificacion([Bind(Include = "user_id,concept_id,solicitud_ampliacion,solicitud_datos_investigador,message")] ConceptSendNotification filter)
        {

            filter.user_id= AuthorizeUserAttribute.UsuarioLogeado().user_id;
            var base_url = ConfigurationManager.AppSettings["site.url"];
            UserBL userBL = new UserBL();
            UserViewModel congresista = userBL.ObtenerUser(filter.user_id);




            ConceptBL oConceptBL = new ConceptBL();
            var concept = oConceptBL.Obtener(filter.concept_id);
            InvestigatorViewModel investigador = userBL.ObtenerInvestigator(concept.investigator_id.Value);
            SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();

            if (filter.solicitud_datos_investigador == 1)
            {
                NotificationConceptMovil oNotificationViewModel = new NotificationConceptMovil();

                oNotificationViewModel.concept_id = concept.concept_id;
                oNotificationViewModel.contact_data_name = investigador.contact_name;
                oNotificationViewModel.contact_data_phone = investigador.phone;
                oNotificationViewModel.contact_data_email = investigador.user_email;


                oNotificationViewModel.name = congresista.contact_name;

              //  oNotificationViewModel.to = congresista.user_email;


                oNotificationViewModel.url_politicas = ConfigurationManager.AppSettings["site.url.politicas"];
                oNotificationViewModel.url_contacto = ConfigurationManager.AppSettings["site.url.contacto"];
                oNotificationViewModel.url_privacidad = ConfigurationManager.AppSettings["site.url.privacidad"];


                oSendEmailNotificationBL.EnviarNotificacionMovil(oNotificationViewModel, "notificacion.movil.investigator.data");
            }


            if (filter.solicitud_ampliacion == 1)
            {
                NotificationConceptMovil oNotificationViewModel = new NotificationConceptMovil();

                oNotificationViewModel.concept_id = concept.concept_id;
                oNotificationViewModel.contact_data_name = congresista.contact_name;
                oNotificationViewModel.contact_data_phone = congresista.phone;
                oNotificationViewModel.contact_data_email = congresista.user_email;
                oNotificationViewModel.message = filter.message;

                oNotificationViewModel.name = investigador.contact_name;

              //  oNotificationViewModel.to = investigador.user_email;


                oNotificationViewModel.url_politicas = ConfigurationManager.AppSettings["site.url.politicas"];
                oNotificationViewModel.url_contacto = ConfigurationManager.AppSettings["site.url.contacto"];
                oNotificationViewModel.url_privacidad = ConfigurationManager.AppSettings["site.url.privacidad"];


                oSendEmailNotificationBL.EnviarNotificacionMovil(oNotificationViewModel, "notificacion.movil.congresista.data");
            }



          
            return  Json(new
            {
                status = 1
            });
        }
    }
}
