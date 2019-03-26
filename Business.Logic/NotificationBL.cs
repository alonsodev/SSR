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
    public class NotificationBL
    {
        private static NotificationRepository oRepositorio;
        private static UnitOfWork oUnitOfWork;

        public NotificationBL()
        {
            oUnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["SSREntities"].ConnectionString);
            oRepositorio = oUnitOfWork.NotificationRepository;
        }



        public List<NotificationViewModel> ObtenerPorUsuario(int user_id)
        {

            return oRepositorio.ObtenerPorUsuario(user_id);
        }

        public int ObtenerNroNoNotificados(int user_id)
        {

            return oRepositorio.ObtenerNroNoNotificados(user_id);
        }

        public void Modificar(NotificationViewModel pNotificationViewModel)
        {
         

        
            notifications onotifications =oRepositorio.FindById(pNotificationViewModel.notification_id);
            onotifications.user_id = pNotificationViewModel.user_id;
            

            onotifications.user_id_modified = pNotificationViewModel.user_id_modified;
            onotifications.url = pNotificationViewModel.url;
            onotifications.message = pNotificationViewModel.message;
          

            onotifications.date_modified = DateTime.Now;
            oRepositorio.Update(onotifications);
            oUnitOfWork.SaveChanges();
        }

        public void Eliminar(int id)
        {
            notifications oNotification = new notifications
            {
                notification_id = id,
            };
            oRepositorio.Delete(oNotification);
            oUnitOfWork.SaveChanges();
        }

        public void Agregar(NotificationViewModel pNotificationViewModel)
        {


            notifications onotifications = new notifications
            {
                notification_id = 0,
                user_id= pNotificationViewModel.user_id,               
                date_created=DateTime.Now,
                user_id_created= pNotificationViewModel.user_id_created,
                url = pNotificationViewModel.url,
                message = pNotificationViewModel.message,
                notified=false,
            };
            oRepositorio.Add(onotifications);
            oUnitOfWork.SaveChanges();
        }

        public void ActualizarLeido(int notification_id)
        {
            notifications ousers = new notifications
            {
                notification_id = notification_id,
                notified = true,
                

            };
            oRepositorio.Update(ousers,
                                    a => a.notification_id,
                                    a => a.notified                                 
                                );
            oUnitOfWork.SaveChanges();
        }

        public GridModel<NotificationViewModel> ObtenerLista(DataTableAjaxPostModel filters,int user_id)
        {
            return oRepositorio.ObtenerLista(filters,user_id);
        }
    }
}
