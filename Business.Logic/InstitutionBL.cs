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
    public class InstitutionBL
    {
        private static InstitutionRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public InstitutionBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.InstitutionRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public InstitutionViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<InstitutionViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(InstitutionViewModel pInstitutionViewModel)
        {
         

        
            institutions oinstitutions =oRepositorio.FindById(pInstitutionViewModel.institution_id);
            oinstitutions.name = pInstitutionViewModel.name;
            
            oinstitutions.user_id_modified = pInstitutionViewModel.user_id_modified;

            oinstitutions.date_modified = DateTime.Now;
            oRepositorio.Update(oinstitutions);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            institutions oInstitution = new institutions
            {
                institution_id = id,
            };
            oRepositorio.Delete(oInstitution);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(InstitutionViewModel pInstitutionViewModel)
        {


            institutions oinstitutions = new institutions
            {
                institution_id = 0,
                name= pInstitutionViewModel.name,               
                date_created=DateTime.Now,
                user_id_created= pInstitutionViewModel.user_id_created

            };
            oRepositorio.Add(oinstitutions);
            oUnitOfWork.SaveChanges();
        }


    }
}
