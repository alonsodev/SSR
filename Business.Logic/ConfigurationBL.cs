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
    public class ConfigurationBL
    {
        private static ConfigurationRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public ConfigurationBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.ConfigurationRepository;
        }

      
       
        public ConfigurationViewModel Obtener()
        {

            return oRepositorio.Obtener();
        }

      

        public void Modificar(ConfigurationViewModel pConfigurationViewModel)
        {
         

        
            configurations oconfigurations =oRepositorio.FindById(pConfigurationViewModel.configuration_id);
            oconfigurations.terms_conditions = pConfigurationViewModel.terms_conditions;
            oconfigurations.remove_titles_speaker = pConfigurationViewModel.remove_titles_speaker;
            oconfigurations.exclude_speakers = pConfigurationViewModel.exclude_speakers;

            oconfigurations.user_id_modified = pConfigurationViewModel.user_id_modified;

            oconfigurations.date_modified = DateTime.Now;
            oRepositorio.Update(oconfigurations);
            oUnitOfWork.SaveChanges();
        }

       


    }
}
