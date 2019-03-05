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
    public class ConsultationRepository : Repository<consultations>
    {

        internal ConsultationRepository(ApplicationDbContext context)
            : base(context)
        {
        }



        public ConsultationViewModel Obtener(int consultation_id)
        {
            var query = Set.Where(a => a.consultation_id == consultation_id).Select(a => new ConsultationViewModel
            {

                consultation_id = a.consultation_id,
                title = a.title,
                message = a.message,
                attended = a.attended,
                interest_areas = a.consultations_interest_areas.Select(b => b.interest_area_id).ToList()
            });

            return query.Take(1).FirstOrDefault();
        }



        public GridModel<ConsultationViewModel> ObtenerLista(DataTableAjaxPostModel filters, int user_id)
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


            GridModel<ConsultationViewModel> resultado = new GridModel<ConsultationViewModel>();
            IQueryable<consultations> queryFilters = Set;
            queryFilters = queryFilters.Where(a => a.user_id_created == user_id);


            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.title.ToLower().Contains(srch))
                // || searchTerms.Any(srch => s.message.ToLower().Contains(srch)) 
                || searchTerms.Any(srch => s.consultations_interest_areas.Where(ci => ci.interest_areas.name.ToLower().Contains(srch)).Count() > 0));


                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new ConsultationViewModel
            {
                consultation_id = a.consultation_id,
                title = a.title,
                message = a.message,
                attended = a.attended,
                date_created = a.date_created,
                interest_areas_list = a.consultations_interest_areas.Select(b => b.interest_areas.name).ToList()
            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "consultation_id";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<ConsultationViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }

       
        public GridModel<ConsultationViewModel> ObtenerListaEnviados(DataTableAjaxPostModel filters)
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


            GridModel<ConsultationViewModel> resultado = new GridModel<ConsultationViewModel>();
            IQueryable<consultations> queryFilters = Set;
            ;


            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());


                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.title.ToLower().Contains(srch))
              // || searchTerms.Any(srch => s.message.ToLower().Contains(srch)) 
              || searchTerms.Any(srch => s.consultations_interest_areas.Where(ci => ci.interest_areas.name.ToLower().Contains(srch)).Count() > 0));

                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new ConsultationViewModel
            {
                consultation_id = a.consultation_id,
                title = a.title,
                message = a.message,
                attended = a.attended,
                date_created = a.date_created,
                interest_areas_list = a.consultations_interest_areas.Select(b => b.interest_areas.name).ToList()
            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "consultation_id";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<ConsultationViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }
    }
}
