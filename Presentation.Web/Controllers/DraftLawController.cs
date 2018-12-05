using Business.Logic;
using CrossCutting.Helper;
using Domain.Entities;
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
            return View();
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
        public ActionResult Crear([Bind(Include = "draft_law_id,draft_law_number,title,author,origin,date_presentation,date_presentation_text,commission_id,debate_speaker,debate_speaker2,status,status_comment,interest_area_id,initiative,summary,link")] DraftLawViewModel pDraftLawViewModel)
        {
            // TODO: Add insert logic here

            if (pDraftLawViewModel == null)
            {
                return HttpNotFound();
            }
            pDraftLawViewModel.date_presentation = DateTime.ParseExact(pDraftLawViewModel.date_presentation_text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            pDraftLawViewModel.draft_law_id = 0;

            pDraftLawViewModel.user_id_created = 0;

            DraftLawBL oBL = new DraftLawBL();

            oBL.Agregar(pDraftLawViewModel);

            return RedirectToAction("Index");

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
            oDraftLawBL.Modificar(pDraftLawViewModel);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [AuthorizeUser(Permissions = new AuthorizeUserAttribute.Permission[] { AuthorizeUserAttribute.Permission.delete_draft_law })]
        public JsonResult Eliminar(int id)
        {

            DraftLawBL oDraftLawBL = new DraftLawBL();

            oDraftLawBL.Eliminar(id);

            return Json(new
            {
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

        public JsonResult ObtenerLista(DraftLawFiltersViewModel ofilters)//DataTableAjaxPostModel model
        {
            DraftLawBL oDraftLawBL = new DraftLawBL();
            //DraftLawFiltersViewModel ofilters = new DraftLawFiltersViewModel();
            GridModel<DraftLawViewModel> grid = oDraftLawBL.ObtenerLista(ofilters);

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

                        List<DraftLawViewModel> lista = ConvertirDatatable(dt, out oErrores);
                        if (oErrores.Count == 0)
                        {

                            DraftLawBL oDraftLawBL = new DraftLawBL();
                            CommissionBL oCommissionBL = new CommissionBL();
                            InterestAreaBL oInterestAreaBL = new InterestAreaBL();

                            Dictionary<string, int> commisions = oCommissionBL.ObtenerDiccionarioPorNombre(lista.Select(a => a.commission).Distinct().ToList(), AuthorizeUserAttribute.UsuarioLogeado().user_id);
                            Dictionary<string, int> interest_areas = oInterestAreaBL.ObtenerDiccionarioPorNombre(lista.Select(a => a.interest_area).Distinct().ToList(), AuthorizeUserAttribute.UsuarioLogeado().user_id);

                            oDraftLawBL.Import(lista, commisions, interest_areas, AuthorizeUserAttribute.UsuarioLogeado().user_id);
                             

                            
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
