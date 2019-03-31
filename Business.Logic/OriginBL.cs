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
    public class OriginBL
    {
        private static OriginRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public OriginBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.OriginRepository;
        }

      
      


        public Dictionary<string,int> ObtenerDiccionarioPorNombre(List<string> commisions,int user_id)
        {
            var olista= oRepositorio.ObtenerTodos();

            
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            foreach (var item in olista) {
                dictionary.Add(item.name, item.origin_id);
            }
            foreach (var commision in commisions)
            {
                if (!String.IsNullOrEmpty(commision) && !dictionary.ContainsKey(commision)){
                    origins oorigins = new origins
                    {
                        origin_id = 0,
                        name = commision,
                        date_created = DateTime.Now,
                        user_id_created = user_id

                    };
                    oorigins = oRepositorio.Add(oorigins);
                    oUnitOfWork.SaveChanges();
                    dictionary.Add(oorigins.name, oorigins.origin_id);

                }
            }
            return dictionary;
        }


       


    }
}
