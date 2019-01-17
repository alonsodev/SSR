using Domain.Entities;
using EntityFramework.Extensions;
using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class NotificationRepository : Repository<notifications>
    {

        internal NotificationRepository(ApplicationDbContext context)
            : base(context)
        {
        }

      

        public List<NotificationViewModel> ObtenerPorUsuario(int user_id)
        {
            var query = Set.Where(a => a.user_id == user_id && a.notified==false).Select(a => new NotificationViewModel
            {

                notification_id = a.notification_id,
                user_id = a.user_id,
                url=a.url,
                notified=a.notified,
                message=a.message
            });

            return query.ToList();
        }



      

    }
}
