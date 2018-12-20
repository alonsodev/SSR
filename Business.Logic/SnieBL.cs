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
    public class SnieBL
    {
        private static SnieRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public SnieBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.SnieRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public SnieViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<SnieViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(SnieViewModel pSnieViewModel)
        {
         

        
            snies osnies =oRepositorio.FindById(pSnieViewModel.snie_id);
            osnies.name = pSnieViewModel.name;
            
            osnies.user_id_modified = pSnieViewModel.user_id_modified;
            osnies.educational_institution_id = pSnieViewModel.educational_institution_id;
            osnies.knowledge_area_id = pSnieViewModel.knowledge_area_id;
            osnies.program_id = pSnieViewModel.program_id;
            osnies.academic_level_id = pSnieViewModel.academic_level_id;
            osnies.education_level_id = pSnieViewModel.education_level_id;
            osnies.date_modified = DateTime.Now;
            oRepositorio.Update(osnies);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            snies oSnie = new snies
            {
                snie_id = id,
            };
            oRepositorio.Delete(oSnie);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(SnieViewModel pSnieViewModel)
        {


            snies osnies = new snies
            {
                snie_id = 0,
                name= pSnieViewModel.name,               
                date_created=DateTime.Now,
                educational_institution_id = pSnieViewModel.educational_institution_id,
                knowledge_area_id = pSnieViewModel.knowledge_area_id,
                program_id = pSnieViewModel.program_id,
                academic_level_id = pSnieViewModel.academic_level_id,
                education_level_id = pSnieViewModel.education_level_id,
               
                user_id_created = pSnieViewModel.user_id_created

            };
            oRepositorio.Add(osnies);
            oUnitOfWork.SaveChanges();
        }


    }
}
