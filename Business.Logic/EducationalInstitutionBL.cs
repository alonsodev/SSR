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
    public class EducationalInstitutionBL
    {
        private static EducationalInstitutionRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public EducationalInstitutionBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.EducationalInstitutionRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public EducationalInstitutionViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<EducationalInstitutionViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(EducationalInstitutionViewModel pEducationalInstitutionViewModel)
        {
         

        
            educational_institutions oeducational_institutions =oRepositorio.FindById(pEducationalInstitutionViewModel.educational_institution_id);
            oeducational_institutions.name = pEducationalInstitutionViewModel.name;
            
            oeducational_institutions.user_id_modified = pEducationalInstitutionViewModel.user_id_modified;

            oeducational_institutions.date_modified = DateTime.Now;
            oRepositorio.Update(oeducational_institutions);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            educational_institutions oEducationalInstitution = new educational_institutions
            {
                educational_institution_id = id,
            };
            oRepositorio.Delete(oEducationalInstitution);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(EducationalInstitutionViewModel pEducationalInstitutionViewModel)
        {


            educational_institutions oeducational_institutions = new educational_institutions
            {
                educational_institution_id = 0,
                name= pEducationalInstitutionViewModel.name,               
                date_created=DateTime.Now,
                user_id_created= pEducationalInstitutionViewModel.user_id_created

            };
            oRepositorio.Add(oeducational_institutions);
            oUnitOfWork.SaveChanges();
        }


    }
}
