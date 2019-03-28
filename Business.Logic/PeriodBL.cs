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
    public class PeriodBL
    {
        private static PeriodRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public PeriodBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.PeriodRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public PeriodViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }


        public PeriodViewModel ObtenerVigente()
        {

            return oRepositorio.Obtener(DateTime.Today);
        }

        public GridModel<PeriodViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(PeriodViewModel pPeriodViewModel)
        {
         

        
            periods operiods =oRepositorio.FindById(pPeriodViewModel.period_id);
            operiods.name = pPeriodViewModel.name;
            
            operiods.user_id_modified = pPeriodViewModel.user_id_modified;
            operiods.start_date = pPeriodViewModel.start_date;
            operiods.end_date = pPeriodViewModel.end_date;

            operiods.date_modified = DateTime.Now;
            oRepositorio.Update(operiods);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            periods oPeriod = new periods
            {
                period_id = id,
            };
            oRepositorio.Delete(oPeriod);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(PeriodViewModel pPeriodViewModel)
        {


            periods operiods = new periods
            {
                period_id = 0,
                name= pPeriodViewModel.name,               
                date_created=DateTime.Now,
                user_id_created= pPeriodViewModel.user_id_created,
                end_date= pPeriodViewModel.end_date,
                start_date = pPeriodViewModel.start_date,

            };
            oRepositorio.Add(operiods);
            oUnitOfWork.SaveChanges();
        }


    }
}
