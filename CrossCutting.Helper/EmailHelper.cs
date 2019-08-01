

using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Mime;
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
        public static void SendMail(string strMessage, string strFromAddress, string strToAddress, string strCcAddress, string strBccAddress, string strSubject, string[] arrAttachmentPath, string[] arrImage)
        {
            try
            {
                SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                MailMessage oMailMessage = new MailMessage();
                oMailMessage.From = new MailAddress(strFromAddress);
                oMailMessage.To.Add(strToAddress.Replace(";", ","));
                //oMailMessage.Body = strMessage;
                //oMailMessage.IsBodyHtml = true;
                oMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                oMailMessage.Subject = strSubject;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(strMessage, null, "text/html");
                if (arrImage != null)
                {
                    int i = 1;
                    foreach (string strImagePath in arrImage)
                    {

                        LinkedResource LinkedImage = new LinkedResource(strImagePath);
                        LinkedImage.ContentId = "image_" + i.ToString();
                        i++;

                        //Added the patch for Thunderbird as suggested by Jorge
                        LinkedImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
                        htmlView.LinkedResources.Add(LinkedImage);
                    }
                }

                oMailMessage.AlternateViews.Add(htmlView);
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
                //smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
                //smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
                NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
                //---Your Email and password
                //smtp.UseDefaultCredentials = secObj.Network.DefaultCredentials;
                smtp.Credentials = NetworkCred;
                //smtp.Port = secObj.Network.Port; //---- SMTP Server port number. This varies from host to host. 
                logger.Info(String.Format("Enviar Notificacion: FROM:{0}, TO:{1}, CC:{2}, BCC:{3}, SUBJECT:{4}, ATTACHMENTS:{5}", oMailMessage.From, oMailMessage.To, oMailMessage.CC, oMailMessage.Bcc, oMailMessage.Subject, oMailMessage.Attachments != null ? oMailMessage.Attachments.Count().ToString() : "0"));
                smtp.Send(oMailMessage);
                logger.Info("Enviar Notificacion: exitoso");
                oMailMessage.Dispose();
                GC.Collect();


            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }


        }

        public static void SendMail(string strMessage, string strFromAddress, string strToAddress, string strCcAddress, string strBccAddress, string strSubject, string[] arrAttachmentPath)
        {
            try
            {
                SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

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

                //smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
                //smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
                NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
                //---Your Email and password
                //smtp.UseDefaultCredentials = secObj.Network.DefaultCredentials;
                smtp.Credentials = NetworkCred;
                //smtp.Port = secObj.Network.Port; //---- SMTP Server port number. This varies from host to host. 

                logger.Info(String.Format("Enviar Notificacion: FROM:{0}, TO:{1}, CC:{2}, BCC:{3}, SUBJECT:{4}, ATTACHMENTS:{5}", oMailMessage.From, oMailMessage.To, oMailMessage.CC, oMailMessage.Bcc, oMailMessage.Subject, oMailMessage.Attachments != null ? oMailMessage.Attachments.Count().ToString() : "0"));
                smtp.Send(oMailMessage);
                logger.Info("Enviar Notificacion: exitoso");
                oMailMessage.Dispose();
                GC.Collect();

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public static void SendMailDisplayName(string strMessage, string strFromAddress, string strFromDisplayName, string strToAddress, string strCcAddress, string strBccAddress, string strSubject, string[] arrAttachmentPath)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }


        #endregion
    }
}
