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
    public class InterestAreaBL
    {
        private static InterestAreaRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public InterestAreaBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.InterestAreaRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

        public Dictionary<string, int> ObtenerDiccionarioPorNombre(List<string> interest_areas, int user_id)
        {
            var olista = oRepositorio.ObtenerTodos();


            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            foreach (var item in olista)
            {
                dictionary.Add(item.name, item.interest_area_id);
            }
            foreach (var interest_area in interest_areas)
            {
               
                if (!String.IsNullOrEmpty(interest_area) && !dictionary.ContainsKey(interest_area))
                {
                    interest_areas ointerest_areas = new interest_areas
                    {
                        interest_area_id = 0,
                        name = interest_area,

                        date_created = DateTime.Now,
                        user_id_created = user_id

                    };
                    ointerest_areas = oRepositorio.Add(ointerest_areas);
                    oUnitOfWork.SaveChanges();
                    dictionary.Add(ointerest_areas.name, ointerest_areas.interest_area_id);

                }
            }
            return dictionary;
        }
        public InterestAreaViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<InterestAreaViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(InterestAreaViewModel pInterestAreaViewModel)
        {
         

        
            interest_areas ointerest_areas =oRepositorio.FindById(pInterestAreaViewModel.interest_area_id);
            ointerest_areas.name = pInterestAreaViewModel.name;
            
            ointerest_areas.user_id_modified = pInterestAreaViewModel.user_id_modified;

            ointerest_areas.date_modified = DateTime.Now;
            oRepositorio.Update(ointerest_areas);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            interest_areas oInterestArea = new interest_areas
            {
                interest_area_id = id,
            };
            oRepositorio.Delete(oInterestArea);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(InterestAreaViewModel pInterestAreaViewModel)
        {


            interest_areas ointerest_areas = new interest_areas
            {
                interest_area_id = 0,
                name= pInterestAreaViewModel.name,
                
                date_created=DateTime.Now,
                user_id_created= pInterestAreaViewModel.user_id_created

            };
            oRepositorio.Add(ointerest_areas);
            oUnitOfWork.SaveChanges();
        }


    }
}
