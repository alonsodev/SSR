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
    public class ReasonRejectBL
    {
        private static ReasonRejectRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public ReasonRejectBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.ReasonRejectRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public ReasonRejectViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<ReasonRejectViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(ReasonRejectViewModel pReasonRejectViewModel)
        {
         

        
            reason_rejects oreason_rejects =oRepositorio.FindById(pReasonRejectViewModel.reason_reject_id);
            oreason_rejects.name = pReasonRejectViewModel.name;
            
            oreason_rejects.user_id_modified = pReasonRejectViewModel.user_id_modified;

            oreason_rejects.date_modified = DateTime.Now;
            oRepositorio.Update(oreason_rejects);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            reason_rejects oReasonReject = new reason_rejects
            {
                reason_reject_id = id,
            };
            oRepositorio.Delete(oReasonReject);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(ReasonRejectViewModel pReasonRejectViewModel)
        {


            reason_rejects oreason_rejects = new reason_rejects
            {
                reason_reject_id = 0,
                name= pReasonRejectViewModel.name,               
                date_created=DateTime.Now,
                user_id_created= pReasonRejectViewModel.user_id_created

            };
            oRepositorio.Add(oreason_rejects);
            oUnitOfWork.SaveChanges();
        }


    }
}
