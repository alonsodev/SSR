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
    public class PermissionBL
    {
        private static PermissionRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public PermissionBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.PermissionRepository;
        }
      

        public PermissionViewModel ObtenerPermission(int pIntID)
        {

            return oRepositorio.ObtenerPermission(pIntID);
        }

        public GridModel<PermissionViewModel> ObtenerLista(PermissionFiltersViewModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(PermissionViewModel pPermissionViewModel)
        {
         

        
            permissions opermissions =oRepositorio.FindById(pPermissionViewModel.id_permission);
            opermissions.name = pPermissionViewModel.name;
            opermissions.title= pPermissionViewModel.title;
            opermissions.user_id_modified = pPermissionViewModel.user_id_modified;

            opermissions.date_modified = DateTime.Now;
            oRepositorio.Update(opermissions);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            permissions oPermission = new permissions
            {
                id_permission = id,
            };
            oRepositorio.Delete(oPermission);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(PermissionViewModel pPermissionViewModel)
        {


            permissions opermissions = new permissions
            {
                id_permission=0,
                name= pPermissionViewModel.name,
                title=pPermissionViewModel.title,
                is_only_for_super_admin=0,
                date_created=DateTime.Now,
                user_id_created= pPermissionViewModel.user_id_created

            };
            oRepositorio.Add(opermissions);
            oUnitOfWork.SaveChanges();
        }


    }
}
