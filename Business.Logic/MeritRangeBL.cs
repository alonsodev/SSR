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
    public class MeritRangeBL
    {
        private static MeritRangeRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public MeritRangeBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.MeritRangeRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public MeritRangeViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<MeritRangeViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public List<MeritRangeViewModel> ObtenerTodos()
        {
            return oRepositorio.ObtenerTodos();
        }

        public void Modificar(MeritRangeViewModel pMeritRangeViewModel)
        {
         

        
            merit_ranges omerit_ranges =oRepositorio.FindById(pMeritRangeViewModel.merit_range_id);
            omerit_ranges.name = pMeritRangeViewModel.name;
            
            omerit_ranges.user_id_modified = pMeritRangeViewModel.user_id_modified;
            omerit_ranges.upper_limit = pMeritRangeViewModel.upper_limit;
            omerit_ranges.lower_limit = pMeritRangeViewModel.lower_limit;
            omerit_ranges.url_image = pMeritRangeViewModel.url_image;
            omerit_ranges.date_modified = DateTime.Now;
            oRepositorio.Update(omerit_ranges);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            merit_ranges oMeritRange = new merit_ranges
            {
                merit_range_id = id,
            };
            oRepositorio.Delete(oMeritRange);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(MeritRangeViewModel pMeritRangeViewModel)
        {


            merit_ranges omerit_ranges = new merit_ranges
            {
                merit_range_id = 0,
                name= pMeritRangeViewModel.name,  
                upper_limit= pMeritRangeViewModel.upper_limit,
                lower_limit = pMeritRangeViewModel.lower_limit,
                url_image = pMeritRangeViewModel.url_image,
                date_created =DateTime.Now,
                user_id_created= pMeritRangeViewModel.user_id_created

            };
            oRepositorio.Add(omerit_ranges);
            oUnitOfWork.SaveChanges();
        }


    }
}
