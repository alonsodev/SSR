using Business.Logic;
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
            qualify_concepts = 82,
            ranking_investigator = 83,
            concepts_received = 84,
            module_educational_institutions = 85,
            list_educational_institutions = 86,
            new_educational_institutions = 87,
            edit_educational_institutions = 88,
            delete_educational_institutions = 89,

            module_knowledge_areas = 90,
            list_knowledge_areas = 91,
            new_knowledge_areas = 92,
            edit_knowledge_areas = 93,
            delete_knowledge_areas = 94,
            module_academic_levels = 95,
            list_academic_levels = 96,
            new_academic_levels = 97,
            edit_academic_levels = 98,
            delete_academic_levels = 99,
            module_education_levels = 100,
            list_education_levels = 101,
            new_education_levels = 102,
            edit_education_levels = 103,
            delete_education_levels = 104,
            module_snies = 105,
            list_snies = 106,
            new_snies = 107,
            edit_snies = 108,
            delete_snies = 109,
            module_merit_ranges = 110,
            list_merit_ranges = 111,
            new_merit_ranges = 112,
            edit_merit_ranges = 113,
            delete_merit_ranges = 114,
            list_consultation_realized = 115,
            list_consultation_send = 116,
            new_consultation = 117,
            view_consultation = 118,
            //attend_consultation = 119,
            evaluate_concept = 120,
            general_report = 1120,
            general_report_title = 1121,
            general_report_number_approved_concepts = 1122,
            general_report_interest_area = 1123,
            general_report_commission = 1124,
            general_report_status = 1125,
            general_report_origin = 1126,
            general_report_institution = 1127,
            general_report_investigator = 1128,
            general_report_gender = 1129,
            general_report_age = 1130,
            general_report_nationality = 1131,
            general_report_program = 1132,
            general_report_interest_areas = 1133,
            general_report_address = 1134,
            general_report_institution_support = 1135,
            general_report_investigation_group = 1136,
            general_report_approved_concepts = 1137,
            general_report_reject_concepts = 1138,
            general_report_qualified_concepts = 1139,
            general_report_ranking = 1140,
            general_report_correo = 1141,
            general_report_telefono = 1142,
            general_report_movil = 1143,
            general_report_filter_interest_area = 1144,
            general_report_filter_commission = 1145,
            general_report_filter_status = 1146,
            general_report_filter_origin = 1147,

            general_report_all_institution_support = 1148,
            general_report_own_institution_support = 1149,
            users_report = 1165,
            module_period = 1150,
            list_period = 1151,
            new_period = 1152,
            edit_period = 1153,
            delete_period = 1154,
            module_consultation_types = 1155,
            list_consultation_types = 1156,
            new_consultation_types = 1157,
            edit_consultation_types = 1158,
            delete_consultation_types = 1159,
            module_reason_rejects = 1160,
            list_reason_rejects = 1161,
            new_reason_rejects = 1162,
            edit_reason_rejects = 1163,
            delete_reason_rejects = 1164,
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
        public static List<NotificationViewModel> Notificaciones()
        {
            CurrentUserViewModel user = (CurrentUserViewModel)HttpContext.Current.Session[ConfigurationManager.AppSettings["session.usuario.actual"]];
            NotificationBL oNotificationBL = new NotificationBL();

            return oNotificationBL.ObtenerPorUsuario(user.user_id);
        }
        public static int ObtenerNroNoNotificados()
        {
            CurrentUserViewModel user = (CurrentUserViewModel)HttpContext.Current.Session[ConfigurationManager.AppSettings["session.usuario.actual"]];
            NotificationBL oNotificationBL = new NotificationBL();

            return oNotificationBL.ObtenerNroNoNotificados(user.user_id);
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