using CrossCutting.Helper;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PruebaCorreo
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
                logger.Info("Inicio Proceso: PRUEBA:CORREO");
                Console.WriteLine("inicio de proceso");
                string From = ConfigurationManager.AppSettings["notificacion.prueba.data.from"];
                string To = ConfigurationManager.AppSettings["notificacion.prueba.data.to"];
                string cc = ConfigurationManager.AppSettings["notificacion.prueba.data.cc"];
                string bcc = ConfigurationManager.AppSettings["notificacion.prueba.data.bcc"];

               EmailHelper.SendMail("Prueba", From, To, cc, bcc, "Prueba", null);

               /*SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                using (MailMessage mm = new MailMessage())
                {
                    mm.From = new MailAddress(secObj.From); //--- Email address of the sender
                    mm.To.Add(To); //---- Email address of the recipient.
                    mm.Subject = "We are trying to send email using SMTP settings."; //---- Subject of email.
                    mm.Body = "Hello this is content of the email"; //---- Content of email.

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
                    smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
                    NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
                    //---Your Email and password
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587; //---- SMTP Server port number. This varies from host to host. 
                    smtp.Send(mm);
                }*/
                //
                Console.WriteLine("fin de proceso");
                //Console.ReadLine();
                logger.Info("Fin de Proceso: PRUEBA:CORREO");
            }
            catch (Exception ex)
            {
                logger.Info("Error en PRUEBA:CORREO:");
                logger.Info("Mensaje: " + ex.Message);
                logger.Info("StackTrace: " + ex.StackTrace);
                logger.Error("PRUEBA:CORREO: ", ex);
            }
        }
    }
}
