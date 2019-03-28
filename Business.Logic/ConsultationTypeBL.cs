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
    public class ConsultationTypeBL
    {
        private static ConsultationTypeRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public ConsultationTypeBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.ConsultationTypeRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public ConsultationTypeViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<ConsultationTypeViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(ConsultationTypeViewModel pConsultationTypeViewModel)
        {       

            consultation_types oconsultation_types =oRepositorio.FindById(pConsultationTypeViewModel.consultation_type_id);
            oconsultation_types.name = pConsultationTypeViewModel.name;
            
            oconsultation_types.user_id_modified = pConsultationTypeViewModel.user_id_modified;

            oconsultation_types.date_modified = DateTime.Now;
            oRepositorio.Update(oconsultation_types);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            consultation_types oConsultationType = new consultation_types
            {
                consultation_type_id = id,
            };
            oRepositorio.Delete(oConsultationType);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(ConsultationTypeViewModel pConsultationTypeViewModel)
        {

            consultation_types oconsultation_types = new consultation_types
            {
                consultation_type_id = 0,
                name= pConsultationTypeViewModel.name,               
                date_created=DateTime.Now,
                user_id_created= pConsultationTypeViewModel.user_id_created

            };
            oRepositorio.Add(oconsultation_types);
            oUnitOfWork.SaveChanges();
        }


    }
}
