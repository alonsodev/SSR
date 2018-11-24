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
        private static RolePermissionRepository oRolePermissionRepository;
        private static UnitOfWork oUnitOfWork;

        public RoleBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.RoleRepository;
            oRolePermissionRepository = oUnitOfWork.RolePermissionRepository;
        }

        public bool VerificarDuplicado(int id_role, string name)
        {
            return oRepositorio.VerificarDuplicado(id_role, name);
        }

        public void GuardarPermisos(int role_id, string ids)
        {
            oRolePermissionRepository.deleteMultiple(role_id);
            string[] permissions = ids.Split(',');
            foreach (string permission in permissions)
            {
                role_permissions orole_permissions = new role_permissions();
                orole_permissions.id_role = role_id;
                orole_permissions.id_permission = Int32.Parse(permission);
                orole_permissions.status = 1;
                oRolePermissionRepository.Add(orole_permissions);
            }
            oUnitOfWork.SaveChanges();
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
