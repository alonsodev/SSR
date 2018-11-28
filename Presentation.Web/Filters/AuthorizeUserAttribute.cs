using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Presentation.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public enum Permission
        {
            module_access_users = 14,
            form_new_user = 15,
            form_edit_user = 16,
            delete_user = 17,
           

        }
        public Permission[] Permissions { get; set; }


        public static CurrentUserViewModel UsuarioLogeado()
        {
            return (CurrentUserViewModel)HttpContext.Current.Session[ConfigurationManager.AppSettings["session.usuario.actual"]];
        }
        public static bool UsuarioEstaLogeado()
        {
            return HttpContext.Current.Session[ConfigurationManager.AppSettings["session.usuario.actual"]] != null;
        }
        public static bool VerificarPerfil(params Permission[] permissions)
        {
            foreach (var permission in permissions)
            {
                if (HttpContext.Current.Session[ConfigurationManager.AppSettings["session.usuario.actual"]] != null)
                {
                    CurrentUserViewModel usuario = (CurrentUserViewModel)HttpContext.Current.Session[ConfigurationManager.AppSettings["session.usuario.actual"]];

                    if (usuario.permissions.Contains((int)permission) )
                        return true;
                }
            }
            return false;
        }


        private enum eMotivoNoAutorizado
        {
            SesionVacia = 1,
            PerfilNoAutorizado = 2
        }


        private eMotivoNoAutorizado motivoNoAutorizado;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if (httpContext.Session[System.Configuration.ConfigurationManager.AppSettings["session.usuario.actual"]] == null)
            {
                motivoNoAutorizado = eMotivoNoAutorizado.SesionVacia;
                return false;
            }

            return verificarPermisoPorPerfil((CurrentUserViewModel)httpContext.Session[System.Configuration.ConfigurationManager.AppSettings["session.usuario.actual"]]);


        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string strControlador = "", strAccion = "";

            if (motivoNoAutorizado == eMotivoNoAutorizado.SesionVacia)
            {
                strControlador = "Account";
                strAccion = "Login";
            }
            else
            {
                strControlador = "Error";
                strAccion = "NoAutorizado";
            }

            RouteValueDictionary tRVD = new RouteValueDictionary(
                            new
                            {
                                controller = strControlador,
                                action = strAccion
                            });
            tRVD["returnUrl"] = filterContext.RequestContext.HttpContext.Request.Url.PathAndQuery;

            filterContext.Result = new RedirectToRouteResult(tRVD);
        }




        private bool verificarPermisoPorPerfil(CurrentUserViewModel usuario)
        {
            foreach (Permission item in Permissions)
            {
                if (usuario.permissions.Contains( (int)item) )
                {
                    return true;
                }
            }

            motivoNoAutorizado = eMotivoNoAutorizado.PerfilNoAutorizado;

            return false;

        }


    }
}