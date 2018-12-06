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

        private static UnitOfWork oUnitOfWork;

        public ConceptBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.ConceptRepository;
            oRepositorioTag = oUnitOfWork.TagRepository;
            oRepositorioConceptTag = oUnitOfWork.ConceptTagRepository;
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
                    user_id_created = pConceptViewModel.user_id_created

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
    }
}
