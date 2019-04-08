using CrossCutting.Helper;
using Domain.Entities;
using Domain.Entities.Movil;
using Domain.Entities.Notifications;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestXSLTMail;

namespace Business.Logic
{
    public class SendEmailNotificationBL
    {
        public void EnviarNotificacionMovil(NotificationConceptMovil  oNotification, string key) //"notificacion.recuperar.cuenta"
        {
            NotificacionConfig oNotificacionConfig = new NotificacionConfig(key);

            string mensaje = ObtenerMensajeMovil(oNotification, oNotificacionConfig.xslPath);

            List<string> images = new List<string>();
            images.Add(ConfigurationManager.AppSettings["site.path"] + @"\Assets\img\logoarca.jpg");
            EmailHelper.SendMail(mensaje, oNotificacionConfig.From, oNotification.to, oNotificacionConfig.Cc, oNotificacionConfig.Bcc, oNotificacionConfig.Asunto, null, images.ToArray());
        }

        private static string ObtenerMensajeMovil(NotificationConceptMovil oNotification, string xslPath)
        {
            StringBuilder msgBody = new StringBuilder();
            if (File.Exists(xslPath))
            {
                MailGenerator mailGenerator = new MailGenerator(xslPath);
                //string serialize = ConvertObjectToXMLString(oAsignacionLancha);

                string message = mailGenerator.Generate(oNotification, typeof(NotificationConceptMovil));
                msgBody.Append(message);
                return msgBody.ToString();
            }
            return string.Empty;
        }

        public void EnviarNotificacionConcepto(NotificationConceptViewModel oNotification, string key) //"notificacion.recuperar.cuenta"
        {
            NotificacionConfig oNotificacionConfig = new NotificacionConfig(key);

            string mensaje = ObtenerMensajeConcepto(oNotification, oNotificacionConfig.xslPath);
            
            List<string> images = new List<string>();
            images.Add(ConfigurationManager.AppSettings["site.path"] + @"\Assets\img\logoarca.jpg");
            EmailHelper.SendMail(mensaje, oNotificacionConfig.From, oNotification.to, oNotificacionConfig.Cc, oNotificacionConfig.Bcc, oNotificacionConfig.Asunto, null, images.ToArray());
        }
        private static string ObtenerMensajeConcepto(NotificationConceptViewModel oNotification, string xslPath)
        {
            StringBuilder msgBody = new StringBuilder();
            if (File.Exists(xslPath))
            {
                MailGenerator mailGenerator = new MailGenerator(xslPath);
                //string serialize = ConvertObjectToXMLString(oAsignacionLancha);

                string message = mailGenerator.Generate(oNotification, typeof(NotificationConceptViewModel));
                msgBody.Append(message);
                return msgBody.ToString();
            }
            return string.Empty;
        }

        public void EnviarNotificacionSolicitudConcepto(NotificationGeneralAccountViewModel oNotification,string subject)
        {
            NotificacionConfig oNotificacionConfig = new NotificacionConfig("notificacion.solicitud.concepto");
            oNotificacionConfig.Asunto = subject;
            string mensaje = ObtenerMensajeGeneralAccount(oNotification, oNotificacionConfig.xslPath);
            List<string> images = new List<string>();
            images.Add(ConfigurationManager.AppSettings["site.path"] + @"\Assets\img\logoarca.jpg");
            EmailHelper.SendMail(mensaje, oNotificacionConfig.From, oNotification.to, oNotificacionConfig.Cc, oNotificacionConfig.Bcc, oNotificacionConfig.Asunto, null, images.ToArray());
        }

        public void EnviarNotificacionInvestigadorProyectosNuevos(NotificationDraftLawViewModel oNotification)
        {
            NotificacionConfig oNotificacionConfig = new NotificacionConfig("notificacion.proyectos.nuevos");

            string mensaje = ObtenerMensajeProyectosNuevos(oNotification, oNotificacionConfig.xslPath);
            List<string> images = new List<string>();
            images.Add(ConfigurationManager.AppSettings["site.path"] + @"\Assets\img\logoarca.jpg");
            EmailHelper.SendMail(mensaje, oNotificacionConfig.From, oNotification.to, oNotificacionConfig.Cc, oNotificacionConfig.Bcc, oNotificacionConfig.Asunto, null, images.ToArray());
        }

        public void EnviarNotificacionRecuperarCuenta(NotificationGeneralAccountViewModel oNotification)
        {
            NotificacionConfig oNotificacionConfig = new NotificacionConfig("notificacion.recuperar.cuenta");

            string mensaje = ObtenerMensajeGeneralAccount(oNotification, oNotificacionConfig.xslPath);
            List<string> images = new List<string>();
            images.Add(ConfigurationManager.AppSettings["site.path"] + @"\Assets\img\logoarca.jpg");
            EmailHelper.SendMail(mensaje, oNotificacionConfig.From, oNotification.to, oNotificacionConfig.Cc, oNotificacionConfig.Bcc, oNotificacionConfig.Asunto, null, images.ToArray());
        }


        public void EnviarNotificacionNuevaCuenta(NotificationGeneralAccountViewModel oNotification)
        {
            NotificacionConfig oNotificacionConfig = new NotificacionConfig("notificacion.nueva.cuenta");

            string mensaje = ObtenerMensajeGeneralAccount(oNotification, oNotificacionConfig.xslPath);
            List<string> images = new List<string>();
            images.Add(ConfigurationManager.AppSettings["site.path"] + @"\Assets\img\logoarca.jpg");
            EmailHelper.SendMail(mensaje, oNotificacionConfig.From, oNotification.to, oNotificacionConfig.Cc, oNotificacionConfig.Bcc, oNotificacionConfig.Asunto, null, images.ToArray());

        }

        public void EnviarNotificacionActivarCuenta(NotificationGeneralAccountViewModel oNotification)
        {
            NotificacionConfig oNotificacionConfig = new NotificacionConfig("notificacion.activar.cuenta");

            string mensaje = ObtenerMensajeGeneralAccount(oNotification, oNotificacionConfig.xslPath);
            List<string> images = new List<string>();
            images.Add(ConfigurationManager.AppSettings["site.path"] + @"\Assets\img\logoarca.jpg");
            EmailHelper.SendMail(mensaje, oNotificacionConfig.From, oNotification.to, oNotificacionConfig.Cc, oNotificacionConfig.Bcc, oNotificacionConfig.Asunto, null, images.ToArray());
        }
        private static string ObtenerMensajeGeneralAccount(NotificationGeneralAccountViewModel oNotification, string xslPath)
        {
            StringBuilder msgBody = new StringBuilder();
            if (File.Exists(xslPath))
            {
                MailGenerator mailGenerator = new MailGenerator(xslPath);
                //string serialize = ConvertObjectToXMLString(oAsignacionLancha);

                string message = mailGenerator.Generate(oNotification, typeof(NotificationGeneralAccountViewModel));
                msgBody.Append(message);
                return msgBody.ToString();
            }
            return string.Empty;
        }
        private static string ObtenerMensajeProyectosNuevos(NotificationDraftLawViewModel oNotification, string xslPath)
        {
            StringBuilder msgBody = new StringBuilder();
            if (File.Exists(xslPath))
            {
                MailGenerator mailGenerator = new MailGenerator(xslPath);
                //string serialize = ConvertObjectToXMLString(oAsignacionLancha);

                string message = mailGenerator.Generate(oNotification, typeof(NotificationDraftLawViewModel));
                msgBody.Append(message);
                return msgBody.ToString();
            }
            return string.Empty;
        }
    }

}
