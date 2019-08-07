

using Business.Logic;
using CrossCutting.Helper;
using Domain.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.IO;

using Presentation.Web.Filters;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using iTextSharp.text.html.simpleparser;
using System.Web.UI;
using System.Net;
using System.Drawing;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing.Imaging;
using Domain.Entities.Notifications;
using Humanizer;
using ClosedXML.Excel;

namespace Presentation.Web.Controllers
{
    public class ReportController : Controller
    {

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.general_report })]
        // GET: User
        public ActionResult Index()
        {
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oCommissions = oSelectorBL.CommissionsSelector();
            List<SelectListItem> commissions = Helper.ConstruirDropDownList<SelectOptionItem>(oCommissions, "Value", "Text", "", true, "0", "TODOS");
            ViewBag.commissions = commissions;

            List<SelectOptionItem> oInterestAreas = oSelectorBL.InterestAreasSelector();
            List<SelectListItem> interest_areas = Helper.ConstruirDropDownList<SelectOptionItem>(oInterestAreas, "Value", "Text", "", true, "0", "TODOS");
            ViewBag.commissions = commissions;
            ViewBag.interest_areas = interest_areas;

            List<SelectOptionItem> oEstados = oSelectorBL.StatusSelector();
            List<SelectListItem> estados = Helper.ConstruirDropDownList<SelectOptionItem>(oEstados, "Value", "Text", "", true, "0", "TODOS");
            ViewBag.commissions = commissions;
            ViewBag.estados = estados;


            List<SelectOptionItem> oOrigins = oSelectorBL.OriginSelector();
            List<SelectListItem> origins = Helper.ConstruirDropDownList<SelectOptionItem>(oOrigins, "Value", "Text", "", true, "0", "TODOS");
            ViewBag.commissions = commissions;
            ViewBag.origins = origins;


            List<SelectOptionItem> oPeriods = oSelectorBL.PeriodsSelector();
            List<SelectListItem> periods = Helper.ConstruirDropDownList<SelectOptionItem>(oPeriods, "Value", "Text", "", false, "", "");
            ViewBag.commissions = commissions;
            ViewBag.periods = periods;
            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();



            ReportFilterViewModel oReportFilterViewModel = new ReportFilterViewModel();
            oReportFilterViewModel.period_id = oPeriod.period_id;
            return View(oReportFilterViewModel);
        }

        public ActionResult Usuarios()
        {
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oRoles = oSelectorBL.RolesSelector();
            List<SelectListItem> roles = Helper.ConstruirDropDownList<SelectOptionItem>(oRoles, "Value", "Text", "", true, "0", "TODOS");
            ViewBag.roles = roles;

            UsersReportFilterViewModel oUsersReportFilterViewModel = new UsersReportFilterViewModel();

            return View(oUsersReportFilterViewModel);
        }



        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.general_report })]
        // GET: User
        public JsonResult ObtenerLista(DataTableAjaxPostModel ofilters, [Bind(Include = "interest_area_id,commission_id,status_id,origin_id,period_id")]  ReportFilterViewModel Reportefiltros)//DataTableAjaxPostModel model
        {
            ConceptBL oConceptBL = new ConceptBL();
            UserBL oUserBL = new UserBL();
            //ConceptFiltersViewModel ofilters = new ConceptFiltersViewModel();
            GridModel<ReportViewModel> grid = new GridModel<ReportViewModel>();
            Reportefiltros.institution_ids = new List<int>();
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_own_institution_support))
                Reportefiltros.institution_ids = oUserBL.ObtenerUser(AuthorizeUserAttribute.UsuarioLogeado().user_id).institution_ids;
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_all_institution_support))
                Reportefiltros.institution_ids = null;

            oConceptBL.ActualizarTablasReporte(Reportefiltros.period_id);
            grid = oConceptBL.ObtenerReporte(ofilters, Reportefiltros);

            grid.rows.ForEach(x => x.age = Helper.CalculateAge(x.birthdate.Value));

            return Json(new
            {
                // this is what datatables wants sending back
                draw = ofilters.draw,
                recordsTotal = grid.total,
                recordsFiltered = grid.recordsFiltered,
                data = grid.rows,
            });


        }

        [HttpGet]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.general_report })]
        //I will explain it later
        public ActionResult Download(string file)
        {
            //get the temp folder and file path in server
            string fullPath = Path.Combine(Server.MapPath("~/temp"), file);

            //return the file for download, this is an Excel 
            //so I set the file content type to "application/vnd.ms-excel"
            return File(fullPath, "application/vnd.ms-excel", file);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.users_report })]
        public JsonResult UsuariosExportExcel([Bind(Include = "role_id")]  UsersReportFilterViewModel filtros)
        {
            UserBL oUserBL = new UserBL();
            var fileName = "Reporte_Usuarios_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xlsx";
            string errorMessage = "";

            UserBL oUsertBL = new UserBL();
            Stream fileBytes;
            string fullPath = "";

            if (Int32.Parse(ConfigurationManager.AppSettings["role_id"]) == filtros.role_id)
            {
                List<InvestigatorViewModel> resultado = oUserBL.ObtenerInvestigadoresParaExcel();
                fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);
                fileBytes = CrearInvestigadoresReporteExcel(resultado, fullPath, true);
            }
            else
            {
                List<UserViewModel> resultado = oUserBL.ObtenerPorRole(filtros.role_id);
                fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);
                fileBytes = CrearUsuariosReporteExcel(resultado, fullPath, true);
            }

            return Json(new { fileName = fileName, errorMessage = errorMessage });
        }

        private Stream CrearInvestigadoresReporteExcel(List<InvestigatorViewModel> lista, string path, bool saveas)
        {
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Investigadores");

            int col = 1;

            ws.Cell(1, col).Value = "PRIMER NOMBRE"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "SEGUNDO NOMBRE"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "PRIMER APELLIDO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "SEGUNDO APELLIDO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "GÉNERO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "TELÉFONO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "FECHA DE NAC."; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "CVLAC"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "EGRESADO DE LA INSTITUCIÓN EDUCATIVA"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "PROGRAMA"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "NIVEL DE FORMACIÓN"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "INSTITUCIÓN QUE LO AVALA"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "GRUPO DE INVESTIGACIÓN"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "GRUPO DE INVESTIGACIÓN CÓDIGO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "COMISIONES"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "ÁREAS DE INTERÉS"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "NOMBRE DE USUARIO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "CORREO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "NRO. DE DOCUMENTO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "NOMBRE DE CONTACTO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "NACIONALIDAD"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "PAÍS"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "DEPARTAMENTO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "CIUDAD/MUNICIPIO"; ws.Column(col).Width = 45; col++;

            

            ws.Row(1).Style.Alignment.WrapText = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Row(1).Style.Font.Bold = true;
            ws.Range(1, 1, 1, col - 1).Style.Font.FontColor = XLColor.Black;
            ws.Range(1, 1, 1, col - 1).Style.Fill.BackgroundColor = XLColor.Silver;

            Set_AllBorder(ws.Row(1));

            int row = 2;
            foreach (InvestigatorViewModel obj in lista)
            {
                col = 1;

                ws.Cell(row, col).Value = obj.first_name.ToString(); col++;
                ws.Cell(row, col).Value = (obj.second_name == null ? "" : obj.second_name.ToString()); col++;
                ws.Cell(row, col).Value = obj.last_name.ToString(); col++;
                ws.Cell(row, col).Value = (obj.second_last_name == null ? "" : obj.second_last_name.ToString()); col++;
                ws.Cell(row, col).Value = obj.gender.ToString(); col++;
                ws.Cell(row, col).Value = obj.phone.ToString(); col++;
                ws.Cell(row, col).Value = (obj.birthdate.HasValue ? obj.birthdate.Value.ToString("yyyy-MM-dd") : ""); col++;
                ws.Cell(row, col).Value = obj.CVLAC.ToString(); col++;
                ws.Cell(row, col).Value = obj.educational_institution.ToString(); col++;
                ws.Cell(row, col).Value = obj.programa.ToString(); col++;
                ws.Cell(row, col).Value = obj.education_level.ToString(); col++;
                ws.Cell(row, col).Value = obj.institution.ToString(); col++;
                ws.Cell(row, col).Value = obj.investigation_group.ToString(); col++;
                ws.Cell(row, col).Value = obj.code_investigation_group.ToString(); col++;
                ws.Cell(row, col).Value = string.Join(",", obj.commissionsStr); col++;
                ws.Cell(row, col).Value = string.Join(",", obj.interest_areasStr); col++;
                ws.Cell(row, col).Value = obj.user_name.ToString(); col++;
                ws.Cell(row, col).Value = obj.user_email.ToString(); col++;
                ws.Cell(row, col).Value = obj.doc_nro.ToString(); col++;
                ws.Cell(row, col).Value = obj.contact_name.ToString(); col++;
                ws.Cell(row, col).Value = obj.nationality.ToString(); col++;
                ws.Cell(row, col).Value = (obj.country == null ? "" : obj.country.ToString()); col++;
                ws.Cell(row, col).Value = (obj.department == null ? "" : obj.department.ToString()); col++;
                ws.Cell(row, col).Value = (obj.municipality == null ? "" : obj.municipality.ToString()); col++;

                

                ws.Row(row).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(row).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                ws.Row(row).Style.Alignment.WrapText = true;

                Set_AllBorder(ws.Row(row));

                row++;
            }
            if (saveas)
            {
                wb.SaveAs(path);
                return null;
            }

            return GetStream(wb);
        }

        private Stream CrearUsuariosReporteExcel(List<UserViewModel> lista, string path, bool saveas)
        {
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Usuarios");

            int col = 1;

            ws.Cell(1, col).Value = "NOMBRE/INSTITUCIÓN"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "INSTITUCIONES QUE REPRESENTA"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "TELÉFONO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "CORREO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "NRO. DE DOCUMENTO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "NOMBRE DE CONTACTO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "NACIONALIDAD"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "PAÍS"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "DEPARTAMENTO"; ws.Column(col).Width = 45; col++;
            ws.Cell(1, col).Value = "CIUDAD/MUNICIPIO"; ws.Column(col).Width = 45; col++;



            ws.Row(1).Style.Alignment.WrapText = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Row(1).Style.Font.Bold = true;
            ws.Range(1, 1, 1, col - 1).Style.Font.FontColor = XLColor.Black;
            ws.Range(1, 1, 1, col - 1).Style.Fill.BackgroundColor = XLColor.Silver;

            Set_AllBorder(ws.Row(1));

            int row = 2;
            foreach (UserViewModel obj in lista)
            {
                col = 1;

                ws.Cell(row, col).Value = obj.user_name.ToString(); col++;
                ws.Cell(row, col).Value = string.Join(",", obj.institutions); col++;
                ws.Cell(row, col).Value = obj.phone.ToString(); col++;
                ws.Cell(row, col).Value = obj.user_email.ToString(); col++;
                ws.Cell(row, col).Value = obj.doc_nro.ToString(); col++;
                ws.Cell(row, col).Value = obj.contact_name.ToString(); col++;
                ws.Cell(row, col).Value = (obj.nationality == null ? "" : obj.nationality.ToString()); col++;
                ws.Cell(row, col).Value = (obj.country == null ? "" : obj.country.ToString()); col++;
                ws.Cell(row, col).Value = (obj.department == null ? "" : obj.department.ToString()); col++;
                ws.Cell(row, col).Value = (obj.municipality == null ? "" : obj.municipality.ToString()); col++;


                ws.Row(row).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(row).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                ws.Row(row).Style.Alignment.WrapText = true;

                Set_AllBorder(ws.Row(row));

                row++;
            }
            if (saveas)
            {
                wb.SaveAs(path);
                return null;
            }

            return GetStream(wb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.general_report })]
        public JsonResult ExportExcel([Bind(Include = "interest_area_id,commission_id,status_id,origin_id,period_id")]  ReportFilterViewModel filtros)
        {
            UserBL oUserBL = new UserBL();
            var fileName = "Reporte_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xlsx";
            string errorMessage = "";
            filtros.institution_ids = new List<int>();
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_own_institution_support))
                filtros.institution_ids = oUserBL.ObtenerUser(AuthorizeUserAttribute.UsuarioLogeado().user_id).institution_ids;
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_all_institution_support))
                filtros.institution_ids = null;


            ConceptBL oConceptBL = new ConceptBL();

            oConceptBL.ActualizarTablasReporte(filtros.period_id);
            List<ReportViewModel> resultado = oConceptBL.ObtenerExportarReporte(filtros);
            //   resultado.ForEach(RecalcularCampos);

            string fullPath = Path.Combine(Server.MapPath("~/temp"), fileName);
            Stream fileBytes = CrearReporteExcel(resultado, fullPath, true);





            return Json(new { fileName = fileName, errorMessage = errorMessage });



        }
        private static void RecalcularCampos(ReportViewModel x)
        {
            x.age = Helper.CalculateAge(x.birthdate.Value);
            x.interest_areas_str = string.Join(",", x.interest_areas);
        }


        private Stream CrearReporteExcel(List<ReportViewModel> lista, string path, bool saveas)
        {
            var wb = new XLWorkbook();

            //Adding a worksheet
            var ws = wb.Worksheets.Add("Servicios Realizados");

            int col = 1;
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_title))
            {
                ws.Cell(1, col).Value = "TITULO PL"; ws.Column(col).Width = 45; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_number_approved_concepts))
            {
                ws.Cell(1, col).Value = "CONCEPTOS APROBADOS"; ws.Column(col).Width = 16; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_interest_area))
            {
                ws.Cell(1, col).Value = "ÁREA DE INTÉRES"; ws.Column(col).Width = 16; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_commission))
            {
                ws.Cell(1, col).Value = "COMISIÓN"; ws.Column(col).Width = 16; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_status))
            {
                ws.Cell(1, col).Value = "ESTADO"; ws.Column(col).Width = 30; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_origin))
            {
                ws.Cell(1, col).Value = "ORIGEN"; ws.Column(col).Width = 16; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_institution))
            {
                ws.Cell(1, col).Value = "UNIVERSIDAD (ESTUDIADO)"; ws.Column(col).Width = 30; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_investigator))
            {
                ws.Cell(1, col).Value = "NOMBRE INVESTIGADOR"; ws.Column(col).Width = 20; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_gender))
            {
                ws.Cell(1, col).Value = "GÉNERO"; ws.Column(col).Width = 16; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_age))
            {
                ws.Cell(1, col).Value = "EDAD"; ws.Column(col).Width = 16; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_nationality))
            {
                ws.Cell(1, col).Value = "NACIONALIDAD"; ws.Column(col).Width = 16; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_program))
            {
                ws.Cell(1, col).Value = "NOMBRE DEL PROGRAMA AL QUE PERTENECE"; ws.Column(col).Width = 30; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_interest_areas))
            {
                ws.Cell(1, col).Value = "ÁREAS DE INTÉRES"; ws.Column(col).Width = 45; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_address))
            {
                ws.Cell(1, col).Value = "LUGAR DE RESIDENCIA"; ws.Column(col).Width = 16; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_institution_support))
            {
                ws.Cell(1, col).Value = "INSTITUCIÓN QUE LO AVALA "; ws.Column(col).Width = 35; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_investigation_group))
            {
                ws.Cell(1, col).Value = "GRUPO DE INVESTIGACIÓN AL QUE PERTENECE"; ws.Column(col).Width = 35; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_approved_concepts))
            {
                ws.Cell(1, col).Value = "CONCEPTOS APROBADOS"; ws.Column(col).Width = 16; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_reject_concepts))
            {
                ws.Cell(1, col).Value = "CONCEPTOS RECHAZADOS"; ws.Column(col).Width = 16; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_qualified_concepts))
            {
                ws.Cell(1, col).Value = "CONCEPTOS CALIFICADOS"; ws.Column(col).Width = 16; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_ranking))
            {
                ws.Cell(1, col).Value = "RANKING DE INVESTIGADORES POR POSICIÓN"; ws.Column(col).Width = 16; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_correo))
            {
                ws.Cell(1, col).Value = "CORREO"; ws.Column(col).Width = 20; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_telefono))
            {
                ws.Cell(1, col).Value = "TÉLEFONO"; ws.Column(col).Width = 16; col++;
            }
            if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_movil))
            {
                ws.Cell(1, col).Value = "MÓVIL"; ws.Column(col).Width = 16; col++;
            }

            ws.Row(1).Style.Alignment.WrapText = true;
            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Row(1).Style.Font.Bold = true;
            ws.Range(1, 1, 1, col - 1).Style.Font.FontColor = XLColor.Black;
            ws.Range(1, 1, 1, col - 1).Style.Fill.BackgroundColor = XLColor.Silver;

            Set_AllBorder(ws.Row(1));

            int row = 2;
            foreach (ReportViewModel obj in lista)
            {
                col = 1;
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_title))
                {
                    ws.Cell(row, col).Value = obj.title.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_number_approved_concepts))
                {
                    ws.Cell(row, col).Value = obj.number_approved_concepts.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_interest_area))
                {
                    ws.Cell(row, col).Value = obj.interest_area.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_commission))
                {
                    ws.Cell(row, col).Value = obj.commission.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_status))
                {
                    ws.Cell(row, col).Value = obj.status.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_origin))
                {
                    ws.Cell(row, col).Value = obj.origin.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_institution))
                {
                    ws.Cell(row, col).Value = obj.institution.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_investigator))
                {
                    ws.Cell(row, col).Value = obj.investigator.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_gender))
                {
                    ws.Cell(row, col).Value = obj.gender.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_age))
                {
                    ws.Cell(row, col).Value = Helper.CalculateAge(obj.birthdate.Value).ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_nationality))
                {
                    ws.Cell(row, col).Value = obj.nationality.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_program))
                {
                    ws.Cell(row, col).Value = obj.program.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_interest_areas))
                {
                    ws.Cell(row, col).Value = string.Join(",", obj.interest_areas); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_address))
                {
                    ws.Cell(row, col).Value = obj.address.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_institution_support))
                {
                    ws.Cell(row, col).Value = obj.institution_support.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_investigation_group))
                {
                    ws.Cell(row, col).Value = obj.investigation_group.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_approved_concepts))
                {
                    ws.Cell(row, col).Value = obj.approved_concepts.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_reject_concepts))
                {
                    ws.Cell(row, col).Value = obj.reject_concepts.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_qualified_concepts))
                {
                    ws.Cell(row, col).Value = obj.qualified_concepts.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_ranking))
                {
                    
                    ws.Cell(row, col).Value = obj.position.ToString();
                    
                    col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_correo))
                {
                    ws.Cell(row, col).Value = obj.correo.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_telefono))
                {
                    ws.Cell(row, col).Value = obj.telefono.ToString(); col++;
                }
                if (AuthorizeUserAttribute.VerificarPerfil(AuthorizeUserAttribute.Permission.general_report_movil))
                {
                    ws.Cell(row, col).Value = obj.movil.ToString(); col++;
                }

                ws.Row(row).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(row).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                ws.Row(row).Style.Alignment.WrapText = true;

                Set_AllBorder(ws.Row(row));

                row++;
            }
            if (saveas)
            {
                wb.SaveAs(path);
                return null;
            }

            return GetStream(wb);
        }
        private void Set_AllBorder(IXLRow iXLRow)
        {

            foreach (var cell in iXLRow.Cells())
            {
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            }
        }
        public Stream GetStream(XLWorkbook excelWorkbook)
        {
            Stream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);

            fs.Position = 0;
            return fs;
        }

    }
}
