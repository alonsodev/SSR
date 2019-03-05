using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic
{
    public class ConsultationBL
    {
        private static ConsultationRepository oRepositorio;
        private static ConsultationInterestAreaRepository oRepositorioConsultationInterestArea;

        private static InvestigatorRepository oRepositorioInvestigator;
        
        private static UnitOfWork oUnitOfWork;

        public ConsultationBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.ConsultationRepository;
            oRepositorioConsultationInterestArea = oUnitOfWork.ConsultationInterestAreaRepository;
            oRepositorioInvestigator = oUnitOfWork.InvestigatorRepository;
        }

       
       
        public ConsultationViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<ConsultationViewModel> ObtenerLista(DataTableAjaxPostModel filters,int user_id)
        {
            return oRepositorio.ObtenerLista(filters, user_id);
        }


        public GridModel<ConsultationViewModel> ObtenerListaEnviados(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerListaEnviados(filters );
        }

        public GridModel<InvestigatorViewModel> ObtenerInvestigadores(DataTableAjaxPostModel filters, List<int> interest_areas)
        {
            return oRepositorioInvestigator.ObtenerInvestigadores(filters, interest_areas);
        }



        public void Agregar(ConsultationViewModel pConsultationViewModel)
        {


            consultations oconsultations = new consultations
            {
                consultation_id = 0,
                title = pConsultationViewModel.title,
                message = pConsultationViewModel.message,
                attended= false,
                
                date_created =DateTime.Now,
                user_id_created= pConsultationViewModel.user_id_created

            };
            oRepositorio.Add(oconsultations);

            foreach (int interest_area_id in pConsultationViewModel.interest_areas)
            {
                oRepositorioConsultationInterestArea.Add(new consultations_interest_areas
                {
                    interest_area_id = interest_area_id,
                    consultation_id = pConsultationViewModel.consultation_id,
                    date_created = DateTime.Now,
                    user_id_created = pConsultationViewModel.user_id_created,
                    date_modified = DateTime.Now,
                    user_id_modified = pConsultationViewModel.user_id_created,
                });
            }

            oUnitOfWork.SaveChanges();
        }


    }
}
