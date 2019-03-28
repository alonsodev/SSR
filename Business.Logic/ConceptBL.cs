using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TestXSLTMail;

namespace Business.Logic
{
    public class ConceptBL
    {
        private static ConceptRepository oRepositorio;
        private static TagRepository oRepositorioTag;
        private static ConceptTagRepository oRepositorioConceptTag;
        private static ConceptStatusLogRepository oRepositorioConceptStatusLog;
        private static ConceptDebateSpeakerRepository oRepositorioConceptDebateSpeaker;

        private static UnitOfWork oUnitOfWork;

        public ConceptBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.ConceptRepository;
            oRepositorioTag = oUnitOfWork.TagRepository;
            oRepositorioConceptTag = oUnitOfWork.ConceptTagRepository;
            oRepositorioConceptStatusLog = oUnitOfWork.ConceptStatusLogRepository;

            oRepositorioConceptDebateSpeaker = oUnitOfWork.ConceptDebateSpeakerRepository;
        }

        public bool ExisteConcepto(int draft_law_id, int investigator_id)
        {
            return oRepositorio.ExisteConcepto(draft_law_id, investigator_id);
        }
        public static string ObtenerHtmlConcept(ConceptHtmlViewModel oConcept, string xslPath)
        {
            StringBuilder msgBody = new StringBuilder();
            if (File.Exists(xslPath))
            {
                MailGenerator mailGenerator = new MailGenerator(xslPath);
                //string serialize = ConvertObjectToXMLString(oAsignacionLancha);

                string message = mailGenerator.Generate(oConcept, typeof(ConceptHtmlViewModel));
                msgBody.Append(message);
                return msgBody.ToString();
            }
            return string.Empty;
        }

        public GridModel<ReportViewModel> ObtenerReporte(DataTableAjaxPostModel ofilters, ReportFilterViewModel Reportefiltros)
        {
            return oRepositorio.ObtenerReporte(ofilters, Reportefiltros);
        }
        public List<ReportViewModel> ObtenerExportarReporte(ReportFilterViewModel filtros)
        {
            return oRepositorio.ObtenerExportarReporte(filtros);
        }
        public static string ObtenerHtmlCertification(CertificationHtmlViewModel oCertification, string xslPath)
        {
            StringBuilder msgBody = new StringBuilder();
            if (File.Exists(xslPath))
            {
                MailGenerator mailGenerator = new MailGenerator(xslPath);
                //string serialize = ConvertObjectToXMLString(oAsignacionLancha);

                string message = mailGenerator.Generate(oCertification, typeof(CertificationHtmlViewModel));
                msgBody.Append(message);
                return msgBody.ToString();
            }
            return string.Empty;
        }

        public void ActualizarStatus(ConceptStatusLogViewModel oConceptStatusLogViewModel)
        {
            using (var scope = new TransactionScope())
            {
                concepts oconcepts = oRepositorio.FindById(oConceptStatusLogViewModel.concept_id);
                oconcepts.concept_status_id = oConceptStatusLogViewModel.concept_status_id;
                oRepositorio.Update(oconcepts);

                concepts_status_logs oconcepts_status_logs = new concepts_status_logs();
                oconcepts_status_logs.concept_status_log_id = 0;
                oconcepts_status_logs.concept_id = oConceptStatusLogViewModel.concept_id;
                oconcepts_status_logs.reason_reject_id = oConceptStatusLogViewModel.reason_reject_id == 0 ? null : oConceptStatusLogViewModel.reason_reject_id;
                oconcepts_status_logs.concept_status_id = oConceptStatusLogViewModel.concept_status_id;
                oconcepts_status_logs.date_created = DateTime.Now;
                oconcepts_status_logs.user_id_created = oConceptStatusLogViewModel.user_id_created;
                oconcepts_status_logs.description = oConceptStatusLogViewModel.description;
                oRepositorioConceptStatusLog.Add(oconcepts_status_logs);


                if (oConceptStatusLogViewModel.speakers != null && oConceptStatusLogViewModel.speakers.Count > 0)
                {

                    foreach (int user_id in oConceptStatusLogViewModel.speakers)
                    {
                        concept_debate_speakers oconcept_debate_speakers = new concept_debate_speakers();
                        oconcept_debate_speakers.concept_id = oConceptStatusLogViewModel.concept_id;
                        oconcept_debate_speakers.user_id = user_id;
                        oconcept_debate_speakers.date_created = DateTime.Now;
                        oconcept_debate_speakers.user_id_created = oConceptStatusLogViewModel.user_id_created;
                        oRepositorioConceptDebateSpeaker.Add(oconcept_debate_speakers);


                    }
                }
                oUnitOfWork.SaveChanges();
                scope.Complete();
            }
        }

        public MyHistoryViewModel ObtenerMiHistorial(int investigator_id)
        {
            return oRepositorio.ObtenerMiHistorial(investigator_id);
        }

