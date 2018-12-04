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
    public class DraftLawBL
    {
        private static DraftLawRepository oRepositorio;
        private static CommissionRepository oCommissionRepositorio;
        private static InterestAreaRepository oInterestAreaRepositorio;


        private static UnitOfWork oUnitOfWork;

        public DraftLawBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.DraftLawRepository;
            oCommissionRepositorio = oUnitOfWork.CommissionRepository;
            oInterestAreaRepositorio = oUnitOfWork.InterestAreaRepository;
        }

        public bool VerificarDuplicado(int id_draft_law, int draft_law_number)
        {
            return oRepositorio.VerificarDuplicado(id_draft_law, draft_law_number);
        }

       
        public DraftLawViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<DraftLawViewModel> ObtenerMisProyectosLey(DraftLawFiltersViewModel ofilters, List<int> commissions, List<int> interest_areas)
        {
            return oRepositorio.ObtenerMisProyectosLey(ofilters, commissions, interest_areas);
        }

        public GridModel<DraftLawViewModel> ObtenerLista(DraftLawFiltersViewModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(DraftLawViewModel pDraftLawViewModel)
        {
         

        
            draft_laws odraft_laws =oRepositorio.FindById(pDraftLawViewModel.draft_law_id);
            
            odraft_laws.title = pDraftLawViewModel.title;
            odraft_laws.draft_law_number = pDraftLawViewModel.draft_law_number;
            odraft_laws.author = pDraftLawViewModel.author;
            odraft_laws.origin = pDraftLawViewModel.origin;
            odraft_laws.date_presentation = pDraftLawViewModel.date_presentation;
            odraft_laws.commission_id = pDraftLawViewModel.commission_id;
            odraft_laws.debate_speaker = pDraftLawViewModel.debate_speaker;
            odraft_laws.debate_speaker2 = pDraftLawViewModel.debate_speaker2;
            odraft_laws.status = pDraftLawViewModel.status;
            odraft_laws.status_comment = pDraftLawViewModel.status_comment;
            odraft_laws.interest_area_id = pDraftLawViewModel.interest_area_id;
            odraft_laws.initiative = pDraftLawViewModel.initiative;
            odraft_laws.summary = pDraftLawViewModel.summary;
            odraft_laws.link = pDraftLawViewModel.link;
            odraft_laws.user_id_modified = pDraftLawViewModel.user_id_modified;
            odraft_laws.date_modified = DateTime.Now;
            oRepositorio.Update(odraft_laws);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            draft_laws oDraftLaw = new draft_laws
            {
                draft_law_id = id,
            };
            oRepositorio.Delete(oDraftLaw);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(DraftLawViewModel pDraftLawViewModel)
        {


            draft_laws odraft_laws = new draft_laws
            {
                draft_law_id=0,    
                draft_law_number=pDraftLawViewModel.draft_law_number,
                title =pDraftLawViewModel.title,
                author =pDraftLawViewModel.author,
                origin =pDraftLawViewModel.origin,
                date_presentation =pDraftLawViewModel.date_presentation,
                commission_id =pDraftLawViewModel.commission_id,
                debate_speaker =pDraftLawViewModel.debate_speaker,
                debate_speaker2 =pDraftLawViewModel.debate_speaker2,
                status =pDraftLawViewModel.status,
                status_comment =pDraftLawViewModel.status_comment,
                interest_area_id =pDraftLawViewModel.interest_area_id,
                initiative =pDraftLawViewModel.initiative,
                summary =pDraftLawViewModel.summary,
                link =pDraftLawViewModel.link,
                date_created =DateTime.Now,
                user_id_created= pDraftLawViewModel.user_id_created

            };
            oRepositorio.Add(odraft_laws);
            oUnitOfWork.SaveChanges();
        }

        public void Import(List<DraftLawViewModel> lista, Dictionary<string, int> commisions, Dictionary<string, int> interest_areas, int user_id)
        {
            using (var scope = new TransactionScope())
            {
                foreach (DraftLawViewModel pDraftLawViewModel in lista)
                {
                    if (commisions.ContainsKey(pDraftLawViewModel.commission))
                        pDraftLawViewModel.commission_id = commisions[pDraftLawViewModel.commission];

                    if (interest_areas.ContainsKey(pDraftLawViewModel.interest_area))
                        pDraftLawViewModel.interest_area_id = interest_areas[pDraftLawViewModel.interest_area];


                    DraftLawViewModel pDraftLawComplementViewModel =oRepositorio.ObtenerPorNroProyectoMigrar(pDraftLawViewModel.draft_law_number);
                    if (pDraftLawComplementViewModel!=null && pDraftLawComplementViewModel.draft_law_id != 0)
                    {
                        draft_laws odraft_laws = new draft_laws
                        {
                            draft_law_id = pDraftLawComplementViewModel.draft_law_id,
                            draft_law_number = pDraftLawViewModel.draft_law_number,
                            title = pDraftLawViewModel.title,
                            author = pDraftLawViewModel.author,
                            origin = pDraftLawViewModel.origin,
                            date_presentation = pDraftLawViewModel.date_presentation,
                            commission_id = pDraftLawViewModel.commission_id,
                            debate_speaker = pDraftLawViewModel.debate_speaker,
                            debate_speaker2 = pDraftLawViewModel.debate_speaker2,
                            status = pDraftLawViewModel.status,
                            status_comment = pDraftLawViewModel.status_comment,
                            interest_area_id = pDraftLawViewModel.interest_area_id,
                            initiative = pDraftLawViewModel.initiative,
                            summary = pDraftLawViewModel.summary,
                            link = pDraftLawViewModel.link,
                            date_created = pDraftLawComplementViewModel.date_created,
                            user_id_created = pDraftLawComplementViewModel.user_id_created,
                            date_modified = DateTime.Now,
                            user_id_modified = user_id

                        };
                        oRepositorio.Update(odraft_laws,

                                            a => a.draft_law_number,
                                            a => a.title,
                                            a => a.author,
                                            a => a.origin,
                                            a => a.date_presentation,
                                            a => a.commission_id,
                                            a => a.debate_speaker,
                                            a => a.debate_speaker2,
                                            a => a.status,
                                            a => a.status_comment,
                                            a => a.interest_area_id,
                                            a => a.initiative,
                                            a => a.summary,
                                            a => a.date_created,
                                            a => a.user_id_created,
                                            a => a.date_modified,
                                             a => a.user_id_modified
                                            );
                    }
                    else {
                        draft_laws odraft_laws = new draft_laws
                        {
                            draft_law_id = 0,
                            draft_law_number = pDraftLawViewModel.draft_law_number,
                            title = pDraftLawViewModel.title,
                            author = pDraftLawViewModel.author,
                            origin = pDraftLawViewModel.origin,
                            date_presentation = pDraftLawViewModel.date_presentation,
                            commission_id = pDraftLawViewModel.commission_id,
                            debate_speaker = pDraftLawViewModel.debate_speaker,
                            debate_speaker2 = pDraftLawViewModel.debate_speaker2,
                            status = pDraftLawViewModel.status,
                            status_comment = pDraftLawViewModel.status_comment,
                            interest_area_id = pDraftLawViewModel.interest_area_id,
                            initiative = pDraftLawViewModel.initiative,
                            summary = pDraftLawViewModel.summary,
                            link = pDraftLawViewModel.link,
                            date_created = DateTime.Now,
                            user_id_created = user_id

                        };
                        oRepositorio.Add(odraft_laws);
                    }
                      
                }
                oUnitOfWork.SaveChanges();

                
                scope.Complete();
            }
        }
    }
}
