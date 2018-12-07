using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Business.Logic
{
    public class ConceptBL
    {
        private static ConceptRepository oRepositorio;
        private static TagRepository oRepositorioTag;
        private static ConceptTagRepository oRepositorioConceptTag;
        private static ConceptStatusLogRepository oRepositorioConceptStatusLog;

        private static UnitOfWork oUnitOfWork;

        public ConceptBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.ConceptRepository;
            oRepositorioTag = oUnitOfWork.TagRepository;
            oRepositorioConceptTag = oUnitOfWork.ConceptTagRepository;
            oRepositorioConceptStatusLog = oUnitOfWork.ConceptStatusLogRepository;
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

                oUnitOfWork.SaveChanges();
                scope.Complete();
            }
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
                int NumeroCalificaciones = calificaciones!=null? calificaciones.Count():0;
                int concept_status_id = 5;
                if (NumeroPonentes == NumeroCalificaciones) {
                    concept_status_id = 6;
                }

                

                concepts oconcepts = oRepositorio.FindById(oConceptStatusLogViewModel.concept_id);
                oconcepts.concept_status_id = concept_status_id;
                if (NumeroCalificaciones > 0) {
                    double qualification = calificaciones.Select(a => a.qualification).ToList().Average();
                    oconcepts.qualification = Math.Round(qualification,2);
                }
                
                oRepositorio.Update(oconcepts);

                oUnitOfWork.SaveChanges();
                scope.Complete();
            }
        }
        public void Leido(ConceptStatusLogViewModel oConceptStatusLogViewModel)
        {
            if (oRepositorioConceptStatusLog.VerificarLeido(oConceptStatusLogViewModel.concept_id, oConceptStatusLogViewModel.user_id_created.Value)) {
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
                if ( NumeroCalificaciones==0)
                {
                    concepts oconcepts = oRepositorio.FindById(oConceptStatusLogViewModel.concept_id);
                    oconcepts.concept_status_id = concept_status_id;
                    oRepositorio.Update(oconcepts);
                }

                

                oUnitOfWork.SaveChanges();
                scope.Complete();
            }
        }
        public ConceptViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<ConceptViewModel> ObtenerLista(DataTableAjaxPostModel filters, int investigator_id)
        {
            return oRepositorio.ObtenerLista(filters, investigator_id);
        }

        public void Modificar(ConceptViewModel pConceptViewModel)
        {

            using (var scope = new TransactionScope())
            {

                concepts oconcepts = oRepositorio.FindById(pConceptViewModel.concept_id);
                oconcepts.concept = pConceptViewModel.concept;
                oconcepts.summary = pConceptViewModel.summary;


                oconcepts.user_id_modified = pConceptViewModel.user_id_modified;

                // oconcepts.tags = oRepositorioTag.TagsByfilters(pConceptViewModel.tag_ids);
                oconcepts.date_modified = DateTime.Now;
                oRepositorio.Update(oconcepts);

                //oconcepts.tags = oRepositorioTag.TagsByfilters(pConceptViewModel.tag_ids);
                oconcepts = oRepositorio.Add(oconcepts);

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
                    concept_status_id = 1
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
            }
        }

        public GridModel<ConceptViewModel> ObtenerEmitidos(DataTableAjaxPostModel ofilters)
        {

            return oRepositorio.ObtenerEmitidos(ofilters);
        }

        public GridModel<ConceptViewModel> ObtenerPorCalificar(DataTableAjaxPostModel ofilters,int user_id)
        {
            return oRepositorio.ObtenerPorCalificar(ofilters, user_id);
        }
    }
}
