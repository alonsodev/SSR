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
    public class InvestigationGroupBL
    {
        private static InvestigationGroupRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public InvestigationGroupBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.InvestigationGroupRepository;
        }

        public bool VerificarDuplicado(int id, string name)
        {
            return oRepositorio.VerificarDuplicado(id,name);
        }

       
        public InvestigationGroupViewModel Obtener(int pIntID)
        {

            return oRepositorio.Obtener(pIntID);
        }

        public GridModel<InvestigationGroupViewModel> ObtenerLista(DataTableAjaxPostModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(InvestigationGroupViewModel pInvestigationGroupViewModel)
        {
         

        
            investigation_groups oinvestigation_groups =oRepositorio.FindById(pInvestigationGroupViewModel.investigation_group_id);
            oinvestigation_groups.name = pInvestigationGroupViewModel.name;
            
            oinvestigation_groups.user_id_modified = pInvestigationGroupViewModel.user_id_modified;

            oinvestigation_groups.date_modified = DateTime.Now;
            oRepositorio.Update(oinvestigation_groups);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            investigation_groups oInvestigationGroup = new investigation_groups
            {
                investigation_group_id = id,
            };
            oRepositorio.Delete(oInvestigationGroup);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(InvestigationGroupViewModel pInvestigationGroupViewModel)
        {


            investigation_groups oinvestigation_groups = new investigation_groups
            {
                investigation_group_id = 0,
                name= pInvestigationGroupViewModel.name,               
                date_created=DateTime.Now,
                user_id_created= pInvestigationGroupViewModel.user_id_created

            };
            oRepositorio.Add(oinvestigation_groups);
            oUnitOfWork.SaveChanges();
        }


    }
}
