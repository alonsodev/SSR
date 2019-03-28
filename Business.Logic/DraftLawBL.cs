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
        private static DebateSpeakerRepository oDebateSpeakerRepositorio;


        private static UnitOfWork oUnitOfWork;

        public DraftLawBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.DraftLawRepository;
            oCommissionRepositorio = oUnitOfWork.CommissionRepository;
            oInterestAreaRepositorio = oUnitOfWork.InterestAreaRepository;
            oDebateSpeakerRepositorio = oUnitOfWork.DebateSpeakerRepository;
        }

        public List<DraftLawViewModel> ObtenerNotificables()
        {
            return oRepositorio.ObtenerNotificables();
        }

        public bool VerificarDuplicado(int id_draft_law, int draft_law_number)
        {
            return oRepositorio.VerificarDuplicado(id_draft_law, draft_law_number);
        }


        public DraftLawViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<DraftLawViewModel> ObtenerMisProyectosLey(DraftLawFiltersViewModel ofilters, List<int> commissions, List<int> interest_areas, GeneralFilterViewModel generalfiltros)
        {
            return oRepositorio.ObtenerMisProyectosLey(ofilters, commissions, interest_areas, generalfiltros);
        }

        public GridModel<DraftLawViewModel> ObtenerLista(DraftLawFiltersViewModel filters, GeneralFilterViewModel generalfiltros)
        {
            return oRepositorio.ObtenerLista(filters, generalfiltros);
        }

        public void Modificar(DraftLawViewModel pDraftLawViewModel)
        {



            draft_laws odraft_laws = oRepositorio.FindById(pDraftLawViewModel.draft_law_id);

            odraft_laws.title = pDraftLawViewModel.title;
            odraft_laws.draft_law_number = pDraftLawViewModel.draft_law_number;
            odraft_laws.author = pDraftLawViewModel.author;
            odraft_laws.origin_id = pDraftLawViewModel.origin_id;
            odraft_laws.date_presentation = pDraftLawViewModel.date_presentation;
            odraft_laws.commission_id = pDraftLawViewModel.commission_id;
            odraft_laws.debate_speaker = pDraftLawViewModel.debate_speaker;
            odraft_laws.debate_speaker2 = pDraftLawViewModel.debate_speaker2;
            //odraft_laws.status = pDraftLawViewModel.draft_laws_status.name;// corregir
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

        public void ActualizarNotificacion(List<DraftLawViewModel> list)
        {

            oRepositorio.ActualizarNotificacion(list);

        }

        public void Eliminar(int id)
        {
            using (var scope = new TransactionScope())
            {
                oDebateSpeakerRepositorio.DeleteMultiple(id);
                draft_laws oDraftLaw = new draft_laws
                {
                    draft_law_id = id,
                };
                oRepositorio.Delete(oDraftLaw);
                oUnitOfWork.SaveChanges();
                scope.Complete();
            }
        }

        public void Agregar(DraftLawViewModel pDraftLawViewModel)
        {


            draft_laws odraft_laws = new draft_laws
            {
                draft_law_id = 0,
                draft_law_number = pDraftLawViewModel.draft_law_number,
                title = pDraftLawViewModel.title,
                author = pDraftLawViewModel.author,
                origin_id = pDraftLawViewModel.origin_id,
                date_presentation = pDraftLawViewModel.date_presentation,
                commission_id = pDraftLawViewModel.commission_id,
                debate_speaker = pDraftLawViewModel.debate_speaker,
                debate_speaker2 = pDraftLawViewModel.debate_speaker2,
                //status = pDraftLawViewModel.status, // corregir
                status_comment = pDraftLawViewModel.status_comment,
                interest_area_id = pDraftLawViewModel.interest_area_id,
                initiative = pDraftLawViewModel.initiative,
                summary = pDraftLawViewModel.summary,
                link = pDraftLawViewModel.link,
                date_created = DateTime.Now,
                user_id_created = pDraftLawViewModel.user_id_created

            };
            oRepositorio.Add(odraft_laws);
            oUnitOfWork.SaveChanges();
        }

        public void Import(PeriodViewModel oPeriod, List<DraftLawViewModel> lista, Dictionary<string, int> origins, Dictionary<string, int> commisions, Dictionary<string, int> interest_areas, Dictionary<string, DraftLawStatusViewModel> draftlaw_status, int user_id)
        {
            using (var scope = new TransactionScope())
            {
                foreach (DraftLawViewModel pDraftLawViewModel in lista)
                {
                    pDraftLawViewModel.period_id = oPeriod.period_id;
                    if (origins.ContainsKey(pDraftLawViewModel.origin))
                        pDraftLawViewModel.origin_id = origins[pDraftLawViewModel.origin];


                    if (commisions.ContainsKey(pDraftLawViewModel.commission))
                        pDraftLawViewModel.commission_id = commisions[pDraftLawViewModel.commission];

                    if (interest_areas.ContainsKey(pDraftLawViewModel.interest_area))
                        pDraftLawViewModel.interest_area_id = interest_areas[pDraftLawViewModel.interest_area];

                    if (draftlaw_status.ContainsKey(pDraftLawViewModel.status))
                        pDraftLawViewModel.draft_law_status_id = draftlaw_status[pDraftLawViewModel.status].draft_law_status_id;

                    DraftLawViewModel pDraftLawComplementViewModel = oRepositorio.ObtenerPorNroProyectoMigrar(pDraftLawViewModel.draft_law_number, pDraftLawViewModel.period_id);
                    if (pDraftLawComplementViewModel != null && pDraftLawComplementViewModel.draft_law_id != 0)
                    {
                        DraftLawViewModel pDraftLawComplementViewModel2 = oRepositorio.ObtenerPorNroProyectoMigrar(pDraftLawViewModel.draft_law_number, pDraftLawViewModel.draft_law_status_id, pDraftLawViewModel.period_id);
                        if (pDraftLawComplementViewModel2 != null && pDraftLawComplementViewModel2.draft_law_id != 0)
                        {
                            draft_laws odraft_laws = new draft_laws
                            {
                                draft_law_id = pDraftLawComplementViewModel.draft_law_id,
                                draft_law_number = pDraftLawViewModel.draft_law_number,
                                title = pDraftLawViewModel.title,
                                author = pDraftLawViewModel.author,
                                origin_id = pDraftLawViewModel.origin_id,
                                date_presentation = pDraftLawViewModel.date_presentation,

                                commission_id = pDraftLawViewModel.commission_id,
                                debate_speaker = pDraftLawViewModel.debate_speaker,
                                debate_speaker2 = pDraftLawViewModel.debate_speaker2,
                                status_id = pDraftLawViewModel.draft_law_status_id,
                                status_comment = pDraftLawViewModel.status_comment,
                                interest_area_id = pDraftLawViewModel.interest_area_id,
                                initiative = pDraftLawViewModel.initiative,
                                summary = pDraftLawViewModel.summary,
                                link = pDraftLawViewModel.link,
                                period_id = pDraftLawViewModel.period_id,
                                date_modified = DateTime.Now,
                                user_id_modified = user_id

                            };
                            oRepositorio.Update(odraft_laws,

                                                a => a.draft_law_number,
                                                a => a.title,
                                                a => a.author,
                                                a => a.origin_id,
                                                a => a.date_presentation,
                                                a => a.commission_id,
                                                a => a.debate_speaker,
                                                a => a.debate_speaker2,
                                                a => a.status_id,
                                                a => a.status_comment,
                                                a => a.interest_area_id,
                                                a => a.initiative,
                                                a => a.summary,
                                                a => a.link,
                                                a => a.date_modified,
                                                a => a.user_id_modified,
                                                a => a.period_id
                                                );
                            oDebateSpeakerRepositorio.DeleteMultiple(pDraftLawComplementViewModel.draft_law_id);
                            foreach (int debate_user_id in pDraftLawViewModel.debate_speakers)
                            {
                                debate_speakers odebate_speakers = new debate_speakers
                                {
                                    user_id = debate_user_id,
                                    draft_law_id = odraft_laws.draft_law_id,
                                    date_created = DateTime.Now,
                                    user_id_created = user_id
                                };
                                oDebateSpeakerRepositorio.Add(odebate_speakers);
                            }

                        }
                        else
                        {
                            draft_laws odraft_laws = new draft_laws
                            {
                                draft_law_id = pDraftLawComplementViewModel.draft_law_id,
                                active = false
                            };
                            oRepositorio.Update(odraft_laws, a => a.active);
                            AddDraftLaw(user_id, pDraftLawViewModel);
                        }

                    }
                    else
                    {
                        AddDraftLaw(user_id, pDraftLawViewModel);

                    }
                    oUnitOfWork.SaveChanges();
                }
                // oUnitOfWork.SaveChanges();


                scope.Complete();
            }
        }

        private static void AddDraftLaw(int user_id, DraftLawViewModel pDraftLawViewModel)
        {
            draft_laws odraft_laws = new draft_laws
            {
                draft_law_id = 0,
                draft_law_number = pDraftLawViewModel.draft_law_number,
                title = pDraftLawViewModel.title,
                author = pDraftLawViewModel.author,
                origin_id = pDraftLawViewModel.origin_id,
                date_presentation = pDraftLawViewModel.date_presentation,
                commission_id = pDraftLawViewModel.commission_id,
                debate_speaker = pDraftLawViewModel.debate_speaker,
                debate_speaker2 = pDraftLawViewModel.debate_speaker2,
                status_id = pDraftLawViewModel.draft_law_status_id,
                status_comment = pDraftLawViewModel.status_comment,
                interest_area_id = pDraftLawViewModel.interest_area_id,
                initiative = pDraftLawViewModel.initiative,
                summary = pDraftLawViewModel.summary,
                link = pDraftLawViewModel.link,
                period_id = pDraftLawViewModel.period_id,
                date_created = DateTime.Now,
                user_id_created = user_id,
                notified = false,
                active = true,

            };

            odraft_laws = oRepositorio.Add(odraft_laws);

            foreach (int debate_user_id in pDraftLawViewModel.debate_speakers)
            {
                debate_speakers odebate_speakers = new debate_speakers
                {
                    user_id = debate_user_id,
                    draft_law_id = odraft_laws.draft_law_id,
                    date_created = DateTime.Now,
                    user_id_created = user_id
                };
                oDebateSpeakerRepositorio.Add(odebate_speakers);
            }
        }
    }
}
