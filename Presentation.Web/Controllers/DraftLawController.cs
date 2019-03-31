using Business.Logic;
using CrossCutting.Helper;
using Domain.Entities;
using Domain.Entities.Notifications;
using Presentation.Web.Filters;
using Presentation.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web.Controllers
{
    public class DraftLawController : Controller
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: User
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_draft_law })]

        public ActionResult Index()
        {
            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();


            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oPeriods = oSelectorBL.PeriodsSelector();
            List<SelectListItem> periods = Helper.ConstruirDropDownList<SelectOptionItem>(oPeriods, "Value", "Text", "", false, "", "");
            
            ViewBag.periods = periods;

            GeneralFilterViewModel oGeneralFilterViewModel = new GeneralFilterViewModel();
            oGeneralFilterViewModel.period_id = oPeriod.period_id;
            return View(oGeneralFilterViewModel);
        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_draft_law })]
        public ActionResult Crear()
        {
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oCommissions = oSelectorBL.CommissionsSelector();
            List<SelectListItem> commissions = Helper.ConstruirDropDownList<SelectOptionItem>(oCommissions, "Value", "Text", "", true, "", "");
            ViewBag.commissions = commissions;
            List<SelectOptionItem> oInterestAreas = oSelectorBL.InterestAreasSelector();
            List<SelectListItem> interest_areas = Helper.ConstruirDropDownList<SelectOptionItem>(oInterestAreas, "Value", "Text", "", true, "", "");
            ViewBag.interest_areas = interest_areas;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_draft_law })]
        public JsonResult Crear([Bind(Include = "draft_law_id,draft_law_number,title,author,origin,date_presentation,date_presentation_text,commission_id,debate_speaker,debate_speaker2,status,status_comment,interest_area_id,initiative,summary,link")] DraftLawViewModel pDraftLawViewModel)
        {
            // TODO: Add insert logic here


            if (pDraftLawViewModel == null)
            {
                // return HttpNotFound();

                return Json(new
                {
                    message_error = "Datos inavalidos",
                    status = "0",

                });
            }
            pDraftLawViewModel.date_presentation = DateTime.ParseExact(pDraftLawViewModel.date_presentation_text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            pDraftLawViewModel.draft_law_id = 0;

            pDraftLawViewModel.user_id_created = AuthorizeUserAttribute.UsuarioLogeado().user_id;

            pDraftLawViewModel.commission = "";
            pDraftLawViewModel.interest_area= "";
            List<ImportError> oErrores = null;
            DraftLawBL oBL = new DraftLawBL();
            List<DraftLawViewModel> lista = new List<DraftLawViewModel>();
            lista.Add(pDraftLawViewModel);
            PeriodBL oPeriodBL = new PeriodBL();
            PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();

            oErrores = ValidarPeriodo(oPeriod, oErrores);
            if (oErrores.Count > 0)
                return Json(new { successfully = 0, errores = oErrores });

            if (VerificarPonentes(lista, out oErrores))
            {

                DraftLawBL oDraftLawBL = new DraftLawBL();
                CommissionBL oCommissionBL = new CommissionBL();
                InterestAreaBL oInterestAreaBL = new InterestAreaBL();
                DraftLawStatusBL oDraftLawStatusBL = new DraftLawStatusBL();
                OriginBL oOriginBL = new OriginBL();
                Dictionary<string, int> commisions = new Dictionary<string, int>();
                Dictionary<string, int> origins = oOriginBL.ObtenerDiccionarioPorNombre(lista.Select(a => a.origin).Distinct().ToList(), AuthorizeUserAttribute.UsuarioLogeado().user_id);
                Dictionary<string, int> interest_areas = new Dictionary<string, int>();
                Dictionary<string, DraftLawStatusViewModel> draftlaw_status = oDraftLawStatusBL.ObtenerDiccionarioPorNombre(lista.Select(a => a.status).Distinct().ToList(), AuthorizeUserAttribute.UsuarioLogeado().user_id);

                oDraftLawBL.Import(oPeriod, lista, origins, commisions, interest_areas, draftlaw_status, AuthorizeUserAttribute.UsuarioLogeado().user_id);
                // NotificacionNuevoProyectoLey(lista);
            }


            // oBL.Agregar(pDraftLawViewModel);
            if (oErrores.Count == 0)
                return Json(new { successfully = 1 });
            else
            {
                return Json(new { successfully = 0, errores = oErrores });
            }

            // return RedirectToAction("Index");

        }
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_draft_law })]
        public ActionResult Editar(string id)
        {
            DraftLawBL oBL = new DraftLawBL();
            int pIntID = 0;
            int.TryParse(id, out pIntID);
            DraftLawViewModel pDraftLawViewModel = oBL.Obtener(pIntID);
            SelectorBL oSelectorBL = new SelectorBL();
            List<SelectOptionItem> oCommissions = oSelectorBL.CommissionsSelector();
            List<SelectListItem> commissions = Helper.ConstruirDropDownList<SelectOptionItem>(oCommissions, "Value", "Text", "", true, "", "");
            ViewBag.commissions = commissions;
            List<SelectOptionItem> oInterestAreas = oSelectorBL.InterestAreasSelector();
            List<SelectListItem> interest_areas = Helper.ConstruirDropDownList<SelectOptionItem>(oInterestAreas, "Value", "Text", "", true, "", "");
            ViewBag.interest_areas = interest_areas;
            if (pDraftLawViewModel.date_presentation.HasValue)
                pDraftLawViewModel.date_presentation_text = pDraftLawViewModel.date_presentation.Value.ToString("dd/MM/yyyy");

            pDraftLawViewModel.period_closed = 1;
            if (pDraftLawViewModel.start_date <= DateTime.Today && pDraftLawViewModel.end_date >= DateTime.Today)
            {
                pDraftLawViewModel.period_closed = 0;
            }

            return View(pDraftLawViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.edit_draft_law })]
        public ActionResult Editar([Bind(Include = "draft_law_id,draft_law_number,title,author,origin,date_presentation,date_presentation_text,commission_id,debate_speaker,debate_speaker2,status,status_comment,interest_area_id,initiative,summary,link")] DraftLawViewModel pDraftLawViewModel)
        {
            // TODO: Add insert logic here
            pDraftLawViewModel.date_presentation = DateTime.ParseExact(pDraftLawViewModel.date_presentation_text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (pDraftLawViewModel == null)
            {
                return HttpNotFound();
            }
            DraftLawBL oDraftLawBL = new DraftLawBL();
            pDraftLawViewModel.user_id_modified = AuthorizeUserAttribute.UsuarioLogeado().user_id;
            oDraftLawBL.Modificar(pDraftLawViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_draft_law })]
        public JsonResult Eliminar(int id)
        {

            DraftLawBL oDraftLawBL = new DraftLawBL();
            DraftLawViewModel pDraftLawViewModel = oDraftLawBL.Obtener(id);

            var period_closed = 1;
            if (pDraftLawViewModel.start_date <= DateTime.Today && pDraftLawViewModel.end_date >= DateTime.Today)
            {
                period_closed = 0;
            }
            if(period_closed==0)
                oDraftLawBL.Eliminar(id);

            return Json(new
            {
                period_closed = period_closed,
                // this is what datatables wants sending back
                status = "1",

            });

        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.new_draft_law, AuthorizeUserAttribute.Permission.edit_draft_law })]
        public JsonResult Verificar(int draft_law_id, int draft_law_number)
        {

            DraftLawBL oDraftLawBL = new DraftLawBL();




            var resultado = oDraftLawBL.VerificarDuplicado(draft_law_id, draft_law_number);

            return Json(new
            {
                // this is what datatables wants sending back
                valido = resultado,

            });

        }

        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_draft_law })]

        public JsonResult ObtenerLista(DraftLawFiltersViewModel ofilters, [Bind(Include = "period_id")]  GeneralFilterViewModel generalfiltros)//DataTableAjaxPostModel model
        {
            DraftLawBL oDraftLawBL = new DraftLawBL();
            //DraftLawFiltersViewModel ofilters = new DraftLawFiltersViewModel();
            GridModel<DraftLawViewModel> grid = oDraftLawBL.ObtenerLista(ofilters, generalfiltros);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = ofilters.draw,
                recordsTotal = grid.total,
                recordsFiltered = grid.recordsFiltered,
                data = grid.rows
            });


        }

        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.list_draft_law })]
        public ActionResult UploadFiles()
        {

            List<ImportError> oErrores = null;
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname = Guid.NewGuid().ToString() + ".xlsx";

                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        file.SaveAs(fname);
                        DataTable dt = Util.ReadExcelToDataTable(fname);
                        PeriodBL oPeriodBL = new PeriodBL();
                        PeriodViewModel oPeriod = oPeriodBL.ObtenerVigente();

                        oErrores= ValidarPeriodo(oPeriod,  oErrores);
                        List<DraftLawViewModel> lista= new List<DraftLawViewModel>();
                        if (oErrores.Count == 0)
                        {
                            lista = ConvertirDatatable(dt, out oErrores);
                        }
                        if (oErrores.Count == 0)
                        {
                            if (VerificarPonentes(lista, out oErrores))
                            {

                                DraftLawBL oDraftLawBL = new DraftLawBL();
                                CommissionBL oCommissionBL = new CommissionBL();
                                InterestAreaBL oInterestAreaBL = new InterestAreaBL();
                                DraftLawStatusBL oDraftLawStatusBL = new DraftLawStatusBL();
                                OriginBL oOriginBL = new OriginBL();
                                Dictionary<string, int> commisions = oCommissionBL.ObtenerDiccionarioPorNombre(lista.Select(a => a.commission).Distinct().ToList(), AuthorizeUserAttribute.UsuarioLogeado().user_id);
                                Dictionary<string, int> origins = oOriginBL.ObtenerDiccionarioPorNombre(lista.Select(a => a.origin).Distinct().ToList(), AuthorizeUserAttribute.UsuarioLogeado().user_id);
                                Dictionary<string, int> interest_areas = oInterestAreaBL.ObtenerDiccionarioPorNombre(lista.Select(a => a.interest_area).Distinct().ToList(), AuthorizeUserAttribute.UsuarioLogeado().user_id);
                                Dictionary<string, DraftLawStatusViewModel> draftlaw_status = oDraftLawStatusBL.ObtenerDiccionarioPorNombre(lista.Select(a => a.status).Distinct().ToList(), AuthorizeUserAttribute.UsuarioLogeado().user_id);

                                oDraftLawBL.Import(oPeriod, lista, origins, commisions, interest_areas, draftlaw_status, AuthorizeUserAttribute.UsuarioLogeado().user_id);
                                // NotificacionNuevoProyectoLey(lista);
                            }

                        }

                    }
                    if (oErrores.Count == 0)
                        return Json(new { successfully = 1 });
                    else
                    {
                        return Json(new { successfully = 0, errores = oErrores });
                    }

                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    if (ex.Data.Contains("RepositoryMessage"))
                    {
                        logger.Error(ex.Data["RepositoryMessage"].ToString());
                    }
                    return Json(new { successfully = -1, message = "Ha ocurrido un error. Detalle error: " + ex.Message });
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        private bool VerificarPonentes(List<DraftLawViewModel> lista, out List<ImportError> oErrores)
        {
            ConfigurationBL oConfigurationBL = new ConfigurationBL();
            ConfigurationViewModel oConfigurationViewModel = oConfigurationBL.Obtener();
            List<string> IgnorarPonentes = oConfigurationViewModel.exclude_speakers.Split(',').ToList();
            //IgnorarPonentes.Add("Ministro de Justicia y del Derecho");
            //IgnorarPonentes.Add("Ministro de Hacienda y Crédito Público");

            List<string> ExcluirTitulos = oConfigurationViewModel.remove_titles_speaker.Split(',').ToList();
            /* ExcluirTitulos.Add("HR");
             ExcluirTitulos.Add("HS");
             ExcluirTitulos.Add("H.R.");
             ExcluirTitulos.Add("H.S.");
             ExcluirTitulos.Add("Dr.");*/


            oErrores = new List<ImportError>();
            UserBL oUserBL = new UserBL();
            int i = 1;
            foreach (DraftLawViewModel obj in lista)
            {
                List<string> errores = new List<string>();
                string mensaje = "";
                i++;
                var authors = obj.author.Split(',').ToList();
                
               
                if (!String.IsNullOrEmpty(obj.debate_speaker)) {
                    var debate_speaker = obj.debate_speaker.Split(',').ToList();
                    authors.AddRange(debate_speaker);
                }
                if (!String.IsNullOrEmpty(obj.debate_speaker2))
                {
                    var debate_speaker2 = obj.debate_speaker2.Split(',').ToList();

                    authors.AddRange(debate_speaker2);
                }
                List<int> debate_speakers = new List<int>();
                foreach (string author in authors)
                {
                    var author_aux = author.Trim();
                    if (IgnorarPonentes.Contains(author_aux))
                        continue;
                    else
                    {
                        author_aux = RemoverTitulos(author_aux, ExcluirTitulos);
                        int? user_id = oUserBL.ObtenerPonente(author_aux);

                        if (!user_id.HasValue || user_id == 0)
                        {
                            mensaje = "El Autor, Ponente de debate 1 o Ponente de debate 2 '" + author_aux + "' no esta registrado como usuario del sistema.";
                            errores.Add(mensaje);
                        }

                        else
                        {
                            debate_speakers.Add(user_id.Value);
                        }
                    }


                }
                obj.debate_speakers = debate_speakers;

                if (errores.Count() == 0)
                    continue;
                else
                {

                    oErrores.Add(new ImportError
                    {
                        nroFila = i,
                        errores = errores

                    });
                    break;
                }

            }
            return oErrores.Count == 0;

        }

        private string RemoverTitulos(string author_aux, List<string> excluirTitulos)
        {
            foreach (string title in excluirTitulos)
            {
                author_aux = Helper.ReplaceFirstOccurrence(author_aux, title, "");


            }
            return author_aux.Trim();
            //throw new NotImplementedException();
        }

       
        private List<DraftLawViewModel> ConvertirDatatable(DataTable dt, out List<ImportError> oErrores)
        {
            List<DraftLawViewModel> lista = new List<DraftLawViewModel>();
            oErrores = new List<ImportError>();
            List<string> erroresCabecera = new List<string>();

           
            ValidarCabecera(dt, out erroresCabecera);
            if (erroresCabecera.Count > 0)
            {
                oErrores.Add(new ImportError
                {
                    nroFila = 0,
                    errores = erroresCabecera,

                });
                return lista;
            }
            int i = 2;
            foreach (DataRow row in dt.Rows)
            {
                DraftLawViewModel obj = new DraftLawViewModel();
                List<string> errores = new List<string>();

                obj.draft_law_number = Util.ValidarInt(row, "myid", errores, true);

                obj.title = Util.ValidarString(row, "Titulo", errores, true);
                obj.author = Util.ValidarString(row, "Autor", errores, true);
                obj.origin = Util.ValidarString(row, "Origen", errores, true);



                obj.date_presentation = Util.ValidarDateTime(row, "F-Presentado", errores, true);


                obj.commission = Util.ValidarString(row, "Comision", errores, true);



                obj.debate_speaker = Util.ValidarString(row, "pon_1_debate", errores, false);
                obj.debate_speaker2 = Util.ValidarString(row, "pon_2_debate", errores, false);


                obj.status = Util.ValidarString(row, "Estado", errores, false);
                obj.status_comment = Util.ValidarString(row, "Comentario_Estado", errores, false);
                obj.interest_area = Util.ValidarString(row, "Tema", errores, false);
                obj.initiative = Util.ValidarString(row, "Iniciativa", errores, false);
                obj.link = Util.ValidarString(row, "Texto_Radicado_Link", errores, false);
                obj.summary = Util.ValidarString(row, "Resumen", errores, false);


                lista.Add(obj);
                if (errores.Count > 0)
                {
                    oErrores.Add(new ImportError
                    {
                        nroFila = i,
                        errores = errores
                    });
                }
                i++;

            }
            return lista;
        }
        private List<ImportError> ValidarPeriodo(PeriodViewModel oPeriod,  List<ImportError> oErrores)
        {
            oErrores = new List<ImportError>();
            List<string> oErrorCabecera = new List<string>();
            string formatError = "No hay un periodo vigente para hoy \"" + DateTime.Today.ToString("dd/MM/yyyy") + "\"";
            oErrorCabecera = new List<string>();
            if (oPeriod == null || oPeriod.period_id <= 0)
                oErrorCabecera.Add(formatError);

            if (oErrorCabecera.Count > 0)
            {
                oErrores.Add(new ImportError
                {
                    nroFila = 0,
                    errores = oErrorCabecera,

                });
              
            }
            return oErrores;
        }
        private void ValidarCabecera(DataTable dt, out List<string> oErrores)
        {
            string formatError = "El archivo Excel no tiene la columna {0}";
            oErrores = new List<string>();
            if (!dt.Columns.Contains("myid"))
                oErrores.Add(String.Format(formatError, "myid"));
            if (!dt.Columns.Contains("titulo"))
                oErrores.Add(String.Format(formatError, "Titulo"));
            if (!dt.Columns.Contains("Autor"))
                oErrores.Add(String.Format(formatError, "Autor"));
            if (!dt.Columns.Contains("Origen"))
                oErrores.Add(String.Format(formatError, "Origen"));
            if (!dt.Columns.Contains("F-Presentado"))
                oErrores.Add(String.Format(formatError, "F-Presentado"));
            if (!dt.Columns.Contains("Comision"))
                oErrores.Add(String.Format(formatError, "Comision"));
            if (!dt.Columns.Contains("pon_1_debate"))
                oErrores.Add(String.Format(formatError, "pon_1_debate"));
            if (!dt.Columns.Contains("pon_2_debate"))
                oErrores.Add(String.Format(formatError, "pon_2_debate"));

            if (!dt.Columns.Contains("Estado"))
                oErrores.Add(String.Format(formatError, "Estado"));
            if (!dt.Columns.Contains("Comentario_Estado"))
                oErrores.Add(String.Format(formatError, "Comentario_Estado"));


            if (!dt.Columns.Contains("Iniciativa"))
                oErrores.Add(String.Format(formatError, "Iniciativa"));
            if (!dt.Columns.Contains("Tema"))
                oErrores.Add(String.Format(formatError, "Tema"));

            if (!dt.Columns.Contains("Resumen"))
                oErrores.Add(String.Format(formatError, "Resumen"));
            if (!dt.Columns.Contains("Texto_Radicado_Link"))
                oErrores.Add(String.Format(formatError, "Texto_Radicado_Link"));




        }


    }
}
