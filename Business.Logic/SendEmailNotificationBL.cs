﻿using CrossCutting.Helper;
using Domain.Entities.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestXSLTMail;

namespace Business.Logic
{
    public class SendEmailNotificationBL
    {
        public void EnviarNotificacionConcepto(NotificationConceptViewModel oNotification , string key) //"notificacion.recuperar.cuenta"
        {
            NotificacionConfig oNotificacionConfig = new NotificacionConfig(key);

            string mensaje = ObtenerMensajeConcepto(oNotification, oNotificacionConfig.xslPath);

            EmailHelper.SendMail(mensaje, oNotificacionConfig.From, oNotification.to, oNotificacionConfig.Cc, oNotificacionConfig.Bcc, oNotificacionConfig.Asunto, null);
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

        public void EnviarNotificacionSolicitudConcepto(NotificationGeneralAccountViewModel oNotification)
        {
            NotificacionConfig oNotificacionConfig = new NotificacionConfig("notificacion.solicitud.concepto");

            string mensaje = ObtenerMensajeGeneralAccount(oNotification, oNotificacionConfig.xslPath);

            EmailHelper.SendMail(mensaje, oNotificacionConfig.From, oNotification.to, oNotificacionConfig.Cc, oNotificacionConfig.Bcc, oNotificacionConfig.Asunto, null);
        }


        public void EnviarNotificacionRecuperarCuenta(NotificationGeneralAccountViewModel oNotification)
        {
            NotificacionConfig oNotificacionConfig = new NotificacionConfig("notificacion.recuperar.cuenta");
          
            string mensaje = ObtenerMensajeGeneralAccount(oNotification, oNotificacionConfig.xslPath);

            EmailHelper.SendMail(mensaje, oNotificacionConfig.From, oNotification.to, oNotificacionConfig.Cc, oNotificacionConfig.Bcc, oNotificacionConfig.Asunto, null);
        }


        public void EnviarNotificacionNuevaCuenta(NotificationGeneralAccountViewModel oNotification)
        {
            NotificacionConfig oNotificacionConfig = new NotificacionConfig("notificacion.nueva.cuenta");

            string mensaje = ObtenerMensajeGeneralAccount(oNotification, oNotificacionConfig.xslPath);

            EmailHelper.SendMail(mensaje, oNotificacionConfig.From, oNotification.to, oNotificacionConfig.Cc, oNotificacionConfig.Bcc, oNotificacionConfig.Asunto, null);
        }

        public void EnviarNotificacionActivarCuenta(NotificationGeneralAccountViewModel oNotification)
        {
            NotificacionConfig oNotificacionConfig = new NotificacionConfig("notificacion.activar.cuenta");
           
            string mensaje = ObtenerMensajeGeneralAccount(oNotification, oNotificacionConfig.xslPath);

            EmailHelper.SendMail(mensaje, oNotificacionConfig.From, oNotification.to, oNotificacionConfig.Cc, oNotificacionConfig.Bcc, oNotificacionConfig.Asunto, null);
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

    }

}