        public void Calificar(ConceptStatusLogViewModel oConceptStatusLogViewModel)
        {
            using (var scope = new TransactionScope())
            {


                concepts_status_logs oconcepts_status_logs = new concepts_status_logs();
                oconcepts_status_logs.concept_status_log_id = 0;
                oconcepts_status_logs.concept_id = oConceptStatusLogViewModel.concept_id;
                oconcepts_status_logs.concept_status_id = 5;
                oconcepts_status_logs.date_created = DateTime.Now;
                oconcepts_status_logs.user_id_created = oConceptStatusLogViewModel.user_id_created;
                oconcepts_status_logs.qualification = oConceptStatusLogViewModel.qualification;

                oRepositorioConceptStatusLog.Add(oconcepts_status_logs);
                oUnitOfWork.SaveChanges();
                List<ConceptStatusLogViewModel> calificaciones = oRepositorioConceptStatusLog.ObtenerCalificaciones(oConceptStatusLogViewModel.concept_id);
                int NumeroPonentes = oRepositorio.NumeroPonentes(oConceptStatusLogViewModel.concept_id);
                int NumeroCalificaciones = calificaciones != null ? calificaciones.Count() : 0;
                int concept_status_id = 5;
                if (NumeroPonentes == NumeroCalificaciones)
                {
                    concept_status_id = 6;
                }



                concepts oconcepts = oRepositorio.FindById(oConceptStatusLogViewModel.concept_id);
                oconcepts.concept_status_id = concept_status_id;
                oconcepts.certification_path = oConceptStatusLogViewModel.certification_path;
                if (NumeroCalificaciones > 0)
                {
                    double qualification = calificaciones.Select(a => a.qualification).ToList().Average();
                    oconcepts.qualification = Math.Round(qualification, 2);
                }

                oRepositorio.Update(oconcepts);

                oUnitOfWork.SaveChanges();
                scope.Complete();
            }
        }

        public RejectConceptViewModel ObtenerRechazo(int concept_id)
        {
            return oRepositorioConceptStatusLog.ObtenerRechazo(concept_id);
        }
        public Boolean VerificarCalificado(int concept_id, int user_id)
        {
            return oRepositorioConceptStatusLog.VerificarCalificado(concept_id, user_id);
        }

        public Boolean VerificarCalificado(int concept_id)
        {
            return oRepositorioConceptStatusLog.VerificarCalificado(concept_id);
        }

