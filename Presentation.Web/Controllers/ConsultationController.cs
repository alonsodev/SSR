

using Business.Logic;
using CrossCutting.Helper;
using Domain.Entities;
using Domain.Entities.Notifications;
using Presentation.Web.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web.Controllers
{
    public class ConsultationController : Controller
    {
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_consultation_realized })]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_consultation_send })]
        // GET: User
        public ActionResult Enviadas()
        {
            return View();
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_consultation })]
        public ActionResult Crear()
        {
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oInterestAreas = oSelectorBL.InterestAreasSelector();
            List<SelectListItem> interest_areas = Helper.ConstruirDropDownList<SelectOptionItem>(oInterestAreas, "Value", "Text", "", false, "", "");
            ViewBag.interest_areas = interest_areas;

            List<SelectOptionItem> oConsultationTypes = oSelectorBL.ConsultationTypesSelector();
            List<SelectListItem> consultation_types = Helper.ConstruirDropDownList<SelectOptionItem>(oConsultationTypes, "Value", "Text", "", true, "", "");
            ViewBag.consultation_types = consultation_types;
            return View();
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_consultation })]
        public ActionResult Crear([Bind(Include = "consultation_id,title,message,interest_areas,consultation_type_id")] ConsultationViewModel pConsultationViewModel)
        {
            // TODO: Add insert logic here

            if (pConsultationViewModel == null)
            {
                return HttpNotFound();
            }
            pConsultationViewModel.consultation_id = 0;
            pConsultationViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;

            ConsultationBL oBL = new ConsultationBL();
            oBL.Agregar(pConsultationViewModel);

            ConsultationTypeBL oConsultationTypeBL = new ConsultationTypeBL();

            var subject = oConsultationTypeBL.Obtener(pConsultationViewModel.consultation_type_id.Value).name;
            NotificacionNuevaSolicitud(pConsultationViewModel,subject);
            return RedirectToAction("Index");

        }

        private void NotificacionNuevaSolicitud(ConsultationViewModel pConsultationViewModel, string subject)
        {



            var base_url = ConfigurationManager.AppSettings["site.url"];

            UserBL userBL = new UserBL();

            List<UserViewModel> evaluadores = userBL.ObtenerPorPermiso((int)AuthorizeUserAttribute.Permission.view_consultation);// 12 = perfil evaluador

            foreach (UserViewModel evaluador in evaluadores)
            {
                SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();

                NotificationGeneralAccountViewModel oNotificationConceptViewModel = new NotificationGeneralAccountViewModel();
                oNotificationConceptViewModel.name = evaluador.contact_name;
                oNotificationConceptViewModel.url_solicitud_concepto = base_url + @"/Consultation/Ver/" + pConsultationViewModel.consultation_id;
                oNotificationConceptViewModel.to = evaluador.user_email;

                oNotificationConceptViewModel.url_home = ConfigurationManager.AppSettings["site.url"];

                oNotificationConceptViewModel.url_politicas = ConfigurationManager.AppSettings["site.url.politicas"];
                oNotificationConceptViewModel.url_contacto = ConfigurationManager.AppSettings["site.url.contacto"];
                oNotificationConceptViewModel.url_privacidad = ConfigurationManager.AppSettings["site.url.privacidad"];


                oSendEmailNotificationBL.EnviarNotificacionSolicitudConcepto(oNotificationConceptViewModel, subject);


                NotificationBL oNotificationBL = new NotificationBL();
                NotificationViewModel pNotificationViewModel = new NotificationViewModel();
                pNotificationViewModel.user_id = evaluador.id;
                pNotificationViewModel.message = "Hay una nueva solicitud de concepto con número '" + pConsultationViewModel.consultation_id + "'";
                pNotificationViewModel.url = @"/Consultation/Ver/" + pConsultationViewModel.consultation_id;

                oNotificationBL.Agregar(pNotificationViewModel);
            }




        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.view_consultation })]
        public ActionResult Ver(string id)
        {
            ConsultationBL oBL = new ConsultationBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            ConsultationViewModel pConsultationViewModel = oBL.Obtener(pIntID);

            SelectorBL oSelectorBL = new SelectorBL();
            pConsultationViewModel.interest_areasMultiSelectList = new MultiSelectList(oSelectorBL.InterestAreasSelector(), "Value", "Text");


            List<SelectOptionItem> oConsultationTypes = oSelectorBL.ConsultationTypesSelector();
            List<SelectListItem> consultation_types = Helper.ConstruirDropDownList<SelectOptionItem>(oConsultationTypes, "Value", "Text", "", false, "", "");
            ViewBag.consultation_types = consultation_types;
            return View(pConsultationViewModel);
        }

       

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_consultation_realized })]
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ConsultationBL oConsultationBL = new ConsultationBL();
            int user_id = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            GridModel<ConsultationViewModel> grid = oConsultationBL.ObtenerLista(ofilters, user_id);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = ofilters.draw,
                recordsTotal = grid.total,
                recordsFiltered = grid.recordsFiltered,
                data = grid.rows.Select(a => new ConsultationViewModel
                {
                    consultation_id = a.consultation_id,
                    title = a.title,
                    message = a.message,
                    consultation_type=a.consultation_type,
                    date_created = a.date_created,
                    interest_areas_str = string.Join(", ", a.interest_areas_list),
                }).ToList()
            });


        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_consultation_send })]
        public JsonResult ObtenerListaEnviados(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ConsultationBL oConsultationBL = new ConsultationBL();
            
            GridModel<ConsultationViewModel> grid = oConsultationBL.ObtenerListaEnviados(ofilters);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = ofilters.draw,
                recordsTotal = grid.total,
                recordsFiltered = grid.recordsFiltered,
                data = grid.rows.Select(a => new ConsultationViewModel
                {
                    consultation_id = a.consultation_id,
                    title = a.title,
                    message = a.message,
                    consultation_type = a.consultation_type,
                    date_created = a.date_created,
                    debate_speaker=a.debate_speaker,
                    interest_areas_str = string.Join(", ", a.interest_areas_list),
                }).ToList()
            });


        }


        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.view_consultation })]
        public JsonResult ObtenerInvestigadores(DataTableAjaxPostModel ofilters)//DataTableAjaxPostModel model
        {
            ConsultationBL oConsultationBL = new ConsultationBL();
            ConsultationViewModel pConsultationViewModel = oConsultationBL.Obtener(ofilters.consultation_id);


            GridModel<InvestigatorViewModel> grid = oConsultationBL.ObtenerInvestigadores(ofilters, pConsultationViewModel.interest_areas);

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