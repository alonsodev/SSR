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
    public class UserBL
    {
        private static UserRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public UserBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.UserRepository;
        }
       


        public UserViewModel ObtenerUser(int pIntID)
        {

            return oRepositorio.ObtenerUser(pIntID);
        }

        public GridModel<UserViewModel> ObtenerLista(UserFiltersViewModel filters)
        {
            return oRepositorio.ObtenerLista(filters);
        }

        public void Modificar(UserViewModel pUserViewModel)
        {



            users ousers = oRepositorio.FindById(pUserViewModel.id);
            ousers.user_name = pUserViewModel.user_name;
            ousers.user_email = pUserViewModel.user_email;
            ousers.user_role_id = pUserViewModel.user_role_id;
            ousers.user_status_id = pUserViewModel.user_status_id;
          
            ousers.user_id_modified = pUserViewModel.user_id_modified;
            ousers.date_modified = DateTime.Now;
            oRepositorio.Update(ousers);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            users oUser = new users
            {
                id = id,
            };
            oRepositorio.Delete(oUser);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(UserViewModel pUserViewModel)
        {


            users ousers = new users
            {
                id = 0,
                user_name = pUserViewModel.user_name,
                user_email = pUserViewModel.user_email,
                user_role_id = pUserViewModel.user_role_id,
                user_status_id = pUserViewModel.user_status_id,
                date_created = DateTime.Now,
                user_id_created = pUserViewModel.user_id_created

            };
            oRepositorio.Add(ousers);
            oUnitOfWork.SaveChanges();
        }

    }
}
