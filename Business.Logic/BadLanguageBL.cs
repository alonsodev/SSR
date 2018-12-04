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
    public class BadLanguageBL
    {
        private static BadLanguageRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public BadLanguageBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.BadLanguageRepository;
        }

        public List<string> ObtenerPalabrasNoAdecuadas()
        {
            return oRepositorio.ObtenerPalabrasNoAdecuadas();
        }
        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public BadLanguageViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<BadLanguageViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(BadLanguageViewModel pBadLanguageViewModel)
        {
         

        
            bad_languages obad_languages =oRepositorio.FindById(pBadLanguageViewModel.bad_language_id);
            obad_languages.name = pBadLanguageViewModel.name;
            
            obad_languages.user_id_modified = pBadLanguageViewModel.user_id_modified;

            obad_languages.date_modified = DateTime.Now;
            oRepositorio.Update(obad_languages);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            bad_languages oBadLanguage = new bad_languages
            {
                bad_language_id = id,
            };
            oRepositorio.Delete(oBadLanguage);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(BadLanguageViewModel pBadLanguageViewModel)
        {


            bad_languages obad_languages = new bad_languages
            {
                bad_language_id = 0,
                name= pBadLanguageViewModel.name,               
                date_created=DateTime.Now,
                user_id_created= pBadLanguageViewModel.user_id_created

            };
            oRepositorio.Add(obad_languages);
            oUnitOfWork.SaveChanges();
        }


    }
}
