using Business.Logic;
using Domain.Entities;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notificacion.Nuevos.Proyectos.Ley
{
    class Program
    {
        public static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public Program()
        {
            try
            {
                XmlConfigurator.Configure();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
        }
        static void Main(string[] args)
        {


            try
            {
                new Program();
                logger.Info("Inicio Proceso: Notificacion.Nuevos.Proyectos.Ley");
                DraftLawBL oDraftLawBL = new DraftLawBL();
                List<DraftLawViewModel> list = oDraftLawBL.ObtenerNotificables();

                logger.Info("Proceso Notificacion.Nuevos.Proyectos.Ley: "+ list.Count());

                if (list.Count() > 0) {
                    UserBL oUserBL = new UserBL();
                    List<InvestigatorViewModel> investigadores = oUserBL.ObtenerInvestigadores();
                    SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();

                    foreach (InvestigatorViewModel investigador in investigadores)
                    {
                        try
                        {
                            logger.Info("Inicio Investigador: " + investigador.contact_name);


                            List<DraftLawViewModel> proyectos_asociados = list.Where(a => investigador.commissions.Contains(a.commission_id.Value) && investigador.interest_areas.Contains(a.interest_area_id.Value)).ToList();
                            logger.Info("Proyectyso Nuevos: " + proyectos_asociados.Count());
                            if (proyectos_asociados != null && proyectos_asociados.Count > 0)
                            {
                                NotificationDraftLawViewModel oNotification = new NotificationDraftLawViewModel();
                                oNotification.name = investigador.contact_name;
                                oNotification.url = ConfigurationManager.AppSettings["site.url"] + "/Investigator/MisProyectosLey";
                                oNotification.to = investigador.user_email;
                                oNotification.DraftLaws = proyectos_asociados;

                                oNotification.url_home = ConfigurationManager.AppSettings["site.url"];

                                oNotification.url_politicas = ConfigurationManager.AppSettings["site.url.politicas"];
                                oNotification.url_contacto = ConfigurationManager.AppSettings["site.url.contacto"];
                                oNotification.url_privacidad = ConfigurationManager.AppSettings["site.url.privacidad"];
                                oSendEmailNotificationBL.EnviarNotificacionInvestigadorProyectosNuevos(oNotification);

                                NotificationBL oNotificationBL = new NotificationBL();
                                NotificationViewModel pNotificationViewModel = new NotificationViewModel();
                                pNotificationViewModel.user_id = investigador.user_id;
                                pNotificationViewModel.message = "Se han registrado proyecto(s) de ley de sus áreas de interés";
                                pNotificationViewModel.url = @"/Investigator/MisProyectosLey";

                                oNotificationBL.Agregar(pNotificationViewModel);

                            }

                            logger.Info("Fin Investigador: " + investigador.contact_name);
                        }
                        catch (Exception ex)
                        {
                            logger.Info("Error en Notificacion.Nuevos.Proyectos.Ley:");
                            logger.Info("Mensaje: " + ex.Message);
                            logger.Info("StackTrace: " + ex.StackTrace);
                            logger.Error("Notificacion.Nuevos.Proyectos.Ley: ", ex);
                        }
                    }

                    oDraftLawBL.ActualizarNotificacion(list);
                }
               

                logger.Info("Fin de Proceso: Notificacion.Nuevos.Proyectos.Ley");
            }
            catch (Exception ex)
            {
                logger.Info("Error en Notificacion.Nuevos.Proyectos.Ley:");
                logger.Info("Mensaje: " + ex.Message);
                logger.Info("StackTrace: " + ex.StackTrace);
                logger.Error("Notificacion.Nuevos.Proyectos.Ley: ", ex);
            }
        }
    }
}
