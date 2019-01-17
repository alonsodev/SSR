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
    public class AcademicLevelBL
    {
        private static AcademicLevelRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public AcademicLevelBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.AcademicLevelRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public AcademicLevelViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<AcademicLevelViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(AcademicLevelViewModel pAcademicLevelViewModel)
        {
         

        
            academic_levels oacademic_levels =oRepositorio.FindById(pAcademicLevelViewModel.academic_level_id);
            oacademic_levels.name = pAcademicLevelViewModel.name;
            
            oacademic_levels.user_id_modified = pAcademicLevelViewModel.user_id_modified;

            oacademic_levels.date_modified = DateTime.Now;
            oRepositorio.Update(oacademic_levels);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            academic_levels oAcademicLevel = new academic_levels
            {
                academic_level_id = id,
            };
            oRepositorio.Delete(oAcademicLevel);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(AcademicLevelViewModel pAcademicLevelViewModel)
        {


            academic_levels oacademic_levels = new academic_levels
            {
                academic_level_id = 0,
                name= pAcademicLevelViewModel.name,               
                date_created=DateTime.Now,
                user_id_created= pAcademicLevelViewModel.user_id_created

            };
            oRepositorio.Add(oacademic_levels);
            oUnitOfWork.SaveChanges();
        }


    }
}
