using System;
using System.Configuration;
using System.Net;
using System.Threading;
using System.Web.Http;
using Arca.WebApi.Models;
using Arca.WebApi.Security;
using Business.Logic;
using CrossCutting.Helper;
using Domain.Entities;
using Domain.Entities.Notifications;

namespace Arca.WebApi.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : System.Web.Http.ApiController
    {
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {


            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);



            int tipo_error = 0;
            UserBL oUserBL = new UserBL();

            CurrentUserViewModel result = oUserBL.ValidarUsuario(login.Username, Helper.Encripta(login.Password), ref tipo_error);




            //List<UsuarioAccion> result = oLoginBL.ValidarUsuario(oLoginModel.usuario, oLoginModel.clave, ref tipo_error);

            if (result != null && tipo_error == 0)
            {


                oUserBL.ActualizarFechaIngreso(result.user_id);



                if (result.permissions.Count > 0)
                {
                    if (result.role_id != 9)
                    {
                        return Ok(new
                        {

                            status = 0,
                            message_error = "Usted no tiene asignado el perfil de congresista. Comuníquese con el Administrador si desea acceder."
                        });
                    }
                    else
                    {

                        var rolename = "Congresista";
                        var token = TokenGenerator.GenerateTokenJwt(login.Username, rolename);

                        return Ok(new
                        {
                            user_id = result.user_id,
                            status = 1,
                            token = token,
                        });


                    }
                }

                else
                {
                    return Ok(new
                    {

                        status = 0,
                        message_error = "Usted no tiene permisos a ninguna página del sistema.Comuníquese con el Administrador si desea acceder."
                    });

                }




            }
            else
            {
                if (tipo_error == -1)
                {
                    return Ok(new
                    {

                        status = 0,
                        message_error = "El usuario no existe."
                    });



                }
                else if (tipo_error == -2)
                {
                    return Ok(new
                    {

                        status = 0,
                        message_error = "El usuario esta en estado inactivo. Comuníquese con el Administrador para activar su cuenta."
                    });


                }
                else if (tipo_error == -3)
                {

                    return Ok(new
                    {

                        status = 0,
                        message_error = "El usuario o la contraseña ingresados son incorrectos."
                    });

                }
            }

            // Unauthorized access 
            return Unauthorized();
        }

        [HttpPost]
        [Route("recovery")]
        public IHttpActionResult Recovery(LoginRequest login)
        {
            UserBL oUserBL = new UserBL();
            UserViewModel oUserViewModel = oUserBL.ObtenerUser(login.Username);
            SendEmailNotificationBL oSendEmailNotificationBL = new SendEmailNotificationBL();

            if (oUserViewModel == null || oUserViewModel.id <= 0)
            {

                return Ok(new
                {
                    message_error = "No hay una cuenta asociada al correo electrónico ingresado.",
                    status = 0,

                });
            }
            if (oUserViewModel.user_status_id == 2)
            {
                return Ok(new
                {
                    message_error = "El usuario esta en inactivo. Por favor comuniquese con el administrador del sistema para activar su cuenta",
                    status = 0,

                });
            }
            string user_code = Guid.NewGuid().ToString();
            oUserBL.ActualizarCodigoRecuperar(oUserViewModel.id, user_code);
            NotificationGeneralAccountViewModel oNotification = new NotificationGeneralAccountViewModel();

            oNotification.url_recuperar_cuenta = ConfigurationManager.AppSettings["site.url"] + "/Account/CambiarPassword/?code=" + user_code;
            oNotification.url_home = ConfigurationManager.AppSettings["site.url"];
            oNotification.url_politicas = ConfigurationManager.AppSettings["site.url.politicas"];
            oNotification.url_contacto = ConfigurationManager.AppSettings["site.url.contacto"];
            oNotification.url_privacidad = ConfigurationManager.AppSettings["site.url.privacidad"];
            oNotification.name = oUserViewModel.contact_name;
            oNotification.to = oUserViewModel.user_email;
            oSendEmailNotificationBL.EnviarNotificacionRecuperarCuenta(oNotification);
            return Ok(new
            {
                // this is what datatables wants sending back
                status = 1,

            });



        }
    }
}
