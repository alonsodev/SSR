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
    public class RoleBL
    {
        private static RoleRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public RoleBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.RoleRepository;
        }
        

        public RoleViewModel ObtenerRole(int pIntID)
        {

            return oRepositorio.ObtenerRole(pIntID);
        }

        public GridModel<RoleViewModel> ObtenerLista(RoleFiltersViewModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(RoleViewModel pRoleViewModel)
        {
         

        
            roles oroles =oRepositorio.FindById(pRoleViewModel.role_id);
            oroles.role = pRoleViewModel.role;         
            oroles.user_id_modified = pRoleViewModel.user_id_modified;
            oroles.date_modified = DateTime.Now;
            oRepositorio.Update(oroles);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            roles oRole = new roles
            {
                role_id = id,
            };
            oRepositorio.Delete(oRole);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(RoleViewModel pRoleViewModel)
        {


            roles oroles = new roles
            {
                role_id=0,
                role= pRoleViewModel.role,
             
                date_created=DateTime.Now,
                user_id_created= pRoleViewModel.user_id_created

            };
            oRepositorio.Add(oroles);
            oUnitOfWork.SaveChanges();
        }


    }
}
