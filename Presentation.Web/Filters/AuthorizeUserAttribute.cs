﻿using Domain.Entities;
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
            list_users = 21,
            new_user = 15,
            edit_user = 16,
            delete_user = 17,
            change_pass = 19,
            change_own_pass = 25,
            module_access_roles = 22,
            list_roles = 27,
            new_role = 26,
            edit_role = 28,
            delete_role = 29,
            role_permissions = 30,
            module_access_permissions = 23,
            list_permissions = 32,
            new_permissions = 31,
            edit_permission = 33,
            delete_permission = 34,
            module_draft_law = 36,
            list_draft_law = 38,
            new_draft_law = 39,
            edit_draft_law = 40,
            delete_draft_law = 41,
            module_institution = 42,
            list_institution = 43,
            new_institution = 44,
            edit_institution = 45,
            delete_institution = 46,
            module_investigation_groups = 47,
            list_investigation_groups = 48,
            new_investigation_groups = 49,
            edit_investigation_groups = 50,
            delete_investigation_groups = 51,
            module_interest_areas = 52,
            list_interest_areas = 53,
            new_interest_areas = 54,
            edit_interest_areas = 55,
            delete_interest_areas = 56,
            module_programs = 57,
            list_programs = 58,
            new_programs = 59,
            edit_programs = 60,
            delete_programs = 61,
            module_commissions = 62,
            list_commissions = 63,
            new_commissions = 64,
            edit_commissions = 65,
            delete_commissions = 66,
            edit_config = 35,
            my_draft_laws = 67,
            my_concepts = 68,
            my_certificates = 69,
            my_backgrounds = 70,
            concepts_emited = 71,
            new_concept = 72,
            edit_concept = 73,
            module_bad_languages = 74,
            list_bad_languages = 75,
            new_bad_languages = 76,
            edit_bad_languages = 76,
            delete_bad_languages = 78,
            view_concept = 79,
            view_draft_law = 80,
            concepts_to_qualify = 81,
            qualify_concepts = 82

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

                    if (usuario.permissions.Contains((int)permission))
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
            if (Permissions.Count() == 0)
                return true;
            foreach (Permission item in Permissions)
            {
                if (usuario.permissions.Contains((int)item))
                {
                    return true;
                }
            }


            motivoNoAutorizado = eMotivoNoAutorizado.PerfilNoAutorizado;

            return false;

        }


    }
}