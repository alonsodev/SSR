using Domain.Entities;
using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class RoleRepository : Repository<roles>
    {
        internal RoleRepository(ApplicationDbContext context)
            : base(context)
        {
        }
      
        public RoleViewModel ObtenerRole(int id_permission)
        {
            var query = Set.Where(a => a.role_id == id_permission).Select(a => new RoleViewModel
            {

                role_id = a.role_id,
                role = a.role,
                date_created = a.date_created,
                date_modified=a.date_modified,
                user_id_created=a.user_id_created,
                user_id_modified=a.user_id_modified
            });

            return query.Take(1).FirstOrDefault();
        }

        public GridModel<RoleViewModel> ObtenerLista(RoleFiltersViewModel filters)
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


            GridModel<RoleViewModel> resultado = new GridModel<RoleViewModel>();
            IQueryable<roles> queryFilters = Set;



            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.role.ToLower().Contains(srch)));


                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new RoleViewModel
            {
                role_id = a.role_id,
                role = a.role,
            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "role_id";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<RoleViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }
    }
}
