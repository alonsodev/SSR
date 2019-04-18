using Business.Logic;
using Domain.Entities;
using Domain.Entities.Movil;
using Domain.Entities.Notifications;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Arca.WebApi.Controllers
{
    /// <summary>
    /// admin controller class for testing security token with role admin
    /// </summary>
    [Authorize(Roles = "Congresista")]
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
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
        [HttpGet]
        [Route("notifications_pending/{user_id}")]
        public IHttpActionResult NotificationsPending(int user_id)
        {

            NotificationBL oNotificationBL = new NotificationBL();
            var num_notificaciones_pendientes = oNotificationBL.ObtenerNroNoNotificados(user_id);
            var result = new
            {
                num_notificaciones_pendientes = num_notificaciones_pendientes,

            };
            return Ok(result);
        }
        [HttpGet]
        [Route("notifications/{user_id}")]
        public IHttpActionResult Notifications(int user_id)
        {

            NotificationBL oNotificationBL = new NotificationBL();
            var num_notificaciones_pendientes = oNotificationBL.ObtenerNroNoNotificados(user_id);
            var result = new
            {

                notifications = oNotificationBL.ObtenerPorUsuario(user_id)
            };
            return Ok(result);
        }

        [HttpPost]
        [Route("notification_read")]
        public IHttpActionResult NotificationUpdateRead(ConceptQualificationViewModel filter)
        {

            NotificationBL oNotificationBL = new NotificationBL();


            NotificationViewModel notification = oNotificationBL.ObtenerPorUrl(filter);

            if (notification != null && notification.notification_id >= 0)
                oNotificationBL.ActualizarLeido(notification.notification_id);
            var result = new
            {

                status = 1
            };
            return Ok(result);
        }
        [HttpPost]
        [Route("calificar")]
        public IHttpActionResult Calificar(ConceptQualificationViewModel filter)
        {

            // falta validar q ya lo haya calificado
            ConceptBL oConceptBL = new ConceptBL();
            var calificado = oConceptBL.VerificarCalificado(filter.concept_id);
            ConceptStatusLogViewModel oConceptStatusLogViewModel = new ConceptStatusLogViewModel();
            oConceptStatusLogViewModel.concept_id = filter.concept_id;

            oConceptStatusLogViewModel.user_id_created = filter.user_id;
            oConceptStatusLogViewModel.qualification = filter.qualification;
            //oConceptStatusLogViewModel.certification_path = pdf_path;

            oConceptBL.Calificar(oConceptStatusLogViewModel);

            if (!calificado)
            {
                ConceptViewModel pConceptViewModel = oConceptBL.Obtener(oConceptStatusLogViewModel.concept_id);
                NotificacionCalificadoConcepto(pConceptViewModel);
            }

            var result = new
            {
                data = 1
            };
            return Ok(result);
        }

        [HttpPost]
        [Route("enviar_notificacion")]
        public IHttpActionResult EnviarNotificacion(ConceptSendNotification filter)
        {
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

                oNotificationViewModel.to = congresista.user_email;


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


                oNotificationViewModel.name = investigador.contact_name;

                oNotificationViewModel.to = investigador.user_email;


                oNotificationViewModel.url_politicas = ConfigurationManager.AppSettings["site.url.politicas"];
                oNotificationViewModel.url_contacto = ConfigurationManager.AppSettings["site.url.contacto"];
                oNotificationViewModel.url_privacidad = ConfigurationManager.AppSettings["site.url.privacidad"];


                oSendEmailNotificationBL.EnviarNotificacionMovil(oNotificationViewModel, "notificacion.movil.congresista.data");
            }



            var result = new
            {
                data = 1
            };
            return Ok(result);
        }


        [HttpPost]
        [Route("concept")]
        public IHttpActionResult Concept(ConceptFilterLiteViewModel filter)
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();

            ConceptDetailLiteViewModel obj = oConceptBL.ObtenerLite(filter);
            obj.tags = String.Join(", ", obj.tags_list);
            var path = ConfigurationManager.AppSettings["pdf.path"];
            obj.pdf_file = obj.pdf_file.Replace(path, "");

            var result = new
            {
                // this is what datatables wants sending back

                data = obj
            };
            return Ok(result);
        }
        [HttpGet]
        [Route("consultation_selectors")]
        public IHttpActionResult ConsultationSelectors()
        {
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oInterestAreas = oSelectorBL.InterestAreasSelector();
          

            List<SelectOptionItem> oConsultationTypes = oSelectorBL.ConsultationTypesSelector();


            var result = new
            {
                interest_areas = oInterestAreas,
                consultation_types = oConsultationTypes,
            };
            return Ok(result);
        }
        [HttpPost]
        [Route("consultation_crear")]
        public IHttpActionResult ConsultationCrear(ConsultationViewModel pConsultationViewModel)
        {
            // TODO: Add insert logic here

            pConsultationViewModel.consultation_id = 0;


            ConsultationBL oBL = new ConsultationBL();
            oBL.Agregar(pConsultationViewModel);

            ConsultationTypeBL oConsultationTypeBL = new ConsultationTypeBL();

            var subject = oConsultationTypeBL.Obtener(pConsultationViewModel.consultation_type_id.Value).name;
            NotificacionNuevaSolicitud(pConsultationViewModel, subject);



            var result = new
            {

                status = 1
            };
            return Ok(result);


        }

        private void NotificacionNuevaSolicitud(ConsultationViewModel pConsultationViewModel, string subject)
        {



            var base_url = ConfigurationManager.AppSettings["site.url"];

            UserBL userBL = new UserBL();

            List<UserViewModel> evaluadores = userBL.ObtenerPorPermiso(118);// 12 = perfil evaluador

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

        [HttpPost]
        [Route("concepts")]
        public IHttpActionResult Concepts(ConceptsFilterLiteViewModel filter)
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();
            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();

            filter.period_id = oPeriod.period_id;
            List<ConceptLiteViewModel> lista = oConceptBL.ObtenerPorCalificarMovil(filter);

            var path = ConfigurationManager.AppSettings["pdf.path"];
            lista.ForEach(a => a.pdf_file = a.pdf_file.Replace(path, ""));


            var result = new
            {
                // this is what datatables wants sending back

                data = lista
            };
            return Ok(result);
        }

        [HttpPost]
        [Route("concepts_all")]
        public IHttpActionResult ConceptsAll(ConceptsFilterLiteViewModel filter)
        {
            ConceptBL oConceptBL = new ConceptBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();
            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();

            filter.period_id = oPeriod.period_id;
            List<ConceptLiteViewModel> lista = oConceptBL.ObtenerMovil(filter);

            var path = ConfigurationManager.AppSettings["pdf.path"];
            lista.ForEach(a => a.pdf_file = a.pdf_file.Replace(path, ""));


            var result = new
            {
                // this is what datatables wants sending back

                data = lista
            };
            return Ok(result);
        }

        [HttpPost]
        [Route("draftlaw")]
        public IHttpActionResult DraftLaw(DraftLawFilterLiteViewModel filter)
        {
            DraftLawBL oBL = new DraftLawBL();
            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();

            filter.period_id = oPeriod.period_id;
            List<DraftLawLiteViewModel> lista = oBL.ObtenerProyectosLeyConConceptosPorCalificar(filter);



            var result = new
            {
                // this is what datatables wants sending back

                data = lista
            };
            return Ok(result);
        }
        [HttpPost]
        [Route("draftlaw_search")]
        public IHttpActionResult DraftLawSearch(DraftLawSearchFilterLiteViewModel filter)
        {
            DraftLawBL oBL = new DraftLawBL();
            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();

            filter.period_id = oPeriod.period_id;
            List<DraftLawLiteViewModel> lista = oBL.ObtenerProyectosLeyMovil(filter);



            var result = new
            {
                // this is what datatables wants sending back

                data = lista
            };
            return Ok(result);
        }
       


        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var adminList = new string[] { "admin-1", "admin-2", "admin-3" };
            return Ok(adminList);
        }
    }
}