        public void Leido(ConceptStatusLogViewModel oConceptStatusLogViewModel)
        {
            if (oRepositorioConceptStatusLog.VerificarLeido(oConceptStatusLogViewModel.concept_id, oConceptStatusLogViewModel.user_id_created.Value))
            {
                return;
            }
            using (var scope = new TransactionScope())
            {



                concepts_status_logs oconcepts_status_logs = new concepts_status_logs();
                oconcepts_status_logs.concept_status_log_id = 0;
                oconcepts_status_logs.concept_id = oConceptStatusLogViewModel.concept_id;
                oconcepts_status_logs.concept_status_id = 4;
                oconcepts_status_logs.date_created = DateTime.Now;
                oconcepts_status_logs.user_id_created = oConceptStatusLogViewModel.user_id_created;

                oRepositorioConceptStatusLog.Add(oconcepts_status_logs);


                int NumeroCalificaciones = oRepositorio.NumeroCalificaciones(oConceptStatusLogViewModel.concept_id);
                int concept_status_id = 4;
                if (NumeroCalificaciones == 0)
                {
                    concepts oconcepts = oRepositorio.FindById(oConceptStatusLogViewModel.concept_id);
                    oconcepts.concept_status_id = concept_status_id;
                    oRepositorio.Update(oconcepts);
                }



                oUnitOfWork.SaveChanges();
                scope.Complete();
            }
        }
        public CertificationHtmlViewModel ObtenerHtmlCertification(int concept_id)
        {

            return oRepositorio.ObtenerHtmlCertification(concept_id);
        }
        public ConceptViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }
        public string ObtenerPdfpath(int pIntID)
        {

            return oRepositorio.ObtenerPdfpath(pIntID);
        }
        public string ObtenerCertificadoPdfpath(int pIntID)
        {

            return oRepositorio.ObtenerCertificadoPdfpath(pIntID);
        }
        public GridModel<ConceptViewModel> ObtenerLista(DataTableAjaxPostModel filters, int investigator_id, GeneralFilterViewModel generalfiltros)
        {
            return oRepositorio.ObtenerLista(filters, investigator_id, generalfiltros);
        }

        public void Modificar(ConceptViewModel pConceptViewModel)
        {

            using (var scope = new TransactionScope())
            {

                concepts oconcepts = oRepositorio.FindById(pConceptViewModel.concept_id);
                oconcepts.concept = pConceptViewModel.concept;
                oconcepts.summary = pConceptViewModel.summary;

                oconcepts.pdf_path = pConceptViewModel.pdf_path;
                oconcepts.user_id_modified = pConceptViewModel.user_id_modified;
                oconcepts.concept_status_id = pConceptViewModel.concept_status_id;
                // oconcepts.tags = oRepositorioTag.TagsByfilters(pConceptViewModel.tag_ids);
                oconcepts.date_modified = DateTime.Now;
                oRepositorio.Update(oconcepts);

                //oconcepts.tags = oRepositorioTag.TagsByfilters(pConceptViewModel.tag_ids);
                // oconcepts = oRepositorio.Add(oconcepts);

                oRepositorioConceptTag.DeleleMultiple(oconcepts.concept_id);
                foreach (int tag_id in pConceptViewModel.tag_ids)
                {
                    concepts_tags oconcepts_tags = new concepts_tags
                    {
                        concept_id = oconcepts.concept_id,
                        tag_id = tag_id,
                        date_created = DateTime.Now,
                        user_id_created = pConceptViewModel.user_id_created

                    };
                    oRepositorioConceptTag.Add(oconcepts_tags);
                }

                oUnitOfWork.SaveChanges();
                scope.Complete();
            }
        }

        public void Eliminar(int id)
        {
            concepts oConcept = new concepts
            {
                concept_id = id,
            };
            oRepositorio.Delete(oConcept);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(ConceptViewModel pConceptViewModel)
        {

            using (var scope = new TransactionScope())
            {
                concepts oconcepts = new concepts
                {
                    concept_id = 0,
                    concept = pConceptViewModel.concept,
                    summary = pConceptViewModel.summary,
                    draft_law_id = pConceptViewModel.draft_law_id,
                    investigator_id = pConceptViewModel.investigator_id,
                    bibliography = pConceptViewModel.bibliography,

                    date_created = DateTime.Now,
                    user_id_created = pConceptViewModel.user_id_created,
                    concept_status_id = 1,
                    pdf_path = pConceptViewModel.pdf_path,
                    hash = Guid.NewGuid()

                };

                //oconcepts.tags = oRepositorioTag.TagsByfilters(pConceptViewModel.tag_ids);
                oconcepts = oRepositorio.Add(oconcepts);
                //oUnitOfWork.SaveChanges();
                foreach (int tag_id in pConceptViewModel.tag_ids)
                {
                    concepts_tags oconcepts_tags = new concepts_tags
                    {
                        concept_id = oconcepts.concept_id,
                        tag_id = tag_id,
                        date_created = DateTime.Now,
                        user_id_created = pConceptViewModel.user_id_created

                    };
                    oRepositorioConceptTag.Add(oconcepts_tags);
                }

                oUnitOfWork.SaveChanges();
                scope.Complete();
                pConceptViewModel.concept_id = oconcepts.concept_id;
            }
        }
        public ConceptHtmlViewModel ObtenerHtmlConcept(int concept_id)
        {
            return oRepositorio.ObtenerHtmlConcept(concept_id);
        }
        public GridModel<ConceptViewModel> ObtenerEmitidos(DataTableAjaxPostModel ofilters, GeneralFilterViewModel generalfiltros)
        {

            return oRepositorio.ObtenerEmitidos(ofilters, generalfiltros);
        }

        public GridModel<ConceptViewModel> ObtenerPorCalificar(DataTableAjaxPostModel ofilters, int user_id, GeneralFilterViewModel generalfiltros)
        {
            return oRepositorio.ObtenerPorCalificar(ofilters, user_id, generalfiltros);
        }

        public GridModel<ConceptViewModel> ObtenerCertificados(DataTableAjaxPostModel ofilters, int investigator_id, GeneralFilterViewModel generalfiltros)
        {
            return oRepositorio.ObtenerCertificados(ofilters, investigator_id, generalfiltros);
        }
        public GridModel<RankingViewModel> ObtenerRanking(DataTableAjaxPostModel filters, int interest_area_id)
        {
            GridModel<RankingViewModel> lista = oRepositorio.ObtenerRanking(filters, interest_area_id);
            int i = filters.start + 1;
            foreach (var row in lista.rows)
            {
                row.position = i++;
            }
            return lista;
        }

        public GridModel<ConceptViewModel> ObtenerRecibidos(DataTableAjaxPostModel ofilters, int user_id, GeneralFilterViewModel generalfiltros)
        {
            return oRepositorio.ObtenerRecibidos(ofilters, user_id, generalfiltros);
        }

        public VerifyCertificationViewModel ObtenerVerificacionCertificado(Guid hash)
        {
            return oRepositorio.ObtenerVerificacionCertificado(hash);
        }

        public void ActualizarTablasReporte(int period_id)
        {
             oRepositorio.ActualizarTablasReporte(period_id);

        }
    }
}
