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
    public class KnowledgeAreaBL
    {
        private static KnowledgeAreaRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public KnowledgeAreaBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.KnowledgeAreaRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public KnowledgeAreaViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<KnowledgeAreaViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(KnowledgeAreaViewModel pKnowledgeAreaViewModel)
        {
         

        
            knowledge_areas oknowledge_areas =oRepositorio.FindById(pKnowledgeAreaViewModel.knowledge_area_id);
            oknowledge_areas.name = pKnowledgeAreaViewModel.name;
            
            oknowledge_areas.user_id_modified = pKnowledgeAreaViewModel.user_id_modified;

            oknowledge_areas.date_modified = DateTime.Now;
            oRepositorio.Update(oknowledge_areas);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            knowledge_areas oKnowledgeArea = new knowledge_areas
            {
                knowledge_area_id = id,
            };
            oRepositorio.Delete(oKnowledgeArea);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(KnowledgeAreaViewModel pKnowledgeAreaViewModel)
        {


            knowledge_areas oknowledge_areas = new knowledge_areas
            {
                knowledge_area_id = 0,
                name= pKnowledgeAreaViewModel.name,               
                date_created=DateTime.Now,
                user_id_created= pKnowledgeAreaViewModel.user_id_created

            };
            oRepositorio.Add(oknowledge_areas);
            oUnitOfWork.SaveChanges();
        }


    }
}
