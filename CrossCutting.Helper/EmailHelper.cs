

using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Helper
{
    public class EmailHelper
    {
        public static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region SendMail Method


        /// <summary>
        /// SendMail Method using SMTP configurate by config.
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="strFromAddress"></param>
        /// <param name="strToAddress"></param>
        /// <param name="strCcAddress"></param>
        /// <param name="strBccAddress"></param>
        /// <param name="strSubject"></param>
        /// <param name="arrAttachmentPath"></param>
        public static void SendMail(string strMessage, string strFromAddress, string strToAddress, string strCcAddress, string strBccAddress, string strSubject, string[] arrAttachmentPath)
        {

            MailMessage oMailMessage = new MailMessage();
            oMailMessage.From = new MailAddress(strFromAddress);
            oMailMessage.To.Add(strToAddress.Replace(";", ","));
            oMailMessage.Body = strMessage;
            oMailMessage.IsBodyHtml = true;
            oMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            oMailMessage.Subject = strSubject;

            if (!String.IsNullOrEmpty(strCcAddress))
                oMailMessage.CC.Add(strCcAddress.Replace(";", ","));
            if (!String.IsNullOrEmpty(strBccAddress))
                oMailMessage.Bcc.Add(strBccAddress.Replace(";", ","));



            if (arrAttachmentPath != null)
            {
                foreach (string strAttachmentPath in arrAttachmentPath)
                {
                    if (!(String.IsNullOrEmpty(strAttachmentPath)
                        || Equals(strAttachmentPath, Convert.DBNull)))
                    {
                        Attachment mlAttachment = new Attachment(strAttachmentPath);
                        mlAttachment.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                        oMailMessage.Attachments.Add(mlAttachment);
                    }
                }
            }
            SmtpClient smtp = new SmtpClient();
            logger.Info(String.Format("Enviar Notificacion: FROM:{0}, TO:{1}, CC:{2}, BCC:{3}, SUBJECT:{4}, ATTACHMENTS:{5}", oMailMessage.From, oMailMessage.To, oMailMessage.CC, oMailMessage.Bcc, oMailMessage.Subject, oMailMessage.Attachments!=null ? oMailMessage.Attachments.Count().ToString(): "0"));
            smtp.Send(oMailMessage);
            logger.Info("Enviar Notificacion: exitoso");
            oMailMessage.Dispose();
            GC.Collect();

        }

        public static void SendMailDisplayName(string strMessage, string strFromAddress, string strFromDisplayName, string strToAddress, string strCcAddress, string strBccAddress, string strSubject, string[] arrAttachmentPath)
        {

            MailMessage oMailMessage = new MailMessage();
            oMailMessage.From = new MailAddress(strFromAddress, strFromDisplayName);
            oMailMessage.To.Add(strToAddress.Replace(";", ","));
            oMailMessage.Body = strMessage;
            oMailMessage.IsBodyHtml = true;
            oMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            oMailMessage.Subject = strSubject;

            if (!String.IsNullOrEmpty(strCcAddress))
                oMailMessage.CC.Add(strCcAddress.Replace(";", ","));
            if (!String.IsNullOrEmpty(strBccAddress))
                oMailMessage.Bcc.Add(strBccAddress.Replace(";", ","));



            if (arrAttachmentPath != null)
            {
                foreach (string strAttachmentPath in arrAttachmentPath)
                {
                    if (!(String.IsNullOrEmpty(strAttachmentPath)
                        || Equals(strAttachmentPath, Convert.DBNull)))
                    {
                        Attachment mlAttachment = new Attachment(strAttachmentPath);
                        mlAttachment.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                        oMailMessage.Attachments.Add(mlAttachment);
                    }
                }
            }
            SmtpClient smtp = new SmtpClient();
            logger.Info(String.Format("Enviar Notificacion: FROM:{0}, TO:{1}, CC:{2}, BCC:{3}, SUBJECT:{4}, ATTACHMENTS:{5}", oMailMessage.From, oMailMessage.To, oMailMessage.CC, oMailMessage.Bcc, oMailMessage.Subject, oMailMessage.Attachments != null ? oMailMessage.Attachments.Count().ToString() : "0"));
            smtp.Send(oMailMessage);
            logger.Info("Enviar Notificacion: exitoso");
            oMailMessage.Dispose();
            GC.Collect();

        }


        #endregion
    }
}
