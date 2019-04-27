using Domain.Entities;
using Domain.Entities.Movil;
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
            var query = Set.Where(a => a.user_id == user_id && a.notified == false).OrderByDescending(a=> a.notification_id).Select(a => new NotificationViewModel
            {

                notification_id = a.notification_id,
                user_id = a.user_id,
                url=a.url,
                notified=a.notified,
                message=a.message
            });

            return query.ToList();
        }



        public int ObtenerNroNoNotificados(int user_id)
        {


            return Set.Where(a => a.user_id == user_id && a.notified == false).Count();
        }

        public GridModel<NotificationViewModel> ObtenerLista(DataTableAjaxPostModel filters,int user_id)
        {
            var searchBy = (filters.search != null) ? filters.search.value : null;


            string sortBy = "";
            string sortDir = "";

            if (filters.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = filters.columns[filters.order[0].column].data;
                sortDir = filters.order[0].dir.ToLower();
            }


            GridModel<NotificationViewModel> resultado = new GridModel<NotificationViewModel>();
            IQueryable<notifications> queryFilters = Set.Where(a=> a.user_id== user_id);



            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.message.ToLower().Contains(srch)));


                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new NotificationViewModel
            {
                notification_id = a.notification_id,
                user_id = a.user_id,
                url = a.url,
                notified = a.notified,
                message = a.message,
                date_created = a.date_created
            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "notification_id";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "desc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<NotificationViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }

        public NotificationViewModel ObtenerPorUrl(ConceptQualificationViewModel filter)
        {
            string url = "/Concept/Calificar/" + filter.concept_id.ToString();
            return Set.Where(a=> a.url==url && a.user_id==filter.user_id && a.notified ==false ).Select(a => new NotificationViewModel
            {
                notification_id = a.notification_id,
                user_id = a.user_id,
                url = a.url,
                notified = a.notified,
                message = a.message,
                date_created = a.date_created
            }).Take(1).FirstOrDefault();
        }


        public void ActualizarNotificacionLeido(string url,int user_id)
        {
            using (var context = new SSREntities())
            {
                var results = context.ActualizarNotificacionLeido(url,user_id);
            }

        }
    }
}
