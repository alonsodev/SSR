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
    public class EducationLevelBL
    {
        private static EducationLevelRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public EducationLevelBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.EducationLevelRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public EducationLevelViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<EducationLevelViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(EducationLevelViewModel pEducationLevelViewModel)
        {
         

        
            education_levels oeducation_levels =oRepositorio.FindById(pEducationLevelViewModel.education_level_id);
            oeducation_levels.name = pEducationLevelViewModel.name;
            
            oeducation_levels.user_id_modified = pEducationLevelViewModel.user_id_modified;

            oeducation_levels.date_modified = DateTime.Now;
            oRepositorio.Update(oeducation_levels);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            education_levels oEducationLevel = new education_levels
            {
                education_level_id = id,
            };
            oRepositorio.Delete(oEducationLevel);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(EducationLevelViewModel pEducationLevelViewModel)
        {


            education_levels oeducation_levels = new education_levels
            {
                education_level_id = 0,
                name= pEducationLevelViewModel.name,               
                date_created=DateTime.Now,
                user_id_created= pEducationLevelViewModel.user_id_created

            };
            oRepositorio.Add(oeducation_levels);
            oUnitOfWork.SaveChanges();
        }


    }
}
