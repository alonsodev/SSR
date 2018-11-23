using Domain.Entities;
using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<users>
    {
        internal UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public List<SelectOptionItem> RolesSelector()
        {
            var lista = this.Context.Set<roles>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.role_id.ToString(),
                Text = a.role,
            });

            return consulta.ToList();
        }
        public List<SelectOptionItem> EstatusUserSelector()
        {
            var lista = this.Context.Set<user_status>();
            var consulta = lista.Select(a => new SelectOptionItem
            {
                Value = a.user_status_id.ToString(),
                Text = a.name,
            });

            return consulta.ToList();
        }

        public UserViewModel ObtenerUser(int id)
        {
            var query = Set.Where(a => a.id == id).Select(a => new UserViewModel
            {

                id = a.id,
                user_name = a.user_name,
                user_email = a.user_email,
                user_pass = a.user_pass,
                user_role_id = a.user_role_id,
                user_status_id = a.user_status_id,
                is_super_admin = a.is_super_admin,
                user_date_last_login = a.user_date_last_login,
                date_created = a.date_created,
                date_modified = a.date_modified,
                user_id_created = a.user_id_created,
                user_id_modified = a.user_id_modified
            });

            return query.Take(1).FirstOrDefault();
        }

        public GridModel<UserViewModel> ObtenerLista(UserFiltersViewModel filters)
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


            GridModel<UserViewModel> resultado = new GridModel<UserViewModel>();
            IQueryable<users> queryFilters = Set;



            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;


            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.user_name.ToLower().Contains(srch) || s.user_email.ToLower().Contains(srch) || s.roles.role.ToLower().Contains(srch)) );


                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new UserViewModel
            {
                id = a.id,
                user_name = a.user_name,
                user_email = a.user_email,
                user_pass = a.user_pass,
                user_role = a.roles.role,

                user_status = a.user_status.name,

            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "id";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<UserViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }
    }
}
