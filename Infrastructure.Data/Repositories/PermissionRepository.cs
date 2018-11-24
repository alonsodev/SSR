using Domain.Entities;
using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PermissionRepository : Repository<permissions>
    {
        internal PermissionRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public bool VerificarDuplicado(int id_permission, string name) {

            name = name.Trim().ToLower();
            var count = Set.Where(a => a.id_permission != id_permission && a.name.ToLower() == name).Count();

            return count == 0;
        }

        public PermissionViewModel ObtenerPermission(int id_permission)
        {
            var query = Set.Where(a => a.id_permission == id_permission).Select(a => new PermissionViewModel
            {

                id_permission = a.id_permission,
                name = a.name,
                title = a.title
            });

            return query.Take(1).FirstOrDefault();
        }

        public List<PermissionViewModel> ObtenerListaPermisos()
        {
            var query = Set.Select(a => new PermissionViewModel
            {
                id_permission = a.id_permission,
                name = a.name,
                title = a.title
            });
            return query.ToList();
        }

        public List<int> ObtenerListaPermisos(int role_id)
        {
            var lista = this.Context.Set<role_permissions>();

            List<int> Ids = lista.Where(a => a.id_role == role_id).Select(c => c.id_permission).Distinct().ToList();

            return Ids;
        }

        public GridModel<PermissionViewModel> ObtenerLista(PermissionFiltersViewModel filters)
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


            GridModel<PermissionViewModel> resultado = new GridModel<PermissionViewModel>();
            IQueryable<permissions> queryFilters = Set;

        

            int count_records = queryFilters.Count();
            int count_records_filtered = count_records;
            

            if (String.IsNullOrWhiteSpace(searchBy) == false)
            {
                // as we only have 2 cols allow the user type in name 'firstname lastname' then use the list to search the first and last name of dbase
                var searchTerms = searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());

                queryFilters = queryFilters.Where(s => searchTerms.Any(srch => s.name.ToLower().Contains(srch))|| searchTerms.Any(srch => s.title.ToLower().Contains(srch)));
                

                count_records_filtered = queryFilters.Count();
            }


            var query = queryFilters.Select(a => new PermissionViewModel
            {
                id_permission=a.id_permission,
                name=a.name,
                title=a.title
            });

            if (String.IsNullOrEmpty(sortBy)) sortBy = "id_permission";
            if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
            string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
            if (sortExpression.Trim() != "")
                query = OrderByDinamic.OrderBy<PermissionViewModel>(query, sortExpression.Trim());
            resultado.rows = query.Skip(filters.start).Take(filters.length).ToList();



            resultado.total = count_records;

            resultado.recordsFiltered = count_records_filtered;
            return resultado;
        }
    }

}

