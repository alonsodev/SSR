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
    public class TagBL
    {
        private static TagRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public TagBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.TagRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public TagViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<TagViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(TagViewModel pTagViewModel)
        {
         

        
            tags otags =oRepositorio.FindById(pTagViewModel.tag_id);
            otags.name = pTagViewModel.name;
            
            otags.user_id_modified = pTagViewModel.user_id_modified;

            otags.date_modified = DateTime.Now;
            oRepositorio.Update(otags);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            tags oTag = new tags
            {
                tag_id = id,
            };
            oRepositorio.Delete(oTag);
            oUnitOfWork.SaveChanges();
        }

        public int Agregar(TagViewModel pTagViewModel)
        {


            tags otags = new tags
            {
                tag_id = 0,
                name= pTagViewModel.name,               
                date_created=DateTime.Now,
                user_id_created= pTagViewModel.user_id_created

            };
            otags= oRepositorio.Add(otags);
            oUnitOfWork.SaveChanges();
            return otags.tag_id;
        }

        public TagViewModel ObtenerPorNombre(string name)
        {
            return oRepositorio.ObtenerPorNombre(name);
        }
    }
}
